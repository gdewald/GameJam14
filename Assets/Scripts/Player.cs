using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool isSplit = false;

	bool doCombine = false;
	bool doSplit = false;
	bool doDestroy = false;

	Vector2[] startPoint = new Vector2[2];
	Vector2[] endPoint = new Vector2[2];
	
	float rate = 0f;
	float[] iArr = {0f, 0f};

	void Start () {
		startPoint[0] = new Vector2(-1, 0);
		startPoint[1] = new Vector2(1, 0);

		endPoint[0] = Vector2.zero;
		endPoint[1] = Vector2.zero;
	}

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

	void getInput(){

	}


	#region Actions
	void combine(){
		MoveObject(Game.cube[0].transform, startPoint[0], endPoint[0], 1f, ref iArr[0]);
		MoveObject(Game.cube[1].transform, startPoint[1], endPoint[0], 1f, ref iArr[1]);
		
		if(Mathf.Approximately(Game.cube[0].transform.position.x, 0f) && 
		   Mathf.Approximately(Game.cube[1].transform.position.x, 0f)
	    ){
			doCombine = false;
			doDestroy = true;
		}
	}

	void split(){

	}

	void destory(){
		doDestroy = false;
		Destroy(Game.cube[1]);
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
