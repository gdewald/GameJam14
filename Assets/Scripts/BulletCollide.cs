using UnityEngine;
using System.Collections;

public class BulletCollide : MonoBehaviour {
	public float speed;

	//GameObject explosion = Resources.Load("Explosion") as GameObject;

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

			//GameObject expl = Instantiate(explosion, obj.transform.position, Quaternion.identity) as GameObject;

			Destroy (obj);
			Destroy (gameObject);

			//Destroy(expl, 3f);
		} 
		else if (obj.tag == "Wall") {
			Destroy (gameObject);
		}
	}
}
