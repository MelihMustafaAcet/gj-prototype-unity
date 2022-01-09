using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int teammateCount = 0;
    private Collider lastCollide=null;
    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag=="TeamMate"){
            Collider temp = other;
            if(lastCollide==null||temp!=lastCollide){
                lastCollide=temp;
                teammateCount+=1;
            }
        }
    }
}
