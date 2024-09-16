using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemyMovement : MonoBehaviour
{
    
    public float fov = 120f;
    public Transform target; 
    public bool isVisible;

    public float awakedistance = 200f; 

    public bool aware ; 

    public NavMeshAgent navigation;

    private void Update()
    {
        float playerDistance = Vector3.Distance(target.position, transform.position );

        Vector3 playerDirection = target.position - transform.position ;  

        float playerAngle = Vector3.Angle(transform.forward,playerDirection);

        if(playerAngle <= fov/2f){
            isVisible = true ; 
            Debug.Log("isVisible");
        }
        else 
        {
            isVisible = false; 
        }
        if(isVisible == true && playerDistance <= awakedistance){
            aware = true ; 
        }
        if(aware==true){
            navigation.SetDestination(target.position);
        }
    }
}
