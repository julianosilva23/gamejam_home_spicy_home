using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	public Sprite[] image;

	public Image imageContainer;

	private bool trigger;

	private int choiseController = 1;

	private GameObject canvas;

	private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {

    	trigger = true;

    	startChoise();
        
    }

    // Update is called once per frame
    void Update()
    {
    	keyPress = Input.GetAxisRaw("Horizontal");

    	if(keyPress != 0 && trigger == true){

    		StartCoroutine(changeChar(keyPress));

    	}

        
    }

    public IEnumerator changeChar(float keyPress){

    	trigger = false;

    	charCount = charCount + (int) keyPress;

    	if(charCount == 0){

    		charCount = (names.Length);

    	}

    	if(charCount > (names.Length)){

    		charCount = 1;

    	}

    	yield return new WaitForSeconds(1);
    	
    	setChar(charCount);

	   	trigger = true;
	   	


    }

    public void startChoise(){

    	playerCount = Keyboard.CountPlayer;

		control = Keyboard.Control;

		charCount = 1;

		setChar(charCount);

		ChoiseNext(choiseController);

    }

    public void setChar(int charCount){

    	if(charCount <= (names.Length) && charCount > 0){

    		if(charCount == 0){

    			// name.text = names[charCount];

    			// imageContainer.sprite = image[charCount];

    		}else{

	    		name.text = names[charCount - 1];

    			imageContainer.sprite = image[charCount - 1];

    		}
    	}
    }

    public void Play(){

    	Debug.Log(Keyboard.TypeInput[charCount - 1]);

    	if(choiseController >= playerCount){

    		canvas = GameObject.Find("Canvas");

    		canvas.SetActive(false);

    		// Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        	SceneManager.LoadScene("Map1", LoadSceneMode.Additive);



    	}else{

    		ChoiseNext(choiseController + 1);

    	}

    	choiseController++;
    }

    private void ChoiseNext(int number_player){

    	player.text = "Player " + number_player.ToString() + ": Select Yout Character";


    }
}
