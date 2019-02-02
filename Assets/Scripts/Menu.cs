using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
	public string[] functions = {
		"Main Play",
		"Main Exit",
		"How To Play",
		"Credits",
		"Options",
		"Play",
		"Play Exit",
		"Choise Play",
		"Choise Exit"
	};

	public int screenStatus = 0;

	public GameObject GoMainMenu;
	public GameObject GoPlayMenu;
	public GameObject GoKeyboardChoise;
	public GameObject GoPlayerChoise;
	public GameObject GoHowToPlay;
	public GameObject GoOptions;
	public GameObject GoCredits;

    GameObject ManageStatus(int status){
    	if(status == 0){
    		return GoMainMenu;

    	}
    	if(status == 1){

    		return GoPlayMenu;    		
    	}


    	if(status == 2){

    		return GoKeyboardChoise;    		
    	}

    	if(status == 3){

    		return GoPlayerChoise;    		
    	}

    	return null;
    }

    public void Clicked()
    {
    	var e = EventSystem.current.currentSelectedGameObject;

    	string methodName = e.name.Replace(" ", "_");

    	Invoke(methodName, 0f);

    }

    public void ChangeSound(bool status){
    	Debug.Log(status);
    }

	private void ChangeScreen(GameObject this_gm, GameObject next_gm){

		Debug.Log(this_gm);
		Debug.Log(next_gm);

		this_gm.SetActive(false);

		next_gm.SetActive(true);
	}

	private void Main_Play(){

		ChangeScreen(ManageStatus(screenStatus), ManageStatus(1));

		screenStatus ++;
	}

	private void Main_Exit(){
		Application.Quit();
	}

	private void How_To_Play(){

		ChangeScreen(ManageStatus(screenStatus), GoHowToPlay);

		screenStatus ++;

	}

	private void Credits(){

		ChangeScreen(ManageStatus(screenStatus), GoCredits);

		screenStatus ++;

	}

	private void Options(){

		ChangeScreen(ManageStatus(screenStatus), GoOptions);

		screenStatus ++;

	}

	private void Play(){

		ChangeScreen(GoPlayMenu, GoKeyboardChoise);

		screenStatus ++;

	}

	private void Play_Exit(){
		Application.Quit();
	}

	private void Choise_Play(){

		ChangeScreen(GoKeyboardChoise, GoPlayerChoise);

		screenStatus++;

	}

	private void Choise_Exit(){

	}

	// private void KeyboardChoise(){

	// 	ChangeScreen(GoPlayMenu, ManageStatus(3));

	// 	screenStatus ++;

	// }

	public void Back(){

		var e = EventSystem.current.currentSelectedGameObject;

		GameObject this_gm;

		string string_gm =  e.name.Replace("Back ", "");

		// Debug.Log(string_gm);

		this_gm = GameObject.Find(string_gm);

		ChangeScreen(this_gm, ManageStatus(screenStatus - 1));

		screenStatus--;

	}

	void two_keyboards(){

		Choise_Play();

		string[] inputs = {"Key1", "Key2"};

		Keyboard.TypeInput = inputs;	

		Keyboard.CountPlayer = 2;

		Keyboard.Control = "keyboard";

	}

	void two_joystics(){

		Choise_Play();

		string[] inputs = {"Joy1", "Joy2"};

		Keyboard.TypeInput = inputs;

		Keyboard.CountPlayer = 2;

		Keyboard.Control = "joystic";

	}

	void four_joystics(){
		
		Choise_Play();

		string[] inputs = {"Joy1", "Joy2", "Joy3", "Joy4"};

		Keyboard.TypeInput = inputs;

		Keyboard.CountPlayer = 4;

		Keyboard.Control = "joystic";

	}

	void four_mixed(){
		Debug.Log("Deu boa aqui");
		Choise_Play();

		string[] inputs = {"Joy1", "Joy2", "Key1", "Key2"};

		Keyboard.TypeInput = inputs;

		Keyboard.CountPlayer = 4;

		Keyboard.Control = "mixed";

		ChangeScreen(GoKeyboardChoise, ManageStatus(screenStatus));

	}

}
