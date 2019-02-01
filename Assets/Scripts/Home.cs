using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public bool fire;
    public GameObject flames;
    public Player owner;

    public int fireTime;
    public Sprite halfDone;
    public Sprite done;

    public AudioClip incendio;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
       fire = false;
       owner.setHome(owner.getHome() + 1);
       audioSource = GetComponent<AudioSource> ();
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Boom" && !fire){
            StartCoroutine(OnFire(fireTime));
        }
        if (col.tag == "Home" && fire){
            StartCoroutine(OnFire(fireTime));
            StartCoroutine(col.gameObject.GetComponent<Home>().OnFire(fireTime));
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if (fire){
        Debug.Log(gameObject.name + " hit on fire "  + col.gameObject.name);
            if (col.tag == "Home"){
                Debug.Log(gameObject.name + " hit a house "  + col.gameObject.name);
                if (col.gameObject.GetComponent<Home>().fire == false){
                    Debug.Log("BURN THE HAUSE"  + col.gameObject.name);
                    StartCoroutine(SetFire(col.gameObject.GetComponent<Home>()));
                }
            }
        } 
    }
    
    IEnumerator OnFire(int timer){
        audioSource.PlayOneShot (incendio, 1f);
        fire = true;
        GameObject burning = Instantiate(flames, transform.position, transform.rotation);
        GetComponent<AudioSource>().Play ();
        yield return new WaitForSeconds(timer/5);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(timer * 4/5);

        owner.LessHome();

        Destroy(burning);
        Destroy(gameObject);
    }

    IEnumerator SetFire(Home target){
        Debug.Log(" == Vai pega fogo! == ");
        yield return new WaitForSeconds(fireTime/5);
        if (target.fire == false){
             Debug.Log(" :::::::::::::::::::: ");
            target.GotFire();
        }
    }

    public void GotFire(){
        StartCoroutine(OnFire(fireTime));
    }
}