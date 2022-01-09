using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeamController : MonoBehaviour
{
    public GameObject target;//main player
    GameObject targetTeammate;
    public float speed;
    public Vector3 targetHitOffset;
    public float runDistance;
    public float attackDistance;
    Vector3 targetPosition;
    bool isRunning=false;
    bool isUnderAttack=false;
    bool isAttacking=false;
    float currentDistance=999f;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(isUnderAttack){
            //
            /*float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,target.transform.position,step);
            */
            if(targetTeammate!=null)
                transform.LookAt(targetTeammate.transform);
            return;
        }
        if(isAttacking){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,target.transform.position-targetHitOffset,step);
            transform.LookAt(target.transform);
            return;
        }
        if(isRunning){
            //start running towards player
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,target.transform.position,step);
            transform.LookAt(target.transform);
            //then teammates need to jump on enemy
            //if it has no teammate then enemy will jump to the player
            //we can make it with controlling teammate count
            //if it has teammate then we will calculate the distances,then closest one
            //will jump to enemy,then no problem
            //if no teammate,we need to give a jumping distance,when the distance is that close
            //or we can use colliders.
            //enemy jumps and game ends

            //Attacking player distance detection
            currentDistance = Vector3.Distance(target.transform.position,transform.position);
            if(currentDistance<attackDistance)
            {
                if(target.GetComponent<TeammateController>().IsAlone()){
                    AttackPlayer();
                }
            }
            return;
        }
        if(currentDistance>runDistance){
            currentDistance = Vector3.Distance(target.transform.position,transform.position);
        }
        else if(currentDistance<runDistance&&(!isRunning))
        {
            isRunning = true;
            this.GetComponent<Animator>().SetBool("isRunning",true);
        }
        
    }
    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            if(target.GetComponent<TeammateController>().IsAlone())
            {
                /*Debug.Log("Player down!");
                target.GetComponent<TeammateController>().UnderAttack();
                this.GetComponent<Animator>().SetBool("isAttacking",true);
                isAttacking=true;
                */
            }
            else
            {
                target.GetComponent<TeammateController>().CalculateNearestTeammate(this.gameObject);
                isUnderAttack=true;
                //this.GetComponent<Animator>().SetBool("isTaken",true);
                //calculate closest one
                //give this as a target
                //perform actions
            }
        }
    }

    public void UnderAttack(GameObject t){
        //target changed into given teammate
        targetTeammate=t;
        isRunning=false;
        isUnderAttack=true;
        this.GetComponent<SphereCollider>().enabled=false;
        this.GetComponent<Animator>().SetBool("isTaken",true);
        Destroy(gameObject,5f);
    }

    void AttackPlayer(){
        target.GetComponent<TeammateController>().UnderAttack();
        this.GetComponent<Animator>().SetBool("isAttacking",true);
        //targetPosition = target.transform.position-targetHitOffset;
        isAttacking=true;
        isRunning=false;
    }
}

    
