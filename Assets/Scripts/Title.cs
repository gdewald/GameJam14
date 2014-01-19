using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public Font font;
	public Texture border;

	public GUIStyle headingStyle = new GUIStyle();
	public GUIStyle textStyle;
	public GUIStyle buttonStyle;
	public GUIStyle subheadingStyle;
	
	public enum TitleState { ENTER, SELECT, INSTRUCTIONS };
	public TitleState titleState = TitleState.ENTER;
	
	void Start(){
		headingStyle.normal.textColor = Color.white;
		headingStyle.font = font;
		headingStyle.fontSize = 36;
		headingStyle.alignment = TextAnchor.MiddleCenter;
		headingStyle.wordWrap = true;
		subheadingStyle = new GUIStyle(headingStyle);
		subheadingStyle.fontSize = 20;		
		textStyle = new GUIStyle(headingStyle);
		textStyle.fontSize = 14;
		textStyle.alignment = TextAnchor.UpperLeft;
		buttonStyle = new GUIStyle(headingStyle);
		buttonStyle.fontSize = 14;
		
		border = Resources.Load ("Images/msg_box") as Texture;
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Resources.Load ("Images/grid") as Texture, ScaleMode.ScaleAndCrop);
		switch (titleState) {
			case TitleState.ENTER:

			GUI.TextArea(new Rect(Screen.width/2 - 500, Screen.height/4 - 50, 1000, 100), "Divide & Conquer", headingStyle);
	

			GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height/2, 400, 100), "Press Start", subheadingStyle);
			
			if(Input.GetKeyUp(KeyCode.Return) || Input.GetButtonUp("Start")){
				titleState = TitleState.SELECT;				
				return;
			}
			
			break;
		case TitleState.SELECT:
			GUI.TextArea(new Rect(Screen.width/2 - 500, Screen.height/4 - 50, 1000, 100), "Divide & Conquer", headingStyle);

			
			if(GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 - 50, 100, 25), "Start", buttonStyle)) {
				Application.LoadLevel("main");
				return;
			}
			if(GUI.Button (new Rect(Screen.width/2 - 100, Screen.height/2, 200, 25), "Instructions", buttonStyle)) {
				titleState = TitleState.INSTRUCTIONS;
				return;
			}
			if(GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 + 50, 100, 25), "Quit", buttonStyle)) {
				Application.Quit();
				return;
			}
			
						
			if(Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start")){
				Application.LoadLevel("main");
				return;
			}
			
			break;
		case TitleState.INSTRUCTIONS:
			if(GUI.Button(new Rect(Screen.width/2 - 50, Screen.height - 50, 100, 25), "Back", buttonStyle)) {
				titleState = TitleState.SELECT;
				return;
			}
			
			GUI.TextArea(new Rect(Screen.width/2 - 500, Screen.height/8 - 50, 1000, 100), "Divide & Conquer", headingStyle);
			
			//Skills
			GUI.TextArea(new Rect(Screen.width/4 - 150, Screen.height/6, 300, 100), "Skills", subheadingStyle);
			GUI.TextArea(new Rect(Screen.width/4, Screen.height/6 + 100, 600, 300), "Use B to switch forms\n\nCombined form: gun\n\n\n\nSplit form: powerchain", textStyle);
			
			
			//Enemies
			GUI.TextArea(new Rect(Screen.width/4 - 150, 4*Screen.height/10, 300, 100), "Enemies", subheadingStyle);
			
			GUI.TextArea(new Rect(Screen.width/4, 4*Screen.height/10 + 100, 1000, 100), "Regular enemy: shoot\n\n\n\nLarge enemy: split with powerchain, then shoot", textStyle);
				
			
			
			
			//Powerups
			GUI.TextArea(new Rect(Screen.width/4 - 150, 2*Screen.height/3, 300, 100), "Powerups", subheadingStyle);
			
			GUI.TextArea(new Rect(Screen.width/4, 2*Screen.height/3 + 100, 1000, 100), "Speed Powerup\n\n\n\nFirechain Powerup: deadly powerchain\n\n\n\nShield Powerup: force field\n\n\n\nSplitfire powerup: split gunfire\t", textStyle);
			
			break;
		default:
			
			break;
		}
	}
	
	
}
