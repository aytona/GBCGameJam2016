using UnityEngine;

[AddComponentMenu("Camera-Control/Smooth Follow")]
public class SmoothFollow2D : MonoBehaviour
{
	// The target we are following.
	public Transform Target;
	
	// How much damping occurs when the target moves.
	public float MovementDamping = 1.0f;
	
	void LateUpdate ()
	{
		// Early out if we don't have a target.
		if (!Target) { return; }
		
		// Get the current position of the camera.
		Vector3 currentPosition = this.transform.position;
		
		// Store the camera's z value.
		// This value will be modified by the next line.
		float cameraZ = currentPosition.z;
		
		// Set the current position to be the target's position, such that we smooth the movement to the target position
		// using the Lerp function.
		// NOTE: Lerp stands for "linear interpolation". See Unity Scripting Reference for more detail.
		currentPosition = Vector3.Lerp(currentPosition, Target.position, this.MovementDamping * Time.deltaTime);
		
		// Restore camera's z value.
		// We want the camera to remain a fixed distance away from the x-y plane.
		currentPosition.z = cameraZ;
		
		// Update the current position with the smoothed position towards the target object.
		this.transform.position = currentPosition;
	}
}
