using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public Font font;

	GUIStyle style = new GUIStyle();
	GUIStyle bg = new GUIStyle();

	void Start(){
		style.normal.textColor = "#fff";
		style.fontSize = 12;
		style.font = font;
	}

	void OnGUI() {
		GUI.color = "#000";
		GUI.Box(new Rect(0, 0, 300, 300));
		GUI.Label(new Rect(10, 10, 100, 20), "Working Title", style);
	}
}
