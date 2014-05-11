using UnityEngine;
using System.Collections;

public class TargetHit : MonoBehaviour {

	GameObject score;
	// Use this for initialization
	void Start () {
		score = GameObject.FindGameObjectWithTag("Score");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Bullet") {
						col.gameObject.SetActive (false);
		
						if (this.tag == "Target1") {
								gameObject.SetActive (false);
								int current_score = int.Parse (score.guiText.text.Substring (7));
								current_score = current_score + 100;
								score.guiText.text = "Score: " + current_score;
						}
						if (this.tag == "Target2") {
								gameObject.SetActive (false);
								int current_score = int.Parse (score.guiText.text.Substring (7));
								current_score = current_score + 200;
								score.guiText.text = "Score: " + current_score;
						}
						if (this.tag == "Target3") {
								gameObject.SetActive (false);
								Debug.Log (gameObject.tag);
								int current_score = int.Parse (score.guiText.text.Substring (7));
								current_score = current_score + 300;
								Debug.Log (current_score);
								score.guiText.text = "Score: " + current_score;
						}
				}
	}
}
