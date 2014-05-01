#pragma strict

var right = 1;
var left = 0;

function Start () {

}

function Update () {

 	if (this.transform.position.x < 770.0f && right==1) {
 		transform.Translate(Vector3.right * Time.deltaTime * 500);
 	}
 	
 	if (this.transform.position.x > 770.0f) {
 		right = 0;
 		left = 1;
 	}
 	
 	if (this.transform.position.x > -770.0f && left==1) {
 		transform.Translate(Vector3.right * Time.deltaTime * -500);
 	}
 	
 	if (this.transform.position.x < -770.0f) {
 		left = 0;
 		right = 1;
 	}
 	
 	transform.position.x = Mathf.Clamp(transform.position.x, -775, 775);
}