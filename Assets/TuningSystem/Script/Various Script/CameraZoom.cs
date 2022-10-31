using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	public Camera Cam;
	public float FOW;

	void Start(){
		Cam = gameObject.GetComponent<Camera>();
	}
	void Update(){
		if (Input.GetKey (KeyCode.Z)) {
			FOW = 30f;
			Cam.fieldOfView = Mathf.Lerp (Cam.fieldOfView, FOW, Time.deltaTime * 3f);
		} else {
			FOW = 45f;
			Cam.fieldOfView = Mathf.Lerp (Cam.fieldOfView, FOW, Time.deltaTime * 3f);
		}
	}
}
