using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutozoomScript : MonoBehaviour {
	public List<GameObject> objects;
	public Camera camera;
	
	Vector3 center;
	float size;
	
	public const float min_size = 5.0f;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		calcBoundingBox();
		camera.transform.position = Vector3.Lerp(camera.transform.position, center, Time.deltaTime);	
		
		if(objects.Count > 1 && size > min_size) {
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, size, Time.deltaTime);	
		}
		else camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, min_size, Time.deltaTime);
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
		
		Debug.Log("ul=("+ul.x+","+ul.y+") lr=("+lr.x+","+lr.y+")"	); 
		
		center = new Vector3((ul.x + lr.x) /2, (ul.y + lr.y) / 2, camera.transform.position.z);
		
		
		
		size = Mathf.Max(Mathf.Abs(ul.y - lr.y), Mathf.Abs(lr.x - ul.x))/2;
		size *= 1.1f;
			
	}

}
