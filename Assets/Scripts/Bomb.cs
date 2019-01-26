using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int timer;
    public GameObject blast;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BombTimer(timer));
    }

    IEnumerator BombTimer(int countdown){
        yield return new WaitForSeconds(countdown);
        Explode();
    }

    void Explode(){
        GameObject objBlast = Instantiate(blast, transform.position, transform.rotation);
        Destroy(gameObject);
    }

   void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Boom"){
            Explode();
        }
    } 
}
