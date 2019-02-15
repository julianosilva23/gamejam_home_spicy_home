using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    Button target;
    GameObject targetChar;
    GameObject selectedChar;
    bool onButton;
    bool onChar;

    public string typeInput;
    public int playerNumber;
    public Color playerColor;

    MenuNew menuManager;

    void Start(){
        onButton = false;
        target = null;
        targetChar = null;
        selectedChar = null;
        menuManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuNew>();
    }

    void FixedUpdate(){
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
            280 * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal" + typeInput),
            280 * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical" + typeInput)
        );
        if (Input.GetButtonDown("Submit") && onButton){
            target.onClick.Invoke();
        }
        if (Input.GetButtonDown("Submit") && onChar){
            if (!targetChar.GetComponent<CharButton>().isSelected){
                if (selectedChar != null){
                    selectedChar.GetComponent<Image>().color = new Color (0,0,0);
                    targetChar.GetComponent<CharButton>().isSelected = false;
                } else {
                    menuManager.playersReady ++;
                }
                selectedChar = targetChar;
                selectedChar.GetComponent<Image>().color = playerColor;
                targetChar.GetComponent<CharButton>().isSelected = true;
                Keyboard.NumberChar[playerNumber] = targetChar.GetComponent<CharButton>().charNumber;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Button"){
            target = col.GetComponent<Button>();
            target.Select();
            onButton = true;
        }
        if (col.tag == "CharButton"){
            targetChar = col.gameObject;
            onChar = true;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.tag == "Button"){
            target = null;
            onButton = false;
        }
        if (col.tag == "CharButton"){
            targetChar = null;
            onChar = false;
        }
    }

    void ActivateButton(){
        if (Input.GetButtonDown("Submit")){
            target.onClick.Invoke();
        }
    }
}