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
			Enemy enemy = other.GetComponent<Enemy>();
			enemy.TakeChainHit(rigidbody2D.velocity, 0);

			/*
			Destroy (obj);
			firstEnemy = Instantiate (enemy, obj.transform.position, Quaternion.identity) as GameObject;
			secondEnemy = Instantiate (enemy, obj.transform.position, Quaternion.identity) as GameObject;

			firstEnemy.GetComponent<Follow>().enabled = false;
			secondEnemy.GetComponent<Follow>().enabled = false;
			//firstEnemy.transform.rigidbody2D.AddForce(new Vector2(-10000, -10000));
			print ("hit");
			firstEnemy.transform.rigidbody2D.velocity = new Vector2(5, 0);
			secondEnemy.transform.rigidbody2D.velocity = new Vector2(-5, 0);
			StartCoroutine(delayedExec());
			*/
		}
	}
}
