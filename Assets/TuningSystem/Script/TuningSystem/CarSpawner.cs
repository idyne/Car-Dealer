using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {


	public int ObjToSpawn=0;
	public Transform SpawnPoint;
	public GameObject[] ObjSelected;

	// Use this for initialization
	void Start () {
		ObjToSpawn = PlayerPrefs.GetInt ("CarSelected", 0);
		SpawnPoint = transform;

		Instantiate (ObjSelected[ObjToSpawn], SpawnPoint.position, SpawnPoint.rotation);
	}

	public void Back(string lev){
		Application.LoadLevel (lev);
	}
}
