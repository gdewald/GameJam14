using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public static GameAudio that;

	public AudioClip shoot;
	public AudioClip wallHit;
	public AudioClip enemyKilled;
	public AudioClip enemySliced;
	public AudioClip separate;
	public AudioClip bgMusic;
	public AudioClip combine;
	public AudioClip waveStart;

	void Awake(){
		that = this;
	}

	void Start(){
		// Get the audiosource attached to camera
		AudioSource source  = GetComponent<AudioSource>();

		//Loop the bg music
		if(!source.isPlaying) {
			source.clip = bgMusic;
			source.volume = .3f;
			source.loop = true;
			source.Play();
		}
	}

	public void playShoot(){
		AudioSource.PlayClipAtPoint(shoot, Player.entity[0].transform.position);
	}

	public void playWallHit(Vector3 pos){
		AudioSource.PlayClipAtPoint(wallHit, pos);
	}

	public void playEnemyKilled(Vector3 pos){
		AudioSource.PlayClipAtPoint(enemyKilled, pos);
	}

	public void playEnemySliced(Vector3 pos){
		AudioSource.PlayClipAtPoint(enemySliced, pos);
	}

	public void playSeparate(){
		AudioSource.PlayClipAtPoint(separate, Player.entity [0].transform.position);
	}

	public void playCombine(){
		AudioSource.PlayClipAtPoint (combine, Player.entity [0].transform.position);
	}

	public void playWaveStart(){
		AudioSource.PlayClipAtPoint (waveStart, Player.entity [0].transform.position);
	}
}
