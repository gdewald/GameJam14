using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateChain : MonoBehaviour {
	public GameObject from, to;
	public float offset = 0.5f;
	
	public GameObject linkPrefab;
	public const float min_link_len = .1f;
	public const float min_link_width = .1f;
	
	public string keyName = "Fire2";
	
	GameObject chain = null;

	void Awake(){
		if(from == null){
			from = this.gameObject;
		}
	}

	void Start () {
		//Rigidbody2D body = from.AddComponent<Rigidbody2D>();
		//body.isKinematic = true;
		to.AddComponent<HingeJoint2D>();
		to.GetComponent<Rigidbody2D>().isKinematic = true;
	
	}

	void Update () {
		if(Input.GetButtonDown(keyName))
			ToggleChain();		
	}
	
	void ToggleChain() {
		if(chain == null) {
			CreateLinks();
		}
		else Destroy(chain);
	}
	
	void CreateLinks() {
		Vector3 pos_from = from.transform.position;
		Vector3 pos_to = to.transform.position;
		Vector3 v1 = (pos_to - pos_from).normalized;
		pos_from += v1 * offset;
		pos_to -= v1*offset;
		
		float dist = Vector3.Distance(pos_from,pos_to);
		
		int num_links = Mathf.FloorToInt(dist/min_link_len);
		float link_len = dist/num_links;
		
		chain= new GameObject("Chain");

		GameObject link = null;
		for(int i = 0; i < num_links; i++) {
			GameObject temp = link;
			link = 	Instantiate(linkPrefab) as GameObject;
			link.transform.parent = chain.transform;
			HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
			if(temp != null)
				joint.connectedBody = temp.rigidbody2D;
			else joint.connectedBody = from.rigidbody2D;
			
			link.AddComponent<BoxCollider2D>();
			
			link.transform.localScale = new Vector3(link_len,min_link_width,1);	
			link.transform.position = pos_from;
			link.transform.rotation = Quaternion.AngleAxis(-Vector3.Angle(Vector3.right, v1), new Vector3(0.0f,0.0f,1.0f));
			//GameObject.Instantiate(link, pos_from, Quaternion.AngleAxis(-Vector3.Angle(Vector3.right, v1), new Vector3(0.0f,0.0f,1.0f)));

			pos_from += v1 * link_len;
		}
		
		HingeJoint2D h = link.AddComponent<HingeJoint2D>();
		h.connectedBody = to.rigidbody2D;
		//link.AddComponent<Rigidbody2D>();
		//to.AddComponent<HingeJoint2D>();
		//to.GetComponent<HingeJoint2D>().connectedBody = link.rigidbody2D;
	}
	
	void ChainSetEnd(GameObject end) {
		to = end;
	}
}
