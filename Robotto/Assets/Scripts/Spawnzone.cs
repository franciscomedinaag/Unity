using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnzone : MonoBehaviour
{
   float timeSinceLastDestruction = 0.0f;
   void Update()
   {
       timeSinceLastDestruction+=Time.deltaTime;
   }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            if(timeSinceLastDestruction>=3.0f){
                LevelGenerator.sharedInstance.AddLevelBlock();
                if(LevelGenerator.sharedInstance.currentLevelBlocks.Count==3){
                    LevelGenerator.sharedInstance.AddLevelBlock();
                }
            LevelGenerator.sharedInstance.RemoveOldestLevelBlock();
            timeSinceLastDestruction = 0.0f;
            }
        }
    }
}
