using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType{
    health, 
    mana
}
public class PlayerBar : MonoBehaviour
{
    // private Slider slider;
    // public BarType type;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     this.slider=GetComponent<Slider>();

        
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     switch(type){
    //         case BarType.health:{
    //             this.slider.value=PlayerController.sharedInstace.GetHealth();
    //             break;
    //         }
    //         case BarType.mana:{
    //             this.slider.value=PlayerController.sharedInstace.GetMana();
    //             break;
    //         }
    //     }
    // }
}
