
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public CharacterController controller ;
    public float moveSpeed = 10f;
    public float gravity = -10f;
    public Transform groundcheck;
    public float groundis = 0.4f;    
    public LayerMask groundmask;
    bool isGround;
    Vector3 velocity ;
       void Update()
    {    isGround = Physics.CheckSphere(groundcheck.position,groundis,groundmask);
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
