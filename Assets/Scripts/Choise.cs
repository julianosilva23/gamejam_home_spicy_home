using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choise : MonoBehaviour
{

	public string[] names;

	// public string[] descriptions;

	public Text name;

	public Text player;

	// Variable for assign attr in gameobject player 
	public GameObject[] players;

	private int playerCount;

	private string control;

	private bool choise = false;

	private float keyPress;

	private int charCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	keyPress = Input.GetAxisRaw("Horizontal");

    	if(keyPress != 0){

    		changeChar(keyPress);
    	}

        
    }

    public void changeChar(float keyPress){

    	charCount = charCount + (int) keyPress;

    	if(charCount > names.Length){

    		charCount = 0;

    	}

    	if(charCount == names.Length){

    		charCount = names.Length;

    	}

    	Debug.Log(charCount);

    	setChar(charCount);

    }

    public void startChoise(){

    	playerCount = Keyboard.CountPlayer;

		control = Keyboard.Control;

		charCount = names.Length;

    }

    public void setChar(int charCount){

    	Debug.Log(names.Length);

    	if(charCount <= names.Length && charCount >= 0){
    		
    		name.text = names[charCount];
    	}


    }
}
