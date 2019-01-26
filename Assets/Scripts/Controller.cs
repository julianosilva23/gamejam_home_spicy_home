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
		StartCoroutine(gameTimer(timer));

	}

	IEnumerator BombTimer(int countdown){
        yield return new WaitForSeconds(countdown);
        Explode();

    // Update is called once per frame
	void Update()
	{

	}

	static EndTime(){
		Debug.log("fim do tempo")
	}
}
