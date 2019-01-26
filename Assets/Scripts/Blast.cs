using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BoomTimer(1));
        //gameObject.GetComponent<AudioSource>().Play ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BoomTimer(int duration){
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
