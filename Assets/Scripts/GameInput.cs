using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour {
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			toggleSplit();
		}
	}

	void toggleSplit(){
		if (!Player.isSplit) {
			Player.startPoint[0] = Player.entity[0].transform.position;
			Player.startPoint[1] = Player.entity[0].transform.position;

			//Vector3 pos0 = new Vector3(Player.entity[0].transform.position.x - 1, Player.entity[0].transform.position.y, 0);
			//Vector3 pos1 = new Vector3(Player.entity[0].transform.position.x + 1, Player.entity[0].transform.position.y, 0);

			Vector3 pos0 = new Vector3(Player.entity[0].transform.position.x - 3, Player.entity[0].transform.position.y - 4, 0);
			Vector3 pos1 = new Vector3(Player.entity[0].transform.position.x + 2, Player.entity[0].transform.position.y + 1, 0);


			Player.endPoint[0] = pos0;
			Player.endPoint[1] = pos1;

			Player.entity[1] = Instantiate(Game.pfCube, Player.entity[0].transform.position, Quaternion.identity) as GameObject;
			Player.doSplit = true;
		} 
		else {
			Player.startPoint[0] = Player.entity[0].transform.position;
			Player.startPoint[1] = Player.entity[1].transform.position;

			float x = Player.entity[0].transform.position.x + Player.entity[1].transform.position.x;
			float y = Player.entity[0].transform.position.y + Player.entity[1].transform.position.y;

			Vector3 pos = new Vector3(x/2, y/2, 0);
						
			Player.endPoint[0] = pos;
			Player.endPoint[1] = pos;

			Player.doCombine = true;
		}
		
		Player.isSplit = !Player.isSplit;
	}
}
