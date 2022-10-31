using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform Target;
	public float FollowIntensity=3f;

	void Update(){
		Target = GameObject.FindGameObjectWithTag ("Player").transform;
		transform.position = Vector3.Lerp(transform.position, Target.position, FollowIntensity * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Vector3.forward), FollowIntensity * Time.deltaTime);
	}
}
