using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public Text scoreLabel, maxScoreLabel;
  
    void Start()
    {
        int maxScore=PlayerPrefs.GetInt("maxscore",0);
        this.maxScoreLabel.text="MaxScore:\n"+maxScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstace.currentState == GameState.inGame){
            int currentObjects= GameManager.sharedInstace.collectedObjects;
            //this.colleactableLabel.text=currentObjects.ToString();  

            int traveledDistance= PlayerController.sharedInstace.GetDistance();
            this.scoreLabel.text="Score:\n"+traveledDistance.ToString();       
        }
    }
}
