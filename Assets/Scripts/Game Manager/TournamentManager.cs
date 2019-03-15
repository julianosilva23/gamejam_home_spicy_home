using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TournamentManager : MonoBehaviour
{
    public Image[] p1Trophies;
    public Image[] p2Trophies;
    public Image[] p3Trophies;
    public Image[] p4Trophies;
    public Color winColor;
    public Color loseColor;

    List<Image[]> allTrophies;

    public void NextLevel(){
        string nextLevel = Keyboard.levelsList[Keyboard.currentRound];
        Keyboard.currentRound++;
        if (Keyboard.currentRound > Keyboard.levelsList.Length){
            Keyboard.currentRound = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }

    void Start()
    {
        for(int player = 0; player <= Keyboard.CountPlayer; player++){
            CountTrophies(allTrophies[player], player);
        }
    }
    
    void CountTrophies(Image[] playerTrophies, int playerNumber){
        int score = Keyboard.playersWins[playerNumber]; 
        foreach (Image trophy in playerTrophies){
            trophy.gameObject.SetActive(true);
            if (score > 0){
                trophy.color = winColor;
                score--;
            } else {
                trophy.color = loseColor;
            }
        }
    }

    public void EndGame(bool draw, int winner, string winnerName){
        Keyboard.playersWins[winner]++;
        CountTrophies(allTrophies[winner], winner);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
