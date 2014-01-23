using UnityEngine;
using System.Collections;

public class EnemySmall : Enemy {
	
	public override void Reset (int value){
		life = value;
		UpdateTexture ();
	}

	public override void TakeHit(Vector2 hitDirection){
		--life;

		// Die
		if (life <= 0) {
			die ();
			return;
		}

		// Push Enemy Back
		UpdateTexture ();
		GetComponent<Follow>().SetDelay(0.5f);
		rigidbody2D.velocity = hitDirection.normalized * 10.0f;
		GameAudio.that.playWallHit(gameObject.transform.position);
	}
	
	public override void TakeChainHit(Vector2 chainDirection, int chainType){
		if (Player.that.chainType == 1) {
			die ();
		}
	}

	private void UpdateTexture(){
		if (life > 0 && life <= 4) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = GameObject.Instantiate (Resources.Load<Sprite> ("Images/Enemy" + (life - 1).ToString ())) as Sprite;
		}
	}
}
