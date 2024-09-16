using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Import SceneManager

public class RayCast : MonoBehaviour
{
    public float damage = 20f;  // Damage dealt by the weapon
    public float range = 100f;  // Range of the weapon
    public Camera mainCam;
    public ParticleSystem muzzleFlash;
    public bool isFiring;
    public float bulletsInPistol;  // Ammo in the pistol (this will still be limited)
    
    public Text bulletcount;  // UI text to show the ammo count

    public AudioClip shootSound;
    public AudioClip shootSoundpickup;
    public AudioClip shootSoundempty;
    public float soundIntensity = 5f;
    public LayerMask zombieLayer;
    private AudioSource audioSource;

    private bool hasPlayedEmptySound = false;  // Track empty sound playback
    private bool isGameOver = false;  // Track game over state

    public string gameOverSceneName = "End_Game";  // Name of the Game Over scene

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Ensure the main camera is assigned
        if (mainCam == null)
        {
            mainCam = Camera.main;
            if (mainCam == null)
            {
                Debug.LogError("Main camera is not assigned and no camera tagged as 'MainCamera' found.");
            }
        }

        // Ensure bullet count UI is initialized correctly
        UpdateBulletUI();
    }

    // Method to reload the pistol (with infinite reserve ammo)
    void reload()
    {
        float bulletsNeeded = 6f - bulletsInPistol;  // Calculate bullets needed to fill the pistol
        bulletsInPistol += bulletsNeeded;  // Refill the pistol with infinite reserve ammo

        // Reset the empty sound flag when reloading
        hasPlayedEmptySound = false;

        // Update the UI after reloading
        UpdateBulletUI();
    }

    // Method to fire the weapon
    void fire()
    {
        if (mainCam == null)
        {
            Debug.LogError("Main Camera is not assigned.");
            return;
        }

        if (bulletsInPistol > 0)  // Only fire if there's ammo in the pistol
        {
            RaycastHit targethit;
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out targethit, range))
            {
                Debug.Log(targethit.transform.name);

                if (muzzleFlash != null)
                {
                    muzzleFlash.Play();  // Play the muzzle flash effect
                }
                else
                {
                    Debug.LogError("Muzzle flash particle system is not assigned.");
                }

                isFiring = true;
                bulletsInPistol--;  // Decrease ammo count after firing
                audioSource.PlayOneShot(shootSound);  // Play the shooting sound

                // Update the UI after firing
                UpdateBulletUI();

                // Alert nearby zombies
                Collider[] zombies = Physics.OverlapSphere(transform.position, soundIntensity, zombieLayer);
                for (int i = 0; i < zombies.Length; i++)
                {
                    zombies[i].GetComponent<enemymovement>().OnAware();
                }

                // Apply damage to the target hit
                Health enemy = targethit.transform.GetComponent<Health>();
                if (enemy != null)
                {
                    enemy.Damage(damage);
                }
            }
        }
        else
        {
            Debug.Log("No bullets left! Reload required.");
            if (!hasPlayedEmptySound)
            {
                PlayEmptyGunSound();  // Play empty gun sound when out of ammo
                hasPlayedEmptySound = true;
            }
        }
    }

    // Method to handle player input and weapon mechanics
    void Update()
    {
        if (isGameOver)
        {
            return;  // Stop any further actions if the game is over
        }

        // Handle firing input
        if (Input.GetButtonDown("Fire1"))
        {
            fire();  // Fire if the player presses the fire button
        }

        // Handle manual reload input with the "R" key
        if (Input.GetKeyDown(KeyCode.R))
        {
            reload();  // Reload manually when "R" is pressed
        }
    }

    // Helper method to play the empty gun sound
    void PlayEmptyGunSound()
    {
        if (shootSoundempty != null)
        {
            audioSource.PlayOneShot(shootSoundempty);
        }
        else
        {
            Debug.LogError("Empty gun sound is not assigned.");
        }
    }

    // Method to update the bullet UI with current ammo counts
    public void UpdateBulletUI()
    {
        if (bulletcount != null)
        {
            bulletcount.text = "Ammo: " + bulletsInPistol.ToString() + " / ∞";  // Display infinite reserve ammo
        }
        else
        {
            Debug.LogError("Bullet count Text UI is not assigned.");
        }
    }

    // Method to handle the Game Over condition and load the Game Over scene
    private void GameOver()
    {
        isGameOver = true;  // Set the game over flag to true
        Debug.Log("Game Over! Loading Game Over Scene...");

        // Load the Game Over scene
        SceneManager.LoadScene(gameOverSceneName);
    }
}
