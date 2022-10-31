using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarRepairSystem : MonoBehaviour {
	[System.Serializable]
	public class Cars
	{
		public string CarName;
		public float CarLife;
		public int[] DamagePrice;
		[Header ("3D Damage Models")]
		public GameObject Damage;
	}

	public Cars[] Car;
	public CarSelector CarSelectorScript;
	public int CarSelected;
	public int PreviousCarSelected;
	[Header ("PlayerStats")]
	public int Money=5000;
	public int DamageLevel;
	[Header("UI Elements")]
	public GameObject CarDamaged;
	public GameObject MainMenu;
	public Text PlayerMoney;
	public Text RepairPrice;
	[Header ("Audio")]
	public AudioSource AuSource;
	public AudioClip ButtonSound;
	public AudioClip RepairSound;
	public AudioClip NotMoneySound;

	void Start(){
		//get car selected from carselector script 
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

	void Load(){
		//load player life
		Money=PlayerPrefs.GetInt("Money");
		PlayerMoney.text = Money.ToString ();
		//load car life
		Car [CarSelected].CarLife = PlayerPrefs.GetFloat (Car [CarSelected].CarName + "_Life",100);
		//check car life
		if (Car [CarSelected].CarLife<90f){
			CarDamaged.SetActive (true); Car[CarSelected].Damage.SetActive (true); MainMenu.SetActive (false);
			//car life 90:50
			if(Car [CarSelected].CarLife<=90f && Car [CarSelected].CarLife>50f){
				DamageLevel = 0;
			}
			//car life 50:50
			if(Car [CarSelected].CarLife<=50f && Car [CarSelected].CarLife>25f){
				DamageLevel = 1;
			}
			//car life 90:50
			if(Car [CarSelected].CarLife<=25f && Car [CarSelected].CarLife>=0f){
				DamageLevel = 2;
			}
		}
		//show damage price
		RepairPrice.text=Car [CarSelected].DamagePrice[DamageLevel].ToString();
	}

	public void Back(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();

		CarDamaged.SetActive (false); Car[CarSelected].Damage.SetActive (false); MainMenu.SetActive (false);
	}

	public void Repair(){
		//load money
		Money=PlayerPrefs.GetInt("Money");
		PlayerMoney.text = Money.ToString ();
		//repair the car if money are enought
		if (Money >= Car [CarSelected].DamagePrice [DamageLevel]) {
			//play sound
			AuSource.clip = RepairSound;
			AuSource.Play ();
			//remove money
			Money = Money - Car [CarSelected].DamagePrice [DamageLevel];
			//save money
			PlayerPrefs.SetInt ("Money", Money);
			//repair the car
			Car [CarSelected].CarLife = 100;
			PlayerPrefs.SetFloat (Car [CarSelected].CarName + "_Life", Car [CarSelected].CarLife);
		} else {
			//play sound
			AuSource.clip = NotMoneySound;
			AuSource.Play ();
		}
	}



}
