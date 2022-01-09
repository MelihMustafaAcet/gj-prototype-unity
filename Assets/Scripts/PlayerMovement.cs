using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed=1f;

    bool isRunning=false;
    void Update()
    {   
        if(isRunning)
            transform.Translate(0,0,speed*Time.deltaTime);
    }

    public void StopRunning(){
        isRunning=false;
    }

    public void StartRunning()
    {
        isRunning=true;
    }
}
