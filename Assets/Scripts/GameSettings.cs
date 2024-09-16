using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;

    public float playerHealth = 100f;
    public float enemyHealth = 100f;
    public float weaponDamage = 10f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object alive across scenes if needed
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    // Method to ensure GameSettings is available
    public static void EnsureInitialized()
    {
        if (instance == null)
        {
            // Create a new GameObject with GameSettings if it doesn't exist
            GameObject gameSettingsObject = new GameObject("GameSettings");
            instance = gameSettingsObject.AddComponent<GameSettings>();
            DontDestroyOnLoad(gameSettingsObject);  // Optionally keep it across scenes
        }
    }
}
