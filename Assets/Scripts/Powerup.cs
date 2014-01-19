﻿using UnityEngine;
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
			endPowerup();
			this.enabled = false;	
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "Powerup") {
			
			Debug.Log ("Collided with " + col.collider.name);
			startPowerup (col.collider.name);
			this.enabled = true;
			Destroy(col.collider.gameObject);
		}
		else if (col.collider.tag == "Resource") {
			switch(col.collider.name) {
				case "Health":
					Player.life++;
					Destroy(col.collider.gameObject);
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
			GetComponent<Controller>().maxSpeed = 15;
			timer = 10.0f;
			break;
		case "ChainPowerup":
			foreach(GameObject o in GameObject.FindGameObjectsWithTag("ChainSplit")) {
				o.tag = "ChainDestroy";
			}
			timer = 10.0f;
			break;
		case "SprayPowerup":
			Player.entity[0].GetComponent<Controller>().fireMode = Controller.FireMode.SPRAY;
			timer = 10.0f;
			break;
			
		default:
			break;
		}
	}
	
	void endPowerup() {
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
		case "SprayPowerup":
			Debug.Log ("Spray powerup ran out");
			
			Player.entity[0].GetComponent<Controller>().fireMode = Controller.FireMode.SINGLE;
			break;
		default:
			break;
		}
	}
}
