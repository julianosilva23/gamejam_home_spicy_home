using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNew : MonoBehaviour
{
    public GameObject[] cursors;
    public Button start;
    public Button goToMap;

    public int playersReady;
    bool ready;
    

    void Start()
    {
        start.Select();
        ready = false;
        playersReady = 0;
    }

    void FixedUpdate(){
        if (playersReady == Keyboard.CountPlayer){
            goToMap.interactable = true;
        }
    }

    public void Key2(){
        string[] inputs = {"Key1", "Key2"};
		Keyboard.TypeInput = inputs;	
		Keyboard.CountPlayer = 2;
		Keyboard.Control = "keyboard";
        CharSelectScreen();
    }

    public void Joy2(){
        string[] inputs = {"Joy1", "Joy2"};
		Keyboard.TypeInput = inputs;	
		Keyboard.CountPlayer = 2;
		Keyboard.Control = "joystick";
        CharSelectScreen();
    }

    public void Joy4(){
        string[] inputs = {"Joy1", "Joy2", "Joy3", "Joy4"};
		Keyboard.TypeInput = inputs;	
		Keyboard.CountPlayer = 4;
		Keyboard.Control = "joystick";
        CharSelectScreen();
    }

    public void KeyJoy(){
        string[] inputs = {"Key1", "Key2", "Joy1", "Joy2"};
		Keyboard.TypeInput = inputs;	
		Keyboard.CountPlayer = 4;
		Keyboard.Control = "mixed";
        CharSelectScreen();
    }

    public void CharSelectScreen(){
        for (int i = 0; i < Keyboard.CountPlayer; i ++){
            Debug.Log(cursors[i]);
            cursors[i].SetActive(true);
            cursors[i].GetComponent<Cursor>().typeInput = Keyboard.TypeInput[i];
        }
    }

    public void NoCursor(){
        for (int i = 0; i < Keyboard.CountPlayer; i ++){
            cursors[i].SetActive(true);
        }
    }



//--------------------------------------------------------------------------------------------------------------------//
//================================================LEVEL SELLECT=======================================================//
//--------------------------------------------------------------------------------------------------------------------//

    public void Hill1(){
        SceneManager.LoadScene("Hill1");
    }

    public void Sand1(){
        SceneManager.LoadScene("Sand1");
    }
}
