using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    public Camera mainCamera; // Make this public to assign it manually if needed
    Ray ray;
    RaycastHit hitInfo;

    public void Start()
    {
        if (mainCamera == null)  // If not assigned in Inspector, fallback to Camera.main
        {
            mainCamera = Camera.main;
        }

        if (mainCamera == null)  // Check again and log if no camera is found
        {
            Debug.LogError("Main Camera is not assigned and no MainCamera tag is found in the scene.");
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (mainCamera != null)  // Safety check to ensure mainCamera is not null
        {
            // Initialize the ray by creating a new Ray object
            ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

            // Perform the raycast
            if (Physics.Raycast(ray, out hitInfo))
            {
                // Move the transform to the hit point
                transform.position = hitInfo.point;
            }
        }
        else
        {
            Debug.LogError("Main Camera is not assigned.");
        }
    }
}
