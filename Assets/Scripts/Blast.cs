using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip, 1f);
        StartCoroutine(BoomTimer(1));
    }


    IEnumerator BoomTimer(int duration){
        yield return new WaitForSeconds(duration);
        //gameObject.SetActive(false);
        //yield return new WaitForSeconds(duration * 2);
        Destroy(gameObject);
    }
}
