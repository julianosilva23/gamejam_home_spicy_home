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

    public GameObject gameWin;
    public Image winImage;
    public Text gameWinText;
    public Text gameWinScoreText;
    public GameObject gameOverOptions;
    public Button gameOverDefaultButton;

    public GameObject matchWin;
    public Text matchWinText;
    public GameObject matchOverOptions;
    public Button matchOverDefaultButton;
    public Image firstImage;
    public Image secondImage;
    public Image thirdImage;

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
        allTrophies = new List<Image[]> {p1Trophies, p2Trophies, p3Trophies, p4Trophies};
        for(int player = 0; player < Keyboard.CountPlayer; player++){
            CountTrophies(allTrophies[player], player);
        }
        if (Keyboard.CountPlayer == 2){
            foreach (Image trophy in p3Trophies){
                Destroy(trophy.gameObject);
            }
            foreach (Image trophy in p4Trophies){
                Destroy(trophy.gameObject);
            }
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
        if (draw){
            Destroy(winImage);
            gameWinText.text = "DRAW!!!";
            gameWinScoreText.text = "No impass shall be accepted! Prepare for the next match!";
        } else {        
            Keyboard.playersWins[winner]++;
            CountTrophies(allTrophies[winner], winner);
            if (Keyboard.playersWins[winner] < 3){
                winImage = Keyboard.ImgChar[winner];
                gameWinText.text = winnerName + "'s tribe owns this Land!";
                gameWinScoreText.text = "<b>" + (3 - Keyboard.playersWins[winner]).ToString() + "</b> more land(s) to win the tournament!";
            } else {
                matchWinText.text = "<b>" + winnerName + "</b> is the winner!!!";
                TournamentRank(winner);
            }
        }
        gameWin.SetActive(true);
        GameEndOptions(gameOverOptions,gameOverDefaultButton);
    }

    IEnumerator GameEndOptions(GameObject endOptions, Button endDefaultButton){
		yield return new WaitForSeconds(1);
		endOptions.SetActive(true);
		endDefaultButton.Select();
		Time.timeScale = 0f;
	}

    void TournamentRank(int winner){
        int firstHighScore = Keyboard.playersWins[winner];
        int secondHighScore = -1;
        int thirdHighScore = -1;
        firstImage = Keyboard.ImgChar[winner];
        for (int player = 0; player < Keyboard.playersWins.Length; player++){
            if (player != winner){
                if (Keyboard.playersWins[player] > secondHighScore){
                    secondHighScore = Keyboard.playersWins[player];
                    secondImage = Keyboard.ImgChar[player];
                } else if (Keyboard.playersWins[player] > thirdHighScore){
                    thirdHighScore = Keyboard.playersWins[player];
                    secondImage = Keyboard.ImgChar[player];
                }
            }
        }
        if (Keyboard.CountPlayer < 3){
            Destroy(thirdImage.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
