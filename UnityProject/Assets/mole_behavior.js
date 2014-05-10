#pragma strict
var yPos = 0;
function Start () {

}

function Update () {
	transform.Rotate(Vector3.up, 1);
	var start = transform.position;
	var interval = .15;
	transform.position = start + (Vector3.up * interval);
	yPos++;
}