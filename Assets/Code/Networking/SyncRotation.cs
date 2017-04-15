using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 1)]
public class SyncRotation : NetworkBehaviour {
    private Quaternion rotation;
    [SerializeField] private float smoothRate = 10f;

    public float positionUpdateRate = 0.2f;

    // Use this for initialization
    void Start() {
        if (isLocalPlayer) {
            StartCoroutine(UpdatePosition());
        }
    }

    // Update is called once per frame
    void Update() {
        LerpPosition();
    }

    void LerpPosition() {
        if (isLocalPlayer) {
            return;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothRate);
    }

    IEnumerator UpdatePosition() {
        while (true) {
            CmdSendRotation(transform.rotation);
            yield return new WaitForSeconds(positionUpdateRate);
        }
    }

    [Command]
    void CmdSendRotation(Quaternion rotation) {
        this.rotation = rotation;
        RpcReceiveRotation(rotation);
    }

    [ClientRpc]
    void RpcReceiveRotation(Quaternion rotation) {
        this.rotation = rotation;
    }
}
