using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFollowRotation : MonoBehaviour {
    public Transform target;
    public Transform Target {
        get { return target; }
        set { this.target = value; }
    }

    [SerializeField]
    private float lerpSpeed;
    public float LerpSpeed {
        get { return lerpSpeed; }
        set { this.lerpSpeed = value; }
    }

    // Update is called once per frame
    void Update() {
        transform.position = target.position;
        transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation,Time.deltaTime*lerpSpeed);
    }
}
