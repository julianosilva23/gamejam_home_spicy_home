using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	public Player[] players;

	public Text[] playerResourses;

	public int timer;

	public Text gameCounter;

	int countPlayers;


	// public int[] resources;

    // Start is called before the first frame update
	void Start()
	{
		StartCoroutine(GameTimer(timer));


		// Set resource atributte in text field
		countPlayers = players.Length;

		for (int i = 0; i < countPlayers; i++){

		 	playerResourses[i].text = players[i].getResource().ToString();

		}

	}

	IEnumerator GameTimer(int timeLeft){
		while (timeLeft > 0) {
        	
        	yield return new WaitForSeconds (1);

        	Debug.Log(timeLeft);

        	SetCount(timeLeft);

         	timeLeft--;
        }

        EndTime();
    }

    // Update is called once per frame
	void Update()
	{
		// Set resource atributte in text field
		countPlayers = players.Length;

		for (int i = 0; i < countPlayers; i++){

		 	playerResourses[i].text = players[i].getResource().ToString();

		}


	}

	public void EndTime(){
		Debug.Log("fim do tempo");
	}

	public void SetCount(int timeLeft){
		gameCounter.text = timeLeft.ToString();
	}
}
