using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupCamera : NetworkBehaviour {
    [SerializeField]
    private GameObject mainCamera;

    // Use this for initialization
    void Start () {
        if(!isLocalPlayer) {
            return;
        }
        if(mainCamera == null) {
            Debug.LogError("ErrorCode: 78984 || Camera is null");
            return;
        }

        GameObject cameraObject = GameObject.Instantiate(mainCamera);

        cameraObject.GetComponent<LerpFollowRotation>().Target = transform;
        GameObject actualCameraObject = null;
        foreach(Transform child in cameraObject.transform) {
            if(child.tag == "MainCamera") {
                actualCameraObject = child.gameObject;
            }
        }
        if(actualCameraObject == null) {
            Debug.LogError("ErrorCode: 78924 | Camera is null");
        }
        actualCameraObject.GetComponent<LookAt>().Target = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
