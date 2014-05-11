using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	// TO DO: Perhaps organize variables by game once combined

	// Denotes the game currently being played for use with buttons, logic, etc
	private string which;
	public bool started;
	public GameObject basketball;
	public GameObject whackAMole;
	public GameObject highStriker;
	public GameObject shooter;
	public GameObject basketballBall;
	private basketballLogic basketballScript;
	private loadStatusBar strikerScript;
	public GameObject shooterGun;
	public GameObject basketballTarget;



	// Use this for initialization
	void Start () {
//		BasketballObjects.SetActive(false);
		basketball.SetActive (false);
		basketballBall.SetActive (false);
		highStriker.SetActive (false);
		shooter.SetActive (false);
		shooterGun.SetActive (false);
		whackAMole.SetActive (false);
		started = false;
//
//		Ball.SetActive(false);
//		Physics.gravity *= 75;
		basketballScript = (basketballLogic) basketball.GetComponent(typeof(basketballLogic));
		strikerScript = (loadStatusBar)highStriker.GetComponent (typeof(loadStatusBar));

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {

		if(!started) {
			// TO DO: show options for free mode and challenge mode
			// Should buttons be placed in center of screen?

			if(GUI.Button (new Rect (0,0,200,100), "BASKETBALL")) {
				basketball.SetActive (true);
				basketballBall.SetActive (true);
				basketballScript = (basketballLogic) basketball.GetComponent(typeof(basketballLogic));
				basketballScript.basketball = true;
				started = true;
			}

			if(GUI.Button (new Rect (Screen.width-200,0,200,100), "WHACK-A-MOLE")) {
				whackAMole.SetActive (true);
				started = true;
			}

			if(GUI.Button (new Rect (Screen.width-200,Screen.height-100,200,100), "SHOOTER")) {
				shooter.SetActive (true);
				shooterGun.SetActive(true);
				started = true;
			}

			if(GUI.Button (new Rect (0,Screen.height-100,200,100), "HIGH STRIKER")) {

				highStriker.SetActive (true);
				started = true;
			}
		}
	}
}