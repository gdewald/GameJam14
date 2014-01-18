using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {

	public Font font;
	GUIStyle style = new GUIStyle();
	
	void Start(){
		style.normal.textColor = Color.white;
		style.font = font;
		style.fontSize = 14;
	}
	
	void OnGUI() {
		GUI.Label(new Rect(Screen.width - 178, 8, 100, 100), GameClock.strTime, style);
		GUI.Label(new Rect(Screen.width - 178, 38, 100, 100), "# Killed: " + Game.numKilled, style);
	}
}
