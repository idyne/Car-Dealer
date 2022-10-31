using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScene : MonoBehaviour {

	public KeyCode RestartKey=KeyCode.R;
	public string Scene ;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(RestartKey))
		Application.LoadLevel (Scene);
	}
}
