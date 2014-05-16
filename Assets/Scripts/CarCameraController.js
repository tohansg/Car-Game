﻿/*
This camera smoothes out rotation around the y-axis and height.
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp function.
Then we apply the smoothed values to the transform's position.
*/



// The distance in the x-z plane to the target
var distance = 10.0;
// the height we want the camera to be above the target
var height = 5.0;
// How much we 
var heightDamping = 2.0;
var rotationDamping = 3.0;



// The target we are following
private var target : Transform;

private var follow : GameObject;

// This will return the game object named Hand in the scene.
follow = GameObject.Find("Follow");

target = follow.transform;


private var car : GameObject;
private var speedTextObj : GameObject;
speedTextObj = GameObject.Find("Car Speed");
private var speedText : GUIText;
speedText = speedTextObj.guiText;

car = GameObject.FindGameObjectWithTag("Player");



function LateUpdate () {
        // Early out if we don't have a target
        if (!target)
                return;
        
        // Calculate the current rotation angles
        wantedRotationAngle = target.eulerAngles.y;
        wantedHeight = target.position.y + height;
                
        currentRotationAngle = transform.eulerAngles.y;
        currentHeight = transform.position.y;
        
        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
        
        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position.y = currentHeight;
        
        // Always look at the target
        transform.LookAt (target);
        
        //car speed
        var veloc : int = car.rigidbody.velocity.magnitude;
        veloc = veloc * 2;
        var carSpeed = veloc.ToString();
        speedText.text = carSpeed + " km/h";
        //print(carSpeed);
}