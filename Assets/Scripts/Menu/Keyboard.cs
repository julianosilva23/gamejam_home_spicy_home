using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public static class Keyboard
{

	public static int CountPlayer;

	public static string Control;

	public static int[] NumberChar = {0,0,0,0,0,0,0,0,0,0,0,0};

	public static string[] NameChar = {"", "", "", ""}; 

	public static Image[] ImgChar = {null, null, null, null}; 

	public static string[] TypeInput;

	public static Animator[] animatorPlayer;

	public static int gamemode;

	//Variáveis específicas para o modo torneio
	public static string[] levelsList;
    public static int[] playersWins;
	public static int currentRound;


}
