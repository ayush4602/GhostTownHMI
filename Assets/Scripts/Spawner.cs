using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject fastZombiePrefab;  // Reference to the Fast Zombie prefab
    public GameObject slowZombiePrefab;  // Reference to the Slow Zombie prefab
    public int numberOfEnemies = 10;  // Total number of enemies to spawn
    public float spawnRadius = 50f;  // Radius of the area where enemies can spawn
    public float spawnInterval = 3f;  // Time between spawns
    public Transform player;  // Reference to the player to avoid spawning too close

    public float groundOffset = 1f;  // Small height offset to avoid placing enemies inside the ground
    public LayerMask groundLayer;  // Set a layer mask for ground detection
    public string enemyTag = "Enemy";  // Tag to assign to each spawned enemy

    public float fastZombieSpawnChance = 0.1f;  // 10% chance to spawn FastZombie, 90% SlowZombie

    private void Start()
    {
        StartCoroutine(SpawnEnemies());  // Start spawning enemies
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnZombie();  // Spawn either a FastZombie or SlowZombie
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnZombie()
    {
        // Randomly choose between FastZombie and SlowZombie based on spawn chance
        GameObject zombiePrefab = (Random.value <= fastZombieSpawnChance) ? fastZombiePrefab : slowZombiePrefab;

        if (zombiePrefab == null)
        {
            Debug.LogError("Zombie prefab is not assigned in the EnemySpawner.");
            return;
        }

        Vector3 spawnPosition = GenerateRandomSpawnPoint();

        // Ensure the zombie spawns on the ground (NavMesh)
        if (NavMesh.SamplePosition(spawnPosition, out NavMeshHit navHit, spawnRadius, NavMesh.AllAreas))
        {
            spawnPosition = navHit.position + Vector3.up * groundOffset;  // Adjust for ground placement

            // Instantiate the selected zombie prefab
            GameObject newZombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            if (newZombie == null)
            {
                Debug.LogError("Failed to instantiate zombie.");
                return;
            }

            // Assign the specified tag to the spawned zombie
            newZombie.tag = enemyTag;

            NavMeshAgent zombieNavMeshAgent = newZombie.GetComponent<NavMeshAgent>();

            // Ensure the NavMeshAgent is enabled and can navigate
            if (zombieNavMeshAgent != null && zombieNavMeshAgent.isOnNavMesh)
            {
                // If the zombie needs to reference the player, assign the target
                enemymovement enemyMovement = newZombie.GetComponent<enemymovement>();
                if (enemyMovement != null && player != null)
                {
                    enemyMovement.target = player;  // Set the player as the target for the zombie
                }
            }
            else
            {
                Debug.LogWarning("Spawned zombie is not on a valid NavMesh, disabling NavMeshAgent.");
                if (zombieNavMeshAgent != null)
                {
                    zombieNavMeshAgent.enabled = false;  // Disable the NavMeshAgent if it's not on the NavMesh
                }
            }
        }
        else
        {
            Debug.LogWarning("Could not find a valid NavMesh position to spawn the zombie.");
        }
    }

    // Generate a random position within the spawn radius
    private Vector3 GenerateRandomSpawnPoint()
    {
        Vector3 randomPoint = Random.insideUnitSphere * spawnRadius;
        randomPoint.y = 0;  // Keep the Y axis zero initially to avoid flying spawns
        randomPoint += transform.position;  // Offset by spawner's position
        return randomPoint;
    }
}
