using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceSelection : MonoBehaviour {

	[System.Serializable]
	public class Tracks
	{
		[Header ("Details")]
		public string name;
		[Tooltip("The SAME name of the track scene! Eg: CarScene1")]
		public string SceneName; 
		public int price;
		public bool unlocked;
		public Sprite image;
		[Header ("Saved Stats")] 
		public int BestScore=0;
	}
	[Header ("Tracks")]
	public Tracks[] track;
	[Header ("UI")]
	public Text TrackName;
	public Text TrackPrice;
	public Text StateText;
	public Text BestScoreText;
	public Image TrackImage;
	public GameObject Buy;
	public GameObject Select;
	public GameObject NotMoney;
	[Header ("Details")]
	public int TrackSelected;
	public int TotalTrack;
	[Header ("PlayerStats")]
	public int Money=5000;
	public Text MoneyText;
	[Header ("Audio")]
	public AudioSource AuSource;
	public AudioClip ButtonSound;
	public AudioClip BuySound;
	public AudioClip NotMoneySound;
	[Header ("Image Effects")]
	public MonoBehaviour Blur;

	void Start(){
		//get array leght
		TotalTrack= track.Length;

		//show first car details UI
		TrackName.text= track [TrackSelected].name;
		TrackPrice.text = track [TrackSelected].price.ToString()+ " $";

		//get enclosed audiosource if it exist
		AuSource=gameObject.GetComponent<AudioSource>();

		//load function called only at the start
		Load();
	}

	void Update(){

		//load money
		Money=PlayerPrefs.GetInt ("Money",Money);
		//show money
		MoneyText.text = Money + " $";

		//output track name
		TrackName.text=track [TrackSelected].name;
		//Set Track Image
		TrackImage.sprite = track [TrackSelected].image;
		//output best score
		BestScoreText.text=track [TrackSelected].BestScore.ToString();

		//check if locked
		if (track [TrackSelected].unlocked == false) {
			StateText.text="Locked";
			TrackPrice.text = track [TrackSelected].price.ToString() + " $";

			//show buy button
			Buy.SetActive(true);
			Select.SetActive(false);
		}
		//check if unlocked
		if (track [TrackSelected].unlocked == true) {
			StateText.text="Unloked";
			TrackPrice.text = "";

			//show select button
			Select.SetActive(true);
			Buy.SetActive(false);
		}

		//track selection input
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Previous ();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Next ();
		}
	}

	public void Next (){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change track
		TrackSelected++;
		if (TrackSelected==TotalTrack){
			TrackSelected = 0;		
		}
	}


	public void Previous (){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change track
		TrackSelected--;
		if (TrackSelected==-1){
			TrackSelected = TotalTrack-1;		
		}
	}

	public void BuyFunc(){
		//check if money are enought
		if (track [TrackSelected].price <= Money) {
			//buy the track
			Money = Money - (track [TrackSelected].price);
			PlayerPrefs.SetInt ("Money", Money);
			track [TrackSelected].unlocked = true;
			//save that player bought the track , setting to 1 "TrackUnlocked" save box
			PlayerPrefs.SetInt (TrackSelected + "TrackUnlocked",1);
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
			//play sound
			AuSource.clip=NotMoneySound;
			AuSource.Play ();
		}
	}

	public void Back(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();

		//disable blur
		Blur.enabled=false;
	}

	public void SelectFunc(){
		//start track scene;
		Application.LoadLevel(track [TrackSelected].SceneName);
	}

	void Load(){
		//LOAD ALL DATA - Called only at the start

		//load if tracks are unlocked
		for(int i = 0; i < TotalTrack; i++) {
			//if car is unlocked load it
			if (PlayerPrefs.GetInt (i + "TrackUnlocked") == 1)
			{
				track [i].unlocked = true;
			}
			//else set it locked
			else {
				track [i].unlocked = false;
			}
		}

		//load best score for each track
		for(int i = 0; i < TotalTrack; i++) {
			track [i].BestScore = PlayerPrefs.GetInt(i + "TrackScore");
		}
	}

	public void ActivateBlur(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//active blur
		Blur.enabled=true;
	}

}
