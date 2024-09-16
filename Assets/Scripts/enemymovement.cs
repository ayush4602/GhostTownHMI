using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemymovement : MonoBehaviour
{
    public float fov = 120f;  // Field of view for spotting the player
    public Transform target;  // The player or target to follow
    public bool isVisible;
    public bool playerinVision;
    public float awakedistance = 200f;  // Distance at which enemy becomes aware of the player
    private Vector3 wanderPoint;
    public bool aware;  // Is the enemy aware of the player?
    public float wanderRadius = 7f;  // Radius for wandering behavior
    public NavMeshAgent navigation;  // NavMeshAgent for movement

    private float originalSpeed;  // Store the original speed of the NavMeshAgent
    private float speedMultiplier = 1.25f;  // Speed increase when zombies spot the player

    private float wanderTimer = 5f;  // Time between each random wandering action
    private float currentWanderTimer = 0f;  // Tracks elapsed time between wander actions

    private void Start()
    {
        // Automatically find and assign the player as the target
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("Player object not found! Ensure the player has the 'Player' tag.");
            }
        }

        if (navigation != null)
        {
            originalSpeed = navigation.speed;  // Store the original speed
        }
        else
        {
            Debug.LogError("NavMeshAgent is not assigned.");
        }
    }

    private void Update()
    {
        // Ensure the NavMeshAgent is active and on the NavMesh before setting destinations
        if (navigation == null || !navigation.isOnNavMesh || target == null)
        {
            Wander();  // If no target or NavMesh, perform wandering
            return;
        }

        drawRay();  // Perform the raycasting logic
        float playerDistance = Vector3.Distance(target.position, transform.position);
        Vector3 playerDirection = target.position - transform.position;
        float playerAngle = Vector3.Angle(transform.forward, playerDirection);

        // Check if the player is within the field of view
        isVisible = playerAngle <= fov / 2f;

        // If the player is visible and close enough, the enemy becomes aware
        if (isVisible && playerDistance <= awakedistance && playerinVision)
        {
            OnAware();
        }

        // If aware of the player, chase the player; otherwise, wander
        if (aware)
        {
            navigation.SetDestination(target.position);
        }
        else
        {
            Wander();
        }
    }

    public void OnAware()
    {
        aware = true;

        // Increase the movement speed by 25% when the zombie becomes aware of the player
        if (navigation.isOnNavMesh)
        {
            navigation.speed = originalSpeed * speedMultiplier;
        }
    }

    // Raycast to check if the player is within the enemy's vision
    void drawRay()
    {
        if (target != null)
        {
            Vector3 playerDirection = target.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerDirection, out hit))
            {
                playerinVision = hit.transform.CompareTag("Player");
            }
        }
    }

    // Make the enemy wander randomly if not aware of the player
    public void Wander()
    {
        currentWanderTimer += Time.deltaTime;

        // Check if it's time to find a new random point
        if (currentWanderTimer >= wanderTimer || Vector3.Distance(transform.position, wanderPoint) < 2f)
        {
            wanderPoint = RandomWanderPoint();
            currentWanderTimer = 0f;  // Reset the timer after selecting a new point
        }

        // Move the enemy toward the random point if on NavMesh
        if (navigation.isOnNavMesh)
        {
            navigation.SetDestination(wanderPoint);
        }
    }

    // Find a random point for the enemy to wander to
    public Vector3 RandomWanderPoint()
    {
        int maxAttempts = 30;  // Maximum attempts to find a valid NavMesh point
        for (int i = 0; i < maxAttempts; i++)
        {
            // Generate a random point within the wander radius
            Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
            NavMeshHit navHit;

            // Increase the distance for sampling to make it more likely to find a valid point
            if (NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius * 2f, NavMesh.AllAreas))
            {
                // Valid point found
                return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
            }
        }

        // If no valid point was found after max attempts, log the error and return the current position
        Debug.LogError("Failed to find a valid NavMesh position for wandering after " + maxAttempts + " attempts.");
        return transform.position;  // Return the current position as a fallback
    }

    // Detect collision and change direction when hitting any collider
    private void OnCollisionEnter(Collision collision)
    {
        if (!aware)  // Only change direction if the enemy is wandering (not aware of the player)
        {
            wanderPoint = RandomWanderPoint();  // Choose a new direction on collision
        }
    }

    // If using trigger-based colliders
    private void OnTriggerEnter(Collider other)
    {
        if (!aware)  // Only change direction if the enemy is wandering (not aware of the player)
        {
            wanderPoint = RandomWanderPoint();  // Choose a new direction on trigger hit
        }
    }
}
