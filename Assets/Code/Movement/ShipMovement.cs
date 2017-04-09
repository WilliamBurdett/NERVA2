using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ShipMovement : MonoBehaviour {
    [SerializeField] private XboxController controller;

    private float percentRotation = 0f;
    private float maxTurnTime = 0.25f;
    private float rotationDegreesPerMaxTurnTime = 45f;
    private float maxBodyRotationDifference = 60f;

    private float forwardSpeed = 100f;

    private float maxPitchRotationDifference = 25f;
    private float fullPitchTime = 0.25f;

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
        //        if (Mathf.Abs(InputMapping.HorizontalAxis(controller)) > 0.05f) {
        //            RotateRoll(InputMapping.HorizontalAxis(controller));
        //            RotateBody(InputMapping.HorizontalAxis(controller));
        //        }
        //        else {
        //            RotateRoll(0f);
        //            RotateBody(0f);
        //        }
        //        if (Mathf.Abs(InputMapping.VerticalAxis(controller)) > 0.05f) {
        //            RotatePitch(InputMapping.VerticalAxis(controller));
        //        }
        //        else {
        //            RotatePitch(0f);
        //        }
        AlertPercentRotation(InputMapping.HorizontalAxis(controller));
        RotatePitch(InputMapping.VerticalAxis(controller));
        Turn();
        LockZTurnAxis();
        MoveForward();


        //Turn(InputMapping.VerticalAxis(controller), InputMapping.HorizontalAxis(controller));
        RotateBody();
    }

    private void Turn() {
        float targetYAngle = rotationDegreesPerMaxTurnTime * percentRotation + transform.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,targetYAngle, 0), Time.deltaTime / maxTurnTime);
    }

    private void AlertPercentRotation(float direction) {
        percentRotation = Mathf.Lerp(percentRotation, direction, Time.deltaTime / maxTurnTime);
        return;
        if (direction > 0) {
            if (percentRotation >= direction) {
                percentRotation = direction;
            } else {
                percentRotation += Time.deltaTime / maxTurnTime;
            }
        } else if (direction < 0) {
            if (percentRotation <= direction) {
                percentRotation = direction;
            } else {
                percentRotation -= Time.deltaTime / maxTurnTime;
            }
        } else {
            //Rotate towards 0
            if (percentRotation > 0.05) {
                percentRotation -= Time.deltaTime / maxTurnTime;
            } else if (percentRotation < -0.05) {
                percentRotation += Time.deltaTime / maxTurnTime;
            }
        }
    }

    void MoveForward() {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void RotateBody() {
        //float rotation = _currentTurnPercent*maxBodyRotationDifference;
        float targetZAngle = -1f * percentRotation * maxBodyRotationDifference;
//        _shipModel.localRotation = Quaternion.Lerp(_shipModel.localRotation,
//            Quaternion.Euler(0, 0, targetZAngle),
//            Time.deltaTime);
        _shipModel.localRotation = Quaternion.Euler(_shipModel.localEulerAngles.x, _shipModel.localEulerAngles.y, targetZAngle);
    }

    void RotatePitch(float direction) {
        float targetXAngle = maxPitchRotationDifference * direction;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(targetXAngle,transform.eulerAngles.y,transform.eulerAngles.z),
            Time.deltaTime * (1 / fullPitchTime));
    }

    //    void Turn(float x, float y) {
    //        float targetXAngle = maxPitchRotationDifference * x;
    //        float targetYAngle = maxBodyRotationDifference * y + transform.eulerAngles.y;
    //        transform.rotation = Quaternion.Slerp(transform.rotation,
    //            Quaternion.Euler(targetXAngle, targetYAngle, transform.eulerAngles.z),
    //            Time.deltaTime * (1 / fullTurnTime));
    //    }

    //    void RotateRoll(float direction) {
    //        float targetYAngle = maxBodyRotationDifference * direction + transform.eulerAngles.y;
    //        transform.rotation = Quaternion.Lerp(transform.rotation,
    //            Quaternion.Euler(transform.eulerAngles.x, targetYAngle, transform.eulerAngles.z),
    //            Time.deltaTime * (1 / fullTurnTime));
    //    }


    void LockZTurnAxis() {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
