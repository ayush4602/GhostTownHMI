using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public CharacterController controller ;
    public float moveSpeed = 10f;
    public float running_speed=15f;
    public float walking_speed =7.5f;
    public float gravity = -10f;
    public Transform groundcheck;
    public float groundis = 0.4f;    
    public LayerMask groundmask;

   // public bool isWalking;
 
    bool isGround;
    
    Vector3 velocity ;
       void Update()
    {    if(Input.GetKey(KeyCode.LeftShift)){
        moveSpeed = running_speed;
     
       // isWalking = false;
        
    }
    else{moveSpeed = walking_speed;
        // isWalking = true;
         }
        isGround = Physics.CheckSphere(groundcheck.position,groundis,groundmask);
        if(isGround && velocity.y<0){
            velocity.y=-2f;
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
       
        
        Vector3 move = transform.right*x +transform.forward*y;
        controller.Move(move*moveSpeed*Time.deltaTime);
        velocity.y += gravity*Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);
    }

}
