﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float maxTime=60f;
    private float countDown=0f;
    // Start is called before the first frame update
    void Start()
    {
        countDown=maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown<=0){
            Debug.Log("Time Over!! "+Coin.coinsCount+" coins left");
            Coin.coinsCount=0;
            SceneManager.LoadScene("MainScene");
        }
        else{
            countDown-=Time.deltaTime;
        }
        
    }
}
