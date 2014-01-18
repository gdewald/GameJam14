using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public Font font;

	GUIStyle style = new GUIStyle();

	void Start(){
		//style.fontStyle;
		style.fontSize = 12;
		style.font = font; 
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 100, 20), "Working Title", style);
	}
}
