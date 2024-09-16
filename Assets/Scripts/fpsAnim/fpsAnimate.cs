
using UnityEngine;

public class fpsAnimate : MonoBehaviour
{    
    public Animator anim;
    public FirstPersonMovement player;

    public RayCast rayCast;
    void Update()
    {
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if(x==0&&y==0){
            anim.SetBool("idle",true);
            anim.SetBool("walking",false);
           // anim.SetBool("running",false);}
        }
        else{
            
            anim.SetBool("idle",false);
            anim.SetBool("walking",true);
          //  anim.SetBool("running",false);}
          //  if(player.isWalking==false){
           // anim.SetBool("idle",false);
            //anim.SetBool("walking",false);
           // anim.SetBool("running",true);}
        }
       
    }
}
