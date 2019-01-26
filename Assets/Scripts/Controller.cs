using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public Player player1;

	public Player player2;

	public int timer;

    // Start is called before the first frame update
	void Start()
	{
		StartCoroutine(GameTimer(timer));

	}

	IEnumerator GameTimer(int countdown){
        yield return new WaitForSeconds(countdown);
        Debug.Log(timer);
        EndTime();
    }

    // Update is called once per frame
	void Update()
	{

	}

	public void EndTime(){
		Debug.Log("fim do tempo");
	}
}
