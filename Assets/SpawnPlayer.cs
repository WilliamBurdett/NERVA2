using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
	[SerializeField] private GameObject ship;
	[SerializeField] private GameObject mainCamera;

	// Use this for initialization
	void Start () {
		if (ship == null || mainCamera == null) {
			Debug.LogError ("ErrorCode: 78923 | Ship or camera are null");
		}
		GameObject shipObject = GameObject.Instantiate (ship);
		GameObject cameraObject = GameObject.Instantiate (mainCamera);
        
        cameraObject.GetComponent<LerpFollowRotation>().Target = shipObject.transform;
        cameraObject.GetComponent<LerpFollowRotation>().LerpSpeed = 5f;
	    GameObject actualCameraObject = null;
	    foreach (Transform child in cameraObject.transform) {
	        if (child.tag == "MainCamera") {
	            actualCameraObject = child.gameObject;
	        }
	    }
	    if (actualCameraObject == null) {
            Debug.LogError("ErrorCode: 78924 | Camera is null");
        }
        actualCameraObject.GetComponent<LookAt>().Target = shipObject;
        actualCameraObject.GetComponent<LookAt>().HorizontalOffset = 10;


        //		cameraObject.GetComponent<SmoothFollow> ().target = shipObject.transform;
        //		cameraObject.GetComponent<LookAt> ().target = targetObject.transform;
    }
}
