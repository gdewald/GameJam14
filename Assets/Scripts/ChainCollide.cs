using UnityEngine;
using System.Collections;

public class ChainCollide : MonoBehaviour {
	public float speed;

	void Update () {
		Vector2 vel = transform.rigidbody2D.velocity;
		if (vel.magnitude < speed) {
			transform.rigidbody2D.velocity = vel.normalized * speed;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		GameObject obj = other.gameObject;
		if (obj.tag == "Enemy") {
			//Debug.Log("Chain hit " + obj.name);
			Enemy enemy = other.GetComponent<Enemy>();
			enemy.TakeChainHit(rigidbody2D.velocity, Player.that.chainType);
		}
	}
}
