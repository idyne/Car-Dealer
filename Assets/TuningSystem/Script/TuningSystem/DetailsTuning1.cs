using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsTuning1 : MonoBehaviour {

	[System.Serializable]
	public class Cars
	{
		public string name;
		public Transform WheelFL;
		public Transform WheelFR;
		public Transform WheelRL;
		public Transform WheelRR;
		public Transform Body;
		public Transform Tuning;
		[Header("Default Position")]
		public Vector3 StockWheelFL;
		public Vector3 StockWheelFR;
		public Vector3 StockWheelRL;
		public Vector3 StockWheelRR;
		public Vector3 StockBody;
		public Vector3 StockTuning;
	}
	public Cars[] car;
	public CarSelector CarSelectorScript;
	public int CarSelected;
	public int PreviousCarSelected;
	[Header ("Audio")]
	public AudioSource AuSource;
	public AudioClip ButtonSound;
	[Header("Debug")]
	public float FrontCamber;
	public float BackCamber;
	public float FrontOffset;
	public float BackOffset;
	public float FrontHeight;
	public float BackHeight;

	void Start(){
		//get car selected from carselector script //change TEST to Carselector script name also do it in update function
		CarSelectorScript = gameObject.GetComponent<CarSelector>();
		CarSelected = CarSelectorScript.CarSelected;
		//set car selected
		PreviousCarSelected = CarSelected;
		//get enclosed audiosource if it exist
		AuSource=gameObject.GetComponent<AudioSource>();
		//load parts and show part function
		Load();
	}

	void Update(){
		//get car selected from carselector script //change TEST to Carselector script name
		CarSelected=CarSelectorScript.CarSelected;
		//chech if car is changed
		if (CarSelected != PreviousCarSelected){
			//set car selected
			PreviousCarSelected = CarSelected;
			//load if performances are unlocked
			Load();
		}
	}

	//camber voids
	public void SetFrontCamber(float Fcam){
		FrontCamber = Fcam;
		//camber
		car[CarSelected].WheelFL.localRotation= Quaternion.Euler(0,FrontCamber,0);
		car[CarSelected].WheelFR.localRotation= Quaternion.Euler(0,-(FrontCamber),0);
	}
	public void SetBackCamber(float Bcam){
		BackCamber = Bcam;
		//camber
		car[CarSelected].WheelRL.localRotation= Quaternion.Euler(0,BackCamber,0);
		car[CarSelected].WheelRR.localRotation= Quaternion.Euler(0,-(BackCamber),0);
	}
	//offset voids
	public void SetFrontOffSet(float Fset){
		FrontOffset = Fset;
		//offset
		car[CarSelected].WheelFL.localPosition= new Vector3(car[CarSelected].StockWheelFL.x + FrontOffset, car[CarSelected].StockWheelFL.y, car[CarSelected].StockWheelFL.z);
		car[CarSelected].WheelFR.localPosition= new Vector3(car[CarSelected].StockWheelFR.x - FrontOffset, car[CarSelected].StockWheelFR.y, car[CarSelected].StockWheelFR.z);
	}
	public void SetBackOffSet(float Bset){
		BackOffset = Bset;
		//offset
		car[CarSelected].WheelRL.localPosition= new Vector3(car[CarSelected].StockWheelRL.x + BackOffset, car[CarSelected].StockWheelRL.y, car[CarSelected].StockWheelRL.z);
		car[CarSelected].WheelRR.localPosition= new Vector3(car[CarSelected].StockWheelRR.x - BackOffset, car[CarSelected].StockWheelRR.y, car[CarSelected].StockWheelRR.z);
	}
	//height voids
	public void SetFrontHeight(float Fhei){
		FrontHeight = Fhei;
		//height
		car[CarSelected].Body.localPosition= new Vector3(car[CarSelected].StockBody.x, car[CarSelected].StockBody.y + FrontHeight, car[CarSelected].StockBody.z);
		car[CarSelected].Tuning.localPosition= new Vector3(car[CarSelected].StockTuning.x, car[CarSelected].StockTuning.y + FrontHeight, car[CarSelected].StockTuning.z);
	}
	public void SetBackHeight(float Bhei){
		BackHeight = Bhei;
	}

	void Load(){
		//load details
		FrontCamber=PlayerPrefs.GetFloat(car[CarSelected].name + "_FrontCamber");
		BackCamber=PlayerPrefs.GetFloat(car[CarSelected].name + "_BackCamber");
		FrontOffset=PlayerPrefs.GetFloat(car[CarSelected].name + "_FrontOffset");
		BackOffset=PlayerPrefs.GetFloat(car[CarSelected].name + "_BackOffset");
		FrontHeight=PlayerPrefs.GetFloat(car[CarSelected].name + "_FrontHeight");
		BackHeight=PlayerPrefs.GetFloat(car[CarSelected].name + "_BackHeight");
		//show details on car
		car[CarSelected].WheelFL.localRotation= Quaternion.Euler(0,FrontCamber,0);
		car[CarSelected].WheelFR.localRotation= Quaternion.Euler(0,-(FrontCamber),0);
		car[CarSelected].WheelRL.localRotation= Quaternion.Euler(0,BackCamber,0);
		car[CarSelected].WheelRR.localRotation= Quaternion.Euler(0,-(BackCamber),0);

		car[CarSelected].WheelFL.localPosition= new Vector3(car[CarSelected].StockWheelFL.x + FrontOffset, car[CarSelected].StockWheelFL.y, car[CarSelected].StockWheelFL.z);
		car[CarSelected].WheelFR.localPosition= new Vector3(car[CarSelected].StockWheelFR.x - FrontOffset, car[CarSelected].StockWheelFR.y, car[CarSelected].StockWheelFR.z);
		car[CarSelected].WheelRL.localPosition= new Vector3(car[CarSelected].StockWheelRL.x + BackOffset, car[CarSelected].StockWheelRL.y, car[CarSelected].StockWheelRL.z);
		car[CarSelected].WheelRR.localPosition= new Vector3(car[CarSelected].StockWheelRR.x - BackOffset, car[CarSelected].StockWheelRR.y, car[CarSelected].StockWheelRR.z);

		car[CarSelected].Body.localPosition= new Vector3(car[CarSelected].StockBody.x, car[CarSelected].StockBody.y + FrontHeight, car[CarSelected].StockBody.z);
		car[CarSelected].Tuning.localPosition= new Vector3(car[CarSelected].StockTuning.x, car[CarSelected].StockTuning.y + FrontHeight, car[CarSelected].StockTuning.z);
	}

	public void Back(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//save details
		PlayerPrefs.SetFloat(car[CarSelected].name + "_FrontCamber", FrontCamber);
		PlayerPrefs.SetFloat(car[CarSelected].name + "_BackCamber", BackCamber);
		PlayerPrefs.SetFloat(car[CarSelected].name + "_FrontOffset", FrontOffset);
		PlayerPrefs.SetFloat(car[CarSelected].name + "_BackOffset", BackOffset);
		PlayerPrefs.SetFloat(car[CarSelected].name + "_FrontHeight", FrontHeight);
		PlayerPrefs.SetFloat(car[CarSelected].name + "_BackHeight", BackHeight);
	}

}
