using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLevel : MonoBehaviour {

	public static GameLevel that;
	public static int curLvl;
	public const int NUM_LVLS = 3;
	bool[] lvlReached;
	bool firstRun = true;
	
	class SmartGameObject {
		public GameObject data;
		public GameObject inst;
		
		public SmartGameObject(Vector3 pos, Vector3 rot, Vector3 scale){
			data = new GameObject();
			data.transform.position = pos;
			data.transform.rotation = Quaternion.Euler(rot);
			data.transform.localScale = scale;

			inst = new GameObject();
		}
	}

	struct Round {
		public List<SmartGameObject> walls;
		public List<SmartGameObject> spawns;
	}
	
	List<Round> theRounds;

	GameObject wall, wallCircle;
	GameObject spawnPoint;
	
	void Start(){
		curLvl = -1;
	}

	void Awake(){
		that = this;
		theRounds = new List<Round>();

		lvlReached = new bool[NUM_LVLS];

		for(int i=0; i < NUM_LVLS; ++i){
			lvlReached[i] = false;
		}

		// load prefabs
		wall = Resources.Load<GameObject>("Wall");
		wallCircle = Resources.Load<GameObject>("WallCircle");
		spawnPoint = Resources.Load<GameObject>("SpawnPoint");

		//----- define the levels
		#region Level 0
		// define walls
		SmartGameObject wallL = new SmartGameObject(new Vector3(-39, 0, 0),	Vector3.zero, wall.transform.localScale);
		SmartGameObject wallR = new SmartGameObject(new Vector3(39, 0, 0), Vector3.zero, wall.transform.localScale);
		SmartGameObject wallT = new SmartGameObject(new Vector3(0, 29, 0), new Vector3(0, 0, 90), new Vector3(2, 76, 0));
		SmartGameObject wallB = new SmartGameObject(new Vector3(0, -29, 0),	new Vector3(0, 0, 90), new Vector3(2, 76, 0));

		// define spawn points
		SmartGameObject spTL = new SmartGameObject(new Vector3(-35, 25, 0),	Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spTR = new SmartGameObject(new Vector3(35, 25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spBL = new SmartGameObject(new Vector3(-35, -25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spBR = new SmartGameObject(new Vector3(35, -25, 0), Vector3.zero, spawnPoint.transform.localScale);

		Round lvl0 = new Round();
		lvl0.walls = new List<SmartGameObject>();
		lvl0.walls.Add(wallL);
		lvl0.walls.Add(wallR);
		lvl0.walls.Add(wallT);
		lvl0.walls.Add(wallB);

		lvl0.spawns = new List<SmartGameObject>();
		lvl0.spawns.Add(spTL);
		lvl0.spawns.Add(spTR);
		lvl0.spawns.Add(spBL);
		lvl0.spawns.Add(spBR);

		// add the round
		theRounds.Add(lvl0);
		#endregion Level 0

		#region Level 1
		// define walls
		SmartGameObject wallL2 = new SmartGameObject(new Vector3(-39, 0, 0), Vector3.zero, wall.transform.localScale);
		SmartGameObject wallR2 = new SmartGameObject(new Vector3(39, 0, 0),	Vector3.zero, wall.transform.localScale);
		SmartGameObject wallT2 = new SmartGameObject(new Vector3(0, 29, 0), new Vector3(0, 0, 90),	new Vector3(2, 76, 0));
		SmartGameObject wallB2 = new SmartGameObject(new Vector3(0, -29, 0), new Vector3(0, 0, 90),	new Vector3(2, 76, 0));
		SmartGameObject leftEye = new SmartGameObject(new Vector3(-11, 8, 0), new Vector3(0, 0, 90), new Vector3(5, 5, 0));
		SmartGameObject rightEye = new SmartGameObject(new Vector3(11, 8, 0), new Vector3(0, 0, 90), new Vector3(5, 5, 0));
		SmartGameObject mouth = new SmartGameObject(new Vector3(0, -14, 0), new Vector3(0, 0, 90), new Vector3(2, 50, 0));
		SmartGameObject mouthL = new SmartGameObject(new Vector3(-23.5f, -10.5f, 0), Vector3.zero,new Vector3(3, 5, 0));
		SmartGameObject mouthR = new SmartGameObject(new Vector3(23.5f, -10.5f, 0), Vector3.zero, new Vector3(3, 5, 0));

		// define spawn points 
		SmartGameObject spTL2 = new SmartGameObject(new Vector3(-35, 25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spTR2 = new SmartGameObject(new Vector3(35, 25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spBL2 = new SmartGameObject(new Vector3(-35, -25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spBR2 = new SmartGameObject(new Vector3(35, -25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spMid = new SmartGameObject(new Vector3(0, -5, 0), Vector3.zero, spawnPoint.transform.localScale);

		Round lvl1 = new Round();
		lvl1.walls = new List<SmartGameObject>();
		lvl1.walls.Add(wallL2);
		lvl1.walls.Add(wallR2);
		lvl1.walls.Add(wallT2);
		lvl1.walls.Add(wallB2);
		lvl1.walls.Add(leftEye);
		lvl1.walls.Add(rightEye);
		lvl1.walls.Add(mouth);
		lvl1.walls.Add(mouthL);
		lvl1.walls.Add(mouthR);

		lvl1.spawns = new List<SmartGameObject>();
		lvl1.spawns.Add(spTL2);
		lvl1.spawns.Add(spTR2);
		lvl1.spawns.Add(spBL2);
		lvl1.spawns.Add(spBR2);
		lvl1.spawns.Add(spMid);

		// add the round
		theRounds.Add(lvl1);
		#endregion Level 1

		#region Level 2
		// define walls
		SmartGameObject wallLi1 = new SmartGameObject(new Vector3(-39, 0, 0), Vector3.zero, wall.transform.localScale);
		SmartGameObject wallRi1 = new SmartGameObject(new Vector3(-13, 15.3f, 0), Vector3.zero, new Vector3(2, 28.62f, 0));
		SmartGameObject wallRi2 = new SmartGameObject(new Vector3(13, 15.3f, 0), Vector3.zero, new Vector3(2, 28.62f, 0));
		SmartGameObject wallRi3 = new SmartGameObject(new Vector3(39, 0, 0), Vector3.zero, wall.transform.localScale);
		SmartGameObject wallTop1 = new SmartGameObject(new Vector3(-25, 29, 0), new Vector3(0, 0, 90), new Vector3(2, 26, 0));
		SmartGameObject wallTop2 = new SmartGameObject(new Vector3(-0.877f, 1.987f, 0), new Vector3(0, 0, 90), new Vector3(2, 26, 0));
		SmartGameObject wallTop3 = new SmartGameObject(new Vector3(25, 29, 0), new Vector3(0, 0, 90), new Vector3(2, 26, 0));
		SmartGameObject wallBot = new SmartGameObject(new Vector3(0, -29, 0), new Vector3(0, 0, 90), new Vector3(2, 80, 0));
		SmartGameObject wallCirc = new SmartGameObject(new Vector3(0, -14.5f, 0), Vector3.zero, wallCircle.transform.localScale);

		// define spawn points
		SmartGameObject spTL3 = new SmartGameObject(new Vector3(-35, 25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spBL3 = new SmartGameObject(new Vector3(-35, -25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spTR3 = new SmartGameObject(new Vector3(25, 25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spTR32 = new SmartGameObject(new Vector3(35, 25, 0), Vector3.zero, spawnPoint.transform.localScale);
		SmartGameObject spTR33 = new SmartGameObject(new Vector3(30, 15, 0), Vector3.zero, spawnPoint.transform.localScale);
		
		Round lvl2 = new Round();
		lvl2.walls = new List<SmartGameObject>();
		lvl2.walls.Add(wallLi1);
		lvl2.walls.Add(wallRi1);
		lvl2.walls.Add(wallRi2);
		lvl2.walls.Add(wallRi3);
		lvl2.walls.Add(wallTop1);
		lvl2.walls.Add(wallTop2);
		lvl2.walls.Add(wallTop3);
		lvl2.walls.Add(wallBot);
		lvl2.walls.Add(wallCirc);
		
		lvl2.spawns = new List<SmartGameObject>();
		lvl2.spawns.Add(spTL3);
		lvl2.spawns.Add(spBL3);
		lvl2.spawns.Add(spTR3);
		lvl2.spawns.Add(spTR32);
		lvl2.spawns.Add(spTR33);
		
		// add the round
		theRounds.Add(lvl2);
		#endregion Level 2
	}

	public void next(){
		// reset player to (0, 0)
		//if(Player.isSplit){
		//	Player.that.combine();
		//}

		Player.entity[0].GetComponent<Controller> ().FreezePlayer (1.0f);

		Player.entity[0].transform.position = Vector3.zero;
		Player.entity[1].transform.position = Vector3.zero;
		Player.entity[0].rigidbody2D.velocity = Vector2.zero;
		Player.entity[1].rigidbody2D.velocity = Vector2.zero;

		// load next level's architecture
		if(curLvl < theRounds.Count){
			// deactivate the old level
			if(!firstRun){
				// TODO: freeze player movement for a few seconds

				for(int i=0; i < theRounds[curLvl].walls.Count; ++i){
					SmartGameObject w = theRounds[curLvl].walls[i];
					w.inst.SetActive(false);
				}

				for(int i=0; i < theRounds[curLvl].spawns.Count; ++i){
					SmartGameObject sp = theRounds[curLvl].spawns[i];
					sp.inst.SetActive(false);
				}
			}

			// instantiatle the new level; TODO: check if already instantiated

			++curLvl;

			if(curLvl >= NUM_LVLS){
				curLvl = 0;
			}

			for(int i=0; i < theRounds[curLvl].walls.Count; ++i){
				SmartGameObject w = theRounds[curLvl].walls[i];

				if(lvlReached[curLvl]){
					w.inst.SetActive(true);
				}
				else {
					// hack for wall circle
					if(curLvl == 2 && i == theRounds[curLvl].walls.Count-1){
						w.inst = Instantiate(wallCircle, w.data.transform.position, Quaternion.identity) as GameObject;
					}
					else {
						w.inst = Instantiate(wall, w.data.transform.position, Quaternion.identity) as GameObject;
					}

					w.inst.transform.localScale = w.data.transform.localScale;
					w.inst.transform.rotation = w.data.transform.rotation;
				}
			}
			
			for(int i=0; i < theRounds[curLvl].spawns.Count; ++i){
				SmartGameObject sp = theRounds[curLvl].spawns[i];

				if(lvlReached[curLvl]){
					sp.inst.SetActive(true);
				}
				else {
					sp.inst = Instantiate(spawnPoint, sp.data.transform.position, Quaternion.identity) as GameObject;
					sp.inst.transform.localScale = sp.data.transform.localScale;
					sp.inst.transform.rotation = sp.data.transform.rotation;
				}
			}
		}

		lvlReached[curLvl] = true;
		firstRun = false;
	}
}
