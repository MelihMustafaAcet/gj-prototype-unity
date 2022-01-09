using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeammateMovement : MonoBehaviour
{
    public float speed=7f;
    public float activationDelay;
    public float sweepDistance=3f;
    public Vector3 hitBoxPos;
    public Vector3 hitBoxSize;
    public Vector3 targetOffset;
    float timer;
    float dist;
    public bool isRunning=false;
    public GameObject parentObject;
    BoxCollider col;
    Collider lastCollider;
    GameObject targetPos;
    GameObject targetEnemy;
    Vector3 holePos;
    bool isFalling=false;
    bool isF=false;
    bool isSweeping=false;
    bool isAttacking=false;
    int positionNumber;
    bool isActivated=false;
    bool isAvailable=true;


    void Start(){
        col = this.GetComponent<BoxCollider>();
    }
    void Update()
    {   
        if(isAttacking){
            float step = 0.5f*speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,targetEnemy.transform.position-targetOffset, step);
            transform.LookAt(targetEnemy.transform);
        }
        if(isFalling){
            if(isF){
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position,holePos, step);
                if(transform.position.x==holePos.x&&transform.position.z==holePos.z){
                    //transform.position=holePos;
                    isF=false;
                    //isRunning=false;
                }
            }
            else if(transform.position.y<-20){
                Destroy(gameObject);
            }
        }
        if(isSweeping){
            //
            if(dist<sweepDistance){
                transform.Translate(0,0,3*Time.deltaTime);
                dist+=3*Time.deltaTime;
            }
            else{
                isSweeping=false;
                Destroy(gameObject,3f);
            }
            
        }

        else if(isRunning){
            if(activationDelay<timer)
            {
                //if(targetPos!=null)
                if(targetPos!=null)
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position,targetPos.transform.position, step);
                    //transform.position = Vector3.MoveTowards(transform.position,target, step);
                    if(transform.position==targetPos.transform.position){
                        transform.position=targetPos.transform.position;
                        isRunning=false;
                    }
                }
            }
            else
                timer+=Time.deltaTime;
        }
    }

    public void ActivateTeammate(GameObject pos,int posN){
        if(!isActivated)
        {
            //col.enabled=false;
            isAvailable=false;
            col.size = hitBoxSize;
            col.center = hitBoxPos;
            isRunning = true;
            isActivated = true;
            timer=0f;
            this.GetComponent<Animator>().SetBool("isRunning",true);
            //targetPos = pos;
            targetPos = pos;
            positionNumber = posN;
            this.transform.parent=parentObject.transform;
        }
    }

    void OnTriggerEnter(Collider other){
        if(lastCollider!=null){
            if(lastCollider==other)
                return;
        }
        if(other.tag=="Hole"){
            parentObject.GetComponent<TeammateController>().TeammateIsGone(positionNumber);
            //targetPos = other.GetComponent<GameObject>();
            holePos = other.transform.position;
            isFalling=true;
            isF=true;
            //isRunning=true;
            this.transform.parent=null;
            this.GetComponent<Rigidbody>().useGravity = true;
            lastCollider=other;
        }
        else if(other.tag=="Water"){
            parentObject.GetComponent<TeammateController>().TeammateIsGone(positionNumber);
            isSweeping=true;
            dist=0;
            this.transform.parent=null;
            //after slip animation.we can destroy it
            this.GetComponent<Animator>().SetBool("isSweeping",true);
            //this.GetComponent<Animator>().SetBool("isRunning",false);
            lastCollider=other;
        }
        else if(other.tag=="EndTrigger"){
            this.GetComponent<Animator>().SetBool("isDancing",true);
            lastCollider=other;
        }
    }

    public void AttackEnemy(GameObject enemy){
        targetEnemy = enemy;
        parentObject.GetComponent<TeammateController>().TeammateIsGone(positionNumber);
        this.transform.parent=null;
        isAttacking=true;//
        this.GetComponent<Animator>().SetBool("isAttacking",true);
        enemy.GetComponent<EnemyTeamController>().UnderAttack(this.gameObject);
        Destroy(gameObject,5f);
    }

    public bool CheckAvailability(){
        return isAvailable;
    }
}
