using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float maxSpeed;
	public float shotDelay;

	private float shotTimer;

	void Start () {
		shotTimer = 0.0f;
		Targets.objects.Add (this.gameObject);
		Resources.Load ("Bullet");
	}

	void Update () {
		float leftH = Input.GetAxis ("LeftStickX");
		float leftV = Input.GetAxis ("LeftStickY");

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
		
		float rightH = Input.GetAxis ("RightStickX");
		float rightV = Input.GetAxis ("RightStickY");
		Vector2 rightStick = new Vector2 (rightH, rightV);
		if(rightStick.magnitude >= 0.25f)
			Shoot (rightStick);
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

			//Rigidbody2D bullet = Instantiate(Resources.Load("Bullet"), pos, Quaternion.identity) as Rigidbody2D;
			//bullet.velocity = direction * BulletCollide.speed;
		}
	}
}
