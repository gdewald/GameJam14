using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
	protected int hitsLeft;

	public abstract void TakeHit (Vector2 hitDirection);
	public abstract void TakeChainHit (Vector2 chainDirection, int chainType);
	public abstract void SetHitsLeft (int hitsLeft);

	private GameObject enemyDeath = Resources.Load<GameObject>("EnemyDeath");

	public void die(){
		++Game.numKilled;
		--GameLogic.EnemyCount;
		
		GameObject explosion = Instantiate(enemyDeath, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy (gameObject);
		Destroy(explosion, 1f);
	}

}
