using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class EnemySet {
	public string enemy;
	public int number;

	public EnemySet(){}
	public EnemySet(string enemy, int number){
		this.enemy = enemy;
		this.number = number;
	}
}

class Wave {
	public float spawnTime = 2.0f;
	public IList<EnemySet> enemySets = new List<EnemySet>();

	static private float noEnemySpawnTime = 2.0f;

	public void Update(){
		// If no enemies, Lower Spawn Time
		if (GameLogic.EnemyCount == 0 && spawnTime > noEnemySpawnTime) {
			spawnTime = noEnemySpawnTime;
			return;
		}

		// Decrease SpawnTime
		if (spawnTime > 0.0f) {
			spawnTime -= Time.deltaTime;
		}

		// Spawn Enemies
		if (spawnTime <= 0.0f) {
			GameObject[] spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");
			foreach(EnemySet enemySet in enemySets){
				for(int i=0; i< enemySet.number; ++i){
					int spawnIndex = Random.Range (0, spawns.Length);
					Vector2 center = spawns[spawnIndex].transform.position;
					Vector2 offset = Random.insideUnitCircle * 4.0f;

					GameObject.Instantiate(Resources.Load (enemySet.enemy), center + offset, Quaternion.identity);
					++GameLogic.EnemyCount;
				}
			}
		}
	}

	public bool IsComplete(){
		return spawnTime <= 0.0f;
	}
}

class Round {
	public IList<Wave> waves = new List<Wave> ();

	public void Update(){
		// Round Complete
		if (waves.Count == 0) {
			return;
		}

		Wave wave = waves [0];

		// Update Wave
		wave.Update();

		// Wave Complete
		if (wave.IsComplete ()) {
			waves.RemoveAt(0);
			++GameLogic.waveNumber;
		}
	}

	public bool IsComplete(){
		if (waves.Count == 0 && GameLogic.EnemyCount == 0) {
			return true;
		}
		return false;
	}
}

public class GameLogic : MonoBehaviour {
	static public int EnemyCount = 0;

	private IList<Round> rounds = new List<Round>();

	static public int roundNumber = 1;
	static public int waveNumber = 0;

	public void Start(){
		SetupGame ();
	}

	public void Update(){
		// Game Complete
		if (rounds.Count == 0) {
			return;
		}

		Round round = rounds [0];

		// Update Round
		round.Update ();

		// Round Complete
		if (round.IsComplete ()) {
			rounds.RemoveAt(0);
			++roundNumber;
			waveNumber = 0;
		}
	}

	void SetupGame(){
		Round round;
		Wave wave;

		// Round 1
		{
			round = new Round();

			// Wave 1
			wave = new Wave();
			wave.spawnTime = 2.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 3));
			round.waves.Add(wave);

			// Wave 2
			wave = new Wave();
			wave.spawnTime = 5.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 5));
			round.waves.Add(wave);
			
			// Wave 3
			wave = new Wave();
			wave.spawnTime = 8.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 10));
			round.waves.Add(wave);
			
			// Wave 4
			wave = new Wave();
			wave.spawnTime = 10.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 20));
			round.waves.Add(wave);
			
			// Wave 5
			wave = new Wave();
			wave.spawnTime = 10.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 20));
			round.waves.Add(wave);

			rounds.Add(round);
		}

		// Round 2
		{
			round = new Round();
			
			// Wave 1
			wave = new Wave();
			wave.spawnTime = 2.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 20));
			round.waves.Add(wave);
			
			// Wave 2
			wave = new Wave();
			wave.spawnTime = 5.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 40));
			round.waves.Add(wave);
			
			// Wave 3
			wave = new Wave();
			wave.spawnTime = 8.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 60));
			round.waves.Add(wave);
			
			// Wave 4
			wave = new Wave();
			wave.spawnTime = 10.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 80));
			round.waves.Add(wave);
			
			// Wave 5
			wave = new Wave();
			wave.spawnTime = 10.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 100));
			round.waves.Add(wave);
			
			rounds.Add(round);
		}
	}
}
