using System.Net.NetworkInformation;
using UnityEngine;

public class WindowFocusMono : MonoBehaviour {
    
    void Start() {
        WindowFocus.hasFocus = true;
    }

    void OnApplicationFocus(bool hasFocus) {
        WindowFocus.hasFocus = hasFocus;
    }

    void OnApplicationPause(bool pauseStatus) {
        WindowFocus.hasFocus = !pauseStatus;
    }
}
