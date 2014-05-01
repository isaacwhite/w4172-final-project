using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	bool basketball = false;
	bool mole = false;
	bool hammer = false;
	bool shooting = false;
	public GameObject Carnival;
	public GameObject Hoop;
	public GameObject Rim;
	public GameObject Ball;

	// Use this for initialization
	void Start () {
		Carnival = GameObject.Find("Carnival");

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
	
	}
}
