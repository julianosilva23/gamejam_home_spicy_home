using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public int bombCooldown;
    public int blastCooldown;
    public int resource;
    public int home;
    bool alive;
    bool hasBomb;

    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        setResource(0);

        setHome(0);

        alive = true;

        hasBomb = true;
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Resource"){
            resource ++;
        }

        if (col.tag == "Boom"){
            alive = false;
            StartCoroutine(BlastCooldown(blastCooldown));
        }
    }

    IEnumerator BlastCooldown(int timer){
        yield return new WaitForSeconds(timer);
        alive = true;
    }

    IEnumerator BombCooldown(int timer){
        while (timer > 0){
            yield return new WaitForSeconds(1);
            timer --;
        }
        hasBomb = true;
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
                StartCoroutine(BombCooldown(bombCooldown));
            }
        }
    }

    void PlayerMove(){
        
    }

    public int getResource(){
        return resource;
    }

    public int setResource(int value){
        resource = value;

        return resource;
    }

    public int getHome(){

        return home;
    }

    public int setHome(int value){

        home = value;

        return home;
    }
}