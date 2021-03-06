﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class EnemySet {
	public string enemy;
	public int count;
	public int value;

	public EnemySet(){}
	public EnemySet(string enemy, int count, int value){
		this.enemy = enemy;
		this.count = count;
		this.value = value;
	}
}

class SpawnWave {
	public delegate void WaveStartFunction();

	public IList<EnemySet> enemySets = new List<EnemySet>();
	public float spawnTime = 2.0f;
	public bool waitForEnemies = false;
	public WaveStartFunction startFunction = null;


	private const float noEnemySpawnTime = 3.0f;
	private bool waveStarted = false;
	private bool waveComplete = false;

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
		if (!waveStarted && spawnTime <= 0.0f) {

			// Wait for enemies to be gone
			if(waitForEnemies && GameLogic.EnemyCount != 0){
				return;
			}

			waveStarted = true;
			if(startFunction != null){
				startFunction();
			}

			// Spawn Waves
			GameLogic.gameLogic.StartCoroutine(Spawn ());

			// Play Spawn Audio
			GameAudio.that.playWaveStart();
		}
	}

	IEnumerator Spawn() {
		// Incriment enemy count first!!!
		foreach (EnemySet enemySet in enemySets) {
			GameLogic.EnemyCount += enemySet.count;
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
			obj.GetComponent<Enemy>().Reset(enemySet.value);
	
			--enemySet.count;
			if(enemySet.count == 0){
				enemySets.RemoveAt(setIndex);
			}

			yield return new WaitForSeconds(0.08f);
		}

		waveComplete = true;
	}

	public bool IsComplete(){
		return waveComplete;
	}
}

class SpawnRound {
	public IList<SpawnWave> waves = new List<SpawnWave> ();
	
	private bool roundOver = false;
	private float roundEndTimer = 0.0f;

	public void Update(){

		if (waves.Count == 0){
			if(!roundOver){
				//if(enemies.Length == 0){
				if(GameLogic.EnemyCount == 0){
					// Debug potential error
					GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
					if(enemies.Length != 0){
						Debug.LogError("ERROR: Enemy Count should be 0!!!");
					}

					roundEndTimer = 5.0f;
					roundOver = true;
					Camera.main.GetComponent<PrintMessage>().printMessage("Round Complete", 5.0f);
				}
			}

			if (roundEndTimer > 0.0f) {
				roundEndTimer -= Time.deltaTime;
			}

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
		return (roundOver && roundEndTimer <= 0.0f);
	}
}

public class GameLogic : MonoBehaviour {
	static public int EnemyCount;
	static public GameLogic gameLogic;
	static public int roundNumber = 1;
	static public int waveNumber = 0;

	static public int spawnRoundNumber = -1;

	private SpawnRound round;

	public void Start(){
		EnemyCount = 0;
		roundNumber = 1;
		waveNumber = 0;
		spawnRoundNumber = -1;

		gameLogic = this;
		round = null;

		//SetupGame ();
	}

