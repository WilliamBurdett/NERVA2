using UnityEngine;
using System.Collections;

public static class InputMapping {
    public static float Up() {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            return 1;
        }
        return 0;
	}

	public static float Down() {
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			return 1;
		}
		return 0;
	}

    public static float Left() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            return 1;
        }
        return 0;
    }

    public static float Right() {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            return 1;
        }
        return 0;
    }

	public static float VerticalAxis(){
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			return 1;
		} else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
			return -1;
		}
		return 0;
	}

	public static float HorizontalAxis(){
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			return -1;
		} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			return 1;
		}
		return 0;
	}

    public static bool Fire1() {
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) {
            return true;
        }
        return false;
    }
}
