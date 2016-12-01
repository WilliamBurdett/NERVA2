using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
	[SerializeField] private GameObject ship;
	[SerializeField] private GameObject target;
	[SerializeField] private GameObject mainCamera;

	// Use this for initialization
	void Start () {
		if (ship == null || target == null || mainCamera == null) {
			Debug.LogError ("ErrorCode: 78923 | Ship, target, or camera are null");
		}
		GameObject t = GameObject.Instantiate (target,Vector3.zero, Quaternion.identity);
		GameObject s = GameObject.Instantiate (ship);
		s.GetComponent<SmoothFollow> ().Target = t.transform;
		s.GetComponent<LookAt> ().Target = t.transform;
		GameObject c = GameObject.Instantiate (mainCamera);
		c.GetComponent<SmoothFollow> ().Target = s.transform;
		c.GetComponent<LookAt> ().Target = t.transform;
	}
}
