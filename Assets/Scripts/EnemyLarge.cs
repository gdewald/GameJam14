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

			Destroy (gameObject);

			firstEnemy.GetComponent<Follow>().enabled = false;
			secondEnemy.GetComponent<Follow>().enabled = false;
			//firstEnemy.transform.rigidbody2D.AddForce(new Vector2(-10000, -10000));
			//print ("hit");
			firstEnemy.transform.rigidbody2D.velocity = new Vector2(5, 0);
			secondEnemy.transform.rigidbody2D.velocity = new Vector2(-5, 0);
			StartCoroutine(delayedExec());
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

	private IEnumerator delayedExec(){
		yield return new WaitForSeconds(0.5f);
		firstEnemy.GetComponent<Follow>().enabled = true;
		secondEnemy.GetComponent<Follow>().enabled = true;
	}
}
