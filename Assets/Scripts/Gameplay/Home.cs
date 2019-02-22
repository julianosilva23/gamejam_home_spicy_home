using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public bool ghost;    
    public bool fire;
    public GameObject flames;
    public Player owner;

    public int fireTime;
    public Sprite halfDone;
    public Sprite done;

    public AudioClip incendio;
    AudioSource audioSource;
    GameObject burning;

    public List<Home> neighborhood;

    // Start is called before the first frame update
    void Start()
    {
       fire = false;
       burning = null;
       audioSource = GetComponent<AudioSource> ();
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Boom" && !fire){
            StartCoroutine(OnFire(fireTime));
        }
        if (col.tag == "Home"){
            neighborhood.Add(col.GetComponent<Home>());
        }
        /*if (col.tag == "Home" && fire){
            StartCoroutine(OnFire(fireTime));
            StartCoroutine(col.gameObject.GetComponent<Home>().OnFire(fireTime));
        }*/
    }

    void FixedUpdate(){
        if (!fire && neighborhood.Count > 0){
            foreach(Home house in neighborhood){
                if (house.fire){
                    StartCoroutine(GetFire(house));
                }
            }
        }
    }

    /*void OnTriggerStay2D(Collider2D col){
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
    }*/
    
    IEnumerator OnFire(int timer){
        if (burning == null){
            audioSource.PlayOneShot (incendio, 1f);
            fire = true;
            burning = Instantiate(flames, transform.position, transform.rotation);
            GetComponent<AudioSource>().Play ();
            yield return new WaitForSeconds(timer/5);
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            yield return new WaitForSeconds(timer * 4/5);

            owner.LessHome();

            Destroy(burning);
            for (int i = 3; i > 0; i--){
                Instantiate(owner.resourceDrop, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator GetFire(Home danger){
        yield return new WaitForSeconds(fireTime/5);
        if (danger != null && !fire){
            StartCoroutine(OnFire(fireTime));
        }
    }

    /*IEnumerator SetFire(Home target){
        Debug.Log(" == Vai pega fogo! == ");
        yield return new WaitForSeconds(fireTime/5);
        if (target.fire == false){
            Debug.Log(" :::::::::::::::::::: ");
            target.GotFire();
        }
    }

    public void GotFire(){
        StartCoroutine(OnFire(fireTime));
    }*/
}