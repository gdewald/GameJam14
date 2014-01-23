using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBlob : Enemy {
	
	protected int level;
	private GameObject enemyBlob = Resources.Load ("EnemyBlob") as GameObject;
	private IDictionary<GameObject, float> touchingEnemies = new Dictionary<GameObject, float> ();

	public override void Reset (int value){
		life = value;
		level = Mathf.CeilToInt(Mathf.Log(value, 2)) + 1;

		// Size = sqrt of level
		float size = Mathf.Sqrt (level);
		transform.localScale = new Vector3 (size, size, 0.0f);
		gameObject.rigidbody2D.mass = level;
		GetComponent<Follow>().maxSpeed = Mathf.Max (1.0f, 5.0f - (level - 1) * 0.5f);
	}

	public override void TakeHit(Vector2 hitDirection){
		if (life == 1) {
			die ();
			return;
		}

		Split (hitDirection);
	}
	
	public override void TakeChainHit(Vector2 chainDirection, int chainType){
		if (Player.that.chainType == 1) {
			die ();
			return;
		}

		if (level > 1) {
			Split (chainDirection);
		} else {
			touchingEnemies.Clear();
		}
	}

	private void Split(Vector2 splitAlong){
		Vector3 directionVec = new Vector3 (splitAlong.y, -splitAlong.x, 0.0f);
		directionVec = directionVec.normalized * (0.5f * Mathf.Sqrt(level));

		GameObject firstEnemy = Instantiate (enemyBlob, gameObject.transform.position + directionVec, Quaternion.identity) as GameObject;
		GameObject secondEnemy = Instantiate (enemyBlob, gameObject.transform.position - directionVec, Quaternion.identity) as GameObject;

		firstEnemy.GetComponent<EnemyBlob>().Reset (life/2);
		secondEnemy.GetComponent<EnemyBlob>().Reset (life - life/2);
		
		firstEnemy.transform.rigidbody2D.velocity = directionVec * 20.0f;
		secondEnemy.transform.rigidbody2D.velocity = -directionVec * 20.0f;
		
		firstEnemy.GetComponent<Follow>().SetDelay(0.05f);
		secondEnemy.GetComponent<Follow>().SetDelay(0.05f);
		
		++GameLogic.EnemyCount;
		Destroy (gameObject);
		
		GameAudio.that.playEnemySliced(gameObject.transform.position);
	}

	private void Join(GameObject other){
		EnemyBlob enemy = other.GetComponent<EnemyBlob> ();
		enemy.touchingEnemies.Clear ();

		// Increase level of object
		Reset (life + enemy.life);

		--GameLogic.EnemyCount;
		Destroy (other);
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.GetComponent<EnemyBlob>() != null) {
			touchingEnemies [other.gameObject] = 0.0f;
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.GetComponent<EnemyBlob>() != null) {
			if(!touchingEnemies.ContainsKey(other.gameObject)){
				touchingEnemies [other.gameObject] = 0.0f;
			}

			touchingEnemies [other.gameObject] += Time.deltaTime;
			if (touchingEnemies [other.gameObject] >= 1.0f) {
				Join (other.gameObject);
			}
		}
	}
	
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.GetComponent<EnemyBlob>() != null) {
			touchingEnemies.Remove (other.gameObject);
		}
	}
}
