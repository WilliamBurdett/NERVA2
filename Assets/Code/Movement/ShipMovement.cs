using UnityEngine;
using UnityEngine.Networking;
using XboxCtrlrInput;

public class ShipMovement : NetworkBehaviour {
    [SerializeField] private XboxController controller;

    private float percentRotation;
    private float maxTurnTime = 0.25f;
    private float rotationDegreesPerMaxTurnTime = 45f;
    private float maxBodyRotationDifference = 60f;

    private float forwardSpeed = 100f;

    private float maxPitchRotationDifference = 25f;
    private float fullPitchTime = 0.25f;
    private bool moving = true;

    private Transform _shipModel;

    // Use this for initialization
    void Start() {
        foreach (Transform child in transform) {
            if (child.tag == "ShipModel") {
                _shipModel = child;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer) {
            return;
        }
        if(!WindowFocus.hasFocus) {
            Debug.Log("WindowFocus.HasFocus()" + WindowFocus.hasFocus);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            moving = !moving;
        }


        AlertPercentRotation(InputMapping.HorizontalAxis(controller));
        RotatePitch(InputMapping.VerticalAxis(controller));
        Turn();
        LockZTurnAxis();
        MoveForward();

        
        RotateBody();
    }

    private void Turn() {
        float targetYAngle = rotationDegreesPerMaxTurnTime * percentRotation + transform.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetYAngle, 0),
            Time.deltaTime / maxTurnTime);
    }

    private void AlertPercentRotation(float direction) {
        percentRotation = Mathf.Lerp(percentRotation, direction, Time.deltaTime / maxTurnTime);
    }

    private void MoveForward() {
        if (moving) {
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        }
    }

    public void RotateBody() {
        float targetZAngle = -1f * percentRotation * maxBodyRotationDifference;
        _shipModel.localRotation = Quaternion.Euler(_shipModel.localEulerAngles.x, _shipModel.localEulerAngles.y,
            targetZAngle);
    }

    public void RotatePitch(float direction) {
        float targetXAngle = maxPitchRotationDifference * direction;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(targetXAngle, transform.eulerAngles.y, transform.eulerAngles.z),
            Time.deltaTime * (1 / fullPitchTime));
    }

    private void LockZTurnAxis() {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
