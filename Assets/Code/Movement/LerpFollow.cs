using UnityEngine;
using System.Collections;
using System.Configuration.Assemblies;

public class LerpFollow : MonoBehaviour {
    [SerializeField] public Transform target;

    private Vector3 offset;

    [SerializeField] private float distanceBehindTarget;
    [SerializeField] private float distanceAboveTarget;
    [SerializeField] private float distanceAdjacentTarget;

    [SerializeField] private bool directionChanged = true;

    [SerializeField] private float lerpSpeed = 0.5f;

	public Transform Target{
		get { return target; }
		set {
			this.target = value;
			directionChanged = true;
		}
	}

    public float DistanceBehindTarget {
        get { return Mathf.Abs(this.offset.z); }
        set {
            this.offset.z = -1f*value;
            directionChanged = true;
        }
    }

    public float DistanceAboveTarget {
        get { return this.offset.y; }
        set {
            this.offset.y = value;
            directionChanged = true;
        }
    }

    public float DistanceAdjacentTarget {
        get { return this.offset.x; }
        set {
            this.offset.x = value;
            directionChanged = true;
        }
    }

    public float LerpSpeed {
        get { return this.offset.x; }
        set {
            this.lerpSpeed = value;
        }
    }


    void Start() {
        distanceBehindTarget *= -1f;
    }

    void Update() {
        if (directionChanged) {
            offset = new Vector3(distanceAdjacentTarget, distanceAboveTarget, distanceBehindTarget);
            directionChanged = false;
        }
        Vector3 modifiedTargetPostition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position,modifiedTargetPostition,Time.deltaTime*lerpSpeed);
        transform.position = modifiedTargetPostition;

        transform.LookAt(transform);
    }
}
