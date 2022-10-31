using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTuning : MonoBehaviour {

	//serializing tuning parts
	[System.Serializable]
	public class FrontBumperParts
	{	public GameObject FrontBumper;}
	[System.Serializable]
	public class RearBumperParts
	{	public GameObject RearBumper;}
	[System.Serializable]
	public class SideSkirtParts
	{	public GameObject SideSkirt;}
	[System.Serializable]
	public class HoodsParts
	{	public GameObject Hood;}
	[System.Serializable]
	public class RoofsParts
	{	public GameObject Roof;}
	[System.Serializable]
	public class SpoilersParts
	{	public GameObject Spoiler;}
	[System.Serializable]
	public class WheelsParts
	{	public GameObject WheelFR;
		public GameObject WheelRR;
		public GameObject WheelFL;
		public GameObject WheelRL;}

	//serializabling cars
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
	[Header ("Tuning car")]
	public TuningCars TuningCar;
	[Tooltip ("The Car Number in Array List")]
	public int CarNumber;
	[Header ("Debug Details")]
	public int FrontBumperSelected=0;
	public int RearBumperSelected=0;
	public int SideSkirtSelected=0;
	public int HoodSelected=0;
	public int RoofSelected=0;
	public int SpoilerSelected=0;
	public int WheelSelected=0;

	void Start(){
		//------------------------------------------------------------------------------------------------------
		//RANDOMIZE all selected parts
		FrontBumperSelected=Random.Range(0,TuningCar.FrontBumpers.Length); 
		RearBumperSelected=Random.Range(0,TuningCar.RearBumpers.Length); 
		SideSkirtSelected=Random.Range(0,TuningCar.SideSkirts.Length); 
		HoodSelected=Random.Range(0,TuningCar.Hoods.Length); 
		RoofSelected=Random.Range(0,TuningCar.Roofs.Length); 
		SpoilerSelected=Random.Range(0,TuningCar.spoilers.Length); 
		WheelSelected=Random.Range(0,TuningCar.Wheels.Length); 
		//show on screen on parts
		//-------------------------------------------------------------------frontbumper
		for(int i = 0; i <TuningCar.FrontBumpers.Length ; i++) {
			GameObject part = TuningCar.FrontBumpers [i].FrontBumper;
			part.SetActive (false);
		}
		TuningCar.FrontBumpers[FrontBumperSelected].FrontBumper.SetActive(true);
		//-------------------------------------------------------------------RearBumpers
		for(int i = 0; i <TuningCar.RearBumpers.Length ; i++) {
			GameObject part = TuningCar.RearBumpers [i].RearBumper;
			part.SetActive (false);
		}
		TuningCar.RearBumpers[RearBumperSelected].RearBumper.SetActive(true);
		//-------------------------------------------------------------------sideskirt
		for(int i = 0; i <TuningCar.SideSkirts.Length ; i++) {
			GameObject part = TuningCar.SideSkirts [i].SideSkirt;
			part.SetActive (false);
		}
		TuningCar.SideSkirts[SideSkirtSelected].SideSkirt.SetActive(true);
		//-------------------------------------------------------------------hood
		for(int i = 0; i <TuningCar.Hoods.Length ; i++) {
			GameObject part = TuningCar.Hoods [i].Hood;
			part.SetActive (false);
		}
		TuningCar.Hoods[HoodSelected].Hood.SetActive(true);
		//-------------------------------------------------------------------roof
		for(int i = 0; i <TuningCar.Roofs.Length ; i++) {
			GameObject part = TuningCar.Roofs [i].Roof;
			part.SetActive (false);
		}
		TuningCar.Roofs[RoofSelected].Roof.SetActive(true);
		//-------------------------------------------------------------------spoiler
		for(int i = 0; i <TuningCar.spoilers.Length ; i++) {
			GameObject part = TuningCar.spoilers [i].Spoiler;
			part.SetActive (false);
		}
		TuningCar.spoilers[SpoilerSelected].Spoiler.SetActive(true);
		//-------------------------------------------------------------------wheels
		for(int i = 0; i <TuningCar.Wheels.Length ; i++) {
			GameObject part = TuningCar.Wheels [i].WheelFL;
			part.SetActive (false);
		}
		for(int i = 0; i <TuningCar.Wheels.Length ; i++) {
			GameObject part = TuningCar.Wheels [i].WheelFR;
			part.SetActive (false);
		}
		for(int i = 0; i <TuningCar.Wheels.Length ; i++) {
			GameObject part = TuningCar.Wheels [i].WheelRL;
			part.SetActive (false);
		}
		for(int i = 0; i <TuningCar.Wheels.Length ; i++) {
			GameObject part = TuningCar.Wheels [i].WheelRR;
			part.SetActive (false);
		}
		TuningCar.Wheels[WheelSelected].WheelFL.SetActive(true);
		TuningCar.Wheels[WheelSelected].WheelFR.SetActive(true);
		TuningCar.Wheels[WheelSelected].WheelRL.SetActive(true);
		TuningCar.Wheels[WheelSelected].WheelRR.SetActive(true);
		//--------------------------------------------------------------------------------------------------
	}
}
