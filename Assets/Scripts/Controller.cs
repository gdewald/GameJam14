using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public bool canShoot;

	public float maxSpeed;
	public float shotDelay;

	private float shotTimer;
	
	public string movementAxis = "LeftStick";
	public string aimAxis = "RightStick";
	

	void Start () {
		shotTimer = 0.0f;
		Targets.objects.Add (this.gameObject);
		Resources.Load ("Bullet");
	}

	void FixedUpdate () {
		float leftH = Input.GetAxis (movementAxis + "X");
		float leftV = Input.GetAxis (movementAxis + "Y");

		Vector2 leftStick = new Vector2 (leftH, leftV);
		rigidbody2D.velocity = leftStick * maxSpeed;

		/*
		// Change direction of player???
		if (vec.magnitude > 0.5f)
		{
			float curAngle = transform.eulerAngles.z;
			float stickAngle = Mathf.Atan2 (v, h);
			if(stickAngle > curAngle && stickAngle - curAngle > Mathf.PI)
				stickAngle -= 2.0f*Mathf.PI;
			if(stickAngle < curAngle && curAngle - stickAngle > Mathf.PI)
				stickAngle += 2.0f*Mathf.PI;
			float newAngle = 0.5f*curAngle + 0.5f*stickAngle;

			transform.eulerAngles = new Vector3(0.0f, 0.0f, newAngle);
		}
		*/

		// Try to shoot
		
		float rightH = Input.GetAxis (aimAxis + "X");
		float rightV = Input.GetAxis (aimAxis + "Y");
		Vector2 rightStick = new Vector2 (rightH, rightV);
		if(canShoot && rightStick.magnitude >= 0.25f)
			Shoot (rightStick);

			

		if(rigidbody2D.velocity.magnitude > .1f)
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0,  -Mathf.Atan2(leftH, leftV) * Mathf.Rad2Deg + 90), Time.deltaTime * rigidbody2D.velocity.magnitude)	;



		// toggle split
		if(Input.GetButtonDown("ToggleSplit")){
			toggleSplit();
		}

	}

	void Shoot(Vector2 direction)
	{
		if (shotTimer > 0.0f) {
			shotTimer -= Time.deltaTime;
		}

		if (shotTimer <= 0.0f) {
			shotTimer = shotDelay;

			Vector3 pos = transform.position;

			GameObject bullet = (GameObject) Instantiate(Resources.Load("Bullet"), pos, Quaternion.identity) ;
			bullet.rigidbody2D.velocity = direction;
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
}
