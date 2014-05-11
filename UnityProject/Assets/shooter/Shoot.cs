using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	private GameObject projectile;
	private int Num_Shots;
	private GameObject shots;
	private GameObject score;

	// Use this for initialization
	void Start () {
		projectile = GameObject.FindGameObjectWithTag("Bullet");
		Num_Shots = 10;
		shots = GameObject.FindGameObjectWithTag("Shots");
		shots.guiText.text = "Shots: " + Num_Shots;

		score = GameObject.FindGameObjectWithTag("Score");

	}
	
	// Update is called once per frame
	void Update () {
		shots.guiText.pixelOffset = new Vector2(Screen.width / 2 - 240, 1 * Screen.height / 2 - 20);
		score.guiText.pixelOffset = new Vector2(-1 * Screen.width / 2 + 20, 1 * Screen.height / 2 - 20);
	}

	void OnGUI () {

		GUIStyle fire_button = new GUIStyle();
		fire_button.fontSize = 18;
		if (GUI.Button (new Rect(Screen.width - 220, Screen.height - 220, 202, 202), "\n  FIRE\t\n")) {
			if(Num_Shots > 0){
				Num_Shots = Num_Shots - 1;
				shots.guiText.text = "Shots: " + Num_Shots;
				Rigidbody clone;
				clone = Instantiate (projectile.rigidbody, transform.position, transform.rotation) as Rigidbody;
				clone.velocity = transform.TransformDirection(Vector3.forward * 10000);
			}
		}
	}
}
