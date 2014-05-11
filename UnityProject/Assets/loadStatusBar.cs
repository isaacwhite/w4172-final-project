using UnityEngine;
using System.Collections;

public class loadStatusBar : MonoBehaviour {
	
	float progress;
	float multiplier;
	int nextPress;
	float startTime;
	int restSeconds;
	int roundedRestSeconds;
	int displaySeconds;
	int displayMinutes;
	int countDownSeconds;
	GameObject dinger;
	GameObject hammer;
	bool hasStarted;
	int timer1;
	int timer2;

	void Awake() {
		Time.timeScale=0;
	}
	
	// Use this for initialization
	void Start () {
		progress = 0;
		multiplier = 0;
		//left = 0; right = 1; default 0
		nextPress = 0;
		timer1 = 0;
		timer2 = 0;
		countDownSeconds = 10;
		dinger = GameObject.Find ("Dinger");
		hammer = GameObject.Find ("strikeHammer");
		dinger.renderer.material.color = Color.gray;
		//false = game not started, true = game started;
		hasStarted = false;
	}
		
	// Update is called once per frame
	void Update () {
		if (hasStarted) {
						progress = multiplier * 0.075f;
						if (0 < progress && progress < 1) {
								multiplier = multiplier - multiplier * 0.005f;
						}
						if (progress > .75) {
								dinger.renderer.material.color = Color.green;
						} else if (progress > .4) {
								dinger.renderer.material.color = Color.blue;
						} else
								dinger.renderer.material.color = Color.gray;

			dinger.transform.localScale = new Vector3 (0.01554071f, 0.70f * progress, 0.0003035038f);
			dinger.transform.localPosition = new Vector3 (-23.03405f, 0.2920714f - (0.65f * progress / 2), 0.0344496f);
			if(timer1!=0 && hammer.transform.localRotation.y<90) {
				hammer.transform.Rotate(Vector3.up, 360 * Time.deltaTime);
				timer1--;
			}
			if(timer1==0 && timer2!=0&& hammer.transform.localRotation.y<90) {
				hammer.transform.Rotate(Vector3.up, -360 * Time.deltaTime);
				timer2--;
			}
		}
	}

	void OnGUI () {

		GUIStyle customButton = new GUIStyle("button");
		customButton.fontSize = 60;

		GUIStyle customText = new GUIStyle();
		customText.fontSize = 60;

		GUIStyle winText = new GUIStyle();
		winText.fontSize = 100;
		winText.normal.textColor = Color.gray;
		GUI.enabled = true;
		if (!hasStarted) {
						if (GUI.Button (new Rect ((Screen.width / 2) - 180, (Screen.height / 2) - 100, 360, 200), "Start!", customButton)) {
								startTime = Time.time;
								Time.timeScale = 1;
								hasStarted = true;
								GUI.enabled = true;
						}
						GUI.enabled = false;
		}

			float guiTime = Time.time - startTime;
			restSeconds = Mathf.RoundToInt(countDownSeconds - (guiTime));

			if (restSeconds <= 0) {
				restSeconds = 0;
				//do stuff here
				Time.timeScale=0;
				GUI.enabled = true;
				GUI.Label (new Rect ((Screen.width/2)-50,(Screen.height/2)-50,100,100), "BOO YOU LOSE!!!", winText);
				if (GUI.Button (new Rect (Screen.width-380, 20, 360, 200), "Replay", customButton)) {
					startTime = Time.time;
					GUI.enabled = true;
					progress = 0;
					multiplier = 0;
					nextPress = 0;
					dinger.renderer.material.color = Color.gray;
					customText.normal.textColor = Color.black;
					hammer.transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
				Time.timeScale=1;
					
				}
				GUI.enabled = false;
			}
			if (progress >= 1) {
				progress = 1;
				//do something for winning
				Time.timeScale=0;
				GUI.enabled = true;
				GUI.Label (new Rect ((Screen.width/2)-50,(Screen.height/2)-50,100,100), "YAY YOU WIN!!!", winText);
				if (GUI.Button (new Rect (Screen.width-380, 20, 360, 200), "Replay", customButton)) {
					startTime = Time.time;
					GUI.enabled = true;
					progress = 0;
					multiplier = 0;
					nextPress = 0;
					dinger.renderer.material.color = Color.gray;
					customText.normal.textColor = Color.black;
				hammer.transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
					
				Time.timeScale=1;
				}
				GUI.enabled = false;
			}
			
			if (restSeconds <= 3) {
				customText.normal.textColor = Color.red;
			}



			roundedRestSeconds = Mathf.CeilToInt(restSeconds);
			displaySeconds = roundedRestSeconds % 60;
			displayMinutes = roundedRestSeconds / 60; 
			
			string text = string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds); 
			GUI.Label (new Rect (180, 0, 100, 30), text, customText);
			GUI.Label (new Rect (0,0,160,50), "Timer:", customText);
			if(GUI.Button(new Rect(20,Screen.height-220,360,200), "Left", customButton)) {
			if(timer1==0 && timer2==0)
				timer1 = timer2 = 10;
			if(nextPress==0)
				{
						multiplier++;
				}
				else {
					if(multiplier > 0)
						multiplier = multiplier - 0.05f;
					else multiplier=0;
				}
				nextPress=1;
			}
			
			if(GUI.Button(new Rect(Screen.width-380,Screen.height-220,360,200), "Right", customButton)) {
			if(timer1==0 && timer2==0)
				timer1 = timer2 = 10;	
			if(nextPress==1)
				{
						multiplier++;
				}
				else {
					if(multiplier > 0)
						multiplier = multiplier - 0.05f;
					else multiplier=0;
				}
					nextPress=0;
			}
	}



}
