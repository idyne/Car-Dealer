using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	[Header ("Stock")]
	public float CarMaxSpeed=0.1f;
	public float CarTorque=0.1f;
	public float CarBrakeTorque=0.1f;
	[Header ("Tuning")]
	public float TuningSpeed=0;
	public float TuningTorque=0;
	public float TuningBrake=0;

	void Update(){
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (0, 0, -(CarMaxSpeed + TuningSpeed));
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (0, 0, CarMaxSpeed + TuningSpeed);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (0, -(CarBrakeTorque + TuningBrake), 0);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (0, (CarBrakeTorque + TuningBrake), 0);
		}
	}
}
