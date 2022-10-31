using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatController : MonoBehaviour {

	[Header ("Cheat Controller")]
	public GameObject CheatUI;
	public bool Enabled = false;
	public string Cheat;
	public InputField CheatField;
	[Header ("Code List:"), Space (1)]
	[Header ("Add Money")]
	public string CodeAddMoney = "MoneyIamPoor";
	public int moneyToAdd = 5000;
	public int PlayerMoney;
	[Header ("EraseGameMemory")]
	public string CodeEraseMemory = "EraseMeIwantForgot";
	[Header ("ScreenShot")]
	public string TakeMeScreen = "TakeMeScreen";
	public GameObject MenuCanvas;
	public bool CanvasEnabled;

	void Update(){
		
		if (Input.GetKey (KeyCode.LeftControl) && Input.GetKeyDown (KeyCode.Backslash)) 
		{
			if (Enabled==false) Enabled=true;
			else Enabled=false;
		}

		if (Enabled == true) {
			CheatUI.SetActive (true);
		}
		else {
			CheatUI.SetActive (false);
			CheatField.text = "";
		}

		Cheat = CheatField.text.ToString();

		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			CheckCheat ();
		}
		//screen cheat check
		if (CanvasEnabled == false && Input.GetKey (KeyCode.LeftControl) && Input.GetKeyDown (KeyCode.Backslash)) {
			MenuCanvas.SetActive (true);
			CanvasEnabled = true;
		} 
	}

	void CheckCheat(){
		//test cheat
		if (Cheat == "RightCheat :D") {
			CheatField.text="Ok, This Works.";
			}else
			
		//money cheat
		if (Cheat == CodeAddMoney) {
			PlayerMoney=PlayerPrefs.GetInt ("Money");
			moneyToAdd = PlayerMoney + moneyToAdd;
			PlayerPrefs.SetInt ("Money", moneyToAdd);

			Enabled=false;
			CheatField.text = "";
			}else
		
		//erase memory for test purpose
		if (Cheat==CodeEraseMemory){
			PlayerPrefs.DeleteAll();

			Enabled=false;
			CheatField.text = "";
				}else
		//remove all UI
		if(Cheat==TakeMeScreen){
			if (CanvasEnabled == true) {
				MenuCanvas.SetActive (false);
				CanvasEnabled = false;
			} else {
				MenuCanvas.SetActive (true);
				CanvasEnabled = true;
			}
		    }


		//Last "Else" Function
		else CheatField.text="Not Lucky :(";
	}


}
