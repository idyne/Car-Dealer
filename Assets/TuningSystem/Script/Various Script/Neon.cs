using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neon : MonoBehaviour {
	public GameObject neon;
	void Start(){
		neon.SetActive (false);
		neon.SetActive (true);
	}
}
