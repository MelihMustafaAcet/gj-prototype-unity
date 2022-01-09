using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float speed=2f;

    bool isUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,speed*Time.deltaTime,0);
    }

    void OnTriggerEnter(Collider other){
        if(isUsed)
            return;
        if(other.tag=="Player")
        {
            isUsed=true;
            other.GetComponent<TeammateController>().GetACoin();
            Destroy(gameObject);
        }
        
    }
}
