using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrintMessage : MonoBehaviour {
	public Font font;
	public GUIStyle style = new GUIStyle();
	public GUITexture msg_box;
	
	Queue<string> messageQueue;
	Queue<float> messageTimerQueue;
	
	string message;
	float timer;

	// Use this for initialization
	void Start () {
		messageQueue = new Queue<string>();
		messageTimerQueue = new Queue<float>();
		
		style.normal.textColor = Color.black;
		style.font = font;
		style.fontSize = 14;
		style.wordWrap = true;
		style.alignment = TextAnchor.MiddleCenter;
		
		this.enabled = false;
		}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer-= Time.deltaTime;
		if(timer > 0) return;
		if(messageQueue.Count > 0) {
			message = messageQueue.Dequeue();
			timer = messageTimerQueue.Dequeue();
		}
		else this.enabled = false;
	}
	
	public void printMessage(string message_in, float time = 5.0f) {
		if(this.enabled){
			messageQueue.Enqueue(message_in);
			messageTimerQueue.Enqueue(time);
			return;
		}
		
		message = message_in;
		timer = time;
		this.enabled = true;
	}
	
	void OnGUI() {
		GUI.DrawTexture(new Rect(Screen.width/2 - 220, Screen.height - 80, 440, 80), msg_box.texture);
		GUI.Label(new Rect(Screen.width/2 - 200, Screen.height - 70, 400, 60), message, style);
	}	
}
