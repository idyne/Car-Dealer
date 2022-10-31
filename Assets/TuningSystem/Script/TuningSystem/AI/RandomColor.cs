using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

	public bool Controller=true;
	public int MatIndex;
	public Renderer CarRender;
	public RandomColor script;
	[Header("material settings")]
	public Material CarMaterial;
	public Shader shader;
	public Texture texture;
	public Texture[] LiveryTexture;
	[Header("material details")]
	public Color color;
	public float metal;
	public float gloss;
	public Color EmitColor;
	public int LiveryIndex;

	// Use this for initialization
	void Start () {

		CarRender = gameObject.GetComponent<Renderer> ();

		if (Controller == true) {
			script=gameObject.GetComponent<RandomColor> ();
			//create new material
			CarRender.materials [MatIndex] = new Material (shader);
			CarMaterial = CarRender.materials [MatIndex];

			color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
			metal = Random.Range (0f, 1f);
			gloss = Random.Range (0.4f, 1f);
			EmitColor = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f)) * 1.5f;
			LiveryIndex = Random.Range (0, LiveryTexture.Length);

			CarMaterial.SetColor ("_Color", color);
			CarMaterial.SetColor ("_EmissionColor", EmitColor);
			CarMaterial.SetFloat ("_Glossiness", gloss);
			CarMaterial.SetFloat ("_Metallic",metal );

			//CarMaterial.SetTexture ("_MainTex", texture);
			CarMaterial.SetTexture ("_MainTex", LiveryTexture[LiveryIndex]);
		} else {
			CarRender.materials [MatIndex] = new Material(shader);
			CarMaterial = CarRender.materials [MatIndex];

			color = script.color;
			metal = script.metal;
			gloss = script.gloss;
			EmitColor = script.EmitColor;
			LiveryIndex = script.LiveryIndex;

			CarMaterial.SetColor ("_Color", color);
			CarMaterial.SetColor ("_EmissionColor", EmitColor);
			CarMaterial.SetFloat ("_Glossiness", gloss);
			CarMaterial.SetFloat ("_Metallic",metal );

			//CarMaterial.SetTexture ("_MainTex", texture);
			CarMaterial.SetTexture ("_MainTex", LiveryTexture[LiveryIndex]);
		}
	}
}
