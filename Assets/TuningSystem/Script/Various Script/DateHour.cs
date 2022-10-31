using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DateHour : MonoBehaviour {
	
	public string Date;
	public string Hour;
	public string Complete;

	public Text Stamp;

	void Update(){
		Complete=System.DateTime.Now.ToString("f");

		Date= System.DateTime.Now.Date.ToString("d");

		Hour= System.DateTime.Now.ToString("t");

		Stamp.text = Complete;
	}
}
