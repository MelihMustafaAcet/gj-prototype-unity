using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float xOffset;
    public float maxX;
    public float minX;

    void FixedUpdate(){ 
        /*if(target.position.x<0){
            offset.x=xOffset;
            //temp = -1*xOffset;
        }
        else if(target.position.x>0){
            offset.x=-1*xOffset;
        }
        else if(target.position.x==0){
            offset.x=0;
        }*/
        //offset.x=(target.position.x/5)*(-1);
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        if(transform.position.x>=maxX){
            transform.position = new Vector3(maxX,transform.position.y,transform.position.z);
        }
        else if(transform.position.x<=minX){
            transform.position = new Vector3(minX,transform.position.y,transform.position.z);
        }



        //transform.LookAt(target);
    }
}
