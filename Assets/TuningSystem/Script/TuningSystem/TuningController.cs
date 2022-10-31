using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TuningController : MonoBehaviour {
	//serializing tuning parts
	[System.Serializable]
	public class FrontBumperParts
	{	public GameObject FrontBumper;
		public int Price;
		public bool unlocked ;}
	[System.Serializable]
	public class RearBumperParts
	{	public GameObject RearBumper;
		public int Price;
		public bool unlocked ;}
	[System.Serializable]
	public class SideSkirtParts
	{	public GameObject SideSkirt;
		public int Price;
		public bool unlocked ;	}
	[System.Serializable]
	public class HoodsParts
	{	public GameObject Hood;
		public int Price;
		public bool unlocked = false;}
	[System.Serializable]
	public class RoofsParts
	{	public GameObject Roof;
		public int Price;
		public bool unlocked = false;}
	[System.Serializable]
	public class SpoilersParts
	{	public GameObject Spoiler;
		public int Price;
		public bool unlocked = false;}
	[System.Serializable]
	public class WheelsParts
	{	public GameObject WheelFR;
		public GameObject WheelRR;
		public GameObject WheelFL;
		public GameObject WheelRL;
		public int Price;
		public bool unlocked = false;}
	
	//Version 0.0.2
	//serializing cars
	[System.Serializable]
	public class TuningCars
	{
		[Header ("Details")]
		public string CarName;
		//tuning section
		[Header ("Tuning:")]
		[Header ("FrontBumper")]
		public FrontBumperParts[] FrontBumpers;
		[Header ("RearBumper")]
		public RearBumperParts[] RearBumpers;
		[Header ("SideSkirt")]
		public SideSkirtParts[] SideSkirts;
		[Header ("Hood")]
		public HoodsParts[] Hoods;
		[Header ("Roof")]
		public RoofsParts[] Roofs;
		[Header ("Spoilers")]
		public SpoilersParts[] spoilers;
		[Header ("Wheel")]
		public WheelsParts[] Wheels;
	}
	[Header ("Tuning cars")]
	public TuningCars[] TuningCar;
	//change test to carselect script name
	public CarSelector CarSelectorScript;
	public int TuningCarSelected;
	public int PreviousTuningCarSelected;
	[Header ("Tuning Category")]
	public int CategorySelected = 0;
	public Sprite[] CategoryImage;
	public Image CategoryImageUI;
	[Header ("")]
	public int FrontBumperSelected=0;
	public int RearBumperSelected=0;
	public int SideSkirtSelected=0;
	public int HoodSelected=0;
	public int RoofSelected=0;
	public int SpoilerSelected=0;
	public int WheelSelected=0;
	[Header ("PlayerStats"),Space(1)]
	public int Money=5000;
	public Text MoneyText;
	[Header ("UI")]
	public Text Locked;
	public Text PartPrice;
	public GameObject Buy;
	public GameObject Fit;
	public GameObject NotMoney;
	[Header ("Audio"),Space(1)]
	public AudioSource AuSource;
	public AudioClip ButtonSound;
	public AudioClip BuySound;
	public AudioClip NotMoneySound;
	public AudioClip[] FitSound;
	private int FSTP=0;
	[Header ("Image Effects")]
	public MonoBehaviour Blur;

	void Start(){
		//get car selected from carselector script //change TEST to Carselector script name also do it in update function
		CarSelectorScript = gameObject.GetComponent<CarSelector>();
		TuningCarSelected = CarSelectorScript.CarSelected;
		//set car selected
		PreviousTuningCarSelected = TuningCarSelected;
		//get enclosed audiosource if it exist
		AuSource=gameObject.GetComponent<AudioSource>();
		//load parts and show part function
		Load();
		//show front bumper------------------------------------------------------------------------------------------------------------
		for(int i = 0; i < TuningCar[TuningCarSelected].FrontBumpers.Length; i++) {
			GameObject FB = TuningCar[TuningCarSelected].FrontBumpers[i].FrontBumper;
			FB.SetActive (false);
		}

		TuningCar[TuningCarSelected].FrontBumpers[FrontBumperSelected].FrontBumper.SetActive (true);
		//show rearbumper------------------------------------------------------------------------------------------------------------
		for(int i = 0; i < TuningCar[TuningCarSelected].RearBumpers.Length; i++) {
			GameObject FB = TuningCar[TuningCarSelected].RearBumpers[i].RearBumper;
			FB.SetActive (false);
		}

		TuningCar[TuningCarSelected].RearBumpers[RearBumperSelected].RearBumper.SetActive (true);
		//show sideskirt------------------------------------------------------------------------------------------------------------
		for(int i = 0; i < TuningCar[TuningCarSelected].SideSkirts.Length; i++) {
			GameObject FB = TuningCar[TuningCarSelected].SideSkirts[i].SideSkirt;
			FB.SetActive (false);
		}

		TuningCar[TuningCarSelected].SideSkirts[SideSkirtSelected].SideSkirt.SetActive (true);
		//show hood------------------------------------------------------------------------------------------------------------
		for(int i = 0; i < TuningCar[TuningCarSelected].Hoods.Length; i++) {
			GameObject FB = TuningCar[TuningCarSelected].Hoods[i].Hood;
			FB.SetActive (false);
		}

		TuningCar[TuningCarSelected].Hoods[HoodSelected].Hood.SetActive (true);
		//show roof------------------------------------------------------------------------------------------------------------
		for(int i = 0; i < TuningCar[TuningCarSelected].Roofs.Length; i++) {
			GameObject FB = TuningCar[TuningCarSelected].Roofs[i].Roof;
			FB.SetActive (false);
		}

		TuningCar[TuningCarSelected].Roofs[RoofSelected].Roof.SetActive (true);
		//show spoiler------------------------------------------------------------------------------------------------------------
		for(int i = 0; i < TuningCar[TuningCarSelected].spoilers.Length; i++) {
			GameObject FB = TuningCar[TuningCarSelected].spoilers[i].Spoiler;
			FB.SetActive (false);
		}

		TuningCar[TuningCarSelected].spoilers[SpoilerSelected].Spoiler.SetActive (true);
		//show wheels------------------------------------------------------------------------------------------------------------
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelFL;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelFR;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelRL;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelRR;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelFL.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelFR.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelRL.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelRR.SetActive (true);
		//----------------------------------------------------------------------------------------------------------------
	}

	void Update(){
		//get car selected from carselector script //change TEST to Carselector script name
		TuningCarSelected=CarSelectorScript.CarSelected;
		//chech if car is changed
		if (TuningCarSelected != PreviousTuningCarSelected){
			//set car selected
			PreviousTuningCarSelected = TuningCarSelected;
			//set all to zero
			//-----------------------------------
			FrontBumperSelected=0;
			RearBumperSelected=0;
			SideSkirtSelected=0;
			HoodSelected=0;
			RoofSelected=0;
			SpoilerSelected=0;
			FrontBumperSelected=0;
			//-----------------------------------
			//load parts
			Load();
		}
		//load money
		Money=PlayerPrefs.GetInt ("Money",Money);
		//show money
		MoneyText.text = Money + " $";
		//set category image
		CategoryImageUI.sprite = CategoryImage [CategorySelected];

		//check if the selected part is unlocked
		//-------------------------------------------------------------------------------------FrontBumper
		if (CategorySelected==0){
			//check if locked
			if (TuningCar [TuningCarSelected].FrontBumpers [FrontBumperSelected].unlocked == false) {
				Locked.text="Locked";
				PartPrice.text = TuningCar [TuningCarSelected].FrontBumpers [FrontBumperSelected].Price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (TuningCar [TuningCarSelected].FrontBumpers [FrontBumperSelected].unlocked == true) {
				Locked.text="Unloked";
				PartPrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//-------------------------------------------------------------------------------------RearBumper
		if (CategorySelected==1){
			//check if locked
			if (TuningCar [TuningCarSelected].RearBumpers [RearBumperSelected].unlocked == false) {
				Locked.text="Locked";
				PartPrice.text = TuningCar [TuningCarSelected].RearBumpers [RearBumperSelected].Price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (TuningCar [TuningCarSelected].RearBumpers [RearBumperSelected].unlocked == true) {
				Locked.text="Unloked";
				PartPrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//-------------------------------------------------------------------------------------SideSkirt
		if (CategorySelected==2){
			//check if locked
			if (TuningCar [TuningCarSelected].SideSkirts [SideSkirtSelected].unlocked == false) {
				Locked.text="Locked";
				PartPrice.text = TuningCar [TuningCarSelected].SideSkirts [SideSkirtSelected].Price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (TuningCar [TuningCarSelected].SideSkirts [SideSkirtSelected].unlocked == true) {
				Locked.text="Unloked";
				PartPrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//-------------------------------------------------------------------------------------Hood
		if (CategorySelected==3){
			//check if locked
			if (TuningCar [TuningCarSelected].Hoods [HoodSelected].unlocked == false) {
				Locked.text="Locked";
				PartPrice.text = TuningCar [TuningCarSelected].Hoods [HoodSelected].Price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (TuningCar [TuningCarSelected].Hoods [HoodSelected].unlocked == true) {
				Locked.text="Unloked";
				PartPrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//-------------------------------------------------------------------------------------Roof
		if (CategorySelected==4){
			//check if locked
			if (TuningCar [TuningCarSelected].Roofs [RoofSelected].unlocked == false) {
				Locked.text="Locked";
				PartPrice.text = TuningCar [TuningCarSelected].Roofs [RoofSelected].Price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (TuningCar [TuningCarSelected].Roofs [RoofSelected].unlocked == true) {
				Locked.text="Unloked";
				PartPrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//-------------------------------------------------------------------------------------Spoiler
		if (CategorySelected==5){
			//check if locked
			if (TuningCar [TuningCarSelected].spoilers [SpoilerSelected].unlocked == false) {
				Locked.text="Locked";
				PartPrice.text = TuningCar [TuningCarSelected].spoilers [SpoilerSelected].Price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (TuningCar [TuningCarSelected].spoilers [SpoilerSelected].unlocked == true) {
				Locked.text="Unloked";
				PartPrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//-------------------------------------------------------------------------------------wheels
		if (CategorySelected==6){
			//check if locked
			if (TuningCar [TuningCarSelected].Wheels [WheelSelected].unlocked == false) {
				Locked.text="Locked";
				PartPrice.text = TuningCar [TuningCarSelected].Wheels [WheelSelected].Price.ToString() + " $";

				//show buy button
				Buy.SetActive(true);
				Fit.SetActive(false);
			}

			//check if unlocked
			if (TuningCar [TuningCarSelected].Wheels [WheelSelected].unlocked == true) {
				Locked.text="Unloked";
				PartPrice.text = "";

				//show select button
				Fit.SetActive(true);
				Buy.SetActive(false);
			}
		}
		//---------------------------------------------------------------------------------------------------------------------
	}

	public void NextTuningCategory(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//
		CategorySelected++;
		if (CategorySelected == CategoryImage.Length) {
			CategorySelected = 0;
		}
	}

	public void PreviousTuningCategory(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//
		CategorySelected--;
		if (CategorySelected == -1) {
			CategorySelected = CategoryImage.Length - 1;
		}
	}
		
	//void for change tuning parts
	public void NextTuningPart(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//------------------------------------------------------------------------------------------------------------------
		//change front bumper
		if (CategorySelected == 0) {
			FrontBumperSelected++;
			if (FrontBumperSelected >= TuningCar [TuningCarSelected].FrontBumpers.Length) {
				FrontBumperSelected=0;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].FrontBumpers.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].FrontBumpers [i].FrontBumper;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].FrontBumpers [FrontBumperSelected].FrontBumper.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change rear bumper
		if (CategorySelected == 1) {
			RearBumperSelected++;
			if (RearBumperSelected >= TuningCar [TuningCarSelected].RearBumpers.Length) {
				RearBumperSelected=0;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].RearBumpers.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].RearBumpers [i].RearBumper;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].RearBumpers [RearBumperSelected].RearBumper.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change sideskirt
		if (CategorySelected == 2) {
			SideSkirtSelected++;
			if (SideSkirtSelected >= TuningCar [TuningCarSelected].SideSkirts.Length) {
				SideSkirtSelected=0;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].SideSkirts.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].SideSkirts [i].SideSkirt;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].SideSkirts [SideSkirtSelected].SideSkirt.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change hood
		if (CategorySelected == 3) {
			HoodSelected++;
			if (HoodSelected >= TuningCar [TuningCarSelected].Hoods.Length) {
				HoodSelected=0;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].Hoods.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].Hoods [i].Hood;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Hoods [HoodSelected].Hood.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change roof
		if (CategorySelected == 4) {
			RoofSelected++;
			if (RoofSelected >= TuningCar [TuningCarSelected].Roofs.Length) {
				RoofSelected=0;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].Roofs.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].Roofs [i].Roof;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Roofs [RoofSelected].Roof.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change spoiler
		if (CategorySelected == 5) {
			SpoilerSelected++;
			if (SpoilerSelected >= TuningCar [TuningCarSelected].spoilers.Length) {
				SpoilerSelected=0;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].spoilers.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].spoilers [i].Spoiler;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].spoilers [SpoilerSelected].Spoiler.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change wheel
		if (CategorySelected == 6) {
			WheelSelected++;
			if (WheelSelected >= TuningCar [TuningCarSelected].Wheels.Length) {
				WheelSelected=0;
			}
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelFL;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelFR;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelRL;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelRR;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelFL.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelFR.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelRL.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelRR.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------

	}
	public void PreviousTuningPart(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();

		//change front bumper
		if (CategorySelected == 0) {
			FrontBumperSelected--;
			if (FrontBumperSelected <= -1) {
				FrontBumperSelected=TuningCar [TuningCarSelected].FrontBumpers.Length -1;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].FrontBumpers.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].FrontBumpers [i].FrontBumper;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].FrontBumpers [FrontBumperSelected].FrontBumper.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change rear bumper
		if (CategorySelected == 1) {
			RearBumperSelected--;
			if (RearBumperSelected <= -1) {
				RearBumperSelected=TuningCar [TuningCarSelected].RearBumpers.Length-1;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].RearBumpers.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].RearBumpers [i].RearBumper;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].RearBumpers [RearBumperSelected].RearBumper.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change sideskirt
		if (CategorySelected == 2) {
			SideSkirtSelected--;
			if (SideSkirtSelected <= -1) {
				SideSkirtSelected=TuningCar [TuningCarSelected].SideSkirts.Length-1;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].SideSkirts.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].SideSkirts [i].SideSkirt;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].SideSkirts [SideSkirtSelected].SideSkirt.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change hood
		if (CategorySelected == 3) {
			HoodSelected--;
			if (HoodSelected <= -1) {
				HoodSelected=TuningCar [TuningCarSelected].Hoods.Length-1;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].Hoods.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].Hoods [i].Hood;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Hoods [HoodSelected].Hood.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change roof
		if (CategorySelected == 4) {
			RoofSelected--;
			if (RoofSelected <= -1) {
				RoofSelected=TuningCar [TuningCarSelected].Roofs.Length-1;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].Roofs.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].Roofs [i].Roof;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Roofs [RoofSelected].Roof.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change spoiler
		if (CategorySelected == 5) {
			SpoilerSelected--;
			if (SpoilerSelected <= -1) {
				SpoilerSelected=TuningCar [TuningCarSelected].spoilers.Length-1;
			}
		}
		for(int i = 0; i <TuningCar [TuningCarSelected].spoilers.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].spoilers [i].Spoiler;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].spoilers [SpoilerSelected].Spoiler.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------
		//change wheel
		if (CategorySelected == 6) {
			WheelSelected--;
			if (WheelSelected <= -1) {
				WheelSelected=TuningCar [TuningCarSelected].Wheels.Length-1;
			}
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelFL;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelFR;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelRL;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelRR;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelFL.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelFR.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelRL.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelRR.SetActive (true);
		//------------------------------------------------------------------------------------------------------------------

	}

	public void BuyPart(){
		//check if money are enought
		//-------------------------------------------------------------------------------------FrontBumper
		if (CategorySelected == 0) {
			if (TuningCar [TuningCarSelected].FrontBumpers [FrontBumperSelected].Price <= Money) {
				//buy the car
				Money = Money - TuningCar [TuningCarSelected].FrontBumpers [FrontBumperSelected].Price;
				PlayerPrefs.SetInt ("Money", Money);
				TuningCar [TuningCarSelected].FrontBumpers [FrontBumperSelected].unlocked = true;
				//save that player bought the car , setting to 1 "carUnlocked" save box
				PlayerPrefs.SetInt (TuningCar[TuningCarSelected].CarName + "_FrontBumper_" + FrontBumperSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//enable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
		//-------------------------------------------------------------------------------------rearbumper
		if (CategorySelected == 1) {
			if (TuningCar [TuningCarSelected].RearBumpers [RearBumperSelected].Price <= Money) {
				//buy the car
				Money = Money - TuningCar [TuningCarSelected].RearBumpers [RearBumperSelected].Price;
				PlayerPrefs.SetInt ("Money", Money);
				TuningCar [TuningCarSelected].RearBumpers [RearBumperSelected].unlocked = true;
				//save that player bought the car , setting to 1 "carUnlocked" save box
				PlayerPrefs.SetInt (TuningCar[TuningCarSelected].CarName + "_RearBumper_" + RearBumperSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//enable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
		//-------------------------------------------------------------------------------------sideskirt
		if (CategorySelected == 2) {
			if (TuningCar [TuningCarSelected].SideSkirts [SideSkirtSelected].Price <= Money) {
				//buy the car
				Money = Money - TuningCar [TuningCarSelected].SideSkirts [SideSkirtSelected].Price;
				PlayerPrefs.SetInt ("Money", Money);
				TuningCar [TuningCarSelected].SideSkirts [SideSkirtSelected].unlocked = true;
				//save that player bought the car , setting to 1 "carUnlocked" save box
				PlayerPrefs.SetInt (TuningCar[TuningCarSelected].CarName + "_SideSkirt_" + SideSkirtSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//enable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
		//-------------------------------------------------------------------------------------hood
		if (CategorySelected == 3) {
			if (TuningCar [TuningCarSelected].Hoods [HoodSelected].Price <= Money) {
				//buy the car
				Money = Money - TuningCar [TuningCarSelected].Hoods [HoodSelected].Price;
				PlayerPrefs.SetInt ("Money", Money);
				TuningCar [TuningCarSelected].Hoods [HoodSelected].unlocked = true;
				//save that player bought the car , setting to 1 "carUnlocked" save box
				PlayerPrefs.SetInt (TuningCar[TuningCarSelected].CarName + "_Hood_" + HoodSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//enable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
		//-------------------------------------------------------------------------------------roof
		if (CategorySelected == 4) {
			if (TuningCar [TuningCarSelected].Roofs [RoofSelected].Price <= Money) {
				//buy the car
				Money = Money - TuningCar [TuningCarSelected].Roofs [RoofSelected].Price;
				PlayerPrefs.SetInt ("Money", Money);
				TuningCar [TuningCarSelected].Roofs [RoofSelected].unlocked = true;
				//save that player bought the car , setting to 1 "carUnlocked" save box
				PlayerPrefs.SetInt (TuningCar[TuningCarSelected].CarName + "_Roof_" + RoofSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//enable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
		//-------------------------------------------------------------------------------------spoiler
		if (CategorySelected == 5) {
			if (TuningCar [TuningCarSelected].spoilers [SpoilerSelected].Price <= Money) {
				//buy the car
				Money = Money - TuningCar [TuningCarSelected].spoilers [SpoilerSelected].Price;
				PlayerPrefs.SetInt ("Money", Money);
				TuningCar [TuningCarSelected].spoilers [SpoilerSelected].unlocked = true;
				//save that player bought the car , setting to 1 "carUnlocked" save box
				PlayerPrefs.SetInt (TuningCar[TuningCarSelected].CarName + "_Spoiler_" + SpoilerSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//enable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
		//-------------------------------------------------------------------------------------wheel
		if (CategorySelected == 6) {
			if (TuningCar [TuningCarSelected].Wheels [WheelSelected].Price <= Money) {
				//buy the car
				Money = Money - TuningCar [TuningCarSelected].Wheels [WheelSelected].Price;
				PlayerPrefs.SetInt ("Money", Money);
				TuningCar [TuningCarSelected].Wheels [WheelSelected].unlocked = true;
				//save that player bought the car , setting to 1 "carUnlocked" save box
				PlayerPrefs.SetInt (TuningCar[TuningCarSelected].CarName + "_Spoiler_" + WheelSelected, 1);
				//update UI
				Buy.SetActive (false);
				Fit.SetActive (true);
				//play sound
				AuSource.clip = BuySound;
				AuSource.Play ();
			} else {
				//not enought money
				NotMoney.SetActive (true);
				//enable blur
				Blur.enabled=true;
				//play sound
				AuSource.clip = NotMoneySound;
				AuSource.Play ();
			}
		}
		//------------------------------------------------------------------------------------------------------------------------
	}

	public void SelectTuningPart(){
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
			PlayerPrefs.SetInt (TuningCarSelected +"FrontBumperSelected", FrontBumperSelected);
		}
		//save front bumper selected
		if (CategorySelected == 1) {
			PlayerPrefs.SetInt (TuningCarSelected +"RearBumperSelected", RearBumperSelected);
		}
		//save front bumper selected
		if (CategorySelected == 2) {
			PlayerPrefs.SetInt (TuningCarSelected +"SideSkirtSelected", SideSkirtSelected);
		}
		//save front bumper selected
		if (CategorySelected == 3) {
			PlayerPrefs.SetInt (TuningCarSelected +"HoodSelected", HoodSelected);
		}
		//save front bumper selected
		if (CategorySelected == 4) {
			PlayerPrefs.SetInt (TuningCarSelected +"RoofSelected", RoofSelected);
		}
		//save front bumper selected
		if (CategorySelected == 5) {
			PlayerPrefs.SetInt (TuningCarSelected +"SpoilerSelected", SpoilerSelected);
		}
		//save front bumper selected
		if (CategorySelected == 6) {
			PlayerPrefs.SetInt (TuningCarSelected +"WheelSelected", WheelSelected);
		}
		//-------------------------------------------------------------------------------------------------------------------
	}

	public void Back(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//load
		Load();
		//show parts fitted--------------------------------------------------------------------------------------------------
		//front bumper
		TuningCar[TuningCarSelected].FrontBumpers[FrontBumperSelected].FrontBumper.SetActive(true);
		//rear bumper
		TuningCar[TuningCarSelected].RearBumpers[RearBumperSelected].RearBumper.SetActive(true);
		//sideskirt
		TuningCar[TuningCarSelected].SideSkirts[SideSkirtSelected].SideSkirt.SetActive(true);
		//hood
		TuningCar[TuningCarSelected].Hoods[HoodSelected].Hood.SetActive(true);
		//roof
		TuningCar[TuningCarSelected].Roofs[RoofSelected].Roof.SetActive(true);
		//spoiler
		TuningCar[TuningCarSelected].spoilers[SpoilerSelected].Spoiler.SetActive(true);
		//wheels
		TuningCar[TuningCarSelected].Wheels[WheelSelected].WheelFL.SetActive(true);
		TuningCar[TuningCarSelected].Wheels[WheelSelected].WheelFR.SetActive(true);
		TuningCar[TuningCarSelected].Wheels[WheelSelected].WheelRL.SetActive(true);
		TuningCar[TuningCarSelected].Wheels[WheelSelected].WheelRR.SetActive(true);
		//---------------------------------------------------------------------------------------------------------------------
	}

	void Load(){
		//load is only for unlocked information
		//load and show if frontbumpers are unlocked
		for(int i = 0; i < TuningCar[TuningCarSelected].FrontBumpers.Length; i++) {
			//if car is unlocked load it
			if (PlayerPrefs.GetInt (TuningCar[TuningCarSelected].CarName + "_FrontBumper_" + i) == 1)
			{
				TuningCar[TuningCarSelected].FrontBumpers[i].unlocked = true;
			}
			//else set it locked
			else {
				TuningCar[TuningCarSelected].FrontBumpers[i].unlocked = false;
			}
		}
		//load front bumper selected
		FrontBumperSelected = PlayerPrefs.GetInt(TuningCarSelected + "FrontBumperSelected",0);
		//--------------------------------------------------------------------------------------------------------------
		//load and show if rearbumpers are unlocked
		for(int i = 0; i < TuningCar[TuningCarSelected].RearBumpers.Length; i++) {
			//if car is unlocked load it
			if (PlayerPrefs.GetInt (TuningCar[TuningCarSelected].CarName + "_RearBumper_" + i) == 1)
			{
				TuningCar[TuningCarSelected].RearBumpers[i].unlocked = true;
			}
			//else set it locked
			else {
				TuningCar[TuningCarSelected].RearBumpers[i].unlocked = false;
			}
		}
		//load front bumper selected
		RearBumperSelected = PlayerPrefs.GetInt(TuningCarSelected +"RearBumperSelected",0);
		//--------------------------------------------------------------------------------------------------------------
		//load and show if sideskirt are unlocked
		for(int i = 0; i < TuningCar[TuningCarSelected].SideSkirts.Length; i++) {
			//if car is unlocked load it
			if (PlayerPrefs.GetInt (TuningCar[TuningCarSelected].CarName + "_SideSkirt_" + i) == 1)
			{
				TuningCar[TuningCarSelected].SideSkirts[i].unlocked = true;
			}
			//else set it locked
			else {
				TuningCar[TuningCarSelected].SideSkirts[i].unlocked = false;
			}
		}
		//load sideskirt selected
		SideSkirtSelected = PlayerPrefs.GetInt(TuningCarSelected +"SideSkirtSelected",0);
		//--------------------------------------------------------------------------------------------------------------
		//load and show if hood are unlocked
		for(int i = 0; i < TuningCar[TuningCarSelected].Hoods.Length; i++) {
			//if car is unlocked load it
			if (PlayerPrefs.GetInt (TuningCar[TuningCarSelected].CarName + "_Hood_" + i) == 1)
			{
				TuningCar[TuningCarSelected].Hoods[i].unlocked = true;
			}
			//else set it locked
			else {
				TuningCar[TuningCarSelected].Hoods[i].unlocked = false;
			}
		}
		//load hood selected
		HoodSelected = PlayerPrefs.GetInt(TuningCarSelected +"HoodSelected",0);
		//--------------------------------------------------------------------------------------------------------------
		//load and show if roof are unlocked
		for(int i = 0; i < TuningCar[TuningCarSelected].Roofs.Length; i++) {
			//if car is unlocked load it
			if (PlayerPrefs.GetInt (TuningCar[TuningCarSelected].CarName + "_Roof_" + i) == 1)
			{
				TuningCar[TuningCarSelected].Roofs[i].unlocked = true;
			}
			//else set it locked
			else {
				TuningCar[TuningCarSelected].Roofs[i].unlocked = false;
			}
		}
		//load roof selected
		RoofSelected = PlayerPrefs.GetInt(TuningCarSelected +"RoofSelected",0);
		//--------------------------------------------------------------------------------------------------------------
		//load and show if spoiler are unlocked
		for(int i = 0; i < TuningCar[TuningCarSelected].spoilers.Length; i++) {
			//if car is unlocked load it
			if (PlayerPrefs.GetInt (TuningCar[TuningCarSelected].CarName + "_Spoiler_" + i) == 1)
			{
				TuningCar[TuningCarSelected].spoilers[i].unlocked = true;
			}
			//else set it locked
			else {
				TuningCar[TuningCarSelected].spoilers[i].unlocked = false;
			}
		}
		//load spoiler selected
		SpoilerSelected = PlayerPrefs.GetInt(TuningCarSelected +"SpoilerSelected",0);
		//--------------------------------------------------------------------------------------------------------------
		//load and show if wheels are unlocked
		for(int i = 0; i < TuningCar[TuningCarSelected].Wheels.Length; i++) {
			//if car is unlocked load it
			if (PlayerPrefs.GetInt (TuningCar[TuningCarSelected].CarName + "_Wheels_" + i) == 1)
			{
				TuningCar[TuningCarSelected].Wheels[i].unlocked = true;
			}
			//else set it locked
			else {
				TuningCar[TuningCarSelected].Wheels[i].unlocked = false;
			}
		}
		//load wheels selected
		WheelSelected = PlayerPrefs.GetInt(TuningCarSelected +"WheelSelected",0);
		//--------------------------------------------------------------------------------------------------------------
		//start showparts function
		ShowParts();
	}

	void ShowParts(){
		//load parts update the parts showed on screen------------------------------------------------------
		//------------------------------------------------FRONT_BUMPER
		for(int i = 0; i <TuningCar [TuningCarSelected].FrontBumpers.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].FrontBumpers [i].FrontBumper;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].FrontBumpers [FrontBumperSelected].FrontBumper.SetActive (true);
		//------------------------------------------------REAR_BUMPER
		for(int i = 0; i <TuningCar [TuningCarSelected].RearBumpers.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].RearBumpers [i].RearBumper;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].RearBumpers [RearBumperSelected].RearBumper.SetActive (true);
		//------------------------------------------------SIDESKIRT
		for(int i = 0; i <TuningCar [TuningCarSelected].SideSkirts.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].SideSkirts [i].SideSkirt;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].SideSkirts [SideSkirtSelected].SideSkirt.SetActive (true);
		//------------------------------------------------HOOD
		for(int i = 0; i <TuningCar [TuningCarSelected].Hoods.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].Hoods [i].Hood;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Hoods [HoodSelected].Hood.SetActive (true);
		//------------------------------------------------ROOF
		for(int i = 0; i <TuningCar [TuningCarSelected].Roofs.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].Roofs [i].Roof;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Roofs [RoofSelected].Roof.SetActive (true);
		//------------------------------------------------SPOILER
		for(int i = 0; i <TuningCar [TuningCarSelected].spoilers.Length ; i++) {
			GameObject part = TuningCar [TuningCarSelected].spoilers [i].Spoiler;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].spoilers [SpoilerSelected].Spoiler.SetActive (true);
		//------------------------------------------------WHEELS
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelFL;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelFR;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelRL;
			part.SetActive (false);
		}
		for (int i = 0; i < TuningCar [TuningCarSelected].Wheels.Length; i++) {
			GameObject part = TuningCar [TuningCarSelected].Wheels [i].WheelRR;
			part.SetActive (false);
		}
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelFL.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelFR.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelRL.SetActive (true);
		TuningCar [TuningCarSelected].Wheels [WheelSelected].WheelRR.SetActive (true);
		//--------------------------------------------------------------------------------------
	}

	public void NotMoneyBack(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();

		//disable blur
		Blur.enabled=false;
	}

}