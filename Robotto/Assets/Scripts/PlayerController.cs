using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce=60f;
    private float walkSpeed=3f;
    private Rigidbody2D rigidbody;
    public LayerMask groundLayer; //el suelo, se asigna en la UI de player
    public Animator animator; 
    public static PlayerController sharedInstace;
    private Vector3 startPosition;
    private int iteration=0;//aumentar velocidad con el tiempo
    public bool walking=false;
    private Vector3 lastPosition;
    // private float deathPosition=0f;
    void Awake() {
        rigidbody=GetComponent<Rigidbody2D>();    
        sharedInstace=this;
        startPosition=this.transform.position;
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        lastPosition=this.transform.position;
        //lastPosition.x-=1f;
        animator.SetBool("IsAlive", true);
        animator.SetBool("IsGrounded", true);//parametros de la sprite(Animator) que se agrego en la UI al public Animator
        this.transform.position=startPosition;
        this.transform.position=new Vector2(startPosition.x,startPosition.y);
    }

  

    // Update is called once per frame
    void Update()
    {
        if(IsInTheFloor()){
            if(Input.GetKeyDown(KeyCode.Space)){
                Jump(true);//super salto
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow)){
                Jump(false);
            }
        }
        else{
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                GoDown();
            }
        }
        animator.SetBool("isGrounded", IsInTheFloor());

    }

    void FixedUpdate() {
        if(walking){
            int multi=GetDistance()/500;
            if(multi>0){
                rigidbody.velocity=new Vector2(walkSpeed+(multi/2), rigidbody.velocity.y);
            }
            else{
                rigidbody.velocity=new Vector2(walkSpeed, rigidbody.velocity.y);
            }
        }
        if(GameManager.sharedInstace.currentState==GameState.gameOver){
            walking=false;
        }
        
        if(GameManager.sharedInstace.currentState==GameState.inGame){
            
            StartCoroutine(Stoping());
            
           // this.lastPosition=this.transform.position;
        }
    }

    IEnumerator Stoping(){
            if(this.transform.position.x==this.lastPosition.x){
                this.lastPosition=this.transform.position;
                yield return new WaitForSeconds(3.0f);
                if(this.transform.position.x==this.lastPosition.x){
                    Kill();
                }
            }
            this.lastPosition=this.transform.position;
    }

    void Jump(bool superJump){     
        
            rigidbody.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse); 
          
    }
    void GoDown(){
       
            rigidbody.AddForce(Vector2.down*jumpForce, ForceMode2D.Impulse); 
        
    }

    bool IsInTheFloor(){
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer)){
            //si estas a 20cm o menos de algun groundLayer
            return true;
        }
        else{
            return false;
        }
    }

    public void Kill(){
        GameManager.sharedInstace.GameOver();
        animator.SetBool("IsAlive", false);

        int maxScore=PlayerPrefs.GetInt("maxscore",0);
        if(maxScore < this.GetDistance()){
            PlayerPrefs.SetInt("maxscore", this.GetDistance());
        }
      //StopCoroutine("Dying");
    }

    public int GetDistance(){
        int traveledDistance = (int)Vector2.Distance(new Vector2(startPosition.x,0),new Vector2(this.transform.position.x,0));
        traveledDistance+=GameManager.sharedInstace.collectedObjects*50;
        return traveledDistance;
    }

    // public void CollectHealth(){
    //     this.healthPoints+=10;
    //     if(this.healthPoints>100){
    //         this.healthPoints=100;
    //     }
    // }

    // public void CollectMana(){
    //     this.manaPoints+=2;
    //     if(this.manaPoints>10){
    //         this.manaPoints=10;
    //     }
    // }

    // public int GetHealth(){
    //     return this.healthPoints;
    // }

    // public int GetMana(){
    //     return this.manaPoints;
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Enemy"){
            Kill();
        }
    }
}
