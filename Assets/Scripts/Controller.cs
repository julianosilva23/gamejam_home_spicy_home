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

	public Animator[] animator;

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

			players[i] = AnimationClip(input, houses[i], animator[i]).GetComponent<Player>();

			i++;
		}

		StartCoroutine(GameTimer(timer));
	}

	private string setMask(int number, string type, int score){
		return score.ToString();
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
			Debug.Log(player.getHome());
			if (player.getHome() > highScore){
				highScore = player.getHome();
				winner = player;
			}
		}
		gameOver.SetActive(true);
		gameOver.GetComponent<Text>().text = winner.getCharName() + "'s tribe now owns the Land!";

	}

	public void SetCount(int timeLeft){
		gameCounter.text = timeLeft.ToString();
	}

	public GameObject AnimationClip(string type_input, GameObject house, Animator anim){

		player = Instantiate(playerPrefab, new Vector3(2.0F, 0, 0), Quaternion.identity);

		player.GetComponent<Player>().setTypeInput(type_input);

		player.GetComponent<Player>().setAnimator(anim);

		player.GetComponent<Player>().builtHome = house;

		return player;

	}

	//void Replay(){
	//
	//}

	public void Return(){
		SceneManager.LoadScene("menu");
	}
}
