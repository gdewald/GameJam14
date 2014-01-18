using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutozoomScript : MonoBehaviour {
	public List<GameObject> objects;
	public Camera camera;
	
	public float minSize = 10.0f;

	Vector3 center;
	float size;

	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		calcBoundingBox();
	
		//Zoom camera
		if(objects.Count > 1 && size > minSize) {
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, size, Time.deltaTime);	
		}
		else camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, minSize, Time.deltaTime);
		
		//Move camera(only if distance is big enough
		if(Vector3.Distance(camera.transform.position, center) > camera.orthographicSize * .1)
			camera.transform.position = Vector3.Lerp(camera.transform.position, center, Time.deltaTime * 3);	
	}
	
	// Returns the 'center' between the followed objects
	void calcBoundingBox() {
		Vector3 ul = objects[0].transform.position;
		Vector3 lr = objects[0].transform.position;
		
		foreach(GameObject o in objects) {
				if(o.transform.position.x < ul.x)
					ul.x = o.transform.position.x;
				if(o.transform.position.y > ul.y)
					ul.y = o.transform.position.y;
				if(o.transform.position.x > lr.x)
					lr.x = o.transform.position.x;			
				if(o.transform.position.y < lr.y)
					lr.y = o.transform.position.y;
		}
		
		//Debug.Log("ul=("+ul.x+","+ul.y+") lr=("+lr.x+","+lr.y+")"	); 
		
		center = new Vector3((ul.x + lr.x) /2, (ul.y + lr.y) / 2, camera.transform.position.z);
		
		//calculate required zoom to see all the objects
		size = Mathf.Max(Mathf.Abs(ul.y - lr.y), Mathf.Abs(lr.x - ul.x))/2;
		//some padding
		size *= 1.1f;	
	}

}
