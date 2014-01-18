using UnityEngine;
using System.Collections;

public class BulletCollide : MonoBehaviour {
	public float speed;

	void Update(){
		Vector2 vel = transform.rigidbody2D.velocity;
		if (vel.magnitude < speed) {
			transform.rigidbody2D.velocity = vel.normalized * speed;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject obj = other.gameObject;

		if (obj.tag == "Enemy") {
			++Game.numKilled;

			Animator anim = other.GetComponent<Animator>();

			obj.animation.Play("follow");

			//Destroy (obj, 3f);
			Destroy (gameObject);
		} 
		else if (obj.tag == "Wall") {
			Destroy (gameObject);
		}
	}
}
