using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	float timer;
	string name;
		
	public GameObject shield;
	
	float speedInit;

	// Use this for initialization
	void Start () {
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0) {
			endPowerup();
			this.enabled = false;	
		}
		else {
			switch(name) {
			case "ChainPowerup":
				foreach(GameObject o in GameObject.FindGameObjectsWithTag("ChainSplit")) {
					o.GetComponent<TrailRenderer>().enabled = true;
					o.tag = "ChainDestroy";
				}
				Player.that.chainType = 1;
				break;
			default:
				break;
			}
		}
	}
	
	void RecvPowerup(GameObject powerup) {
		if(powerup.tag == "Powerup") {
			Game.numPowerups--;
			//Debug.Log ("Collided with " + powerup.name);
			startPowerup (powerup.name);
			this.enabled = true;
			Destroy(powerup.gameObject);
		}
		else if (powerup.tag == "Resource") {
			switch(powerup.name) {
				case "Health":
					Player.life++;
					Destroy(powerup);
					break;
				default:
					break;
			}
		}
		return;
	}
	
	void startPowerup(string name_new) {
		//stop current powerup
		if(this.enabled) endPowerup();
	
		name = name_new;
		
		switch(name) {
		case "SpeedPowerup":
			speedInit = GetComponent<Controller>().maxSpeed;
			GetComponent<Controller>().maxSpeed = speedInit * 1.5f;
			timer = 10.0f;
			break;
		case "ChainPowerup":

			foreach(GameObject o in GameObject.FindGameObjectsWithTag("ChainSplit")) {
				o.tag = "ChainDestroy";
				o.GetComponent<TrailRenderer>().enabled = true;
			}
			Player.that.chainType = 1;

			timer = 10.0f;
			break;
		case "SprayPowerup":
			Player.entity[0].GetComponent<Controller>().fireMode = Controller.FireMode.SPRAY;
			timer = 10.0f;
			break;
			
		case "ShieldPowerup":
			shield.GetComponent<CircleCollider2D>().enabled = true;
			shield.GetComponent<SpriteRenderer>().enabled = true;
			timer = 10.0f;
			break;
			
		default:
			break;
		}
	}
	
	void endPowerup() {
		switch(name) {
		case "SpeedPowerup":
			//Debug.Log ("Speed powerup ran out");
			
			GetComponent<Controller>().maxSpeed = speedInit;
			break;
		case "ChainPowerup":
			//.Log ("Chain powerup ran out");
			
			foreach(GameObject o in GameObject.FindGameObjectsWithTag("ChainDestroy")) {
				o.GetComponent<TrailRenderer>().enabled = false;
				o.tag = "ChainSplit";
			}
			Player.that.chainType = 0;

			break;
		case "SprayPowerup":
			//Debug.Log ("Spray powerup ran out");
			
			Player.entity[0].GetComponent<Controller>().fireMode = Controller.FireMode.SINGLE;
			break;
		case "ShieldPowerup":			
			//Debug.Log ("Shield powerup ran out");		
			shield.GetComponent<CircleCollider2D>().enabled = false;
			shield.GetComponent<SpriteRenderer>().enabled = false;
			
			break;
		default:
			break;
		}
	}
}
