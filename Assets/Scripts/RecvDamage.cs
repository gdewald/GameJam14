﻿using UnityEngine;
using System.Collections;

public class RecvDamage : MonoBehaviour {
	bool god;
	float godTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		godTimer -= Time.deltaTime;
		blink(godTimer);
		if(godTimer <= 0) {
			god = false;
			this.enabled = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag != "Enemy") return;
		
		if(!god && !Player.isCombining) {
			godTimer = 3;
			god = true;
			this.enabled = true;

			if(Player.life > 0) {
				--Player.life;
			}
			else
			{                   
				Time.timeScale = 0.0f;

			}

			// Destroy all enemies
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject obj in enemies){
				Enemy enemy = obj.GetComponent<Enemy>();
				enemy.die (false); // Don't count deaths
			}
		}
	}
	
	void blink(float t) {
		Color c = gameObject.GetComponent<SpriteRenderer>().material.color;
		if(t <= 0)
			c.a = 1.0f;
		else if(c.a == 0)
			c.a = 1.0f;
		else c.a = 0.0f;
		gameObject.GetComponent<SpriteRenderer>().material.color = c;
	}
}
