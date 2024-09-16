using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotSound : MonoBehaviour
{
    public AudioClip shootSound;
    public float soundIntensity=5f;
    public LayerMask zombieLayer;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public void Fire()
    {
        audioSource.PlayOneShot(shootSound);  
        Collider[] zombies=Physics.OverlapSphere(transform.position,soundIntensity,zombieLayer);
        for(int i=0;i<zombies.Length;i++)
        {
            zombies[i].GetComponent<enemymovement>().OnAware();
        }
    }
}
