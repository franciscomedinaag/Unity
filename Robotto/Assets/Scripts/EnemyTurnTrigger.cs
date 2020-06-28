using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnTrigger : MonoBehaviour
{
    public bool movingForward;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Collectable" || other.tag=="ExitZone" || other.tag=="Player"){
            return;
        }
            if(movingForward==true){
                Enemy.turnAround=true;
            }    
            else{
                Enemy.turnAround=false;
            }
            movingForward=!movingForward;

        
        
    }
}
