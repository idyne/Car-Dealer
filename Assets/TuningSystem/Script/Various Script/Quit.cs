using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour {
	public KeyCode esc;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (esc))
			Application.Quit ();
	}
}
