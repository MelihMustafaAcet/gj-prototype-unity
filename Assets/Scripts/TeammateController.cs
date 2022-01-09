using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeammateController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject parent;
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;
    public float sweepDistance;
    float dist;
    Vector3 holePos;
    TeammateMovement t1;
    TeammateMovement t2;
    TeammateMovement t3;
    bool isEmptyPos1=true;
    bool isEmptyPos2=true;
    bool isEmptyPos3=true;
    bool isPlayerActive=true;
    bool isSweeping=false;
    bool isFalling=false;
    bool isF=false;
    int teammateCount=0;
    // Update is called once per frame
    void Update()
    {
        if(isSweeping){
            //
            if(dist<sweepDistance){
                transform.Translate(0,0,3*Time.deltaTime);
                dist+=3*Time.deltaTime;
            }
            else{
                isSweeping=false;
                //Destroy(gameObject,3f);
            }
        }
        if(isFalling){
            if(isF){
                float step = 5 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position,holePos, step);
                if(transform.position.x==holePos.x&&transform.position.z==holePos.z){
                    //transform.position=holePos;
                    isF=false;
                    this.GetComponent<Rigidbody>().useGravity = false;
                    //isRunning=false;
                }
            }
            else if(transform.position.y<-20){
                //Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other){
        if(!isPlayerActive)
            return;
        if(other.tag=="TeamMate")
        {
            if(!other.GetComponent<TeammateMovement>().CheckAvailability())
            {
                return;
            }
            else if(isEmptyPos1){
                t1 = other.GetComponent<TeammateMovement>();
                t1.ActivateTeammate(Pos1,1);
                //other.GetComponent<TeammateMovement>().ActivateTeammate(Pos1,1);
                teammateCount++;
                isEmptyPos1=false;
            }
            else if(isEmptyPos2){
                t2 = other.GetComponent<TeammateMovement>();
                t2.ActivateTeammate(Pos2,2);
                //other.GetComponent<TeammateMovement>().ActivateTeammate(Pos2,2);
                teammateCount++;
                isEmptyPos2=false;
            }
            else if(isEmptyPos3){
                t3 = other.GetComponent<TeammateMovement>();
                t3.ActivateTeammate(Pos3,3);
                //other.GetComponent<TeammateMovement>().ActivateTeammate(Pos3,3);
                teammateCount++;
                isEmptyPos3=false;
            }
        }
        if(other.tag=="Water")
        {
            isSweeping=true;
            this.GetComponent<Animator>().SetBool("isSweeping",true);
            DisablePlayer();
        }
        if(other.tag=="Hole")
        {
            holePos = other.transform.position;
            DisablePlayer();
            isFalling=true;
            isF=true;
            this.GetComponent<Rigidbody>().useGravity = true;
        }
        if(other.tag=="EndTrigger")
        {
            this.GetComponent<Animator>().SetBool("isDancing",true);
            gameManager.CompleteLevel();
        }
    }

    public void UnderAttack(){
        this.GetComponent<Animator>().SetBool("isUnderAttack",true);
        DisablePlayer();
    }
    public void DisablePlayer(){
        parent.GetComponent<PlayerMovement>().StopRunning();
        parent.GetComponent<SwerveInputSystem>().DisableSwerveInput();
        parent.GetComponent<SwerveMovement>().DisableSwerveMove();
        gameManager.PlayerFailed();
        isPlayerActive=false;
    }
    public void TeammateIsGone(int posNumber){
        if(posNumber==1){
            teammateCount--;
            isEmptyPos1=true;
            t1=null;
        }
        else if(posNumber==2){
            teammateCount--;
            t2=null;
            isEmptyPos2=true;
        }
        else if(posNumber==3){
            teammateCount--;
            t3=null;
            isEmptyPos3=true;
        }
    }

    public bool IsAlone(){
        if(teammateCount<=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CalculateNearestTeammate(GameObject enemy){
        float distance = 9999f;
        float d=99999f;
        int pos=0;
        if(!isEmptyPos1&&(t1!=null)){
            d = Vector3.Distance(enemy.transform.position,Pos1.transform.position);
            if(d<distance)
            {
                distance=d;
                pos=1;
            }
        }
        if(!isEmptyPos2&&(t2!=null)){
            d = Vector3.Distance(enemy.transform.position,Pos2.transform.position);
            if(d<distance)
            {
                distance=d;
                pos=2;
            }
        }
        if(!isEmptyPos3&&(t3!=null)){
            d = Vector3.Distance(enemy.transform.position,Pos3.transform.position);
            if(d<distance)
            {
                distance=d;
                pos=3;
            }
        }

        if(pos==1){
            t1.AttackEnemy(enemy);
        }
        else if(pos==2){
            t2.AttackEnemy(enemy);
        }
        else if(pos==3){
            t3.AttackEnemy(enemy);
        }
        else{
            Debug.Log("No Teammate");
        }
    }

    public void GetACoin()
    {
        gameManager.IncrementCoin();
    }
}
