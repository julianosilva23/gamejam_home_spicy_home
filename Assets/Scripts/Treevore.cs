using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject drop;

    public int amount;

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Boom"){
            Debug.Log("Arvore Imortal eh o krl!!!");
            while (amount > 0){
                Instantiate(drop, transform.position, transform.rotation);
                amount --;
            }
            Destroy(gameObject);
        }
    }
}
