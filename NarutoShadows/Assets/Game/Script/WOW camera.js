var target : Transform;

var targetHeight = 2.0;
var distance = 5.0;

var maxDistance = 20;
var minDistance = 2.5;

var xSpeed = 250.0;
var ySpeed = 120.0;

var yMinLimit = -20;
var yMaxLimit = 80;

var zoomRate = 20;

var rotationDampening = 3.0;

private var x = 0.0;
private var y = 0.0;

@script AddComponentMenu("Camera-Control/WoW Camera")

function Start () {
    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;

   // Make the rigid body not change rotation
      if (rigidbody)
      rigidbody.freezeRotation = true;
}

function LateUpdate () {
	if(!target)
		return;
	
	// If either mouse buttons are down, let them govern camera position
   if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
   {
	x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
	y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
	
	// otherwise, ease behind the target if any of the directional keys are pressed
   } else if(Input.GetAxis("Vertical") || Input.GetAxis("Horizontal")) {
	   var targetRotationAngle = target.eulerAngles.y;
	   var currentRotationAngle = transform.eulerAngles.y;
	   x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
   }
   
	distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
	distance = Mathf.Clamp(distance, minDistance, maxDistance);
   
   y = ClampAngle(y, yMinLimit, yMaxLimit);
   
	var rotation:Quaternion = Quaternion.Euler(y, x, 0);
	var position = target.position - (rotation * Vector3.forward * distance + Vector3(0,-targetHeight,0));
	
	transform.rotation = rotation;
	transform.position = position;
}

static function ClampAngle (angle : float, min : float, max : float) {
   if (angle < -360)
      angle += 360;
   if (angle > 360)
      angle -= 360;
   return Mathf.Clamp (angle, min, max);
}