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
		string num = ((Game.numKilled < 10) ? "0" : "") + Game.numKilled;

		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = 11;

		GUI.Label (new Rect (Screen.width - 94, 5, 100, 100), GameClock.strTime, style);
		GUI.Label (new Rect (Screen.width - 138, 21, 100, 100), "# Killed: " + num, style);
		GUI.Label (new Rect (Screen.width - 105, 37, 100, 100), "Round:  " + GameLogic.roundNumber, style);
		GUI.Label (new Rect (Screen.width - 94, 53, 100, 100), "Wave:  " + GameLogic.waveNumber, style);
		GUI.Label (new Rect (0, 5, 100, 100), "Lives: " + Player.life, style);

		if (Time.timeScale == 0.0f) {
			style.fontSize = 33;
			style.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect ((Screen.width * 0.5f) - 75, (Screen.height * 0.5f - 50), 150, 100), "GAME OVER", style); 
			style.fontSize = 22;
			GUI.Label (new Rect((Screen.width  * 0.5f) - 75, (Screen.height * 0.5f + 100), 150, 50), "Press START for menu", style);

			if(Input.GetButtonDown("Start") || Input.GetKeyDown (KeyCode.Return)){
				Application.LoadLevel ("title");
			}
		}
	}
}
