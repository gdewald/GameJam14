using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {

	public Font font;
	GUIStyle style = new GUIStyle();
	
	void Start(){
		style.normal.textColor = Color.white;
		style.font = font;
		style.fontSize = 11;
	}
	
	void OnGUI() {
		GUI.Label(new Rect(Screen.width - 95, 5, 100, 100), GameClock.strTime, style);
		GUI.Label(new Rect(Screen.width - 127, 21, 100, 100), "# Killed: " + Game.numKilled, style);
		GUI.Label(new Rect(Screen.width - 95, 37, 100, 100), "Round: " + Game.numKilled, style);
		GUI.Label(new Rect(Screen.width - 84, 53, 100, 100), "Wave: " + Game.numKilled, style);
	}
}
