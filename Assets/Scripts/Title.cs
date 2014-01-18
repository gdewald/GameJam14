using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public Font font;

	GUIStyle style = new GUIStyle();
	GUIStyle bg = new GUIStyle();

	void Start(){
		style.normal.textColor = Color.white;
		style.font = font;
	}

	void OnGUI() {
		style.fontSize = 36;
		GUI.Label(new Rect(Screen.width/2 - 214, Screen.height/4, 100, 100), "Working Title", style);

		style.fontSize = 14;
		GUI.Label(new Rect(Screen.width/2 - 78, Screen.height/2, 100, 100), "Press Enter", style);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Return)){
			Application.LoadLevel("main");
		}
	}
}
