using UnityEngine;
using System.Collections;

public class loadStatusBar : MonoBehaviour {
	Texture pB;
	Texture pF;
	int progress;
	Vector2 pos;
	Vector2 size;

	// Use this for initialization
	void Start () {
		progress = 0;
		pos = new Vector2 (20, 40);
		size = new Vector2(20,100);
		pB = new Texture();
		pF = new Texture();
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), pB);
		GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y*progress), pB);
	}
	
	// Update is called once per frame
	void Update () {
		while(progress < 100) {
			progress = Mathf.RoundToInt(Time.time);
		}
	}
}
