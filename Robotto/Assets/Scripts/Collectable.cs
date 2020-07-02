using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableTypes{
    healthPotion,
    manaPotion,
    money
}
public class Collectable : MonoBehaviour
{
    public CollectableTypes types=CollectableTypes.money; //se le asigna a cada prefab en la UI
    bool isCollected=false;
    public AudioClip collectSound;
    //public int value=0;//cambiar en la UI si quiere tener diferentes colleactables con diferente valor 
    void Show(){
        this.GetComponent<SpriteRenderer>().enabled=true; //sprite renderer tambien activa la animacion
        this.GetComponent<CircleCollider2D>().enabled=true;
        isCollected=false;
    }

    void Collect(){
        isCollected=true;
        Hide();      

        AudioSource audio=GetComponent<AudioSource>();
        if(audio!=null && this.collectSound!=null){
            audio.PlayOneShot(this.collectSound);
        }

        if(types==CollectableTypes.money){
            GameManager.sharedInstace.CollectObject();
        }
        // else if(types==CollectableTypes.healthPotion){
        //     PlayerController.sharedInstace.CollectHealth();
        // }
        // else if(types==CollectableTypes.manaPotion){
        //     PlayerController.sharedInstace.CollectMana();
        // } 
    }

    void Hide(){
        this.GetComponent<SpriteRenderer>().enabled=false; //sprite renderer tambien activa la animacion
        this.GetComponent<CircleCollider2D>().enabled=false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"){
            Collect();
        }
    }
}
