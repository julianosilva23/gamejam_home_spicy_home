using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
	public static bgm instance = null;
    
    void Awake()
    {
    	if (instance == null){
    		DontDestroyOnLoad(gameObject);
    		instance = this;
    	} else {
    		Destroy(gameObject);
    	}
    	
    }
}
