using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
	public Player[] players;

	public Text[] playerNames;

	public Image[] playerImg;

	public GameObject[] houses;

	public Text[] playerResources;

	public Text[] playerHouses;

	public GameObject[] playerHUD;

	public int timer;

	public Text gameCounter;

	public GameObject playerPrefab;

	public GameObject gameOver;
	public GameObject gameOverOptions;
	public Button gameOverDefaultButton;
	public Text gameOverText;

	public GameObject tournamentHUD;

	public RuntimeAnimatorController[] rac;

	int highScore;

	Player winner;

	int countPlayers;

	private GameObject player;

	AudioSource audioSource;

	public GameObject pauseMenu;
	public Button pauseMenuDefaultButton;

	bool paused;

	bool finish;

    // Start is called before the first frame update
	void Start()
	{
		audioSource = FindObjectOfType<bgm>().GetComponent<AudioSource>();

		finish = false;

		paused = false;

		highScore = 0;

		int i = 0;

		foreach (string input in Keyboard.TypeInput){

			int value = Keyboard.NumberChar[i];

			//Debug.Log(value);
			//Debug.Log(rac[value]);

			players[i] = StartPlayer(input, houses[i], rac[value], i).GetComponent<Player>();
			playerNames[i].text = Keyboard.NameChar[i];
			playerImg[i].sprite = Keyboard.ImgChar[i].sprite;

			i++;
		}
		if (Keyboard.CountPlayer == 2){
			Destroy(playerHUD[2]);
			Destroy(playerHUD[3]);
		}
		if (Keyboard.gamemode==2){
			Instantiate(tournamentHUD, new Vector2(0,0), new Quaternion());
		}
		StartCoroutine(GameTimer(timer));
	}

	private int getChar(int key){

		return Keyboard.NumberChar[key];
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

		for (int i = 0; i < countPlayers; i++){

			playerResources[i].text = players[i].getResource().ToString() + "/15";

			if (players[i].getResource() == 15){
				playerResources[i].color = new Color(255,0,0);
			} else{
				playerResources[i].color = new Color(255,255,255);
			}

			playerHouses[i].text = players[i].getHome().ToString();

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
		int winnerNumber;

		for (int i = 0; i < countPlayers; i++){
			Debug.Log(players[i]);
			if (players[i].getHome() > highScore){

				highScore = players[i].getHome();

				winner = players[i];
				winnerNumber = i;

				tie = false;

				nameWin = Keyboard.NameChar[i];
			} else if (players[i].getHome() == 0) {
				noScore ++;
			} else if (players[i].getHome() == highScore) {
				tie = true;
			}

		}
		finish = true;
		if (Keyboard.gamemode == 1){
			gameOver.SetActive(true);
			if (!tie && noScore < countPlayers) {
				gameOver.GetComponent<AudioSource>().Play();
				gameOverText.text = nameWin + "'s tribe now owns the Land!";
			} else {
				gameOverText.text = "DRAW!";
			}
			StartCoroutine(GameOverOptions());
			//Time.timeScale = 0f;
		}
		if (Keyboard.gamemode == 2){
			TournamentManager tournamentManager = GameObject.FindGameObjectWithTag("tournament").GetComponent<TournamentManager>();
			tournamentManager.EndGame(tie || noScore >= countPlayers, winnerNumber, nameWin);
		}
	}

	IEnumerator GameOverOptions(){
		yield return new WaitForSeconds(1);
		gameOverOptions.SetActive(true);
		gameOverDefaultButton.Select();
		Time.timeScale = 0f;
	}

	public void SetCount(int timeLeft){
		gameCounter.text = timeLeft.ToString();
		if (timeLeft <= 20 && gameCounter.color != new Color (255,0,0)){
			gameCounter.color = new Color (255,0,0);
			audioSource.pitch = 1.2f;
		}
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
		pauseMenuDefaultButton.Select();
	}

	public void unpause(){
		Time.timeScale = 1f;
		paused = false;
		pauseMenu.SetActive(false);
		gameOver.SetActive(false);
	}

	public void Return(){
		Time.timeScale = 1f;
		Destroy(audioSource.gameObject);
		SceneManager.LoadScene("menu");
	}

	public void Retry(){
		Time.timeScale = 1f;
		audioSource.pitch = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
