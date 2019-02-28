using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : MonoBehaviour
{
    public GameObject owner;
    //public GameObject forbiddenImage;
    public bool canBuild = false;

    public List<GameObject> obstacles;

    void Update()
    {
        GetComponent<Transform>().position = new Vector2(
            owner.transform.position.x + owner.GetComponent<Player>().direction, owner.transform.position.y);
        CheckObstacles();
    }

    void CheckObstacles(){
        if (canBuild){
            if (obstacles.Count > 0){
                canBuild = false;
                GetComponent<SpriteRenderer>().color = new Color32(255, 29, 0, 130);
                Debug.Log("Mudou p/ vermelho");
                //forbiddenImage.SetActive(true);
            }
        } else if (obstacles.Count == 0){
            canBuild = true;
            GetComponent<SpriteRenderer>().color = new Color32(0, 175, 255, 130);
            Debug.Log("Mudou p/ azul");
            //forbiddenImage.SetActive(false);
        } else {
            GetComponent<SpriteRenderer>().color = new Color32(255, 29, 0, 130);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Home" && col is CircleCollider2D){
            Debug.Log("bateu, é o circulo da casa");
            return;
        } else{
            Debug.Log(col is CircleCollider2D);
            obstacles.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.tag == "Home" && col is CircleCollider2D){
            return;
        } else {
            Debug.Log("saiu");
            obstacles.Remove(col.gameObject);
            //GetComponent<SpriteRenderer>().color = new Color32();
        }
    }
}
