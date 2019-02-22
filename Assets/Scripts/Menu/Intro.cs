using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    float duration;

    void Start()
    {
        duration = (float)gameObject.GetComponent<VideoPlayer>().clip.length; 
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro(){
        yield return new WaitForSeconds(duration + 1);
        SceneManager.LoadScene("menu");
    }
}
