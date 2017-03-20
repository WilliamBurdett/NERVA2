using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {
    [SerializeField] private float speed=100f;
	
	// Update is called once per frame
	void Update () {
	    transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
