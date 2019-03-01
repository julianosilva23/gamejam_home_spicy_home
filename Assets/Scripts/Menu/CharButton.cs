using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharButton : MonoBehaviour
{
    public bool isSelected;
    public int charNumber;
    public string charName;
    public Text displayName;
    public Image displayImage;

    void Start(){
        displayName.text = charName;
    }

}
