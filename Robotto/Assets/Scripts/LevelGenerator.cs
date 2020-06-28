using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;
    public List<LevelBlock> allLevelBlocks=new List<LevelBlock>();
    public Transform levelStartPoint;
    public List<LevelBlock> currentLevelBlocks=new List<LevelBlock>();
    //public LevelBlock firstBlock; variable para seleccionar un bloque

    void Awake()
    {
        sharedInstance=this;        
    }

    void Start()
    {
        GenerateInitialBlocks();
    }
    public void AddLevelBlock(){
        int randomIndex = Random.Range(0,allLevelBlocks.Count);//numero aleatoria de lista de bloques
        LevelBlock currentBlock= (LevelBlock) Instantiate(allLevelBlocks[randomIndex]);//current block es una instancia con el LevelBlock aleatorio
        currentBlock.transform.SetParent(this.transform,false);

        Vector3 spawnPosition=Vector3.zero;
        if(currentLevelBlocks.Count==0){
            //currentBlock=(LevelBlock)Instantiate(firstBlock); Instanciar first block en vez de un aleatorio
            //currentBlock.transform.SetParent(this.transform,false);

            spawnPosition=levelStartPoint.position;//si es es el primero nivel posicion del game object levelStartPoint
            currentBlock.transform.position=spawnPosition;
        }
        else{
            spawnPosition=currentLevelBlocks[currentLevelBlocks.Count-1].endPoint.position;//generar en el endpoint del ultimo de la lista
            Vector3 correction = new Vector3(currentLevelBlocks[currentLevelBlocks.Count-1].transform.position.x+(spawnPosition.x-currentLevelBlocks[currentLevelBlocks.Count-1].startPoint.position.x),
            currentLevelBlocks[currentLevelBlocks.Count-1].transform.position.y,
            0);
           
           //correction en x se calcula sumando la x del bloque anterior + (la distancia del startPoint.x al endpoint.x) del bloque anterior
            currentBlock.transform.position=correction;
        }        
        currentLevelBlocks.Add(currentBlock);
    }

    public void RemoveOldestLevelBlock(){
        //quitar penultimo
        LevelBlock oldestBlock=currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject); 
    }

    public void RemoveAllBlocks(){
        while(currentLevelBlocks.Count>0){
            RemoveOldestLevelBlock();
        }
    }

    public void GenerateInitialBlocks(){
        for(int i=0;i<3;i++){
            AddLevelBlock();
        }
    }
}
