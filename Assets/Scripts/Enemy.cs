using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
	protected int hitsLeft;

	private GameObject enemyDeath = Resources.Load<GameObject>("EnemyDeath");

	public abstract void TakeHit (Vector2 hitDirection);
	public abstract void TakeChainHit (Vector2 chainDirection, int chainType);
	public abstract void SetHitsLeft (int hitsLeft);

	public void die(bool countDeath = true){
		if (countDeath) {
			++Game.numKilled;
		}

		--GameLogic.EnemyCount;
		
		GameObject explosion = Instantiate(enemyDeath, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy (gameObject);
		Destroy(explosion, 1f);
	}

	public void Split(GameObject newType){
		GameObject firstEnemy = Instantiate (newType, gameObject.transform.position, Quaternion.identity) as GameObject;
		GameObject secondEnemy = Instantiate (newType, gameObject.transform.position, Quaternion.identity) as GameObject;
		
		firstEnemy.GetComponent<Enemy>().SetHitsLeft(hitsLeft);
		secondEnemy.GetComponent<Enemy>().SetHitsLeft(hitsLeft);
		
		Vector3 distanceVec = Player.entity[0].transform.position - Player.entity[1].transform.position;
		Vector3 directionVec = Vector3.Cross(distanceVec, new Vector3(0, 0, 1));
		
		firstEnemy.transform.rigidbody2D.velocity = directionVec;
		secondEnemy.transform.rigidbody2D.velocity = -directionVec;
		
		firstEnemy.GetComponent<Follow>().SetDelay(0.5f);
		secondEnemy.GetComponent<Follow>().SetDelay(0.5f);
		
		++GameLogic.EnemyCount;
		Destroy (gameObject);
		
		GameAudio.that.playEnemySliced(gameObject.transform.position);
	}
}
