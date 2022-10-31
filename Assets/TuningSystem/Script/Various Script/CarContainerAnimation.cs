using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarContainerAnimation : MonoBehaviour {

	public float RotationIntensity=0.5f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, RotationIntensity, 0);
	}
}
