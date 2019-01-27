using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	public Player[] players;

	public GameObject[] houses;

	public Text[] playerResources;

	public Text[] playerHouses;

	public int timer;

	public Text gameCounter;

	public GameObject playerPrefab;

	int highScore;

	Player winner;

	int countPlayers;

	private GameObject player;


    // Start is called before the first frame update
	void Start()
	{
		highScore = 0;

		int i = 0;

		foreach (string input in Keyboard.TypeInput){

			addPlayer(input, houses[i]);

			i++;
		}

		StartCoroutine(GameTimer(timer));
	}

	private string setMask(int number, string type, int score){
		return "P" + number.ToString() + " " + type.ToString() + ": " + score.ToString();
	}

	IEnumerator GameTimer(int timeLeft){
		while (timeLeft > 0) {
        	
        	yield return new WaitForSeconds (1);

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

	public GameObject addPlayer(string type_input, GameObject house){

		player = Instantiate(playerPrefab, new Vector3(2.0F, 0, 0), Quaternion.identity);

		player.GetComponent<Player>().setTypeInput(type_input);

		player.GetComponent<Player>().builtHome = house;

		return player;

	}
}
