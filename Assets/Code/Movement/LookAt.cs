using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
    public GameObject target;
    public GameObject Target {
        get { return target; }
        set { this.target = value; Debug.Log("set target"); }
    }

    private float horizontalOffset;
    public float HorizontalOffset {
        get { return horizontalOffset; }
        set { this.horizontalOffset = value; }
    }


// Update is called once per frame
    void Update() {
        if (target == null) {
            Debug.LogError("ErrorCode: 78924 | target is null");
        }
        else {
            transform.LookAt(target.transform.position + (target.transform.forward*horizontalOffset));
        }
    }
}
