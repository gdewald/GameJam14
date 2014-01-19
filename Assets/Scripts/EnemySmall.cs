using UnityEngine;
using System.Collections;

public class EnemySmall : Enemy {
	
	public override void TakeHit(Vector2 hitDirection){
		SetHitsLeft(hitsLeft-1);

		// Die
		if (hitsLeft <= 0) {
			//gameObject.rigidbody.velocity = hitDirection;
			die ();
		}
	}
	
	public override void TakeChainHit(Vector2 chainDirection, int chainType){
		
	}
	
	public override void SetHitsLeft (int hitsLeft){
		this.hitsLeft = hitsLeft;
		if(hitsLeft > 0)
			gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Instantiate(Resources.Load<Sprite>("Images/Enemy" + (hitsLeft-1).ToString())) as Sprite;
	}

	void Update(){

	}
}
