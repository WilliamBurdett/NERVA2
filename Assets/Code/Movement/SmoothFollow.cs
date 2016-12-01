using UnityEngine;


public class SmoothFollow : MonoBehaviour
{

	// The target we are following
	[SerializeField] private Transform target;

	// The distance in the x-z plane to the target
	[SerializeField] private float distance;
	// the height we want the camera to be above the target
	[SerializeField] private float height;
	[SerializeField] private float rotationDamping;
	[SerializeField] private float heightDamping;

	public Transform Target{
		get { return target; }
		set {
			this.target = value;
		}
	}

	public float Distance{
		get { return distance;}
		set {
			this.distance = value;
		}
	}

	public float Height{
		get {return height;}
		set {
			this.height = value;
		}
	}

	public float RotationDamping{
		get {return rotationDamping; }
		set {
			this.rotationDamping = value;
		}
	}

	public float HeightDamping{
		get {return heightDamping; }
		set {
			this.heightDamping = value;
		}
	}

	// Update is called once per frame
	void LateUpdate()
	{
		// Early out if we don't have a target
		if (!target)
			return;

		// Calculate the current rotation angles
		var wantedRotationAngle = target.eulerAngles.y;
		var wantedHeight = target.position.y + height;

		var currentRotationAngle = transform.eulerAngles.y;
		var currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Set the height of the camera
		transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

		// Always look at the target
		transform.LookAt(target);
	}
}
