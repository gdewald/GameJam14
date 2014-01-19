using UnityEngine;
using System.Collections;

public class EnemyLarge : Enemy {

	private GameObject smallEnemy = Resources.Load("Enemy") as GameObject;
	private GameObject largeEnemy = Resources.Load ("SplitEnemy") as GameObject;

	public override void TakeHit(Vector2 hitDirection){
		// React to hit
		if (hitsLeft < 4) {
			SetHitsLeft (hitsLeft + 1);
			GetComponent<Follow>().SetDelay(0.25f);
			rigidbody2D.velocity = hitDirection.normalized * 20.0f;
			GameAudio.that.playWallHit(gameObject.transform.position);
		} else {
			SetHitsLeft (1);
			Split (largeEnemy);
		}
	}
	
	public override void TakeChainHit(Vector2 chainDirection, int chainType){
		switch (chainType) {
		// Split
		case 0: 
		{
			Split (smallEnemy);
			break;
		}
		// Die
		case 1:
			die ();
			break;
		}
	}

	public override void SetHitsLeft (int hitsLeft){
		this.hitsLeft = hitsLeft;
		if(hitsLeft > 0)
			gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Instantiate(Resources.Load<Sprite>("Images/Enemy" + (hitsLeft-1).ToString())) as Sprite;
	}
}
