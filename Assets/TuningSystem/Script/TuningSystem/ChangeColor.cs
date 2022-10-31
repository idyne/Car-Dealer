using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour {

	[Header ("Body:")]
	public Color Paint;
	[Tooltip ("Body Material for each car. Ex: first car has first color, and etc...")]
	public Material[] Mat;
	[Header ("RGB")]
	[Range(0f,1f)] public float R;
	[Range(0f,1f)] public float G;
	[Range(0f,1f)] public float B;
	[Header ("Glossines"), Space (1)]
	[Range(0f,1f)] public float Glossiness;
	[Range(0f,1f)] public float Metallic;
	[Header ("Livery"), Space (1)]
	public Texture[] Livery;
	public int SelectedLivery = 0;

	[Header ("Glass:")]
	public Color Glass;
	public Material[] GMat;
	[Range(0f,1f)] public float gR;
	[Range(0f,1f)] public float gG;
	[Range(0f,1f)] public float gB;
	[Range(0.5f,1f)] public float gA;

	[Header ("Wheel#1:")]
	public Color Wheel1;
	public Material[] w1Mat;
	[Range(0f,1f)] public float w1R;
	[Range(0f,1f)] public float w1G;
	[Range(0f,1f)] public float w1B;

	[Header ("Wheel#2:")]
	public Color Wheel2;
	public Material[] w2Mat;
	[Range(0f,1f)] public float w2R;
	[Range(0f,1f)] public float w2G;
	[Range(0f,1f)] public float w2B;

	[Header ("Neon:")]
	public Color neon;
	public Material[] Nmat;
	[Range(0f,1f)] public float nR;
	[Range(0f,1f)] public float nG;
	[Range(0f,1f)] public float nB;

	[Header ("TireSmoke:")]
	public Color Smoke;
	public Material[] Smat;
	[Range(0f,1f)] public float sR;
	[Range(0f,1f)] public float sG;
	[Range(0f,1f)] public float sB;

	[Header ("Audio"), Space (1)]
	public AudioSource AuSource;
	public AudioClip ButtonSound;
	public AudioClip SprayLoadSound;

	[Header ("UserInterface"), Space (1)]
	public Text SectionText;
	public int Section=1;
	public GameObject BodySection;
	public GameObject DetailsSection;

	[Header ("Details")]//change test to carselect script name
	public CarSelector CarSelectorScript;
	public int CarSelected;
	public int PreviousCarSelected;

	void Start(){
		//get car selected from carselector script //change TEST to Carselector script name also do it in update function
		CarSelectorScript = gameObject.GetComponent<CarSelector> ();
		CarSelected = CarSelectorScript.CarSelected;
		//Load Material Colors
		LoadColor();
	}

	//void activated from button
	public void PaintStart(){
		//get enclosed audiosource if it exist
		AuSource=gameObject.GetComponent<AudioSource>();
		//play sound
		AuSource.clip=SprayLoadSound;
		AuSource.Play ();
	}

	void Update(){
		//get car selected from carselector script //change TEST to Carselector script name
		CarSelected=CarSelectorScript.CarSelected;
		//chech if car is changed
		if (CarSelected != PreviousCarSelected){
			//set car selected
			PreviousCarSelected = CarSelected;
			//load parts
			LoadColor();
		}

		//set paint color
		Paint = new Color (R, G, B);
		Glass = new Color (gR, gG, gB, gA);
		Wheel1 = new Color (w1R, w1G, w1B);
		Wheel2 = new Color (w2R, w2G, w2B);
		neon = new Color (nR, nG, nB);
		Smoke = new Color (sR, sG, sB);

		//set colors of body
		Mat[CarSelected].SetColor ("_Color", Paint);
		Mat[CarSelected].SetFloat ("_Glossiness", Glossiness);
		Mat[CarSelected].SetFloat ("_Metallic", Metallic);
		//set colors of glass
		GMat[CarSelected].SetColor ("_Color", Glass);
		//set neon color 
		Nmat[CarSelected].SetColor("_Color",neon);
	}

	public void ConfirmColor(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//set material colors
		Mat[CarSelected].SetColor ("_Color", Paint);
		Mat[CarSelected].SetFloat ("_Glossiness", Glossiness);
		Mat[CarSelected].SetFloat ("_Metallic", Metallic);
		//save color value
		//body
		PlayerPrefs.SetFloat(CarSelected + "RedMat",R);
		PlayerPrefs.SetFloat(CarSelected +"GreenMat",G);
		PlayerPrefs.SetFloat(CarSelected +"BlueMat",B);
		PlayerPrefs.SetFloat(CarSelected +"GlossMat",Glossiness);
		PlayerPrefs.SetFloat(CarSelected +"MetalMat",Metallic);
		//livery
		PlayerPrefs.SetInt(CarSelected +"Livery",SelectedLivery);
		//glass
		PlayerPrefs.SetFloat(CarSelected +"GlassR",gR);
		PlayerPrefs.SetFloat(CarSelected +"GlassG",gG);
		PlayerPrefs.SetFloat(CarSelected +"GlassB",gB);
		PlayerPrefs.SetFloat(CarSelected +"GlassA",gA);
		//wheel1
		PlayerPrefs.SetFloat(CarSelected +"wheel1R",w1R);
		PlayerPrefs.SetFloat(CarSelected +"wheel1G",w1G);
		PlayerPrefs.SetFloat(CarSelected +"wheel1B",w1B);
		//wheel2
		PlayerPrefs.SetFloat(CarSelected +"wheel2R",w2R);
		PlayerPrefs.SetFloat(CarSelected +"wheel2G",w2G);
		PlayerPrefs.SetFloat(CarSelected +"wheel2B",w2B);
		//neon
		PlayerPrefs.SetFloat(CarSelected +"neonR",nR);
		PlayerPrefs.SetFloat(CarSelected +"neonG",nG);
		PlayerPrefs.SetFloat(CarSelected +"neonB",nB);
		//tire smoke
		PlayerPrefs.SetFloat(CarSelected +"smokeR",sR);
		PlayerPrefs.SetFloat(CarSelected +"smokeG",sG);
		PlayerPrefs.SetFloat(CarSelected +"smokeB",sB);
	}
	//------------------------------------------------------------------------------------------------------------------
	//Change Value From UI
	public void SetR(float red){
		R = red;
	}
	public void SetG(float green){
		G = green;
	}
	public void SetB(float blue){
		B = blue;
	}
	public void SetGloss(float gloss){
		Glossiness = gloss;
	}
	public void SetMetal(float metall){
		Metallic = metall;
	}
	//set livery from UI
	public void SetLivery(int liv){
		//convert liv to selectedlivery because liv can't be saved
		SelectedLivery=liv;
		//set livery
		Mat[CarSelected].SetTexture("_MainTex",Livery[SelectedLivery]);
		//save livery
		PlayerPrefs.SetInt(CarSelected +"Livery",SelectedLivery);
	}
	//set others material
	//set glass
	public void SetGlassR(float gRed){
		gR = gRed;
	}
	public void SetGlassG(float gGreen){
		gG = gGreen;
	}
	public void SetGlassB(float gBlue){
		gB = gBlue;
	}
	public void SetGlassA(float gAlpha){
		gA = gAlpha;
	}
	//set neon
	public void SetNeonR(float nRed){
		nR = nRed;
	}
	public void SetNeonG(float nGreen){
		nG = nGreen;
	}
	public void SetNeonB(float nBlue){
		nB = nBlue;
	}

	//-----------------------------------------------------------------------------------------------------------------
	//Load function called only at the start and when car is changed
	void LoadColor(){
		R = PlayerPrefs.GetFloat (CarSelected +"RedMat");
		G = PlayerPrefs.GetFloat (CarSelected +"GreenMat");
		B = PlayerPrefs.GetFloat (CarSelected +"BlueMat");
		Glossiness = PlayerPrefs.GetFloat (CarSelected +"GlossMat",0.95f);
		Metallic = PlayerPrefs.GetFloat (CarSelected +"MetalMat");
		//load livery
		SelectedLivery=PlayerPrefs.GetInt (CarSelected +"Livery");
		Mat[CarSelected].SetTexture("_MainTex",Livery[SelectedLivery]);
		//load glass
		gR = PlayerPrefs.GetFloat (CarSelected +"GlassR");
		gG = PlayerPrefs.GetFloat (CarSelected +"GlassG");
		gB = PlayerPrefs.GetFloat (CarSelected +"GlassB");
		gA = PlayerPrefs.GetFloat (CarSelected +"GlassA",1f);
		//load neon
		nR = PlayerPrefs.GetFloat (CarSelected +"neonR",0);
		nG = PlayerPrefs.GetFloat (CarSelected +"neonG",0);
		nB = PlayerPrefs.GetFloat (CarSelected +"neonB",0);
	}

	//UI section control void
	public void NextSection(){
		//play sound
		AuSource.clip=ButtonSound;
		AuSource.Play ();
		//increase section
		Section++;
		//chech if section is enclosed
		if (Section == 3) {
			Section = 1;
		}
		//make Body section visible
		if (Section == 1) {
			SectionText.text = "Body";
			BodySection.SetActive (true);
			DetailsSection.SetActive (false);
		} else {
			SectionText.text = "Details";
			BodySection.SetActive (false);
			DetailsSection.SetActive (true);
		}
	}

}
