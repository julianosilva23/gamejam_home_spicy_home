using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	public Player[] players;

	public Text[] playerResources;

	public Text[] playerHouses;

	public int timer;

	public Text gameCounter;

	int highScore;

	Player winner;

	int countPlayers;


    // Start is called before the first frame update
	void Start()
	{
		highScore = 0;
		StartCoroutine(GameTimer(timer));




	}

	private string setMask(int number, string type, int score){
		return "P" + number.ToString() + " " + type.ToString() + ": " + score.ToString();
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
		// assigned resource value in text field
		countPlayers = players.Length;

		for (int i = 0; i < countPlayers; i++){

		 	playerResources[i].text = setMask(i, "res", players[i].getResource());

		 	playerHouses[i].text = setMask(i, "home", players[i].getHome());

		}


	}

	public void EndTime(){
		SetCount(0);
		foreach (Player player in players){
			if (player.getHome() > highScore){
				highScore = player.getHome();
				winner = player;
			}
		}
	}

	public void SetCount(int timeLeft){
		gameCounter.text = timeLeft.ToString();
	}
}
