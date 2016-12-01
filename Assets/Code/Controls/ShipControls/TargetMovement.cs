using UnityEngine;
using System.Collections;

public class TargetMovement : MonoBehaviour {
	[SerializeField] private float speed = 15;
	[SerializeField] private float rotationSpeedHorizontal = 100;
	[SerializeField] private float rotationSpeedVertical = 100;
	[SerializeField] private float maxHeight = 20;

	public float Speed {
		get { return speed; }
		set { speed = value; }
	}

	public float RotationSpeedHorizontal {
		get { return rotationSpeedHorizontal; }
		set { rotationSpeedHorizontal = value; }
	}

	public float RotationSpeedVertical {
		get { return rotationSpeedVertical; }
		set { rotationSpeedVertical = value; }
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0);
		transform.Rotate (Vector3.up , Time.deltaTime*rotationSpeedHorizontal * InputMapping.HorizontalAxis());
		transform.Rotate (Vector3.right*-1f,Time.deltaTime*rotationSpeedVertical * InputMapping.VerticalAxis());


		Vector3 newEularAngles = transform.eulerAngles;
		newEularAngles.z = 0;
		if (transform.eulerAngles.x > 30f && transform.eulerAngles.x < 300f) {
			newEularAngles.x = 30;
			//transform.eulerAngles = new Vector3(15,transform.eulerAngles.y,0);
		}
		if (transform.eulerAngles.x > 300f && transform.eulerAngles.x < 330f) {
			newEularAngles.x = 330;
			//transform.eulerAngles = new Vector3(345,transform.eulerAngles.y,0);
		}

		//For debugging, 
		if(Input.GetKeyDown(KeyCode.LeftControl)){
			speed=0;
			rotationSpeedVertical=0;
			rotationSpeedHorizontal=0;
		}

		//Target cannot go above or below level heights
		if (transform.position.y > maxHeight) {
			transform.position = new Vector3(transform.position.x,maxHeight,transform.position.z);
			newEularAngles.x=0;
			//transform.eulerAngles.x = 0;
		}
		transform.eulerAngles = newEularAngles;
	}
}
