using UnityEngine;
using System.Collections;

public class ChainCollide : MonoBehaviour {
	public float speed;
	GameObject enemy = Resources.Load("Enemy") as GameObject;

	void Update () {
		Vector2 vel = transform.rigidbody2D.velocity;
		if (vel.magnitude < speed) {
			transform.rigidbody2D.velocity = vel.normalized * speed;
		}
	}

	GameObject firstEnemy;
	GameObject secondEnemy;

	void OnTriggerEnter2D(Collider2D other){
		GameObject obj = other.gameObject;
		if (obj.tag == "SplitEnemy") {
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

		}
	}

	IEnumerator delayedExec(){
		yield return new WaitForSeconds(1.5f);
		firstEnemy.GetComponent<Follow>().enabled = true;
		secondEnemy.GetComponent<Follow> ().enabled = true;
	}
}
