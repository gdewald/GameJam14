using UnityEngine;
using System.Collections;

public class ChainCollide : MonoBehaviour {
	public float speed;

	GameObject enemy = Resources.Load<GameObject>("Enemy");
	GameObject firstEnemy;
	GameObject secondEnemy;

	void Update () {
		Vector2 vel = transform.rigidbody2D.velocity;
		if (vel.magnitude < speed) {
			transform.rigidbody2D.velocity = vel.normalized * speed;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		GameObject obj = other.gameObject;
		if (obj.tag == "SplitEnemy") {
			Destroy (obj);
			firstEnemy = Instantiate (enemy, obj.transform.position, Quaternion.identity) as GameObject;
			secondEnemy = Instantiate (enemy, obj.transform.position, Quaternion.identity) as GameObject;
			
			Vector3 distanceVec = Player.entity[0].transform.position - Player.entity[1].transform.position;
			Vector3 directionVec = Vector3.Cross(distanceVec, new Vector3(0, 0, 1));
			
			firstEnemy.GetComponent<Follow>().enabled = false;
			secondEnemy.GetComponent<Follow>().enabled = false;
			
			firstEnemy.transform.rigidbody2D.velocity = directionVec;
			secondEnemy.transform.rigidbody2D.velocity = -directionVec;
			StartCoroutine(delayedExec());
		}
	}

	IEnumerator delayedExec(){
		yield return new WaitForSeconds(0.5f);
		firstEnemy.GetComponent<Follow>().enabled = true;
		secondEnemy.GetComponent<Follow> ().enabled = true;
	}
}
