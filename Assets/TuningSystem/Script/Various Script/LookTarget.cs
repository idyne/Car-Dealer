using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTarget : MonoBehaviour {

	public Transform Target;
	public float SmoothTime=3f;

	void FixedUpdate(){
		var Rotation = Quaternion.LookRotation (Target.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, Rotation, SmoothTime * Time.deltaTime);
	}
}
