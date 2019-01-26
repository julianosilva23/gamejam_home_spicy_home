using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	public Player player1;

	public Player player2;

	public int timer;

	public Text countText;

    // Start is called before the first frame update
	void Start()
	{
		StartCoroutine(GameTimer(timer));

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

	}

	public void EndTime(){
		Debug.Log("fim do tempo");
	}

	public void SetCount(int timeLeft){
		countText.text = timeLeft.ToString();
	}
}
