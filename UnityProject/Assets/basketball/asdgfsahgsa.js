#pragma strict

var right = 1;
var left = 0;
var xStart:float;
var farLeft:float;
var farRight:float;

function Start () {
	xStart = this.transform.localPosition.y;
	farLeft = xStart - 20;
	farRight = xStart + 20;
	

}

function Update () {

 	if (this.transform.localPosition.x < farRight && right==1) {
 		transform.Translate(Vector3.right * Time.deltaTime * 500);
 	}
 	
 	if (this.transform.localPosition.x > farRight) {
 		right = 0;
 		left = 1;
 	}
 	
 	if (this.transform.localPosition.x > farLeft && left==1) {
 		transform.Translate(Vector3.right * Time.deltaTime * -500);
 	}
 	
 	if (this.transform.localPosition.x < farLeft) {
 		left = 0;
 		right = 1;
 	}
 	
 	transform.localPosition.x = Mathf.Clamp(transform.localPosition.x, farLeft - 5.0f, farRight + 5.0f);
}