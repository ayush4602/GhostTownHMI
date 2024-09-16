using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Import SceneManager for scene transitions

public class Player_damage : MonoBehaviour
{
    public float maxHealth = 40f;  // Maximum player health
    public float currentHealth;  // Current player health
    public slider_health healthbar;  // Reference to the health bar UI
    public string nextSceneName = "End_Game";  // The name of the next scene to load when the player dies

    private bool isTouchingEnemy = false;  // Tracks if player is touching an enemy
    private float damagePerSecond = 0f;  // Damage rate, depends on the type of enemy

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    private void Update()
    {
        // Apply damage continuously while touching an enemy
        if (isTouchingEnemy)
        {
            takedamage(damagePerSecond * Time.deltaTime);  // Apply damage per second
        }

        // Check if player health has dropped to zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void takedamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthbar.setHealth(currentHealth);  // Update health bar

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            isTouchingEnemy = true;  // Start applying damage

            // Determine the type of zombie and set the damage rate
            if (collision.gameObject.name.Contains("FastZombie"))
            {
                damagePerSecond = 1.1f;  // FastZombie damage rate
                Debug.Log("Colliding with FastZombie, damage per second: " + damagePerSecond);
            }
            else if (collision.gameObject.name.Contains("SlowZombie"))
            {
                damagePerSecond = 0.8f;  // SlowZombie damage rate
                Debug.Log("Colliding with SlowZombie, damage per second: " + damagePerSecond);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Stop applying damage when no longer in contact with the enemy
        if (collision.gameObject.tag == "Enemy")
        {
            isTouchingEnemy = false;  // Stop applying damage
            damagePerSecond = 0f;  // Reset the damage rate
            Debug.Log("No longer colliding with enemy, stopping damage.");
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Transition to the next scene when the player dies
        SceneManager.LoadScene(nextSceneName);  // Load the next scene
    }
}
