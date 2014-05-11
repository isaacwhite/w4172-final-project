using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	// TO DO: Perhaps organize variables by game once combined

	// Denotes the game currently being played for use with buttons, logic, etc
	bool basketball = false;
	bool mole = false;
	bool hammer = false;
	bool shooting = false;
	bool started = false;
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

	// Basketball variables
	public int ballsLeft;
	public int points;

	float timer;

	// Use this for initialization
	void Start () {
		BasketballObjects.SetActive(false);

		Ball.SetActive(false);
		Physics.gravity *= 75;

	}
	
	// Update is called once per frame
	void Update () {

		// playing basketball game
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

		if(!started) {

			// TO DO: show options for free mode and challenge mode
			// Should buttons be placed in center of screen?

			if(GUI.Button (new Rect (0,0,200,100), "BASKETBALL")) {
				basketball = true;
				started = true;
			}

			if(GUI.Button (new Rect (Screen.width-200,0,200,100), "WHACK-A-MOLE")) {
				mole = true;
				started = true;
			}

			if(GUI.Button (new Rect (Screen.width-200,Screen.height-100,200,100), "SHOOTER")) {
				hammer = true;
				started = true;
			}

			if(GUI.Button (new Rect (0,Screen.height-100,200,100), "HIGH STRIKER")) {
				shooting = true;
				started = true;
			}
		}
	
		if (basketball) {
			BasketballObjects.SetActive(true);
			Ball.SetActive(true);

			// Start game, reset values
			if(!playing1 && !gameOver1 && !checking1) {
				if(GUI.Button (new Rect ((Screen.width/4),(Screen.height/4),(Screen.width/2),(Screen.height/2)), "START GAME")) {
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
				if (GUI.Button(new Rect(Screen.width-200,Screen.height-100,200,100), ballTexture)) {
					if(ballsLeft >= 0) {
						ballsLeft--;
						Rigidbody ballClone;
						ballClone = Instantiate (Ball.rigidbody, (Ball.transform.position + (new Vector3(0,0,450))), Ball.transform.rotation) as Rigidbody;
						ballClone.transform.parent = ARCamera.transform;
						ballClone.constraints = RigidbodyConstraints.None;
						//ballClone.AddTorque(Vector3.up * 100);
						//ballClone.AddTorque(Vector3.right * 100);
						ballClone.velocity += Vector3.forward*1400;
						ballClone.velocity += Vector3.up*600;
						ballClone.useGravity = true;
						ballClone.transform.parent = BasketballObjects.transform;
					}
				}

				// end game button
				if (GUI.Button(new Rect(0,Screen.height-100,200,100), "END GAME")) {
					checking1 = true;
					playing1 = false;
				}

			}

			// confirm user exit
			if(checking1) {
				
				GUI.Box(new Rect((Screen.width/4),(Screen.height/4),(Screen.width/2),(Screen.height/4)), endGame);
				
				// Continue game
				if(GUI.Button (new Rect ((Screen.width/4),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "NO")) {
					playing1 = true;
					checking1 = false;
				}
				
				// End game, show results
				if(GUI.Button (new Rect ((Screen.width/2),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "YES")) {
					timer = 0;
					gameOver1 = true;
					checking1 = false;
				}
			}
			
			if(gameOver1) {

				// POINT TOTALS 
				GUI.Box(new Rect((Screen.width/4),(Screen.height/4),(Screen.width/2),(Screen.height/4)), "Final Score: " + points.ToString());

				// RETRY
				if(GUI.Button (new Rect ((Screen.width/4),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "RETRY")) {
					points = 0;
					timer = 30;
					ballsLeft = 10;
					playing1 = true;
					gameOver1 = false;
				}

				// EXIT GAME, GO HOME
				if(GUI.Button (new Rect ((Screen.width/2),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "HOME")) {
					started = false;
					gameOver1 = false;
					basketball = false;
					BasketballObjects.SetActive(false);
					Ball.SetActive(false);
				}
			}
		}
		
		if (mole) {

			// set gameobjects active
			
		}

		if (hammer) {

			// set gameobjects active
		
		}

		if (shooting) {
	
			// set gameobjects active

		}
	}
}