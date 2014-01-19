using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Follow : MonoBehaviour {
	public float maxSpeed;

	private float delayTime = 0.0f;

	void Start(){

	}

	void Update () {
		if (delayTime > 0.0f) {
			delayTime -= Time.deltaTime;
			return;
		}

		GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
		if (targets.Length == 0){
			return;
		}

		GameObject closestObj = targets[0];
		float closestDist = (targets[0].transform.position - transform.position).magnitude;

		if(targets.Length > 1)
		{
			foreach(GameObject obj in targets)
			{
				Vector2 dir = obj.transform.position - transform.position;
				float distance = dir.magnitude;

				if(distance < closestDist)
				{
					closestObj = obj;
					closestDist = distance;
				}
			}
		}

		Vector2 moveDir = closestObj.transform.position - transform.position;
		rigidbody2D.velocity = moveDir.normalized * maxSpeed;
	}

	public void SetDelay(float delay){
		delayTime = delay;
	}
}
