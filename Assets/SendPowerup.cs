using UnityEngine;
using System.Collections;

public class SendPowerup : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Target") {
			col.gameObject.GetComponent("Powerup").SendMessage("RecvPowerup", this.gameObject);
		}
	}
}
