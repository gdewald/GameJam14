using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public static GameObject pfCube;
	
	void Awake(){
		pfCube = Resources.Load("Cube") as GameObject;
	}
	
	void Start () {
		Player.entity[0] = Instantiate(pfCube) as GameObject;
	}
}
