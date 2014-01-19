using UnityEngine;
using System.Collections;

public class EnemySmall : Enemy {
	
	public override void TakeHit(Vector2 hitDirection){
		SetHitsLeft(hitsLeft-1);

		// PushBack
		GetComponent<Follow>().SetDelay(0.5f);
		rigidbody2D.velocity = hitDirection.normalized * 10.0f;

		// Die
		if (hitsLeft <= 0) {
			//gameObject.rigidbody.velocity = hitDirection;
			GameAudio.that.playEnemyKilled(gameObject.transform.position);
			die ();
		}
	}
	
	public override void TakeChainHit(Vector2 chainDirection, int chainType){
		if (Player.that.chainType == 1) {
			die ();
		}
	}
	
	public override void SetHitsLeft (int hitsLeft){
		this.hitsLeft = hitsLeft;
		if(hitsLeft > 0)
			gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Instantiate(Resources.Load<Sprite>("Images/Enemy" + (hitsLeft-1).ToString())) as Sprite;
	}

	void Update(){

	}
}
