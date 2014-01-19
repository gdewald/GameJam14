using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {

	public Font font;
	public static Hud that;
	GUIStyle style = new GUIStyle();
	
	void Start(){
		style.normal.textColor = Color.white;
		style.font = font;
		style.fontSize = 11;

		that = this;
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
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = 11;

		GUI.Label(new Rect(Screen.width - 114, 5, 100, 100), GameClock.strTime, style);
		
		GUI.Label(new Rect(Screen.width - 168, 21, 100, 100), "# Killed: " + num, style);
		GUI.Label(new Rect(Screen.width - 135, 37, 100, 100), "Round:   " + GameLogic.roundNumber, style);
		GUI.Label(new Rect(Screen.width - 124, 53, 100, 100), "Wave:   " + GameLogic.waveNumber, style);
		GUI.Label (new Rect (0, 5, 100, 100), "Lives: " + Player.life, style);

		if (Time.timeScale == 0.0f) {
			style.fontSize = 33;
			style.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect ((Screen.width * 0.5f) - 75, (Screen.height * 0.5f - 50), 150, 100), "GAME OVER", style); 
			style.fontSize = 22;
			GUI.Label (new Rect((Screen.width  * 0.5f) - 75, (Screen.height * 0.5f + 100), 150, 50), "Press SPACE for menu", style);

			if(Input.GetButtonDown("ToggleSplit")){
				Application.LoadLevel ("title");
			}
		}
	}
}
