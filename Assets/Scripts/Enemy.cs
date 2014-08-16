using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {

	protected int life;
	
	public abstract void Reset (int value);
	public abstract void TakeHit (Vector2 hitDirection);
	public abstract void TakeChainHit (Vector2 chainDirection, int chainType);
	
	private GameObject enemyDeath = Resources.Load<GameObject>("EnemyDeath");
	private string[] powerups = new string[]{ "ChainPowerup", "ShieldPowerup", "SpeedPowerup", "SprayPowerup"};

	public void die(bool countDeath = true){
		if (countDeath) {
			++Game.numKilled;
			if(Game.numKilled % 15 == 1 && Game.numPowerups <= 3) {
				string powerup_name = powerups[Random.Range(0, powerups.Length)];
				Object powerup = Instantiate (Resources.Load(powerup_name), this.transform.position, this.transform.rotation);
				powerup.name = powerup_name;
				Game.numPowerups++;
			}
		}

		--GameLogic.EnemyCount;
		
		GameAudio.that.playEnemyKilled(gameObject.transform.position);
		GameObject explosion = Instantiate(enemyDeath, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy (gameObject);
		Destroy(explosion, 1f);
	}
}
