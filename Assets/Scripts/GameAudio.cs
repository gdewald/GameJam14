using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public static GameAudio that;

	public AudioClip shoot;
	public AudioClip wallHit;

	void Awake(){
		that = this;
	}

	public void playShoot(){
		AudioSource.PlayClipAtPoint(shoot, Player.entity[0].transform.position);
	}

	public void playWallHit(Vector3 pos){
		AudioSource.PlayClipAtPoint(wallHit, pos);
	}
}
