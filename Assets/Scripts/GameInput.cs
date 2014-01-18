using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour {
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			toggleSplit();
		}
	}

	void toggleSplit(){
		if (!isSplit) {
			Game.cube[0].transform.Translate(-1f, 0, 0);
			Game.cube[1] = Instantiate(Game.pfCube, new Vector2 (1f, 0), Quaternion.identity) as GameObject;
		} 
		else {
			doCombine = true;
		}
		
		isSplit = !isSplit;
	}
}
