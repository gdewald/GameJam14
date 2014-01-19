using UnityEngine;
using System.Collections;

public class BulletCollide : MonoBehaviour {
	public float speed;
	GameObject enemyDeath = Resources.Load<GameObject>("EnemyDeath");

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


			Destroy (gameObject);
			--GameLogic.EnemyCount;

			GameObject explosion = Instantiate(enemyDeath, obj.transform.position, Quaternion.identity) as GameObject;
			Destroy (obj);
			Destroy(explosion, 1f);
		} 
		else if(obj.tag == "Wall"){
			GameAudio.that.playWallHit(other.transform.position);
			Destroy (gameObject);
		}
	}
}
