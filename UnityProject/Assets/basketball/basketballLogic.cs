using UnityEngine;
using System.Collections;

public class basketballLogic : MonoBehaviour {

	public bool basketball = false;
	bool playing1 = false;
	bool gameOver1 = false;
	bool checking1 = false;
	
	string endGame = "Are you sure you want to end game?";
	
	// Game Objects and Prefabs
	public GameObject Carnival;
	public GameObject Hoop;
	public GameObject Rim;
	public GameObject Ball;
	public GameObject BasketballObjects; // holder for game objects
	public Texture ballTexture; // texture for ball button
	public GameLogic ARCamera;
	public AudioClip spaceJam;
	private AudioSource speaker;
	public GameObject arrows;
	
	// Basketball variables
	public int ballsLeft;
	public int points;
	
	float timer;
	
	// Use this for initialization
	void Start () {
		Physics.gravity *= 75;
		speaker = (AudioSource) gameObject.GetComponent (typeof(AudioSource));

	}
	// Update is called once per frame
	void Update () {
		if(playing1) {
			timer -= Time.deltaTime;
			if(timer <= 0) {
				timer = 0;
				playing1 = false;
				gameOver1 = true;
			}
			
		}
	}

	void OnGUI () {
		GUIStyle button_text = new GUIStyle ("button");
		button_text.fontSize = 40;
		if (basketball) {
			// Start game, reset values
			if(!playing1 && !gameOver1 && !checking1) {

				if(GUI.Button (new Rect ((Screen.width/4),(Screen.height/4),(Screen.width/2),(Screen.height/2)), "START GAME",button_text)) {
					speaker.PlayOneShot (spaceJam);
					arrows.SetActive (false);
					points = 0;
					ballsLeft = 10;
					timer = 30;
					playing1 = true;
				}
			}
			
			if(playing1) {
				// Display remaining time on screen
				GUI.Box(new Rect(0, 0, 100, 50), "Time: " + timer.ToString("0"));
				
				// Display score on screen
				GUI.Box(new Rect(Screen.width - 100, 0, 100, 50), "Score: " + points.ToString());
				
				// Display shots left
				GUI.Box(new Rect((Screen.width/2) - 50, 0, 100, 50), "Balls: " + ballsLeft.ToString());
				
				// TO DO: increment points when ball collides with rim
				// points++;
				
				// press button to shoot ball
				if (GUI.Button(new Rect(Screen.width-200,Screen.height-100,200,100), ballTexture,button_text)) {
					if(ballsLeft > 0) {
						ballsLeft--;
						Rigidbody ballClone;
						ballClone = Instantiate (Ball.rigidbody, (Ball.transform.position + (new Vector3(0,0,450))), Ball.transform.rotation) as Rigidbody;
						ballClone.constraints = RigidbodyConstraints.None;

						ballClone.velocity += Vector3.forward*1400;
						ballClone.velocity += Vector3.up*600;
						ballClone.useGravity = true;
						ballClone.transform.parent = gameObject.transform;
					}
				}
				
				// end game button
				if (GUI.Button(new Rect(0,Screen.height-100,200,100), "EXIT",button_text)) {
					checking1 = true;
					playing1 = false;
				}
				
			}
			
			// confirm user exit
			if(checking1) {
				
				GUI.Box(new Rect((Screen.width/4),(Screen.height/4),(Screen.width/2),(Screen.height/4)), endGame);
				
				// Continue game
				if(GUI.Button (new Rect ((Screen.width/4),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "NO",button_text)) {
					playing1 = true;
					checking1 = false;
				}
				
				// End game, show results
				if(GUI.Button (new Rect ((Screen.width/2),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "YES",button_text)) {
					timer = 0;
					gameOver1 = true;
					checking1 = false;
				}
			}
			
			if(gameOver1) {
				
				// POINT TOTALS 
				GUI.Box(new Rect((Screen.width/4),(Screen.height/4),(Screen.width/2),(Screen.height/4)), "Final Score: " + points.ToString());
				
				// RETRY
				if(GUI.Button (new Rect ((Screen.width/4),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "RETRY",button_text)) {
					points = 0;
					timer = 30;
					ballsLeft = 10;
					playing1 = true;
					gameOver1 = false;
				}
				
				// EXIT GAME, GO HOME
				if(GUI.Button (new Rect ((Screen.width/2),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "HOME",button_text)) {
					ARCamera.started = false;
					gameOver1 = false;
					basketball = false;
					BasketballObjects.SetActive(false);
					Ball.SetActive(false);
				}
			}
		}
	}
}
