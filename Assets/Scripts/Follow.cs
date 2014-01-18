using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targets {
	public static IList<GameObject> objects = new List<GameObject>();
}

public class Follow : MonoBehaviour {
	public float maxSpeed;

	void Start(){

	}

	void Update () {
		if (Targets.objects.Count == 0)
			return;

		GameObject closestObj = Targets.objects[0];
		float closestDist = (Targets.objects [0].transform.position - transform.position).magnitude;

		if(Targets.objects.Count > 1)
		{
			foreach(GameObject obj in Targets.objects)
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
}
