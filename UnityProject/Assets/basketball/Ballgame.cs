using UnityEngine;
using System.Collections;

public class Ballgame : MonoBehaviour {

	public GameObject rim;
	float rotSpeed = 60;
	public GameLogic otherScript;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime, Space.World);
	}

	// Destroys ball on contact with hoop after a time delay
	void OnTriggerEnter(Collider other) {
		if (other.gameObject == rim) {
			otherScript.points++;
		}
		Destroy(this.gameObject,2.0f);
	}

}