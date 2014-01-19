using UnityEngine;
using System.Collections;

public class EnemyLarge : Enemy {

	private GameObject enemy = Resources.Load("Enemy") as GameObject;
	private GameObject firstEnemy;
	private GameObject secondEnemy;

	public override void TakeHit(Vector2 hitDirection){
		// React to hit
		print ("hit large");
	}
	
	public override void TakeChainHit(Vector2 chainDirection, int chainType){
		switch (chainType) {
		// Split
		case 0: 
		{
			firstEnemy = Instantiate (enemy, gameObject.transform.position, Quaternion.identity) as GameObject;
			secondEnemy = Instantiate (enemy, gameObject.transform.position, Quaternion.identity) as GameObject;

			firstEnemy.GetComponent<Enemy>().SetHitsLeft(hitsLeft);
			secondEnemy.GetComponent<Enemy>().SetHitsLeft(hitsLeft);

			Vector3 distanceVec = Player.entity[0].transform.position - Player.entity[1].transform.position;
			Vector3 directionVec = Vector3.Cross(distanceVec, new Vector3(0, 0, 1));


			firstEnemy.transform.rigidbody2D.velocity = directionVec;
			secondEnemy.transform.rigidbody2D.velocity = -directionVec;

			firstEnemy.GetComponent<Follow>().SetDelay(0.5f);
			secondEnemy.GetComponent<Follow>().SetDelay(0.5f);
			
			Destroy (gameObject);

			GameAudio.that.playEnemySliced(gameObject.transform.position);

			//firstEnemy.GetComponent<Follow>().enabled = false;
			//secondEnemy.GetComponent<Follow>().enabled = false;

			//Destroy (rigidbody2D);
			//Destroy (collider2D);
			//Destroy (renderer);

			//StartCoroutine(delayedExec());

			break;
		}
		// Die
		case 1:

			break;
		}
	}

	public override void SetHitsLeft (int hitsLeft){
		this.hitsLeft = hitsLeft;
		if(hitsLeft > 0)
			gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Instantiate(Resources.Load<Sprite>("Images/Enemy" + (hitsLeft-1).ToString())) as Sprite;
	}

	/*
	private IEnumerator delayedExec(){
		yield return new WaitForSeconds(0.5f);
		firstEnemy.GetComponent<Follow>().enabled = true;
		secondEnemy.GetComponent<Follow>().enabled = true;
		Destroy (gameObject);
	}
	*/
}
