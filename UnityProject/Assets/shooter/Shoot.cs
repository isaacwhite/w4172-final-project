using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject arCamera;
	private GameObject projectile;
	private int Num_Shots;
	private GameObject shots;
	private GameObject score;
	private GameObject[] target1s;
	private GameObject[] target2s;
	private GameObject[] target3s;
	public GameObject arrows;
	public AudioClip gunshot;
	private AudioSource speaker;

	// Use this for initialization
	void Start () {
		projectile = GameObject.FindGameObjectWithTag("Bullet");
		Num_Shots = 10;
		shots = GameObject.FindGameObjectWithTag("Shots");
		shots.guiText.text = "Shots: " + Num_Shots;
		score = GameObject.FindGameObjectWithTag("Score");

		target1s = GameObject.FindGameObjectsWithTag("Target1");
		target2s = GameObject.FindGameObjectsWithTag("Target2");
		target3s = GameObject.FindGameObjectsWithTag("Target3");
		speaker = (AudioSource) gameObject.GetComponent (typeof(AudioSource));
	}
	
	// Update is called once per frame
	void Update () {
		shots.guiText.pixelOffset = new Vector2(Screen.width / 2 - 480, 1 * Screen.height / 2 - 20);
		score.guiText.pixelOffset = new Vector2(-1 * Screen.width / 2 + 20, 1 * Screen.height / 2 - 20);
		if (Num_Shots == 9) {
			arrows.SetActive (false);
		}
	}

	void OnGUI () {

		GUIStyle button_text = new GUIStyle ("button");
		button_text.fontSize = 40;
		if (Num_Shots == 0 || int.Parse (score.guiText.text.Substring (7)) == 1800) 
		{
			// POINT TOTALS 
			GUI.Box(new Rect((Screen.width/4),(Screen.height/4),(Screen.width/2),(Screen.height/4)), "Final Score: " + score.guiText.text, button_text);
			
			// RETRY
			if(GUI.Button (new Rect ((Screen.width/4),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "RETRY", button_text)) {
				Num_Shots = 10;
				shots.guiText.text = "Shots: 10";
				score.guiText.text = "Score: 0";
				foreach(GameObject target in target1s)
				{
					target.SetActive(true);
				}
				foreach(GameObject target in target2s)
				{
					target.SetActive(true);
				}
				foreach(GameObject target in target3s)
				{
					target.SetActive(true);
				}
			}
			
			// EXIT GAME, GO HOME
			if(GUI.Button (new Rect ((Screen.width/2),(Screen.height/2),(Screen.width/4),(Screen.height/4)), "HOME", button_text)) {
				//started = false;
				//gameOver1 = false;
				GameObject.FindGameObjectWithTag("Shooter_Objects").SetActive(false);
				GameObject.FindGameObjectWithTag("Shooter_Gun").SetActive(false);
				GameLogic master = (GameLogic) arCamera.GetComponent(typeof(GameLogic));
				master.started = false;
			}
		} 
		else 
		{
			if (GUI.Button (new Rect (Screen.width - 220, Screen.height - 220, 202, 202), "\n  FIRE\t\n", button_text)) {
				if (Num_Shots > 0) 
				{
					speaker.PlayOneShot (gunshot);
					Num_Shots = Num_Shots - 1;
					shots.guiText.text = "Shots: " + Num_Shots;
					Rigidbody clone;
					clone = Instantiate (projectile.rigidbody, transform.position, transform.rotation) as Rigidbody;
					clone.velocity = transform.TransformDirection (Vector3.forward * 10000);
				}
			}
		}
	}
}
