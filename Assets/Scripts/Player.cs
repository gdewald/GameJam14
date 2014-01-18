using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static GameObject[] entity = new GameObject[2];

	public static bool isSplit = false;
	public static bool doCombine = false;
	public static bool doSplit = false;

	public static Vector3[] startPoint = new Vector3[2];
	public static Vector3[] endPoint = new Vector3[2];

	bool doDestroy = false;

	float splitSpeed = 0.3f;	// lower is faster
	float combineSpeed = 0.5f;	// lower is faster
	float rate = 0f;
	float[] iArr = {0f, 0f};

	void Update () {
		if(doCombine){
			combine();
		}

		if(doSplit){
			split();
		}

		if(doDestroy){
			destory();
		}
	}


	#region Actions
	bool csHelp(float speed){
		MoveObject(entity[0].transform, startPoint[0], endPoint[0], speed, ref iArr[0]);
		MoveObject(entity[1].transform, startPoint[1], endPoint[1], speed, ref iArr[1]);
		
		if(Mathf.Approximately(entity[0].transform.position.x, endPoint[0].x) && 
		   Mathf.Approximately(entity[1].transform.position.x, endPoint[1].x) &&
		   Mathf.Approximately(entity[0].transform.position.y, endPoint[0].y) && 
		   Mathf.Approximately(entity[1].transform.position.y, endPoint[1].y)
	    ){
			iArr[0] = 0f;
			iArr[1] = 0f;

			return true;
		}

		return false;
	}

	void combine(){
		if(csHelp(combineSpeed)){
			doCombine = false;
			doDestroy = true;
			entity[0].GetComponent<Controller>().canShoot = true;
		}
	}

	void split(){
		if(csHelp(splitSpeed)){
			doSplit = false;
			entity[0].GetComponent<Controller>().canShoot = false;
		}
	}

	void destory(){
		doDestroy = false;
		entity[1].SetActive(false);
	}
	#endregion Actions

	#region Utilities
	void MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time, ref float i) {
		rate = 1f / time;
		
		if (i < 1f){
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
		}
	}
	#endregion Utilities
}
