using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce=10f;
    public float walkSpeed=10f;
    private Rigidbody2D rigidbody;
    public LayerMask groundLayer; //el suelo, se asigna en la UI de player
    public Animator animator; 
    public static PlayerController sharedInstace;
    private Vector3 startPosition;
    private int iteration=0;//aumentar velocidad con el tiempo
    public bool walking=false;
    private int healthPoints, manaPoints;
    private float deathPosition=0f;
    void Awake() {
        rigidbody=GetComponent<Rigidbody2D>();    
        sharedInstace=this;
        startPosition=this.transform.position;
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        animator.SetBool("IsAlive", true);
        animator.SetBool("IsGrounded", true);//parametros de la sprite(Animator) que se agrego en la UI al public Animator
        this.transform.position=startPosition;
        this.healthPoints=100;
        this.manaPoints=10;
        deathPosition=0f;

        StartCoroutine("TirePlayer");
    }

    IEnumerator TirePlayer(){
        while(this.healthPoints>0){
            this.healthPoints--;
            yield return new WaitForSeconds(1.0f);
        }
        yield return null;
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

        if(this.transform.position.y<=-10f && deathPosition==0f){
            this.deathPosition=this.transform.position.x;
            this.transform.position=new Vector2(this.transform.position.x,-10f);
        }
        else if(this.transform.position.y<=-10f && deathPosition!=0f){
            this.transform.position=new Vector2(deathPosition,-10f);
        }
    }

    void FixedUpdate() {
        
        if(walking){
            float currentSpeed=(walkSpeed-1.0f)*this.healthPoints / 100f; //vas más rapido entre más vida tengas 0.5 es lo minimo de vel
            rigidbody.velocity=new Vector2(currentSpeed, rigidbody.velocity.y);
        }
        if(GameManager.sharedInstace.currentState==GameState.gameOver){
            walking=false;
        }
        
    }

    void Jump(bool superJump){     
        if(superJump && this.manaPoints>2){
            rigidbody.AddForce(Vector2.up*jumpForce*2, ForceMode2D.Impulse); 
            this.manaPoints-=2;
        }
        else{
            rigidbody.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse); 
        }     
    }
    void GoDown(){
        if(this.manaPoints>0){
            rigidbody.AddForce(Vector2.down*jumpForce*2, ForceMode2D.Impulse); 
        }
        else{
            rigidbody.AddForce(Vector2.down*jumpForce, ForceMode2D.Impulse); 
        } 
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
        StopCoroutine("TirePlayer");
    }

    public int GetDistance(){
        int traveledDistance = (int)Vector2.Distance(new Vector2(startPosition.x,0),new Vector2(this.transform.position.x,0));

        return traveledDistance;
    }

    public void CollectHealth(){
        this.healthPoints+=10;
        if(this.healthPoints>100){
            this.healthPoints=100;
        }
    }

    public void CollectMana(){
        this.manaPoints+=2;
        if(this.manaPoints>10){
            this.manaPoints=10;
        }
    }

    public int GetHealth(){
        return this.healthPoints;
    }

    public int GetMana(){
        return this.manaPoints;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Enemy"){
            this.healthPoints-=20;
        }

        if(GameManager.sharedInstace.currentState == GameState.inGame && this.healthPoints<=0){
            Kill();
        }
    }
}
