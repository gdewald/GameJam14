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
	
	
		string num;// = ((Game.numKilled < 10) ? "0" : "") + Game.numKilled;

		if(Game.numKilled < 100) {
			num = "00";
		}
		else if(Game.numKilled < 10){
			num = "0";
		}
		else num="";
		num += Game.numKilled;

		GUI.Label(new Rect(Screen.width - 114, 5, 100, 100), GameClock.strTime, style);
		
		GUI.Label(new Rect(Screen.width - 168, 21, 100, 100), "# Killed: " + num, style);
		GUI.Label(new Rect(Screen.width - 135, 37, 100, 100), "Round:   " + GameLogic.roundNumber, style);
		GUI.Label(new Rect(Screen.width - 124, 53, 100, 100), "Wave:   " + GameLogic.waveNumber, style);
		GUI.Label (new Rect (0, 5, 100, 100), "Lives: " + Player.life, style);
	}
}
