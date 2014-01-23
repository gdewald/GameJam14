using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public Font font;
	public Texture border;

	public GUIStyle headingStyle = new GUIStyle();
	public GUIStyle textStyle;
	public GUIStyle buttonStyle;
	public GUIStyle subheadingStyle;
	GUIStyle blinkFadeStyle;
	
	public enum TitleState { ENTER, SELECT, INSTRUCTIONS };
	public TitleState titleState = TitleState.ENTER;

	bool fadeOut = true;
	float alphaCounter = 0f;

	TitleState ctaPointer = TitleState.ENTER;
	string[] cta = new string[3]{"Start", "Instructions", "Quit"};

	void Start(){
		headingStyle.normal.textColor = Color.white;
		headingStyle.font = font;
		headingStyle.fontSize = 40;
		headingStyle.alignment = TextAnchor.MiddleCenter;
		headingStyle.wordWrap = true;
		subheadingStyle = new GUIStyle(headingStyle);
		subheadingStyle.fontSize = 18;		
		blinkFadeStyle = new GUIStyle(subheadingStyle);
		textStyle = new GUIStyle(headingStyle);
		textStyle.fontSize = 14;
		textStyle.alignment = TextAnchor.UpperLeft;
		buttonStyle = new GUIStyle(headingStyle);
		buttonStyle.fontSize = 14;
		
		border = Resources.Load<Texture>("Images/msg_box");
	}

	void Update(){

		switch (titleState) {
			case TitleState.ENTER:
				// blink cta
				if(alphaCounter >= 1.5f){
					fadeOut = true;
				}
				else if(alphaCounter <= 0f){
					fadeOut = false;
				}

				if(Time.frameCount % 1 == 0){
					alphaCounter += fadeOut ? -0.028f : 0.034f;
				}

				blinkFadeStyle.normal.textColor = new Color(255, 255, 255, alphaCounter);

				break;
			case TitleState.SELECT:
//				// select cta
//				float num = Input.GetAxis("LeftStickY");
//				print (num);
//
//				if(Input.geta)
//
//				if(num < 0){
//					if(ctaPointer != TitleState.SELECT){
//						++ctaPointer;
//					}
//				}
//				else if(num > 0){
//					if(ctaPointer != TitleState.ENTER){
//						--ctaPointer;
//					}
//				}

				break;
		}
	}

	void OnGUI() {
		Time.timeScale = 1.0f;
		GameLevel.curLvl = -1;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Resources.Load<Texture>("Images/bgTitle"), ScaleMode.ScaleAndCrop);

		switch (titleState) {
			case TitleState.ENTER:

			GUI.TextArea(new Rect(Screen.width/2 - 500, Screen.height/4 - 50, 1000, 100), "Divide & Conquer", headingStyle);
			GUI.TextArea(new Rect(Screen.width/2 - 500, Screen.height/4 + 25, 1000, 100), "A game by Nicholas Dedenbach,\nGarrett Dewald, Isaiah Hines, and Jon Wiedmann", subheadingStyle);

			GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height/2, 400, 100), "Press Start", blinkFadeStyle);
			
			if(Input.GetKeyUp(KeyCode.Return) || Input.GetButtonUp("Start")){
				titleState = TitleState.SELECT;				
				return;
			}
			
			break;
		case TitleState.SELECT:
			GUI.TextArea(new Rect(Screen.width/2 - 500, Screen.height/4 - 50, 1000, 100), "Divide & Conquer", headingStyle);

			string[] c = new string[3];
			cta.CopyTo(c, 0);

//			if(ctaPointer == TitleState.ENTER){
//				c[0] = ">> " + cta[0];
//			}
//			else if(ctaPointer == TitleState.INSTRUCTIONS){
//				c[1] = ">> " + cta[1];
//			}
//			else if(ctaPointer == TitleState.SELECT){	// actually "Quit"
//				c[2] = ">> " + cta[2];
//			}

			if(GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 - 50, 100, 25), c[0], buttonStyle)) {
				Application.LoadLevel("main");
				return;
			}
			if(GUI.Button (new Rect(Screen.width/2 - 100, Screen.height/2, 200, 25), c[1], buttonStyle)) {
				titleState = TitleState.INSTRUCTIONS;
				return;
			}
			if(GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 + 50, 100, 25), c[2], buttonStyle)) {
				Application.Quit();
				return;
			}
			
						
			if(Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start")){
				Application.LoadLevel("main");
				return;
			}
			
			break;
		case TitleState.INSTRUCTIONS:
			if(GUI.Button(new Rect(Screen.width/2 - 50, Screen.height - 50, 100, 25), "Back", buttonStyle)){
				titleState = TitleState.SELECT;
				return;
			}
			
			GUI.TextArea(new Rect(Screen.width/2 - 500, Screen.height/8 - 50, 1000, 100), "Divide & Conquer", headingStyle);
			
			//Skills
			GUI.TextArea(new Rect(Screen.width/4 - 150, Screen.height/6, 300, 100), "Skills", subheadingStyle);
			GUI.TextArea(new Rect(Screen.width/4, Screen.height/6 + 100, 600, 300), "Use LB/RB to switch forms\n\nCombined form: gun\n\n\n\nSplit form: powerchain", textStyle);

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
