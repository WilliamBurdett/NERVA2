using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
	[SerializeField] private Transform target;

	public Transform Target{
		get { return target; }
		set {
			this.target = value;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			Debug.LogError ("ErrorCode: 78924 | Ship, target, or camera are null");
		}
		transform.LookAt (target);
	}
}
