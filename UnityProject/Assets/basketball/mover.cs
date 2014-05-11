using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour {

	private Vector3 farRight;
	private Vector3 farLeft;
	private Vector3 startPosition;
	private int direction;
	private float distance;
	private float speed;
	private float minDifference;
	// Use this for initialization
	void Start () {
		direction = -1;
		startPosition = gameObject.transform.localPosition;
		distance = 500.0f;
		speed = 1.5f;
		minDifference = 15.0f;
		//we use y as the center point in local position.
		farRight = new Vector3(startPosition.x,startPosition.y,startPosition.z + distance);
		farLeft = new Vector3(startPosition.x,startPosition.y,startPosition.z - distance);
		gameObject.transform.localPosition = farRight;

	}
	
	// Update is called once per frame
	void Update () {
		if (direction == -1) {
			if(Vector3.Distance (gameObject.transform.localPosition,farLeft) > minDifference) {
				gameObject.transform.localPosition = Vector3.Lerp (gameObject.transform.localPosition,farLeft,Time.deltaTime * speed);
			} else {
				direction = 1;
			}
		}
		if (direction == 1) {
			if(Vector3.Distance (gameObject.transform.localPosition,farRight) > minDifference) {
				gameObject.transform.localPosition = Vector3.Lerp (gameObject.transform.localPosition,farRight,Time.deltaTime * speed);
			} else {
				direction = -1;
			}
		}	
	}
}
