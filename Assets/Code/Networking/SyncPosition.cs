using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 1)]
public class SyncPosition : NetworkBehaviour {
    private Vector3 position;
    [SerializeField] private float smoothRate = 10f;

    public float positionUpdateRate = 0.2f;

	// Use this for initialization
	void Start () {
	    if (isLocalPlayer) {
	        StartCoroutine(UpdatePosition());
	    }
	}
	
	// Update is called once per frame
	void Update () {
		LerpPosition();
	}

    void LerpPosition() {
        if (isLocalPlayer) {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothRate);
    }

    IEnumerator UpdatePosition() {
        while (true) {
            CmdSendPosition(transform.position);
            yield return new WaitForSeconds(positionUpdateRate);
        }
    }

    [Command]
    void CmdSendPosition(Vector3 position) {
        this.position = position;
        RpcReceivePosition(position);
    }

    [ClientRpc]
    void RpcReceivePosition(Vector3 position) {
        this.position = position;
    }
}