	public void Update(){
		// Game Complete
		if (round == null) {
		
			GameLevel.that.next();
			//if(GameLevel.curLvl == 0){
			++spawnRoundNumber;
			//}

			GetSpawnRound (spawnRoundNumber);

			Camera.main.GetComponent<PrintMessage>().printMessage("Round " + roundNumber, 3);
		}

		// Update Round
		round.Update ();

		// Round Complete
		if (round.IsComplete ()) {
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
			if(spawnRoundNumber == 0){
				wave.startFunction = () => {
					Camera.main.GetComponent<PrintMessage>().printMessage("Left Stick: Move\n\nRight Stick: Shoot", 15.0f);
				};
			}
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
			wave.enemySets.Add (new EnemySet("Enemy", 12, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 8, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 2, 4));
			round.waves.Add(wave);
			
			// Wave 4
			wave = new SpawnWave();
			wave.spawnTime = 10.0f;
			wave.enemySets.Add (new EnemySet("SplitEnemy", 2, 1));
			if(spawnRoundNumber == 0){
				Player.canSplit = true;
			}
			round.waves.Add(wave);

			// Wave 5
			wave = new SpawnWave();
			wave.spawnTime = 10.0f;
			wave.enemySets.Add (new EnemySet("SplitEnemy", 2, 1));
			if(spawnRoundNumber == 0){
				wave.startFunction = () => {
					Camera.main.GetComponent<PrintMessage>().printMessage("XBox Bumpers:\nSplit your ship appart!\n\nCut large enemies in half", 20.0f);
				};
			}
			round.waves.Add(wave);

			// Wave 6
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("EnemyBlob", 1, 100));
			wave.waitForEnemies = true;
			wave.startFunction = () => {
				Camera.main.GetComponent<PrintMessage>().printMessage("Boss Wave", 5.0f);
			};
			round.waves.Add(wave);

		} 
		else if (roundIndex == 1) {
			// Wave 1
			wave = new SpawnWave();
			wave.spawnTime = 5.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 8, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 3, 2));
			round.waves.Add(wave);
			
			// Wave 2
			wave = new SpawnWave();
			wave.spawnTime = 8.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 12, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 8, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 2, 4));
			round.waves.Add(wave);

			// Wave 3
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 8, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 5, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 3, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 2, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 2, 1));
			round.waves.Add(wave);
			
			// Wave 4
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 8, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 5, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 3, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 2, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 8, 2));
			round.waves.Add(wave);

			// Wave 5
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("EnemyBlob", 2, 100));
			wave.waitForEnemies = true;
			wave.startFunction = () => {
				Camera.main.GetComponent<PrintMessage>().printMessage("Boss Wave", 5.0f);
			};
			round.waves.Add(wave);
			
		} 
		else if (roundIndex == 2) {
			// Wave 1
			wave = new SpawnWave();
			wave.spawnTime = 8.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 12, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 8, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 2, 4));
			round.waves.Add(wave);
			
			// Wave 2
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 8, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 5, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 3, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 2, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 2, 1));
			round.waves.Add(wave);

			// Wave 3
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 20, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 10, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 8, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 5, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 8, 3));
			round.waves.Add(wave);
			
			// Wave 4
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 20, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 10, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 8, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 5, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 8, 1));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 6, 2));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 4, 3));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 2, 4));
			round.waves.Add(wave);
			
			// Wave 5
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("EnemyBlob", 3, 100));
			wave.waitForEnemies = true;
			wave.startFunction = () => {
				Camera.main.GetComponent<PrintMessage>().printMessage("Boss Wave", 5.0f);
			};
			round.waves.Add(wave);
			
		} 
		/*
		else if (roundIndex == 3) {
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
			wave.enemySets.Add (new EnemySet("EnemyBlob", 4, 100));
			wave.waitForEnemies = true;
			wave.startFunction = () => {
				Camera.main.GetComponent<PrintMessage>().printMessage("Boss Wave", 5.0f);
			};
			round.waves.Add(wave);
		}
		*/
		else// (roundIndex == 3)
		{
			int extra = (roundIndex-1) * 5;

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
			/*
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("Enemy", 50 + extra, 1));
			wave.enemySets.Add (new EnemySet("Enemy", 40 + extra, 2));
			wave.enemySets.Add (new EnemySet("Enemy", 30 + extra, 3));
			wave.enemySets.Add (new EnemySet("Enemy", 25 + extra, 4));
			wave.enemySets.Add (new EnemySet("SplitEnemy", 12 + extra, 1));
			round.waves.Add(wave);
			*/

			// Wave 5
			wave = new SpawnWave();
			wave.spawnTime = 30.0f;
			wave.enemySets.Add (new EnemySet("EnemyBlob", extra, 50));
			wave.waitForEnemies = true;
			wave.startFunction = () => {
				Camera.main.GetComponent<PrintMessage>().printMessage("Boss Wave", 5.0f);
			};
			round.waves.Add(wave);
		}
	}
}
