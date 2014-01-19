using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public static GameObject pfPlayerPrimary, pfPlayerSecondary;
	public static int numKilled = 0;

	void Awake(){
		pfPlayerPrimary = Resources.Load("PlayerPrimary") as GameObject;
		pfPlayerSecondary = Resources.Load("PlayerSecondary") as GameObject;
	}
	
	void Start(){
		Player.entity[0] = Instantiate(pfPlayerPrimary) as GameObject;
		Player.entity[1] = Instantiate(pfPlayerSecondary) as GameObject;
		
		Player.entity[0].GetComponent("CreateChain").SendMessage("ChainSetEnd", Player.entity[1]);
		
		Player.entity[1].SetActive(false);
	}
	
}
