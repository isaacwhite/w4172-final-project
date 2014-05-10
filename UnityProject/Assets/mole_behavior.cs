using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mole_behavior : MonoBehaviour {
	
	private GameObject[][] moles;
	private float maxY;
	private float minY;
	private GameObject aMole;
	private ArrayList jumpingMoles;
	private Dictionary<string,int> isJumping;
	private int jumpThreshold;
//	private abstract[]
//	private ArrayList telephoneBooths;
//	private ArrayList airplanes;
//	private ArrayList subways;
//	private GameObject airplane;
//	private GameObject telephoneBooth;
//	private GameObject subway;
//	private GameObject car;
//	private GameObject target;
//	private Vector3 location;
//	private TextMesh modeLabel;
//	private TextMesh scaleFactor;
//	private TextMesh warning;
//	private double scale;
//	private GameObject selected;
//	private int count;
//	private string[] modes;
//	private int activeMode;
//	private bool confirmed;
	// Use this for initialization
	void Start () {
		
	}
	void Awake() {
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
		maxY = aMole.transform.position.y;
		jumpThreshold = 90;
		jumpingMoles = new ArrayList();

		minY = maxY - 100.0f;
		isJumping = new Dictionary<string,int>(); //assign the dictionary.

		//now set up the values
		//would this be better if we used the object names instead?
		for(int j = 0; j< moles.Length; j++) {
			GameObject[] row = moles[j];
			for(int i = 0; i < row.Length; i++) {
				//instead of using the row count, let's switch to mole name. This should be easier to check against.
//				string hashIndex = j.ToString () + i.ToString ();
				GameObject theMole = moles[j][i];
				string hashIndex = theMole.transform.name;
				isJumping[hashIndex] = 0; //we use zero to indicate it is not moving.
				//after we set it to false, why don't we also set the mole position to the minimum.
				theMole.transform.position = new Vector3(theMole.transform.position.x,minY,theMole.transform.position.z);
			}
		}



		//we need to determine the max height of the moles, and the min height of them
		//probably best to define a method that will move them up and down.
		//maybe attach the collision handling to the hammer? It will be involved in every collision. This might be easier than attaching it to the moles.
		//maybe we can get the name of the object we are colliding with, and if it is an active mole, then we can grant a point. If the mole isn't active, we shouldn't count it.

//		subway = GameObject.Find ("subway");
//		telephoneBooth = GameObject.Find ("photobooth");
//		car = GameObject.Find ("racecar");
//		airplane = GameObject.Find ("airplane");
//		target = GameObject.Find ("groundTarget");
//		modeLabel = (TextMesh)GameObject.Find ("modeText").GetComponent (typeof(TextMesh));
//		scaleFactor = (TextMesh)GameObject.Find ("factorText").GetComponent (typeof(TextMesh));
//		warning = (TextMesh)GameObject.Find ("warning").GetComponent (typeof(TextMesh));
//		confirmed = false;
//		
//		location = target.transform.position;
//		print ("Awake!!");
//		modes = new string[3];
//		modes [0] = "translate";
//		modes [1] = "rotate";
//		modes [2] = "scale";
//		modeLabel.text = "Updated!";
//		scale = 1.0;
//		scaleFactor.text = "" + scale;
//		activeMode = 0;
	}
	
	// Update is called once per frame
	void Update () {
		TapSelect ();//add any necessary moles as moving
		moveMoles ();//move the moles.
	}
//	void switchMode() {
//		if (activeMode < modes.Length - 1) {
//			activeMode ++;
//		} else {
//			activeMode = 0;
//		}
//		modeLabel.text = modes [activeMode];
//	}
	void moveMoles() {
//		print ("trying to move!");
//		print (jumpingMoles.Count.ToString () + " jumping moles!");
		ArrayList toRemove = new ArrayList ();
		foreach (GameObject mole in jumpingMoles) {
				//we may store a waiting period in the dictionary at some point as well.
				string moleName = mole.transform.name;
				int direction = isJumping [moleName];
//				print ("Mole position:" + direction.ToString ());
				float speed = 50.0f;
				//		int initialY = mole.transform.Translate.Y;
				if ((direction == 1) || (direction == -1)) {
//				print ("is anyone there??");
						//move it in the designated direction
//						mole.transform.Translate (Vector3.up * Time.deltaTime * direction);
						float newPosition = mole.transform.position.y + (Time.deltaTime * direction * speed);
						mole.transform.position = new Vector3(mole.transform.position.x,newPosition,mole.transform.position.z);
						if ((direction == -1) && (mole.transform.position.y < minY)) {
								//time to make the mole stop moving.
							print ("finished animating a mole!");
							isJumping [moleName] = 0;

//					print(jumpingMoles.Count.ToString ());
//							jumpingMoles.Remove (mole); //take it out of the list.
							toRemove.Add (mole);
							mole.transform.position = new Vector3(mole.transform.position.x, minY, mole.transform.position.z);
//					print (jumpingMoles.Count.ToString ());


						} else if((direction == 1) && (mole.transform.position.y > maxY)) {
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
	
//	void buttonPressed(string which) {
//		modeLabel.text = which;
//		switch (which) {
//		case "+x":
//			adjustPositioning("x",false);
//			break;
//		case "+y":
//			adjustPositioning ("y",false);
//			break;
//		case "+z":
//			adjustPositioning("z",false);
//			break;
//		case "-x":
//			adjustPositioning("x",true);
//			break;
//		case "-y":
//			adjustPositioning("y",true);
//			break;
//		case "-z":
//			adjustPositioning ("z",true);
//			break;
//		case "+factor":
//			scale = scale * 1.5;
//			scaleFactor.text = "" + scale;
//			break;
//		case "-factor":
//			scale = scale * 0.6666667;
//			scaleFactor.text = "" + scale;
//			break;
//		case "toggleMode":
//			switchMode();
//			break;
//		case "clone":
//			Behaviour h = (Behaviour)selected.GetComponent ("Halo");
//			h.enabled = false;
//			GameObject newObject = (GameObject)Instantiate(selected);
//			selected = newObject;
//			newObject.transform.position = location;
//			newObject.transform.parent = target.transform;
//			float currentScale = newObject.transform.localScale.x * 10;
//			newObject.transform.localScale = new Vector3(currentScale,currentScale,currentScale);
//			h = (Behaviour)selected.GetComponent ("Halo");
//			h.enabled = true;
//			break;
//		case "delete":
//			if(confirmed) {
//				selected.SetActive(false);
//				warning.text = "";
//				confirmed = false;
//			} else {
//				warning.text = "Are you sure?";
//				confirmed = true;
//			}
//			break;
//		}
//		if (which != "delete") {
//			//in the event that we didn't choose delete, make sure confirmed is set to false.
//			confirmed = false;
//			warning.text = "";
//		}
//		
//	}
	
//	void adjustPositioning(string axis, bool isNegative) {
//		if (selected) {
//			double magnitude;
//			if(activeMode == 0) {
//				magnitude = scale * 0.1;
//				if(isNegative) {
//					magnitude = (-magnitude);
//				}
//				float x = selected.transform.localPosition.x;
//				float y = selected.transform.localPosition.y;
//				float z = selected.transform.localPosition.z;
//				if(axis.Equals("x")){
//					x += (float)magnitude;
//				} else if (axis.Equals ("y")) {
//					y += (float)magnitude;
//				} else {
//					z += (float)magnitude;
//				}
//				selected.transform.localPosition = new Vector3(x,y,z);
//				//translate
//			} else if (activeMode == 1) {
//				magnitude = scale;
//				if(isNegative) {
//					magnitude = (-magnitude);
//				}
//				if(axis.Equals("x")) {
//					selected.transform.Rotate(Vector3.forward,(float)magnitude);
//				} else if (axis.Equals ("y")) {
//					selected.transform.Rotate (Vector3.right,(float)magnitude);
//				} else {
//					selected.transform.Rotate (Vector3.up,(float)magnitude);
//				}
//				//rotate
//			} else if (activeMode == 2) {
//				magnitude = scale;
//				//this is nice, we don't care what direction it's in. Ha!
//				float newScale = selected.transform.localScale.x * (float)magnitude;
//				selected.transform.localScale = new Vector3(newScale,newScale,newScale);
//			}
//			
//		}
//		
//	}
//	
	void TapSelect() {
		//we'll want to be able to randomly select a row, and only initiate a mole jump if we don't have too many moles jumping already.
		//maybe we need to keep an array list of the moles that are moving?
		int moveAMole = Random.Range (0, 100);
		if ((moveAMole > jumpThreshold) && (jumpingMoles.Count < 4)) {
			foreach(GameObject[] row in moles) {
				foreach (GameObject mole in row) {
					int moveThisMole = Random.Range (0,100);
					if((moveThisMole > jumpThreshold) && (jumpingMoles.Count < 4)) {
						if(isJumping[mole.transform.name] == 0) {
							//let's move the mole slightly below the minimum in order to signal that it will move up.
							//this is for user experience :)
//							mole.transform.position = new Vector3(mole.transform.position.x,mole.transform.position.y - 20, mole.transform.position.z);
							isJumping[mole.transform.name] = 1; //move it up.
							jumpingMoles.Add(mole);
//							moveMole(mole);
						} //only act if the mole isn't jumping

					}
				}
			}
		}

//		bool alreadyChanged = false;
//		if (Input.GetMouseButtonDown (0)) {
//			print ("tapped something!!");
//			//also make sure confirmed is false here.
//			confirmed = false;
//			warning.text = "";
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			RaycastHit hit;
//			if(Physics.Raycast (ray,out hit, 1000)) {
//				Behaviour h;
//				string objectName = hit.collider.gameObject.name;
//				if(selected) { //if we already selected something.
//					h = (Behaviour)selected.GetComponent ("Halo");
//					h.enabled = false; // turn off the old one.
//				}
//				if(selected && selected.name.Equals(objectName)) {
//					count++;
//					if(!alreadyChanged) {
//						alreadyChanged = true;
//						switchMode ();
//					}
//				} else {
//					count = 1;
//					activeMode = 0;
//				}
//				scaleFactor.text = "" + count;
//				selected = hit.collider.gameObject; //assign the new one
//				h = (Behaviour)selected.GetComponent ("Halo");
//				if(!h.enabled) {
//					h.enabled = true;//turn on the halo.
//				} 
//				//				GameObject newObject = (GameObject)Instantiate(airplane);
//				//				newObject.transform.position = location;
//				//				newObject.transform.parent = target.transform;
//				//				float currentScale = newObject.transform.localScale.x * 10;
//				//				newObject.transform.localScale = new Vector3(currentScale,currentScale,currentScale);
//				//				//				newObject.
//				//				hit.collider.gameObject.transform.Translate (Vector3.left * Time.deltaTime);
//				//				print(objectName);
//				//				modeLabel.text = objectName;
//			} else {
//				//we didn't hit anything. Let's turn off the selection.
//				if(selected) {
//					Behaviour h = (Behaviour)selected.GetComponent ("Halo");
//					h.enabled = false;
//				}
//			}
//			
//		}
//		foreach (Touch touch in Input.touches) {
//			Ray ray = Camera.main.ScreenPointToRay (touch.position);
//			//also make sure confirmed is false here.
//			confirmed = false;
//			warning.text = "";
//			RaycastHit hit;
//			if(Physics.Raycast (ray,out hit, 1000)) {
//				Behaviour h;
//				string objectName = hit.collider.gameObject.name;
//				if(selected.name.Equals(objectName)) {
//					if(!alreadyChanged) {
//						alreadyChanged = true;
//						switchMode ();
//					}
//					count++;
//				} else {
//					count = 1;
//				}
//				modeLabel.text = objectName;
//				//				scaleFactor.text = "" + count;
//				if(selected) { //if we already selected something.
//					h = (Behaviour)selected.GetComponent ("Halo");
//					h.enabled = false; // turn off the old one.
//				}
//				selected = hit.collider.gameObject; //assign the new one
//				h = (Behaviour)selected.GetComponent ("Halo");
//				if(!h.enabled) {
//					h.enabled = true;//turn on the halo.
//				} 
//			}
//		}
//		
	}
}
