using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoadOptions : MonoBehaviour {

	public MonoBehaviour Bloom;

	void Update(){
		if (PlayerPrefs.GetInt ("Bloom") == 0)
			Bloom.enabled = false;
		if (PlayerPrefs.GetInt ("Bloom") == 1)
			Bloom.enabled = true;
	}
}
