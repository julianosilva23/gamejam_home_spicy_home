using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
	public Player[] players;

	public GameObject[] houses;

	public Text[] playerResources;

	public Text[] playerHouses;

	public int timer;

	public Text gameCounter;

	public GameObject playerPrefab;

	public GameObject gameOver;

	public RuntimeAnimatorController[] rac;

	int highScore;

	Player winner;

	int countPlayers;

	private GameObject player;

	AudioSource audioSource;

    // Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource> ();

		audioSource.Play();

		highScore = 0;

		int i = 0;

		foreach (string input in Keyboard.TypeInput){

			int value = Keyboard.NumberChar[i];

			Debug.Log(value);
			Debug.Log(rac[value]);

			players[i] = StartPlayer(input, houses[i], rac[value], i).GetComponent<Player>();

			i++;
		}

		StartCoroutine(GameTimer(timer));
	}

	private int getChar(int key){

		return Keyboard.NumberChar[key];
	}

	private string setMask(int number, string type, int score){

		return score.ToString();
	}

	IEnumerator GameTimer(int timeLeft){
		SetCount(timeLeft);
		
		while (timeLeft > 0) {
        	
        	yield return new WaitForSeconds (1);

			timeLeft--;

        	SetCount(timeLeft);
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
		SetCount(999);

		for (int i = 0; i < countPlayers; i++){
			Debug.Log(players[i].getCharName());
			if (players[i].getHome() > highScore){

				highScore = players[i].getHome();

				winner = players[i];
			}

		}

		gameOver.SetActive(true);

		gameOver.GetComponent<Text>().text = winner.getCharName() + "'s crew now owns the Land!";

	}

	public void SetCount(int timeLeft){
		gameCounter.text = timeLeft.ToString();
	}

	public GameObject StartPlayer(string type_input, GameObject house, RuntimeAnimatorController rac, int number){

		player = Instantiate(playerPrefab, new Vector3(2.0F, 0, 0), Quaternion.identity);

		player.GetComponent<Player>().setTypeInput(type_input);

		player.GetComponent<Player>().setAnimator(rac);

		player.GetComponent<Player>().builtHome = house;

		player.name = "player" + number.ToString(); 

		return player;

	}

	//void Replay(){
	//
	//}

	public void Return(){
		SceneManager.LoadScene("menu");
	}
}
