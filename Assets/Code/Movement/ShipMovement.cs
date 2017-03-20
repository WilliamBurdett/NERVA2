using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ShipMovement : MonoBehaviour {
    [SerializeField] private XboxController controller;

    private float forwardSpeed = 2f;

    private float maxPitchRotationDifference = 25f;
    private float fullPitchTime = 0.5f;

    private float maxTurnSpeed = 100f;
    private float _currentTurnPercent = 0f;
    private float fullTurnTime = .45f;
    private float maxBodyRotationDifference = 60f;
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
        if(Mathf.Abs(InputMapping.HorizontalAxis(controller)) > 0.05f) {
            AlterTurnPercent(InputMapping.HorizontalAxis(controller));
        } else {
            CorrectHorizontal();
        }
        Turn();
        if(Mathf.Abs(InputMapping.VerticalAxis(controller)) > 0.05f) {
            RotatePitch(InputMapping.VerticalAxis(controller));
        } else {
            RotatePitch(0f);
        }
        LockZTurnAxis();
        MoveForward();
        RotateBody();
    }

    private void Turn() {
        transform.Rotate(Vector3.up*Time.deltaTime*maxTurnSpeed*_currentTurnPercent);
    }

    void MoveForward() {
        transform.Translate(Vector3.forward*forwardSpeed);
    }

    void RotateBody() {
        float rotation = _currentTurnPercent*maxBodyRotationDifference;
        _shipModel.localRotation = Quaternion.Euler(_shipModel.localEulerAngles.x, _shipModel.localEulerAngles.y,
            -1f*rotation);
    }

    private void AlterTurnPercent(float direction) {
        _currentTurnPercent += Time.deltaTime*direction*(1/fullTurnTime);
        if (_currentTurnPercent > direction && _currentTurnPercent > 0.05f) {
            _currentTurnPercent = direction;
        } else if (_currentTurnPercent < direction && _currentTurnPercent < 0.05f) {
            _currentTurnPercent = direction;
        }
    }

    void CorrectHorizontal() {
        if (_currentTurnPercent > 0.05f) {
            AlterTurnPercent(-1f);
        }
        else if (_currentTurnPercent < -0.05f) {
            AlterTurnPercent(1f);
        }
    }

    void RotatePitch(float direction) {
        float targetXAngle = maxPitchRotationDifference*direction;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(targetXAngle, transform.eulerAngles.y, transform.eulerAngles.z),
            Time.deltaTime*(1/fullPitchTime));
    }

    void LockZTurnAxis() {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
