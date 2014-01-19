using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
	protected int hitsLeft;

	public abstract void TakeHit (Vector2 hitDirection);
	public abstract void TakeChainHit (Vector2 chainDirection, int chainType);
	public abstract void SetHitsLeft (int hitsLeft);

	public void die(){
		++Game.numKilled;
		
		Animator anim = GetComponent<Animator>();
		anim.SetBool("Dying", true);
		//Destroy (rigidbody2D);
		Destroy (collider2D);
		//Destroy (tag);
		GetComponent<Follow>().enabled = false; 
		
		Destroy (gameObject, 1f);
		--GameLogic.EnemyCount;
	}

}
