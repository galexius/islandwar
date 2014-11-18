using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {
	public float speed = 1.0F;
	void Update() {
		var touch = MyInput.GetTouch ();
		if (touch != null && touch.phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = touch.deltaPosition;
			transform.Translate( -touchDeltaPosition.x * touch.deltaTime,  0, -touchDeltaPosition.y * touch.deltaTime);
		}
	}
}