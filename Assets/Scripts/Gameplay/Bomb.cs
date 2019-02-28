using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int timer;
    public GameObject blast;

    bool alive;

    void Start()
    {
        StartCoroutine(BombTimer(timer));
        alive = true;
    }

    IEnumerator BombTimer(int countdown){
        yield return new WaitForSeconds(countdown);
        if (alive){
            Explode();
            Destroy(gameObject);
        }
    }

    void Explode(){
        alive = false;
        GameObject objBlast = Instantiate(blast, transform.position, transform.rotation);
        Destroy(gameObject);
    }

   void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Boom" && alive){
            Explode();
        }
    } 
}
