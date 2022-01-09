using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    //private SwerveInputSystem _swerveInputSystem;
    public SwerveInputSystem swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;

    public GameObject player;
    public GameObject cameraT;
    public float playerMultiplier;
    public float cameraMultiplier;
    public float maxX;
    public float minX;

    bool isActive=false;
    /*private void Awake()
    {
        if(isActive)
        {
            //_swerveInputSystem = this.GetComponent<SwerveInputSystem>();
        }

    }*/

    private void Update()
    {   
        if(isActive)
        {
            //float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
            float swerveAmount = Time.deltaTime * swerveSpeed * swerveInputSystem.MoveFactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);

            player.transform.Translate(swerveAmount*playerMultiplier, 0, 0);
            cameraT.transform.Translate(swerveAmount*cameraMultiplier, 0, 0);
            //transform.Translate(swerveAmount, 0, 0);

            
            if(player.transform.position.x>=maxX){
                player.transform.position = new Vector3(maxX,player.transform.position.y,player.transform.position.z);
            }
            else if(player.transform.position.x<=minX){
                player.transform.position = new Vector3(minX,player.transform.position.y,player.transform.position.z);
            }

            if(cameraT.transform.position.x>=maxX*cameraMultiplier){
                cameraT.transform.position = new Vector3(maxX*cameraMultiplier,cameraT.transform.position.y,cameraT.transform.position.z);
            }
            else if(cameraT.transform.position.x<=minX*cameraMultiplier){
                cameraT.transform.position = new Vector3(minX*cameraMultiplier,cameraT.transform.position.y,cameraT.transform.position.z);
            }
        }
    }

    public void DisableSwerveMove(){
        isActive=false;
    }

    public void EnableSwerveMove(){
        isActive=true;
    }
}