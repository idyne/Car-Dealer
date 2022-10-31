using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneWithReturn : MonoBehaviour {

	public string scene;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Return)) {
			Application.LoadLevel (scene);
		}
	}
}
