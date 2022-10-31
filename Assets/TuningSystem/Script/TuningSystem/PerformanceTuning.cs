using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceTuning : MonoBehaviour {
	//tuning
	[System.Serializable]
	public class Engine{
		public int price;
		public float HP;
		public bool Bought;
	}
	[System.Serializable]
	public class Turbo{
		public int price;
		public float NM;
		public bool Bought;
	}
	[System.Serializable]
	public class Brake{
		public int price;
		public float BNM;
		public bool Bought;
	}
	//cars
	[System.Serializable]
	public class Cars{
		public string CarName;
		public Engine[] CarEngine;
		public Turbo[] CarTurbo;
		public Brake[] CarBrake;
		[Header ("Stats")] //only for UI purpose
		[Tooltip ("Must be the same of car selector!")]
		[Range(0f,1f)] public float Torque;
		[Tooltip ("Must be the same of car selector!")]
		[Range(0f,1f)] public float HorsePower;
		[Tooltip ("Must be the same of car selector!")]
		[Range(0f,1f)] public float BrakeTorque;
	}
	//inspector
	public Cars[] car;
	[Header("UI")]
	public Text LevelText;
	public Text PerformancePrice;
	public GameObject Buy;
	public GameObject Fit;
	public GameObject NotMoney;
	public Slider CarTorqueUI;
	public Slider CarHpUI;
	public Slider CarBrakeUI;
	[Header ("Tuning Category")]
	public int CategorySelected = 0;
	public Sprite[] CategoryImage;
	public Image CategoryImageUI;
	public Image NextCategoryImageUI;
	public Image PreviuosCategoryImageUI;
	[Header ("Audio")]
	public AudioSource AuSource;
	public AudioClip ButtonSound;
	public AudioClip BuySound;
	public AudioClip NotMoneySound;
	public AudioClip[] FitSound;
	private int FSTP=0;
	[Header("Debug:")]
	public CarSelector CarSelectorScript;
	public int CarSelected;
	public int PreviousCarSelected;
	[Header("Performance")]
	public int EngineLevelSelected;
	public int TurboLevelSelected;
	public int BrakeLevelSelected;
	[Header ("PlayerStats")]
	public int Money=5000;
	public Text MoneyText;
	[Header ("Image Effects")]
	public MonoBehaviour Blur;

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
		//load money
		Money=PlayerPrefs.GetInt ("Money",Money);
		//show money
		MoneyText.text = Money + " $";
		//check if parts are boughted
		//-------------------------------------------------------------------------------------Engine
		if (CategorySelected==0){
			//check if engine is locked
			if (car [CarSelected].CarEngine [EngineLevelSelected].Bought == false) {
				PerformancePrice.text = car [CarSelected].CarEngine [EngineLevelSelected].price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (car [CarSelected].CarEngine [EngineLevelSelected].Bought == true) {
				PerformancePrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//-------------------------------------------------------------------------------------Turbo
		if (CategorySelected==1){
			//check if Turbo is locked
			if (car [CarSelected].CarTurbo [TurboLevelSelected].Bought == false) {
				PerformancePrice.text = car [CarSelected].CarTurbo [TurboLevelSelected].price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (car [CarSelected].CarTurbo [TurboLevelSelected].Bought == true) {
				PerformancePrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//-------------------------------------------------------------------------------------Brake
		if (CategorySelected==2){
			//check if Brake is locked
			if (car [CarSelected].CarBrake [BrakeLevelSelected].Bought == false) {
				PerformancePrice.text = car [CarSelected].CarBrake [BrakeLevelSelected].price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (car [CarSelected].CarBrake [BrakeLevelSelected].Bought == true) {
				PerformancePrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//update car stats slider
		CarTorqueUI.value=CarSelectorScript.car[CarSelected].Torque;
		CarHpUI.value=CarSelectorScript.car[CarSelected].HorsePower;
		CarBrakeUI.value=CarSelectorScript.car[CarSelected].BrakeTorque;
	}

	void Load(){
		//load is only for unlocked information
		//------------------------------------------------------------------------------------Engine
		//load engine level 
		EngineLevelSelected=PlayerPrefs.GetInt(CarSelected +"_EngineLevelSelected_");
		//check if engine is unlocked
		for(int i = 0; i <car[CarSelected].CarEngine.Length ; i++) {
			if (PlayerPrefs.GetInt (car[CarSelected].CarName + "_EngineUnlocked_" + i) == 1) car [CarSelected].CarEngine [i].Bought = true;
			else car [CarSelected].CarEngine [i].Bought = false;
		}
		//------------------------------------------------------------------------------------Turbo
		//load Turbo level 
		TurboLevelSelected=PlayerPrefs.GetInt(CarSelected +"_TurboLevelSelected_");
		//check if Turbo is unlocked
		for(int i = 0; i <car[CarSelected].CarTurbo.Length ; i++) {
			if (PlayerPrefs.GetInt (car[CarSelected].CarName + "_TurboUnlocked_" + i) == 1) car [CarSelected].CarTurbo [i].Bought = true;
			else car [CarSelected].CarTurbo [i].Bought = false;
		}
		//------------------------------------------------------------------------------------Brake
		//load Brake level 
		BrakeLevelSelected=PlayerPrefs.GetInt(CarSelected +"_BrakeLevelSelected_");
		//check if Brake is unlocked
		for(int i = 0; i <car[CarSelected].CarBrake.Length ; i++) {
			if (PlayerPrefs.GetInt (car[CarSelected].CarName + "_BrakeUnlocked_" + i) == 1) car [CarSelected].CarBrake [i].Bought = true;
			else car [CarSelected].CarBrake [i].Bought = false;
		}
		////show category level selected
		if(CategorySelected==0)LevelText.text = "Level " + (EngineLevelSelected + 1);
		if(CategorySelected==1)LevelText.text = "Level " + (TurboLevelSelected + 1);
		if(CategorySelected==2)LevelText.text = "Level " + (BrakeLevelSelected + 1);
		////load and update car stats
		CarSelectorScript.car [CarSelected].HorsePower = PlayerPrefs.GetFloat (car [CarSelected].CarName + "_EngineStats",CarSelectorScript.car [CarSelected].HorsePower);
		CarSelectorScript.car [CarSelected].Torque = PlayerPrefs.GetFloat (car [CarSelected].CarName + "_TurboStats",CarSelectorScript.car [CarSelected].Torque);
		CarSelectorScript.car [CarSelected].BrakeTorque = PlayerPrefs.GetFloat (car [CarSelected].CarName + "_BrakeStats",CarSelectorScript.car [CarSelected].BrakeTorque);
	}

	public void NextCategory(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change category
		CategorySelected++;
		if (CategorySelected >= CategoryImage.Length) CategorySelected = 0;
		//show updated UI category info
		CategoryImageUI.sprite = CategoryImage [CategorySelected];
		//show previous and next category UI
		if (CategorySelected + 1 < 3)	NextCategoryImageUI.sprite = CategoryImage [CategorySelected + 1];
		else NextCategoryImageUI.sprite = CategoryImage [0];
		if (CategorySelected - 1 > -1)	PreviuosCategoryImageUI.sprite = CategoryImage [CategorySelected - 1];
		else PreviuosCategoryImageUI.sprite = CategoryImage [CategoryImage.Length-1];
		//show category level selected
		if(CategorySelected==0)LevelText.text = "Level " + (EngineLevelSelected + 1);
		if(CategorySelected==1)LevelText.text = "Level " + (TurboLevelSelected + 1);
		if(CategorySelected==2)LevelText.text = "Level " + (BrakeLevelSelected + 1);
	}
	public void PreviousCategory(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change category
		CategorySelected--;
		if (CategorySelected <= -1) CategorySelected = CategoryImage.Length-1;
		//show updated UI category info
		CategoryImageUI.sprite = CategoryImage [CategorySelected];
		//show previous and next category UI
		if (CategorySelected + 1 < 3)	NextCategoryImageUI.sprite = CategoryImage [CategorySelected + 1];
		else NextCategoryImageUI.sprite = CategoryImage [0];
		if (CategorySelected - 1 > -1)	PreviuosCategoryImageUI.sprite = CategoryImage [CategorySelected - 1];
		else PreviuosCategoryImageUI.sprite = CategoryImage [CategoryImage.Length-1];
		//show category level selected
		if(CategorySelected==0)LevelText.text = "Level " + (EngineLevelSelected + 1);
		if(CategorySelected==1)LevelText.text = "Level " + (TurboLevelSelected + 1);
		if(CategorySelected==2)LevelText.text = "Level " + (BrakeLevelSelected + 1);
	}

	public void NextLevel(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//select level
		//Engine
		if (CategorySelected == 0) {
			EngineLevelSelected++;
			if (EngineLevelSelected >= car [CarSelected].CarEngine.Length)
				EngineLevelSelected = 0;
			LevelText.text = "Level " + (EngineLevelSelected + 1);
		}
		//Turbo
		if (CategorySelected == 1) {
			TurboLevelSelected++;
			if (TurboLevelSelected >= car [CarSelected].CarTurbo.Length)
				TurboLevelSelected = 0;
			LevelText.text = "Level " + (TurboLevelSelected + 1);
		}
		//Brake
		if (CategorySelected == 2) {
			BrakeLevelSelected++;
			if (BrakeLevelSelected >= car [CarSelected].CarBrake.Length)
				BrakeLevelSelected = 0;
			LevelText.text = "Level " + (BrakeLevelSelected + 1);
		}
	}

	public void PreviousLevel(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//select level
		//Engine
		if (CategorySelected == 0) {
			EngineLevelSelected--;
			if (EngineLevelSelected <= -1)
				EngineLevelSelected = car [CarSelected].CarEngine.Length -1;
			LevelText.text = "Level " + (EngineLevelSelected + 1);
		}
		//Turbo
		if (CategorySelected == 1) {
			TurboLevelSelected--;
			if (TurboLevelSelected <= -1)
				TurboLevelSelected = car [CarSelected].CarTurbo.Length -1;
			LevelText.text = "Level " + (TurboLevelSelected + 1);
		}
		//Brake
		if (CategorySelected == 2) {
			BrakeLevelSelected--;
			if (BrakeLevelSelected <= -1)
				BrakeLevelSelected = car [CarSelected].CarBrake.Length -1;
			LevelText.text = "Level " + (BrakeLevelSelected + 1);
		}
	}

	public void BuyPerformance(){
		//check if money are enought
		//-------------------------------------------------------------------------------------Engine
		if (CategorySelected == 0) {
			if (car[CarSelected].CarEngine[EngineLevelSelected].price <= Money) {
				//buy the paerformance
				Money = Money - car[CarSelected].CarEngine[EngineLevelSelected].price;
				PlayerPrefs.SetInt ("Money", Money);
				car[CarSelected].CarEngine[EngineLevelSelected].Bought = true;
				//save that player bought the performance , setting to 1 "performance" save box
				PlayerPrefs.SetInt (car[CarSelected].CarName + "_EngineUnlocked_" + EngineLevelSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//disable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
		//-------------------------------------------------------------------------------------Turbo
		if (CategorySelected == 1) {
			if (car[CarSelected].CarTurbo[TurboLevelSelected].price <= Money) {
				//buy the paerformance
				Money = Money - car[CarSelected].CarTurbo[TurboLevelSelected].price;
				PlayerPrefs.SetInt ("Money", Money);
				car[CarSelected].CarTurbo[TurboLevelSelected].Bought = true;
				//save that player bought the performance , setting to 1 "performance" save box
				PlayerPrefs.SetInt (car[CarSelected].CarName + "_TurboUnlocked_" + TurboLevelSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//disable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
		//-------------------------------------------------------------------------------------Brake
		if (CategorySelected == 2) {
			if (car[CarSelected].CarBrake[BrakeLevelSelected].price <= Money) {
				//buy the paerformance
				Money = Money - car[CarSelected].CarBrake[BrakeLevelSelected].price;
				PlayerPrefs.SetInt ("Money", Money);
				car[CarSelected].CarBrake[BrakeLevelSelected].Bought = true;
				//save that player bought the performance , setting to 1 "performance" save box
				PlayerPrefs.SetInt (car[CarSelected].CarName + "_BrakeUnlocked_" + BrakeLevelSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//disable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
	}

	public void FitPerformance(){
		//play sound
		AuSource.clip=FitSound[FSTP];
		AuSource.Play ();
		FSTP++;
		if (FSTP == FitSound.Length) {
			FSTP = 0;
		}
		//select and save tuning part-----------------------------------------------------------------------------------------------
		//save front bumper selected
		if (CategorySelected == 0) {
			PlayerPrefs.SetInt (CarSelected +"_EngineLevelSelected_", EngineLevelSelected);
			CarSelectorScript.car [CarSelected].HorsePower = car[CarSelected].HorsePower + car [CarSelected].CarEngine [EngineLevelSelected].HP;
			PlayerPrefs.SetFloat (car [CarSelected].CarName + "_EngineStats", CarSelectorScript.car [CarSelected].HorsePower);
		}
		if (CategorySelected == 1) {
			PlayerPrefs.SetInt (CarSelected +"_TurboLevelSelected_", TurboLevelSelected);
			CarSelectorScript.car [CarSelected].Torque = car[CarSelected].Torque + car [CarSelected].CarTurbo [TurboLevelSelected].NM;
			PlayerPrefs.SetFloat (car [CarSelected].CarName + "_TurboStats", CarSelectorScript.car [CarSelected].Torque);
		}
		if (CategorySelected == 2) {
			PlayerPrefs.SetInt (CarSelected +"_BrakeLevelSelected_", BrakeLevelSelected);
			CarSelectorScript.car [CarSelected].BrakeTorque = car[CarSelected].BrakeTorque + car [CarSelected].CarBrake [BrakeLevelSelected].BNM;
			PlayerPrefs.SetFloat (car [CarSelected].CarName + "_BrakeStats", CarSelectorScript.car [CarSelected].BrakeTorque);
		}
	}

	public void NotMoneyBack(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();

		//disable blur
		Blur.enabled=false;
	}

}