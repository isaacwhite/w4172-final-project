using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mole_behavior : MonoBehaviour {
	
	private GameObject[][] moles;
	private GameObject hammer;
	private GameObject startButton;
	private float maxY;
	private float minY;
	private GameObject aMole;
	private TextMesh yourScoreboard;
	private TextMesh highScoreboard;
	private TextMesh livesLeft;
	private ArrayList jumpingMoles;
	private Dictionary<string,int> isJumping;
	private Dictionary<string,Vector3> targets;
	private Dictionary<string,bool> wasHit;
	private Queue<string> molesToWhack;
	private AudioSource speaker;
	private int jumpThreshold;
	private int maxMoles;
	public bool letsPlay;
	private bool reset;
	private Vector3 currentTarget;
	private float hammerSpeed;
	private int hammerDirection;
	private float strikingSpeed;
	private int score;
	private int highscore;
	private int lives;
	public AudioClip boing;
	public AudioClip gameOver;
	public AudioClip carnival;
	private bool isGameOver;
	void Start () {
		
	}
	void Awake() {
//		letsPlay = true;
		//we could do something more concise than this, but this is faster for now.
		moles = new GameObject[3][];
		moles [0] = new GameObject[4];
		moles [1] = new GameObject[3];
		moles [2] = new GameObject[4];
		//top row
		moles [0] [0] = GameObject.Find ("mole_top_01");
		moles [0] [1] = GameObject.Find ("mole_top_02");
		moles [0] [2] = GameObject.Find ("mole_top_03");
		moles [0] [3] = GameObject.Find ("mole_top_04");
		//middle row
		moles [1] [0] = GameObject.Find ("mole_middle_01");
		moles [1] [1] = GameObject.Find ("mole_middle_02");
		moles [1] [2] = GameObject.Find ("mole_middle_03");
		//botom row
		moles [2] [0] = GameObject.Find ("mole_bottom_01");
		moles [2] [1] = GameObject.Find ("mole_bottom_02");
		moles [2] [2] = GameObject.Find ("mole_bottom_03");
		moles [2] [3] = GameObject.Find ("mole_bottom_04");


		aMole = moles [0] [0];
		hammer = GameObject.Find ("mole_hammer");
		highScoreboard = (TextMesh) GameObject.Find ("mole_high_score").GetComponent (typeof(TextMesh));
		yourScoreboard = (TextMesh) GameObject.Find ("mole_your_score").GetComponent (typeof(TextMesh));
		livesLeft = (TextMesh)GameObject.Find ("mole_lives_left").GetComponent (typeof(TextMesh));
		maxY = aMole.transform.localPosition.y;
		print (maxY);
		jumpThreshold = 92;
		jumpingMoles = new ArrayList();
		maxMoles = 6;
		hammerSpeed = 15.0f;
		strikingSpeed = 20.0f;
		minY = maxY - 150.0f;
		isJumping = new Dictionary<string,int>(); //assign the dictionary.
		targets = new Dictionary<string,Vector3> ();
		wasHit = new Dictionary<string,bool> ();
		score = 0;
		highscore = 0;
		lives = 10;

		speaker = (AudioSource) gameObject.GetComponent (typeof(AudioSource));
		//now set up the values
		//would this be better if we used the object names instead?
		ResetGame ();
		hammer.transform.localPosition = targets["mole_middle_02"];

		//we need to determine the max height of the moles, and the min height of them
		//probably best to define a method that will move them up and down.
		//maybe attach the collision handling to the hammer? It will be involved in every collision. This might be easier than attaching it to the moles.
		//maybe we can get the name of the object we are colliding with, and if it is an active mole, then we can grant a point. If the mole isn't active, we shouldn't count it.

	}
	void ResetGame() {
		for(int j = 0; j< moles.Length; j++) {
			GameObject[] row = moles[j];
			for(int i = 0; i < row.Length; i++) {
				//instead of using the row count, let's switch to mole name. This should be easier to check against.
				//				string hashIndex = j.ToString () + i.ToString ();
				GameObject theMole = moles[j][i];
				string hashIndex = theMole.transform.name;
				isJumping[hashIndex] = 0; //we use zero to indicate it is not moving.
				//this sets all the moles to the start position.
				Vector3 target = new Vector3(theMole.transform.localPosition.x,minY,theMole.transform.localPosition.z);
				theMole.transform.localPosition = target;
				target = new Vector3(theMole.transform.localPosition.x,theMole.transform.localPosition.y + 600,theMole.transform.localPosition.z);
				targets[hashIndex] = target; 
				wasHit[hashIndex] = false;
			}
		}
		highScoreboard.text = highscore.ToString ();
		score = 0;
		yourScoreboard.text = score.ToString();
		isGameOver = false;
		lives = 10;
		livesLeft.text = lives.ToString();
	}
	// Update is called once per frame
	void Update () {


		if ((lives < 0) && (!isGameOver)) {
			isGameOver = true;
			letsPlay = false;
			print ("Your score: " + score.ToString ());
			if(score > highscore) {
				highscore = score;
				highScoreboard.text = highscore.ToString ();
			}
			speaker.PlayOneShot(gameOver);
		} 
		if (letsPlay) {
			if(lives > -1) {
				livesLeft.text = lives.ToString ();
			}
			reset = false;
			ListenForTaps();
			TapSelect ();//add any necessary moles as moving
			moveMoles ();//move the moles.
			Vector3 downPosition = new Vector3(currentTarget.x,currentTarget.y - 200,currentTarget.z);
			if(hammerDirection == 0) {
				hammer.transform.localPosition = Vector3.Lerp (hammer.transform.localPosition,currentTarget,Time.deltaTime * hammerSpeed);
			}
			//if we're at our destination or in the process of going down.
			if(hammerDirection != -999) { //use this code for finished.
				float distance = Vector3.Distance (hammer.transform.localPosition,currentTarget);
				if((distance < 1.0f) || (hammerDirection == -1)) {
					hammerDirection = -1;
					hammer.transform.localPosition = Vector3.Lerp (hammer.transform.localPosition,downPosition,Time.deltaTime * strikingSpeed);
				} 
				distance = Vector3.Distance (hammer.transform.localPosition,downPosition);
				if ((distance < 1.0f) || (hammerDirection == 1)) {
					hammerDirection = 1;
					hammer.transform.localPosition = Vector3.Lerp (hammer.transform.localPosition,currentTarget,Time.deltaTime * strikingSpeed * 0.66f);
					distance = Vector3.Distance (hammer.transform.localPosition,currentTarget);
					if(distance < 1.1f) {
						hammerDirection = -999;
					}
				}

			} else {
				//we finished with the current target. Set the next target so it will get lerped to.
			}
		} else {
			ListenForTaps();
		}
			//else if (!reset){
			//ResetGame();
			//reset = true;
		//}
	}

	void moveMoles() {
		ArrayList toRemove = new ArrayList ();
		foreach (GameObject mole in jumpingMoles) {
				//we may store a waiting period in the dictionary at some point as well.
				string moleName = mole.transform.name;
				int direction = isJumping [moleName];
				float speed = 200.0f;
				//		int initialY = mole.transform.Translate.Y;
				if ((direction == 1) || (direction == -1)) {
						//move it in the designated direction
//						mole.transform.Translate (Vector3.up * Time.deltaTime * direction);
						float newPosition = mole.transform.localPosition.y + (Time.deltaTime * direction * speed);
						mole.transform.localPosition = new Vector3(mole.transform.localPosition.x,newPosition,mole.transform.localPosition.z);
						if ((direction == -1) && (mole.transform.localPosition.y < minY)) {
								//time to make the mole stop moving.
							if(wasHit[moleName] == false) {
								lives--;
							} else {
								wasHit[moleName] = false; //set it back.
							}
							isJumping [moleName] = 0;

							toRemove.Add (mole);
						mole.transform.localPosition = new Vector3(mole.transform.localPosition.x, minY, mole.transform.localPosition.z);


						} else if((direction == 1) && (mole.transform.localPosition.y > maxY)) {
							//we need to start the delay.
							direction++;
							isJumping[moleName] = direction;
						}
				} else {
						//I guess we're letting it sit there for a little while.
						//increment its counter
						direction++;
						//if we've been waiting for 30ish cycles, move it back down.
						if(direction > 30) {
							direction = -1;
						}
						isJumping [moleName] = direction;
				}
		} //end iteration
		//clear out any objects that have finished.
		foreach (GameObject finished in toRemove) {
			jumpingMoles.Remove (finished);
		}
//		if(mole.transform
	}
	

	void TapSelect() {
		//we'll want to be able to randomly select a row, and only initiate a mole jump if we don't have too many moles jumping already.
		//maybe we need to keep an array list of the moles that are moving?
		int moveAMole = Random.Range (0, 100);
		if ((moveAMole > jumpThreshold) && (jumpingMoles.Count < maxMoles)) {
			foreach(GameObject[] row in moles) {
				foreach (GameObject mole in row) {
					int moveThisMole = Random.Range (0,100);
					if((moveThisMole > jumpThreshold) && (jumpingMoles.Count < maxMoles)) {
						if(isJumping[mole.transform.name] == 0) {
							//let's move the mole slightly below the minimum in order to signal that it will move up.
							//this is for user experience :)
							mole.transform.localPosition = new Vector3(mole.transform.localPosition.x,mole.transform.localPosition.y - 20, mole.transform.localPosition.z);
							isJumping[mole.transform.name] = 1; //move it up.
							jumpingMoles.Add(mole);
//							moveMole(mole);
						} //only act if the mole isn't jumping

					}
				}
			}
		}
	}

	public void reportCollidedWith(string name) {
		score += 100;
		yourScoreboard.text = score.ToString ();
//		print (name);
		hammerDirection = 1;//go back up, we hit something!
		speaker.PlayOneShot (boing);
		wasHit [name] = true;

	}

	void ListenForTaps() {
		if (Input.GetMouseButtonDown (0)) {
			print ("tapped something!!");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast (ray,out hit, 20000)) {
				Behaviour h;
				string objectName = hit.collider.gameObject.name;
				try {
					if((objectName == "mole_play") && (letsPlay == false)) {
						speaker.PlayOneShot (carnival);
						ResetGame ();
						letsPlay = true;
					} else if(letsPlay) {
						if(objectName != "mole_hammer") {
							currentTarget = targets[objectName];
							hammerDirection = 0;
						}
					}
				} catch (System.Exception e) {
					print (e.ToString ());
				}
				
			//				hammer.transform.localPosition = targets[objectName];
			} else {
				//we didn't hit anything. Let's turn off the selection.
//				print ("no collision!");
			}
			
		}
		foreach (Touch touch in Input.touches) {
			Ray ray = Camera.main.ScreenPointToRay (touch.position);
			RaycastHit hit;
			if(Physics.Raycast (ray,out hit, 20000)) {
				Behaviour h;
				string objectName = hit.collider.gameObject.name;
				print (objectName);
				if((objectName == "mole_play") && (letsPlay == false)) {
					speaker.PlayOneShot (carnival);
					ResetGame ();
					letsPlay = true;
				} else if (letsPlay) {
					try {
						if(objectName != "mole_hammer") {
							currentTarget = targets[objectName];
							hammerDirection = 0;
						}
					} catch (System.Exception e) {
						print (e.ToString ());
					}
				}
			}
		}
	}

}
