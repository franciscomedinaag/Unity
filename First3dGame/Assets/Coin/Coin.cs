using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinsCount=0;

    // Start is called before the first frame update
    void Start()
    {
      Coin.coinsCount++;
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Coin.coinsCount--;
            Debug.Log(Coin.coinsCount+" coins left!!!");
            Destroy(gameObject);
            if(Coin.coinsCount==0){
                Debug.Log("You Win!!");
                GameObject gameManager= GameObject.Find("GameManager");
                Destroy(gameManager);
                
                GameObject[] fireworks=GameObject.FindGameObjectsWithTag("Firework");
                foreach (GameObject firework in fireworks)
                {
                    firework.GetComponent<ParticleSystem>().Play();
                }
            }
        }
    }
}
