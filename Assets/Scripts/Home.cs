using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    bool fire;
    public Player owner;

    // Start is called before the first frame update
    void Start()
    {
       fire = false;
       owner.setHome(owner.getHome() + 1);
    }

     void OnTriggerEnter2D(Collider2D col){
        if (fire){
           return;
        } else {
            if (col.tag == "Boom"){
            fire = true;
            StartCoroutine(OnFire(10));
            Debug.Log("Ta pegando fogo bixo!!!");
            }
        }
    } 
    
    IEnumerator OnFire(int timer){
        yield return new WaitForSeconds(timer);
        owner.setHome(owner.getHome() - 1);
        Destroy(gameObject);
    }

}
