using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;  // Default health value

    // Method to apply damage to the object
    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();  // Call Die() if health is 0 or below
        }
    }

    // Method to handle death logic
    private void Die()
    {
        // Check if the object is tagged as "Zombie"
        if (gameObject.CompareTag("Enemy"))
        {
            if (ScoreManager.instance != null)
            {
                // Assign score based on the zombie type
                if (gameObject.name.Contains("FastZombie"))
                {
                    // FastZombie gives 10 points
                    ScoreManager.instance.AddScore(10);
                    Debug.Log("FastZombie killed! +10 points");
                }
                else if (gameObject.name.Contains("SlowZombie"))
                {
                    // SlowZombie gives 1 point
                    ScoreManager.instance.AddScore(1);
                    Debug.Log("SlowZombie killed! +1 point");
                }
            }
            else
            {
                Debug.LogWarning("ScoreManager instance is null!");
            }
        }

        // Destroy the zombie GameObject after death
        Destroy(gameObject);
    }
}
