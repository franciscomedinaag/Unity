using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float walkSpeed=0.5f;
    private Rigidbody2D rigidbody;
    public static bool turnAround;
    void Awake() {
        rigidbody=GetComponent<Rigidbody2D>();    
    }
    void FixedUpdate() {

        float currentWalkSpeed=walkSpeed;

        if(turnAround){//girar sprite
        //derecha
            currentWalkSpeed=walkSpeed;
            this.transform.eulerAngles=new Vector3(0f, 180f, 0f);
        }
        else{
        //izquierda
            currentWalkSpeed= -walkSpeed;
            this.transform.eulerAngles=new Vector3(0f, 0f, 0f);
        }

       // if(GameManager.sharedInstace.currentState==GameState.inGame){
            rigidbody.velocity=new Vector2(currentWalkSpeed, rigidbody.velocity.y);
        //}  
    }

}
