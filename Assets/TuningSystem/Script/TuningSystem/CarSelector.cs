using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour {


	[System.Serializable]
	public class Cars
	{
		[Header ("Details")]
		public string name;
		public GameObject carObj;
		public int price;
		public bool Unlocked;
		[Header ("Stats")] //only for UI purpose
		[Range(0f,1f)] public float Torque;
		[Range(0f,1f)] public float HorsePower;
		[Range(0f,1f)] public float BrakeTorque;
	}

	[Header ("Cars"),Space(1)]
	public Cars[] car;
	[Header ("UI"),Space(1)]
	public Text Locked;
	public Text CarName;
	public Text CarPrice;
	public GameObject Buy;
	public GameObject Select;
	public GameObject NotMoney;
	//car stats
	public Slider CarTorqueUI;
	public Slider CarHpUI;
	public Slider CarBrakeUI;
	[Header ("Details"),Space(1)]
	public int CarSelected;
	public int TotalCar;
	[Header ("PlayerStats"),Space(1)]
	public int StartMoney=50000;
	public int Money=5000;
	public Text MoneyText;
	[Header ("Audio"),Space(1)]
	public AudioSource AuSource;
	public AudioClip ButtonSound;
	public AudioClip BuySound;
	public AudioClip NotMoneySound;
	[Header ("Image Effects")]
	public MonoBehaviour Blur;

	void Start(){
		//get array leght
		TotalCar=car.Length;

		//show first car details UI
		CarName.text=car [CarSelected].name;
		CarPrice.text = car [CarSelected].price.ToString()+ " $";

		//get enclosed audiosource if it exist
		AuSource=gameObject.GetComponent<AudioSource>();

		//load function called only at the start
		Load();
	}


	void Update(){

		//car selection
		for(int i = 0; i < TotalCar; i++) {
			GameObject cars = car[i].carObj;
			cars.SetActive (false);
		}

		car [CarSelected].carObj.SetActive (true);

		//check if locked
		if (car [CarSelected].Unlocked == false) {
			Locked.text="Locked";
			CarPrice.text = car [CarSelected].price.ToString() + " $";

			//show buy button
			Buy.SetActive(true);
			Select.SetActive(false);
		}

		//check if unlocked
		if (car [CarSelected].Unlocked == true) {
			Locked.text="Unloked";
			CarPrice.text = "";

			//show select button
			Select.SetActive(true);
			Buy.SetActive(false);
		}

		//output car name
		CarName.text=car [CarSelected].name;

		//car selection input
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Previous ();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Next ();
		}

		//load money
		Money=PlayerPrefs.GetInt ("Money",StartMoney);
		//show money
		MoneyText.text = Money + " $";

		//Show Car Stats on UI
		CarTorqueUI.value = Mathf.Lerp( CarTorqueUI.value, car [CarSelected].Torque, Time.deltaTime * 3.0F);
		CarHpUI.value = Mathf.Lerp( CarHpUI.value, car [CarSelected].HorsePower, Time.deltaTime * 3.0F);
		CarBrakeUI.value = Mathf.Lerp( CarBrakeUI.value, car [CarSelected].BrakeTorque, Time.deltaTime * 3.0F);

	}

	public void Next (){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change car
		CarSelected++;
		if (CarSelected==TotalCar){
			CarSelected = 0;		
		}
	}


	public void Previous (){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change car
		CarSelected--;
		if (CarSelected==-1){
			CarSelected = TotalCar-1;		
		}
	}

	//generic function for button sound
	public void genBut(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();
	}

	public void BuyFunc(){
		//check if money are enought
		if (car [CarSelected].price <= Money) {
			//buy the car
			Money = Money - car [CarSelected].price;
			PlayerPrefs.SetInt ("Money", Money);
			car [CarSelected].Unlocked = true;
			//save that player bought the car , setting to 1 "carUnlocked" save box
			PlayerPrefs.SetInt (CarSelected + "CarUnlocked",1);
			//update UI
			Buy.SetActive (false);
			Select.SetActive (true);
			//play sound
			AuSource.clip=BuySound;
			AuSource.Play ();
		} 
		else {
			//not enought money
			NotMoney.SetActive (true);
			//active blur
			Blur.enabled=true;
			//play sound
			AuSource.clip=NotMoneySound;
			AuSource.Play ();
		}

	}

	public void SelectFunc(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();

		//save car selected
		PlayerPrefs.SetInt("CarSelected",CarSelected);
		//show car selected
		for(int i = 0; i < TotalCar; i++) {
			GameObject cars = car[i].carObj;
			cars.SetActive (false);
		}
		car [CarSelected].carObj.SetActive (true);
	}

	public void BackFunc(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();

		//load car selected
		CarSelected = PlayerPrefs.GetInt ("CarSelected");
		//show car selected
		for(int i = 0; i < TotalCar; i++) {
			GameObject cars = car[i].carObj;
			cars.SetActive (false);
		}
		car [CarSelected].carObj.SetActive (true);
	}

	void Load(){
		//LOAD ALL DATA - Called only at the start

		//load car selected
		CarSelected = PlayerPrefs.GetInt ("CarSelected");

		//load if cars are unlocked
		for(int i = 0; i < TotalCar; i++) {
			//if car is unlocked load it
			if (PlayerPrefs.GetInt (i + "CarUnlocked") == 1)
			{
				car [i].Unlocked = true;
			}
			//else set it locked
			else {
				car [i].Unlocked = false;
			}
		}

	}

	public void NotMoneyBack(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//active blur
		Blur.enabled=false;
	}

}
