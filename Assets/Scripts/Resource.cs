using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    bool immortal;
    
    // Start is called before the first frame update
    void Start()
    {
        immortal = true;
        StartCoroutine(Mortality());
    }

    IEnumerator Mortality(){
        yield return new WaitForSeconds(2);
        immortal = false;
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Boom" && immortal == false){
            Destroy(gameObject);
        }
    }   
}
