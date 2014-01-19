﻿using UnityEngine;
using System.Collections;

public class BulletCollide : MonoBehaviour {
	public float speed;

	void Update(){
		Vector2 vel = transform.rigidbody2D.velocity;
		if(vel.magnitude < speed){
			transform.rigidbody2D.velocity = vel.normalized * speed;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject obj = other.gameObject;

		if(obj.tag == "Enemy"){
			++Game.numKilled;

			print ("dying");

			Animator anim = other.GetComponent<Animator>();
			anim.SetBool("Dying", true);
			//Destroy (obj.rigidbody2D);
			Destroy (obj.collider2D);
			//Destroy (obj.tag);
			other.GetComponent<Follow>().enabled = false; 



			Destroy (obj, 1f);
			Destroy (gameObject);
			--GameLogic.EnemyCount;
		} 
		else if(obj.tag == "Wall"){
			Destroy (gameObject);
		}
	}
}
