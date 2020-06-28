using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset= new Vector3(10.0f, 1.0f,-10.0f);
    public float dampTime=0.3f;
    public Vector3 velocity=Vector3.zero;
  
    void Awake()
    {
        Application.targetFrameRate=60;      
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point= GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta=target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        
        Vector3 destination= point+delta;//a donde quiero que siga MENOS el offset
        destination=new Vector3(target.position.x+8f, offset.y, offset.z);//que no salte con el personaje
        this.transform.position=Vector3.SmoothDamp(this.transform.position,destination, ref velocity, dampTime);
    }

/*
    que al inciar un juego la camara no se barra al inicio
*/
    public void ResetCameraPosition(){
        Vector3 point= GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta=target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        
        Vector3 destination= point+delta;//a donde quiero que siga MENOS el offset
        destination=new Vector3(target.position.x+8f, offset.y, offset.z);//que no salte con el personaje
        this.transform.position=destination;
    }
}
