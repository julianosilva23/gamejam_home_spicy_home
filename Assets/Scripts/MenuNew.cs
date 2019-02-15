using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNew : MonoBehaviour
{
    public GameObject[] cursors;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        cursors[4].SetActive(false);
        for (int i = 0; i < Keyboard.CountPlayer; i ++){
            Debug.Log(cursors[i]);
            cursors[i].SetActive(true);
            cursors[i].GetComponent<Cursor>().typeInput = Keyboard.TypeInput[i];
        }
    }

    public void CharSelect(){

    }

    public void SingleCursor(){
        for (int i = 0; i < Keyboard.CountPlayer; i ++){
            cursors[i].SetActive(true);
        }
        cursors[4].SetActive(true);
        cursors[4].GetComponent<Cursor>().typeInput = "";
    }



//--------------------------------------------------------------------------------------------------------------------
//============================LEVEL SELLECT===========================================================================
//--------------------------------------------------------------------------------------------------------------------

    public void Hill1(){
        SceneManager.LoadScene("Hill1");
    }

    public void Sand1(){
        SceneManager.LoadScene("Sand1");
    }
}
