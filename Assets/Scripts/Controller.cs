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
	public Text gameOverText;

	public RuntimeAnimatorController[] rac;

	int highScore;

	Player winner;

	int countPlayers;

	private GameObject player;

	AudioSource audioSource;

	public GameObject pauseMenu;

	bool paused;

	bool finish;

    // Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource> ();

		audioSource.Play();

		finish = false;

		paused = false;

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
	void FixedUpdate()
	{
		// assigned resource value in text field
		//countPlayers = players.Length;
		countPlayers = Keyboard.TypeInput.Length;

		if (!finish){
			for (int i = 0; i < countPlayers; i++){
			
				playerResources[i].text = setMask(i, "res", players[i].getResource());

				playerHouses[i].text = setMask(i, "home", players[i].getHome());

			}
		}
	}

	void Update()
	{
		if (Input.GetButtonDown("Pause")){
			if (paused){
				unpause();
			} else {
				Pause();
			}
		}
	}
	

	public void EndTime(){
		SetCount(0);
		string nameWin = "";
		bool tie = false;
		int noScore = 0;

		for (int i = 0; i < countPlayers; i++){
			Debug.Log(players[i]);
			if (players[i].getHome() > highScore){

				highScore = players[i].getHome();

				winner = players[i];

				tie = false;

				nameWin = "Player " + (i + 1).ToString();
			} else if (players[i].getHome() == 0) {
				noScore ++;
			} else if (players[i].getHome() == highScore) {
				tie = true;
			}

		}
		finish = true;
		//Debug.Log("Veio ate aqui");
		gameOver.SetActive(true);

		if (!tie && noScore < countPlayers) {
			gameOverText.text = nameWin + "'s tribe now owns the Land!";
		} else {
			gameOverText.text = "DRAW!";
		}
		Time.timeScale = 0f;
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

	public void Pause(){
		paused = true;
		Time.timeScale = 0f;
		pauseMenu.SetActive(true);
	}

	public void unpause(){
		Time.timeScale = 1f;
		paused = false;
		pauseMenu.SetActive(false);
		gameOver.SetActive(false);
	}

	public void Return(){
		Time.timeScale = 1f;
		SceneManager.LoadScene("menu");
	}
}
