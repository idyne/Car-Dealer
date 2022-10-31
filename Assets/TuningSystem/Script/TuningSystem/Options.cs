using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

	[Header ("Options:")]
	[Header ("PLayerName")]
	public InputField PlayerNameInput;
	public string PlayerName;
	[Header ("Resolution")]
	public int ResolutionSelected = 0;
	public Text ResolutionOutput;
	[Header ("FullScreen")]
	public bool FullScreen=true;
	public Text FullScreenOutput;
	[Header ("Quality")]
	public int QualitySelected = 0;
	public Text QualityOutput;
	[Header ("Bloom")]
	public bool Bloom=true;
	public Text BloomOutput;
	[Header ("Audio"),Space(1)]
	public AudioSource AuSource;
	public AudioClip ButtonSound;
	public AudioClip Accepted;
	public AudioClip Back;
	[Header ("Image Effects")]
	public MonoBehaviour Blur;

	void Start(){
		//get enclosed audiosource if it exist
		AuSource=gameObject.GetComponent<AudioSource>();
		//load all options
		ResolutionSelected=PlayerPrefs.GetInt("Resolution");
		QualitySelected=PlayerPrefs.GetInt("Quality");
		if (PlayerPrefs.GetInt ("FullScreen") == 0) FullScreen = false;
		if (PlayerPrefs.GetInt ("FullScreen") == 1) FullScreen = true;
		if (PlayerPrefs.GetInt ("Bloom") == 0) Bloom = false;
		if (PlayerPrefs.GetInt ("Bloom") == 1) Bloom = true;
		//load player name
		PlayerName = PlayerPrefs.GetString ("PLayerName");
		PlayerNameInput.text = PlayerName;

	}

	void Update(){
		//SREEN_RESOLUTION_SECTION_UpdateFunction
		if (ResolutionSelected == 0) {Screen.SetResolution(640,480,FullScreen); ResolutionOutput.text="640x480";
		}
		if (ResolutionSelected == 1) {Screen.SetResolution(800,600,FullScreen); ResolutionOutput.text="800x600";
		}
		if (ResolutionSelected == 2) {Screen.SetResolution(1280,720,FullScreen); ResolutionOutput.text="1280x720";
		}
		//FULL_SCREEN_SECTION_UpdateFunction
		if (FullScreen == true) {Screen.fullScreen=true; FullScreenOutput.text="True";
		}
		if (FullScreen == false) {Screen.fullScreen=false; FullScreenOutput.text="False";
		}
		//QUALITY_SECTION_UpdateFunction
		if (QualitySelected == 0) {QualitySettings.SetQualityLevel(0); QualityOutput.text="Fastest";
		}
		if (QualitySelected == 1) {QualitySettings.SetQualityLevel(1); QualityOutput.text="Simple";
		}
		if (QualitySelected == 2) {QualitySettings.SetQualityLevel(2); QualityOutput.text="Good";
		}
		if (QualitySelected == 3) {QualitySettings.SetQualityLevel(3); QualityOutput.text="Beautiful";
		}
		if (QualitySelected == 4) {QualitySettings.SetQualityLevel(4); QualityOutput.text="Fantastic";
		}
		//BLOOM_SECTION_UpdateFunction
		if (Bloom == true) {BloomOutput.text="True";
		}
		if (Bloom == false) {BloomOutput.text="False";
		}
		//PLAYER_NAME_SECTION_UpdateFunction
		PlayerName=PlayerNameInput.text;
		//save player name
		PlayerPrefs.SetString ("PLayerName", PlayerName);

	}

	//-----------SREEN_RESOLUTION_SECTION---------------------------------
	public void NextResolution(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change resolution
		ResolutionSelected++;
		//loop
		if (ResolutionSelected == 3)
			ResolutionSelected = 0;
		//Save
		PlayerPrefs.SetInt("Resolution",ResolutionSelected);
	}
	public void PreviousResolution(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change resolution
		ResolutionSelected--;
		//loop
		if (ResolutionSelected == -1)
			ResolutionSelected = 2;
		//Save
		PlayerPrefs.SetInt("Resolution",ResolutionSelected);
	}
	//-----------FULL_SCREEN_SECTION---------------------------------
	public void ChangeFullScreen(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change fullscreen
		FullScreen= !FullScreen;
		//Save
		if(FullScreen==false) PlayerPrefs.SetInt("FullScreen",0);
		if(FullScreen==true) PlayerPrefs.SetInt("FullScreen",1);
	}
	//-----------QUALITY_SECTION---------------------------------
	public void NextQuality(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change resolution
		QualitySelected++;
		//loop
		if (QualitySelected == 5)
			QualitySelected = 0;
		//Save
		PlayerPrefs.SetInt("Quality",QualitySelected);
	}
	public void PreviousQuality(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change resolution
		QualitySelected--;
		//loop
		if (QualitySelected == -1)
			QualitySelected = 4;
		//Save
		PlayerPrefs.SetInt("Quality",QualitySelected);
	}
	//-----------BLOOM_SECTION---------------------------------
	public void ChangeBloom(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//change fullscreen
		Bloom= !Bloom;
		//Save
		if(Bloom==false) PlayerPrefs.SetInt("Bloom",0);
		if(Bloom==true) PlayerPrefs.SetInt("Bloom",1);
	}

	public void BackVoid(){
		//play sound for each button
		AuSource.clip=Back;
		AuSource.Play ();
		//active blur
		Blur.enabled=false;
	}

	public void OpenOptions(){
		//play sound for each button
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//active blur
		Blur.enabled=true;
	}

}
