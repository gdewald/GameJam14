using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player that;

	public static GameObject[] entity = new GameObject[2];
	
	public static int life = 3;

	public static bool isCombining = false;
	public static bool isSplit = false;
	public static bool isAnimating = false;

	public int chainType = 0;

	float splitSpeed = 0.3f;	// lower is faster
	float combineSpeed = 0.5f;	// lower is faster
	int doneMoving = 0;

	void Start(){
		life = 3;
	}

	void Awake(){
		that = this;
	}


	#region Actions
	public void combine(){
		Player.isCombining = true;
		Player.isAnimating = true;
		doneMoving = 0;

		float x = Player.entity[0].transform.position.x + Player.entity[1].transform.position.x;
		float y = Player.entity[0].transform.position.y + Player.entity[1].transform.position.y;
		
		Vector3 newPos = new Vector3(x/2, y/2, 0);

		GameAudio.that.playCombine ();
		
		StartCoroutine(MoveToPosition(
			Player.entity[0].transform, 
			newPos,
			combineSpeed,
			false
		));
		
		StartCoroutine(MoveToPosition(
			Player.entity[1].transform,
			newPos,
			combineSpeed,
			false
		));

	}

	void combineFinished(){
		Player.isAnimating = false;
		entity[0].GetComponent<CreateChain>().SendMessage("ToggleChain");
		entity[1].SetActive(false);
		entity[0].GetComponent<Controller>().canShoot = true;
	}
		
	public void split(){
		Player.isAnimating = true;
		doneMoving = 0;

		entity[0].GetComponent<SpriteRenderer>().sprite = GameObject.Instantiate(Resources.Load<Sprite>("Images/spaceshipBlue")) as Sprite;
		Player.entity[1].transform.position = Player.entity[0].transform.position;
		entity[0].GetComponent<Controller>().canShoot = false;

		GameAudio.that.playSeparate ();

		StartCoroutine(MoveToPosition(
			Player.entity[0].transform, 
			new Vector3(Player.entity[0].transform.position.x - 2, Player.entity[0].transform.position.y),
			splitSpeed,
			true
		));

		StartCoroutine(MoveToPosition(
			Player.entity[1].transform,
			new Vector3(Player.entity[1].transform.position.x + 2, Player.entity[1].transform.position.y),
			splitSpeed,
			true
		));
		
	}

	void splitFinished(){
		Player.isAnimating = false;
		entity[0].GetComponent<CreateChain>().SendMessage("ToggleChain");
		
	}
	
	#endregion Actions

	#region Utilities
	IEnumerator MoveToPosition(Transform tForm, Vector3 newPos, float time, bool fromSplit){
		float elapsedTime = 0;
		Vector3 startingPos = tForm.position;

		while (elapsedTime < time){
			tForm.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
			elapsedTime += Time.deltaTime;


			if(elapsedTime >= time){
				if(++doneMoving == 2){

					// TODO: use a lambda callback instead

					if(fromSplit){
						splitFinished();
					}
					else {
						combineFinished();
						Player.isCombining = false;
						entity[0].GetComponent<SpriteRenderer>().sprite = GameObject.Instantiate(Resources.Load<Sprite>("Images/spaceshipFull")) as Sprite;
					}
						
				}
			}

			yield return null;
		}
	}
	#endregion Utilities
}
