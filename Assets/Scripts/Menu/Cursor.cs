using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    Button target;
    public GameObject targetChar;
    public GameObject selectedChar;
    Color32 playerColor;
	Image image;
    bool onButton;
    bool onChar;

    public MenuNew menuManager;
    public string typeInput;
    public int playerNumber;

    void Start(){
        onButton = false;
        target = null;
        targetChar = null;
        selectedChar = null;
    }

    void Update(){
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
            280 * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal" + typeInput),
            280 * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical" + typeInput)
        );
        if ((Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire1" + typeInput) || Input.GetButtonDown("Fire2" + typeInput)) && onButton){
            if(target.interactable){
                target.onClick.Invoke();
            }
        }
        if ((Input.GetButtonDown("Fire1" + typeInput) || Input.GetButtonDown("Fire2" + typeInput)) && onChar){
			//Debug.Log ("Tá clicando");
            if (!targetChar.GetComponent<CharButton>().isSelected){
				//Debug.Log ("esse botao nao foi escolhido por alguem");
                if (selectedChar != null){
                    UnselectChar();
                    //Debug.Log("pt2");
                } else {
                    menuManager.playersReady ++;
                    //Debug.Log("pt3");
                }
                SelectChar();
            }
        }
    }

    public void SelectChar(){
        selectedChar = targetChar;
        image = selectedChar.GetComponent<Image> ();
        image.color = GetComponent<Image>().color;
        selectedChar.GetComponent<CharButton>().isSelected = true;
        Keyboard.NumberChar[playerNumber] = targetChar.GetComponent<CharButton>().charNumber;
        Keyboard.NameChar[playerNumber] = targetChar.GetComponent<CharButton>().displayName.text;
        Keyboard.ImgChar[playerNumber] = targetChar.GetComponent<CharButton>().displayImage;
    }

    public void UnselectChar(){
        if (selectedChar != null){
            selectedChar.GetComponent<Image>().color = new Color32(0,0,0,80);
            selectedChar.GetComponent<CharButton>().isSelected = false;
            selectedChar = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Button"){
            target = col.GetComponent<Button>();
            //target.Select();
            onButton = true;
            Debug.Log(target.interactable);
        }
        if (col.tag == "CharButton"){
            targetChar = col.gameObject;
            //targetChar.GetComponent<Button>().Select();
            onChar = true;
			Debug.Log ("tá em cima do char");
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

    /*void ActivateButton(){
        if (Input.GetButtonDown("Submit") && target.interactable == true){
            target.onClick.Invoke();
        }
    } */
}