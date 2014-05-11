using UnityEngine;
using System.Collections;

public class hammer_reporter : MonoBehaviour {

	delegate void MyDelegate(string name);
	MyDelegate mydelegate;
	// Use this for initialization
	void Start () {
		GameObject delegateObject = GameObject.Find ("whack_a_mole");
		mole_behavior otherScript = (mole_behavior) delegateObject.GetComponent(typeof(mole_behavior));
		mydelegate = otherScript.reportCollidedWith;
		             

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
				print (contact.otherCollider.name);
		}
	}

	void OnTriggerEnter(Collider other) {
		mydelegate(other.name);
	}
}
