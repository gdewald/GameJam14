using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class EnemySet {
	public string enemy;
	public int number;
	public int type;

	public EnemySet(){}
	public EnemySet(string enemy, int number, int type){
		this.enemy = enemy;
		this.number = number;
		this.type = type;
	}
}

class SpawnWave {
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
			// Print help on the screen!
			if(GameLogic.roundNumber == 1){
				if(GameLogic.waveNumber == 0){
					Camera.main.GetComponent<PrintMessage>().printMessage("Left Stick: Move\n\nRight Stick: Shoot!", 15);
				}
				else if(GameLogic.waveNumber == 3){
					Camera.main.GetComponent<PrintMessage>().printMessage("XBox Bumpers:\nSplit your ship appart!\n\nCut large enemies in half!", 20);
					//Camera.main.GetComponent<PrintMessage>().printMessage("Split the large orbs apart!");
				}
			}

			GameLogic.gameLogic.StartCoroutine(Spawn ());
			GameAudio.that.playWaveStart();
		}
	}

	IEnumerator Spawn() {
		// Incriment enemy count first!!!
		foreach (EnemySet enemySet in enemySets) {
			GameLogic.EnemyCount += enemySet.number;
		}

		GameObject[] spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");
		while(true){
			if(enemySets.Count == 0){
				break;
			}

			int setIndex = Random.Range(0, enemySets.Count);
			EnemySet enemySet = enemySets[setIndex];

			int spawnIndex = Random.Range (0, spawns.Length);
			Vector2 center = spawns[spawnIndex].transform.position;
			Vector2 offset = Random.insideUnitCircle * (spawns[spawnIndex].transform.localScale.x / 2.0f);
			
			GameObject obj = (GameObject) GameObject.Instantiate(Resources.Load (enemySet.enemy), center + offset, Quaternion.identity);
			obj.GetComponent<Enemy>().SetHitsLeft(enemySet.type);

			--enemySet.number;
			if(enemySet.number == 0){
				enemySets.RemoveAt(setIndex);
			}

			yield return new WaitForSeconds(0.08f);
		}
	}

	public bool IsComplete(){
		return spawnTime <= 0.0f;
	}
}

class SpawnRound {
	public IList<SpawnWave> waves = new List<SpawnWave> ();

	public void Update(){
		// Round Complete
		if (waves.Count == 0) {
			return;
		}

		SpawnWave wave = waves [0];

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
	static public GameLogic gameLogic;

	//private IList<Round> rounds = new List<Round>();
	SpawnRound round;

	static public int roundNumber = 1;
	static public int waveNumber = 0;

	static private int spawnRoundNumber = -1;

	public void Start(){
		gameLogic = this;
		round = null;
		roundNumber = 1;
		waveNumber = 0;
		spawnRoundNumber = -1;

		//SetupGame ();
	}

	public void Update(){
		// Game Complete
		if (round == null) {
			if(GameLevel.curLvl == 0){
				++spawnRoundNumber;
			}

			GetSpawnRound (spawnRoundNumber);

			Camera.main.GetComponent<PrintMessage>().printMessage("Round " + roundNumber, 3);
		}

		// Update Round
		round.Update ();

		// Round Complete
		if (round.IsComplete ()) {
			GameLevel.that.next();

			round = null;
			++roundNumber;
			waveNumber = 0;
		}
	}

	void GetSpawnRound(int roundIndex){
		SpawnWave wave;
		round = new SpawnRound();

		if (roundIndex == 0) {
			// Wave 1
			wave = new SpawnWave();
			wave.spawnTime = 2.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 3, 1));
			round.waves.Add(wave);
			
			// Wave 2
			wave = new SpawnWave();
			wave.spawnTime = 5.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 8, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 3, 2));
			round.waves.Add(wave);
			
			// Wave 3
			wave = new SpawnWave();
			wave.spawnTime = 8.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 8, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 2, 4));
			round.waves.Add(wave);
			
			// Wave 4
			wave = new SpawnWave();
			wave.spawnTime = 10.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 6, 1));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 2, 1));
			round.waves.Add(wave);
			
			// Wave 5
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 10, 1));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 8, 1));
			round.waves.Add(wave);
			
			// Wave 6
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 20, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 10, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 8, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 5, 4));
			round.waves.Add(wave);
			
			// Wave 7
			wave = new SpawnWave();
			wave.spawnTime = 2.0f;
			wave.enemySets.Add (new EnemySet("SplitEnemy", 8, 1));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 6, 2));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 4, 3));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 2, 4));
			round.waves.Add(wave);
		} else if (roundIndex == 1) {
			// Wave 1
			wave = new SpawnWave();
			wave.spawnTime = 2.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 20, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 5, 2));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 3, 1));
			round.waves.Add(wave);
			
			// Wave 2
			wave = new SpawnWave();
			wave.spawnTime = 20.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 30, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 10, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 5, 3));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 6, 1));
			round.waves.Add(wave);
			
			// Wave 3
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 40, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 20, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 10, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 5, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 8, 1));
			round.waves.Add(wave);
			
			// Wave 4
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 50, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 30, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 20, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 15, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 10, 1));
			round.waves.Add(wave);
			
			// Wave 5
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 50, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 40, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 30, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 25, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 12, 1));
			round.waves.Add(wave);
		}
		else// (roundIndex == 2)
		{
			int extra = roundIndex * 5;

			// Wave 1
			wave = new SpawnWave();
			wave.spawnTime = 2.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 20 + extra, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 5 + extra, 2));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 3 + extra, 1));
			round.waves.Add(wave);
			
			// Wave 2
			wave = new SpawnWave();
			wave.spawnTime = 20.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 30 + extra, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 10 + extra, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 5 + extra, 3));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 6 + extra, 1));
			round.waves.Add(wave);
			
			// Wave 3
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 40 + extra, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 20 + extra, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 10 + extra, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 5 + extra, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 8 + extra, 1));
			round.waves.Add(wave);
			
			// Wave 4
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 50 + extra, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 30 + extra, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 20 + extra, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 15 + extra, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 10 + extra, 1));
			round.waves.Add(wave);
			
			// Wave 5
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 50 + extra, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 40 + extra, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 30 + extra, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 25 + extra, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 12 + extra, 1));
			round.waves.Add(wave);
		}
	}
}
