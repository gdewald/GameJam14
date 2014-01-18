using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public static GameObject pfCube;
	public static GameObject[] cube = new GameObject[2];

	void Awake(){
		pfCube = Resources.Load("Cube") as GameObject;
	}

	void Start () {
		cube[0] = Instantiate(pfCube) as GameObject;
	}

	void Update () {
	
	}
}
