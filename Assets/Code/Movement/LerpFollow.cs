using UnityEngine;
using System.Collections;
using System.Configuration.Assemblies;

public class LerpFollow : MonoBehaviour {
    [SerializeField] public GameObject target;

    private Vector3 difference;

    [SerializeField] private float distanceBehindTarget;
    [SerializeField] private float distanceAboveTarget;
    [SerializeField] private float distanceAdjacentTarget;

    [SerializeField] private bool differenceChanged = true;

    [SerializeField] private float lerpSpeed = 0.5f;

	public GameObject Target{
		get { return target; }
		set {
			this.target = value;
			differenceChanged = true;
		}
	}

    public float DistanceBehindTarget {
        get { return Mathf.Abs(this.difference.z); }
        set {
            this.difference = new Vector3(0, 0, -1*value) + difference;
            differenceChanged = true;
        }
    }

    public float DistanceAboveTarget {
        get { return this.difference.y; }
        set {
            this.difference = new Vector3(0, value, 0) + difference;
            differenceChanged = true;
        }
    }

    public float DistanceAdjacentTarget {
        get { return this.difference.x; }
        set {
            this.difference = new Vector3(value,0,0) + difference;
            differenceChanged = true;
        }
    }

    public float LerpSpeed {
        get { return this.difference.x; }
        set {
            this.lerpSpeed = value;
            differenceChanged = true;
        }
    }


    void Start() {
        distanceBehindTarget *= -1f;
    }

    void Update() {
        if (differenceChanged) {
            difference = new Vector3(distanceAdjacentTarget, distanceAboveTarget, distanceBehindTarget);
            differenceChanged = false;
        }
        Vector3 modifiedTargetPostition = target.transform.position + difference;
        transform.position = Vector3.Lerp(transform.position, modifiedTargetPostition, Time.deltaTime*lerpSpeed);

        transform.LookAt(target.transform);
    }
}
