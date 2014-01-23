using UnityEngine;
using System.Collections;

public class EnemyLarge : Enemy {

	private GameObject smallEnemy = Resources.Load("Enemy") as GameObject;
	private GameObject largeEnemy = Resources.Load ("SplitEnemy") as GameObject;

	public override void Reset (int value){
		life = value;
		UpdateTexture ();
	}

	public override void TakeHit(Vector2 hitDirection){
		++life;

		if (life > 4) {
			life = 1;
			Split (largeEnemy);
			return;
		}
		
		// Push Enemy Back
		UpdateTexture();
		GetComponent<Follow>().SetDelay(0.25f);
		rigidbody2D.velocity = hitDirection.normalized * 20.0f;
		GameAudio.that.playWallHit(gameObject.transform.position);
	}
	
	public override void TakeChainHit(Vector2 chainDirection, int chainType){
		if(chainType == 0){
			Split (smallEnemy);
		}
		else if(chainType == 1){
			die ();
		}
	}

	private void UpdateTexture(){
		if (life > 0 && life <= 4) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = GameObject.Instantiate (Resources.Load<Sprite> ("Images/Enemy" + (life - 1).ToString ())) as Sprite;
		}
	}
	
	private void Split(GameObject newType){
		
		Vector3 distanceVec = Player.entity[0].transform.position - Player.entity[1].transform.position;
		Vector3 directionVec = Vector3.Cross(distanceVec, new Vector3(0, 0, 1)).normalized;
		
		GameObject firstEnemy = Instantiate (newType, gameObject.transform.position + directionVec, Quaternion.identity) as GameObject;
		GameObject secondEnemy = Instantiate (newType, gameObject.transform.position - directionVec, Quaternion.identity) as GameObject;
		
		firstEnemy.GetComponent<Enemy>().Reset(life);
		secondEnemy.GetComponent<Enemy>().Reset(life);
		
		firstEnemy.transform.rigidbody2D.velocity = directionVec * 40.0f;;
		secondEnemy.transform.rigidbody2D.velocity = -directionVec * 40.0f;
		
		firstEnemy.GetComponent<Follow>().SetDelay(0.05f);
		secondEnemy.GetComponent<Follow>().SetDelay(0.05f);
		
		++GameLogic.EnemyCount;
		Destroy (gameObject);
		
		GameAudio.that.playEnemySliced(gameObject.transform.position);
	}
}
