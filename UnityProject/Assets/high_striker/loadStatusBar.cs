using UnityEngine;
using System.Collections;

public class loadStatusBar : MonoBehaviour {
	
	private float progress;
	private float multiplier;
	private int nextPress;
	private float startTime;
	private int restSeconds;
	private int roundedRestSeconds;
	private int displaySeconds;
	private int displayMinutes;
	private int countDownSeconds;
	private GameObject dinger;
	private GameObject hammer;
	private bool hasStarted;
	private bool isPaused;
	private int timer1;
	private int timer2;
	private string text;
	private float xScale;
	private float zScale;
	private float xPos;
	private float yPos;
	private float zPos;
	public AudioClip bell;
	private AudioSource speaker;
	private string resultText;
	public GameObject ARCamera;
	public GameObject arrows;
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
		text = string.Format ("{0:00}:{1:00}", 0, 0); 
		restSeconds = 10;
		xScale = dinger.transform.localScale.x;
		zScale = dinger.transform.localScale.z;
		xPos = dinger.transform.localPosition.x;
		yPos = dinger.transform.localPosition.y;
		zPos = dinger.transform.localPosition.z;
		speaker = (AudioSource) gameObject.GetComponent (typeof(AudioSource));
	}
		
	// Update is called once per frame
	void Update () {
		if (hasStarted && !isPaused) {
						progress = multiplier * 0.1f;
						if (0 < progress && progress < 1) {
								multiplier = multiplier - multiplier * 0.005f;
						}
						if (progress > .75) {
								dinger.renderer.material.color = Color.green;
						} else if (progress > .4) {
								dinger.renderer.material.color = Color.blue;
						} else {
								dinger.renderer.material.color = Color.gray;
			}
			dinger.transform.localScale = new Vector3 (xScale, 6.8f * progress, zScale);
			dinger.transform.localPosition = new Vector3 (xPos, yPos, zPos  - (6.8f * progress / 2));
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

				GUIStyle customButton = new GUIStyle ("button");
				customButton.fontSize = 60;

				GUIStyle customText = new GUIStyle ();
				customText.fontSize = 60;
		
				GUIStyle winText = new GUIStyle ();
				winText.fontSize = 100;
				winText.normal.textColor = Color.gray;

				GUI.enabled = true;
				if (!hasStarted) {
						if (GUI.Button (new Rect ((Screen.width / 2) - 180, (Screen.height / 2) - 100, 360, 200), "Start!", customButton)) {
								arrows.SetActive (false);
								startTime = Time.time;
								hasStarted = true;
								GUI.enabled = true;
								isPaused = false;
						}
						GUI.enabled = false;
				} 
		else {
			if (!isPaused) {
				float guiTime = Time.time - startTime;
				restSeconds = Mathf.RoundToInt (countDownSeconds - (guiTime));
				
				roundedRestSeconds = Mathf.CeilToInt (restSeconds);
				displaySeconds = roundedRestSeconds % 60;
				displayMinutes = roundedRestSeconds / 60; 
				
				text = string.Format ("{0:00}:{1:00}", displayMinutes, displaySeconds); 
			}

			if (restSeconds <= 3) {
				customText.normal.textColor = Color.red;
			}

			if ((restSeconds <= 0) || (progress >= 1)) {
				if(progress>=1) {
					progress = 1;
					if(!isPaused){
						speaker.PlayOneShot (bell);
					}
					resultText="Congrats! You won!";
				}
				if(restSeconds <=0) {
					restSeconds = 0;
					resultText="Boo, you lost!";
				}
				// POINT TOTALS 
				GUIStyle button_text = new GUIStyle ("button");
				button_text.fontSize = 40;
				GUI.Box(new Rect((Screen.width/4),(Screen.height/4),(Screen.width/2),(Screen.height/4)), resultText, button_text);

				isPaused = true;
				GUI.enabled = true;
				if(GUI.Button (new Rect ((Screen.width/4),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "RETRY", button_text)) {
					startTime = Time.time;
					GUI.enabled = true;
					progress = 0;
					multiplier = 0;
					nextPress = 0;
					dinger.renderer.material.color = Color.gray;
					customText.normal.textColor = Color.black;
					hammer.transform.localRotation = Quaternion.AngleAxis (0, Vector3.up);
					isPaused = false;

				}
				// EXIT GAME, GO HOME
				if(GUI.Button (new Rect ((Screen.width/2),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "HOME", button_text)) {
					//started = false;
					//gameOver1 = false;
					gameObject.SetActive (false);
					GameLogic master = (GameLogic) ARCamera.GetComponent(typeof(GameLogic));
					master.started = false;
				}

				GUI.enabled = false;
			}

			if (GUI.Button (new Rect (20, Screen.height - 220, 360, 200), "Left", customButton)) {
				if (timer1 == 0 && timer2 == 0)
					timer1 = timer2 = 10;
				if (nextPress == 0) {
					multiplier++;
				} else {
					if (multiplier > 0)
						multiplier = multiplier - 0.05f;
					else
						multiplier = 0;
				}
				nextPress = 1;
			}
			
			if (GUI.Button (new Rect (Screen.width - 380, Screen.height - 220, 360, 200), "Right", customButton)) {
				if (timer1 == 0 && timer2 == 0)
					timer1 = timer2 = 10;	
				if (nextPress == 1) {
					multiplier++;
				} else {
					if (multiplier > 0)
						multiplier = multiplier - 0.05f;
					else {
						multiplier = 0;
					}
				}
				nextPress = 0;
			}
			GUI.Label (new Rect (180, 0, 100, 30), text, customText);
			GUI.Label (new Rect (0, 0, 160, 50), "Timer:", customText);
		}
	}
	
	
	
}
