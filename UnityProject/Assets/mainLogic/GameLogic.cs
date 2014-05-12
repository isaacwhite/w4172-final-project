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
//	public GameObject basketballTarget;
//	public GameObject shooterTarget;
//	public GameObject highStrikerTarget;
//	public GameObject whackAMoleTarget;
//	public GameObject inter1;
//	public GameObject inter2;
//	public GameObject inter3;
//	public GameObject inter4;

	public GameObject strikerGuide;
	public GameObject shooterGuide;
	public GameObject moleGuide;
	public GameObject basketballGuide;

	private Vector3 whackAMolePos;
	private Vector3 shooterTargetPos;
	private Vector3 highStrikerPos;
	private Vector3 basketballPos;
	private Vector3 inter1Pos;
	private Vector3 inter2Pos;
	private Vector3 inter3Pos;
	private Vector3 inter4Pos;
	private Vector3 fromPoint;
	private Vector3 toPoint;
	private Vector3 destinationPoint;
	private Vector3[] wayfindingPoints;
	private GameObject guide;
	private int currentTargetCount;
	private float speed;
	// Use this for initialization
	void Start () {
		float distance = 800.0f;
//		BasketballObjects.SetActive(false);
		basketball.SetActive (false);
		basketballBall.SetActive (false);
		highStriker.SetActive (false);
		shooter.SetActive (false);
		shooterGun.SetActive (false);
		whackAMole.SetActive (false);
		moleGuide.SetActive (false);
		strikerGuide.SetActive (false);
		shooterGuide.SetActive (false);
		basketballGuide.SetActive (false);
		started = false;
		//wayfinding code
//		guide = GameObject.Find ("wayfinder");
//		whackAMolePos = whackAMoleTarget.transform.localPosition;
//		whackAMolePos = new Vector3 (whackAMolePos.x + 200.0f, whackAMolePos.y, whackAMolePos.z);
//		inter1Pos = inter1.transform.localPosition;
//		inter1Pos = new Vector3 (inter1Pos.x + 200.0f, inter1Pos.y, inter1Pos.z);
//		shooterTargetPos = whackAMoleTarget.transform.localPosition;
//		shooterTargetPos = new Vector3 (shooterTargetPos.x, shooterTargetPos.y, shooterTargetPos.z + 200.0f);
//		inter2Pos = inter2.transform.localPosition;
//		inter2Pos = new Vector3 (inter2Pos.x, inter2Pos.y, inter2Pos.z + 200.0f);
//		basketballPos = basketballTarget.transform.localPosition;
//		basketballPos = new Vector3(basketballPos.x, basketballPos.y, basketballPos.z - 200.0f);
//		inter3Pos = inter3.transform.localPosition;
//		inter3Pos = new Vector3 (inter3Pos.x, inter3Pos.y, inter3Pos.z - 200.0f);
//		highStrikerPos = highStrikerTarget.transform.localPosition;
//		highStrikerPos = new Vector3 (highStrikerPos.x - 200.0f, highStrikerPos.y, highStrikerPos.z);
//		inter4Pos = inter4.transform.localPosition;
//		inter4Pos = new Vector3 (inter4Pos.x - 200.0f, inter4Pos.y, inter4Pos.z);
//
//		wayfindingPoints = new Vector3[8];
//		wayfindingPoints [0] = whackAMolePos;
//		wayfindingPoints [1] = inter1Pos;
//		wayfindingPoints [2] = shooterTargetPos;
//		wayfindingPoints [3] = inter2Pos;
//		wayfindingPoints [4] = highStrikerPos;
//		wayfindingPoints [5] = inter3Pos;
//		wayfindingPoints [6] = basketballPos;
//		wayfindingPoints [7] = inter4Pos;
//		toPoint = wayfindingPoints [0];
////		fromPoint = wayfindingPoints [7];
//		speed = 5.0f;
//		currentTargetCount = 0;

//
//		Ball.SetActive(false);
//		Physics.gravity *= 75;
		basketballScript = (basketballLogic) basketball.GetComponent(typeof(basketballLogic));
		strikerScript = (loadStatusBar)highStriker.GetComponent (typeof(loadStatusBar));

	}
	
	// Update is called once per frame
	void Update () {
//		toPoint = wayfindingPoints [currentTargetCount];
//		if (Vector3.Distance (guide.transform.localPosition, toPoint) > 15.0f) {
//				guide.transform.localPosition = Vector3.Lerp (guide.transform.localPosition,toPoint, Time.deltaTime * speed);
//		} else {
//			print ("moving to: " + currentTargetCount.ToString ());
//				
//				if(currentTargetCount < 7) {
//					currentTargetCount++;
//				} else {
//					currentTargetCount = 0;
//				}
//		}
	}

	void OnGUI () {
		GUIStyle button_text = new GUIStyle ("button");
		button_text.fontSize = 40;
		if(!started) {
			// TO DO: show options for free mode and challenge mode
			// Should buttons be placed in center of screen?

			if(GUI.Button (new Rect (0,0,Screen.width/4,Screen.height/4), "BASKETBALL",button_text)) {
				basketballGuide.SetActive(true);
				basketball.SetActive (true);
				basketballBall.SetActive (true);
				basketballScript = (basketballLogic) basketball.GetComponent(typeof(basketballLogic));
				basketballScript.basketball = true;
				started = true;
			}

			if(GUI.Button (new Rect (Screen.width-(Screen.width/4),0,Screen.width/4,Screen.height/4), "WHACK-A-MOLE",button_text)) {
				moleGuide.SetActive (true);
				whackAMole.SetActive (true);
				started = true;
			}

			if(GUI.Button (new Rect (Screen.width-(Screen.width/4),Screen.height-(Screen.height/4),Screen.width/4,Screen.height/4), "SHOOTER",button_text)) {
				shooterGuide.SetActive (true);
				shooter.SetActive (true);
				shooterGun.SetActive(true);
				started = true;
			}

			if(GUI.Button (new Rect (0,Screen.height-(Screen.height/4),Screen.width/4,Screen.height/4), "HIGH STRIKER",button_text)) {
				strikerGuide.SetActive (true);
				highStriker.SetActive (true);
				started = true;
			}
		}
	}
}