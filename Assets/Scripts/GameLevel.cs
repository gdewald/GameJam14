using UnityEngine;
using System.Collections;

public class GameLevel : MonoBehaviour {
	
	GameObject[] roundWalls = new GameObject[2];

	void Awake(){
		// load prefabs
		GameObject wall = Resources.Load<GameObject>("Wall");
		GameObject spawnPoint = Resources.Load<GameObject>("SpawnPoint");

		// instantiate walls
		GameObject wallL = Instantiate(wall, new Vector3(-39, 0, 0), Quaternion.identity) as GameObject;
		GameObject wallR = Instantiate(wall, new Vector3(39, 0, 0), Quaternion.identity) as GameObject;

		GameObject wallT = Instantiate(wall, new Vector3(0, 29, 0), Quaternion.identity) as GameObject;
		wallT.transform.localScale = new Vector3(2, 76, 0);
		wallT.transform.rotation = Quaternion.Euler(0, 0, 90);

		GameObject wallB = Instantiate(wall, new Vector3(0, -29, 0), Quaternion.identity) as GameObject;
		wallB.transform.localScale = new Vector3(2, 76, 0);
		wallB.transform.rotation = Quaternion.Euler(0, 0, 90);

		// instantiate spawn points
		GameObject spTL = Instantiate(spawnPoint, new Vector3(-35, 25, 0), Quaternion.identity) as GameObject;
		GameObject spTR = Instantiate(spawnPoint, new Vector3(35, 25, 0), Quaternion.identity) as GameObject;
		GameObject spBL = Instantiate(spawnPoint, new Vector3(-35, -25, 0), Quaternion.identity) as GameObject;
		GameObject spBR = Instantiate(spawnPoint, new Vector3(35, -25, 0), Quaternion.identity) as GameObject;
	}

	void Start () {

	}

	void Update () {
	
	}
}
