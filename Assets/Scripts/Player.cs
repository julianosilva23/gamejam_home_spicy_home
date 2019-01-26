using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    int resource;
    int home;
    bool alive;
    bool hasBomb;

    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        resource = 0;
        home = 0;
        alive = true;
        hasBomb = true;
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Resource"){
            resource ++;
        }

        if (col.tag == "Boom"){
            alive = false;
            StartCoroutine(Cooldown(alive, 5));
        }
    }

    IEnumerator Cooldown(bool state, int timer){
        yield return new WaitForSeconds(timer);
        state = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive){
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2
            (moveSpeed * Time.fixedDeltaTime * Input.GetAxis("Horizontal"), moveSpeed * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (hasBomb){
            if (Input.GetButtonDown("Fire1")){
                hasBomb = false;
                Instantiate(bomb, transform.position, transform.rotation);
                StartCoroutine(Cooldown(hasBomb, 5));
            }
        }
    }

    void PlayerMove(){
        
    }
}