using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    public bool isFiring=false ;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffects;
    public Transform RaycastOrigin;
    public Transform RaycastDestination;
    Ray ray;
    RaycastHit hitInfo;

   public void StartFiring()
    {
        isFiring = true;
        foreach(var particle in muzzleFlash)
        {
            particle.Emit(1);
        }
        ray.origin = RaycastOrigin.position;
        ray.direction = RaycastDestination.position - RaycastOrigin.position;
        if (Physics.Raycast(ray,out hitInfo))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);//this will draw a ray from gun to destination
            hitEffects.transform.position = hitInfo.point;
            hitEffects.transform.forward = hitInfo.normal;
            hitEffects.Emit(1);

        }
    }
    public void StopFiring()
    {
        isFiring = false;
    }
   
}
