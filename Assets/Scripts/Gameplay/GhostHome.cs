using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : MonoBehaviour
{
    public GameObject owner;
    //public GameObject forbiddenImage;
    public bool canBuild;

    public List<GameObject> obstacles;
    public List<GameObject> playerObstacles;

    void Start(){
       canBuild = true;
        CheckObstacles();
    }

    void Update()
    {
        GetComponent<Transform>().position = new Vector2(
            owner.transform.position.x + owner.GetComponent<Player>().direction, owner.transform.position.y);
        CheckObstacles();
    }

    void CheckObstacles(){
        if (canBuild){
            if (obstacles.Count > 0 || CheckPlayerObstacles()){
                canBuild = false;
                GetComponent<SpriteRenderer>().color = new Color32(255, 29, 0, 130);
                //Debug.Log("Mudou p/ vermelho");
                //forbiddenImage.SetActive(true);
            }
        } else if (obstacles.Count == 0 && !CheckPlayerObstacles()) {
            canBuild = true;
            GetComponent<SpriteRenderer>().color = new Color32(0, 175, 255, 130);
            //Debug.Log("Mudou p/ azul");
            //forbiddenImage.SetActive(false);
        }
    }

    bool CheckPlayerObstacles(){
        if (playerObstacles.Count > 0){
            foreach (GameObject player in playerObstacles){
                if (player.GetComponent<Player>().alive){
                    return true;
                }
            }
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D col){
        switch (ValidCollision(col)){
            case 0:
                return;
            case 1:
                obstacles.Add(col.gameObject);
                return;
            case 2:
                playerObstacles.Add(col.gameObject);
                return;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        switch (ValidCollision(col)){
            case 0:
                return;
            case 1:
                obstacles.Remove(col.gameObject);
                return;
            case 2:
                playerObstacles.Remove(col.gameObject);
                return;
        }
    }

    int ValidCollision(Collider2D col){
        if ((col.tag == "Home" && col is CircleCollider2D) || col.tag == "Resource" || col.tag == "Bomb" || col.tag == "Boom"){
            return 0;
        }
        if (col.tag == "Player"){
            return 2;
        }
        return 1;
    }
}
