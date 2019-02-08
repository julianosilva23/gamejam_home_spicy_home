using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    Button target;
    bool onButton;

    public string typeInput;

    void Start(){
        onButton = false;
        target = null;
    }

    void Update(){
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
            28000 * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal" + typeInput),
            28000 * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical" + typeInput)
        );
        if (Input.GetButtonDown("Submit") && onButton){
            target.onClick.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Button"){
            target = col.GetComponent<Button>();
            onButton = true;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.tag == "Button"){
            target = null;
            onButton = false;
        }
    }

    void ActivateButton(){
        if (Input.GetButtonDown("Submit")){
            target.onClick.Invoke();
        }
    }
}