using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	float timer;
	string name;

	// Use this for initialization
	void Start () {
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0) {
			this.enabled = false;	
			switch(name) {
			case "SpeedPowerup":
				Debug.Log ("Speed powerup ran out");
				
				GetComponent<Controller>().maxSpeed = 10;
				break;
			case "ChainPowerup":
				Debug.Log ("Chain powerup ran out");
				
				foreach(GameObject o in GameObject.FindGameObjectsWithTag("ChainDestroy")) {
					o.tag = "ChainSplit";
				}
				break;
				
			default:
				break;
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag != "Powerup") return;
		switch(col.collider.name) {
			case "SpeedPowerup":
				Debug.Log ("Collided with speed powerup");
				name = col.collider.name;
				GetComponent<Controller>().maxSpeed = 15;
				timer = 5.0f;
				this.enabled = true;
				Destroy(col.collider.gameObject);
			break;
			case "ChainPowerup":
				Debug.Log ("Collided with chain powerup");
				name = col.collider.name;
				foreach(GameObject o in GameObject.FindGameObjectsWithTag("ChainSplit")) {
					o.tag = "ChainDestroy";
				}
				timer = 5.0f;
				this.enabled = true;
				Destroy(col.collider.gameObject);
			break;
			default:
			break;
		}
		return;
	}
}
