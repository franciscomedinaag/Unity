using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    menu,
    inGame,
    gameOver
}
public class GameManager : MonoBehaviour
{
    public GameState currentState=GameState.inGame;
    public static GameManager sharedInstace;
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;

    public int collectedObjects=0;
    void Awake()
    {
        sharedInstace=this;    //por medio de esta instancia puedo acceder desde cualquier script al GameManager
    }

  private void Update() {
    if(Input.GetKeyDown(KeyCode.Escape)){     
        ExitGame(); 
    }
  }
  public void StartGame(){
      if(currentState==GameState.menu || currentState==GameState.gameOver){
        SetGameState(GameState.inGame);
        GameObject camera= GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow=camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();//asi se llama a la instancia (script) de un GameObject

           if(PlayerController.sharedInstace.transform.position.x>=16){
                LevelGenerator.sharedInstance.RemoveAllBlocks();
                LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }
        PlayerController.sharedInstace.StartGame(); 
        PlayerController.sharedInstace.walking=true; 
        this.collectedObjects=0;
      } 
  }

  public void GameOver(){
      SetGameState(GameState.gameOver);
  }

  public void BackToMenu(){
      SetGameState(GameState.menu);
  }

  public void ExitGame(){
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
      #else
        Application.Quit();
      #endif
  }

  void SetGameState(GameState state){
      this.currentState=state;

      if (state==GameState.menu){
          menuCanvas.enabled=true;
          gameCanvas.enabled=false;
          gameOverCanvas.enabled=false;
      }
      else if(state==GameState.inGame){
          menuCanvas.enabled=false;
          gameCanvas.enabled=true;
          gameOverCanvas.enabled=false;
      }
      else if( state==GameState.gameOver){
          menuCanvas.enabled=false;
          gameCanvas.enabled=false;
          gameOverCanvas.enabled=true;
      }
  }

  public void CollectObject(){
      this.collectedObjects+=1;
  }
}
