using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    void Update(){

    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(

        28000 * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal"),

        28000 * Time.fixedDeltaTime * Input.GetAxisRaw("Vertical"));
    }
}
