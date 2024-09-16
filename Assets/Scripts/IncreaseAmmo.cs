using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAmmo : MonoBehaviour
{
    // public float ammoAmount = 6f;  // Amount of ammo to be added to reserve

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))  // Check if the player collided with the ammo pickup
    //     {
    //         RayCast playerAmmo = other.GetComponent<RayCast>();  // Get the RayCast component on the player
    //         if (playerAmmo != null)
    //         {
    //             // Increase the reserve ammo, without automatically reloading
    //             playerAmmo.AmmoIncrease(ammoAmount);  // Increase only the reserve ammo (maxAvailableAmmo)

    //             // No automatic reload; bulletsInPistol will remain the same, and the player will manually reload
    //             playerAmmo.UpdateBulletUI();  // Update the UI to reflect the new ammo counts
    //         }

    //         Destroy(gameObject);  // Destroy the ammo pickup after it's used
    //     }
    // }
}
