using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public bool canShoot;

	public float maxSpeed;
	public float shotDelay;
	
	public string movementAxis = "LeftStick";
	public string aimAxis = "RightStick";
	
	public float thetaSpread = 15;
	
	public enum FireMode { SINGLE, SPRAY };	
	public FireMode fireMode = FireMode.SINGLE;
	
	private float shotTimer;
	private float freezeTimer;

	void Start(){
		shotTimer = 0.0f;
		//Targets.objects.Add (this.gameObject);
		Resources.Load ("Bullet");
	}

	void FixedUpdate(){
		if(shotTimer > 0.0f){
			shotTimer -= Time.deltaTime;
		}
		if (freezeTimer > 0.0f) {
			freezeTimer -= Time.deltaTime;
			return;
		}

		float leftH = Input.GetAxis (movementAxis + "X");
		float leftV = Input.GetAxis (movementAxis + "Y");

		Vector2 leftStick = new Vector2 (leftH, leftV);
		rigidbody2D.velocity = leftStick * maxSpeed;

		// Try to shoot
		float rightH = Input.GetAxis (aimAxis + "X");
		float rightV = Input.GetAxis (aimAxis + "Y");
		Vector2 rightStick = new Vector2 (rightH, rightV);
		if(canShoot && rightStick.magnitude >= 0.25f)
			Shoot (rightStick);

		if(rigidbody2D.velocity.magnitude > 0.5f) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0,  -Mathf.Atan2(leftH, leftV) * Mathf.Rad2Deg + 90), Time.deltaTime * rigidbody2D.velocity.magnitude)	;
		}

		// toggle split
		if(Input.GetButtonDown("ToggleSplit")){
			toggleSplit();
		}

	}

	void Shoot(Vector2 direction){
		if(shotTimer <= 0.0f){
			GameAudio.that.playShoot();
			shotTimer = shotDelay;

			direction.Normalize();

			Vector3 pos = transform.position + new Vector3(direction.x * 0.8f, direction.y * 0.8f, 0.0f);

			GameObject bullet = (GameObject) Instantiate(Resources.Load("Bullet"), pos, Quaternion.identity) ;
			bullet.rigidbody2D.velocity = direction;
			if(fireMode == FireMode.SPRAY) {
				bullet = (GameObject) Instantiate(Resources.Load("Bullet"), pos, Quaternion.identity) ;
				float theta = thetaSpread * Mathf.Deg2Rad;
				Vector3 dir = new Vector3(0,0,0);
				dir.x = direction.x * Mathf.Cos(theta) - direction.y * Mathf.Sin (theta);
				dir.y = direction.x * Mathf.Sin(theta) + direction.y * Mathf.Cos(theta);
				bullet.rigidbody2D.velocity = dir;
				
				bullet = (GameObject) Instantiate(Resources.Load("Bullet"), pos, Quaternion.identity) ;
				theta = -thetaSpread * Mathf.Deg2Rad;
				dir.x = direction.x * Mathf.Cos(theta) - direction.y * Mathf.Sin (theta);
				dir.y = direction.x * Mathf.Sin(theta) + direction.y * Mathf.Cos(theta);
				bullet.rigidbody2D.velocity = dir;
			}
		}
	}

	void toggleSplit(){
		// don't toggle until split or combine has finished
		if(Player.isAnimating){
			return;
		}
		
		if(!Player.isSplit){
			Player.entity[1].SetActive(true);
			Player.that.split();
		} 
		else {
			Player.that.combine();
		}
		
		Player.isSplit = !Player.isSplit;
	}

	public void FreezePlayer(float freezeTime){
		freezeTimer = freezeTime;
	}
}
