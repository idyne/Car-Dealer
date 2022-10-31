using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

	public Texture2D Default;
	public Texture2D Over;
	public Texture2D OverUI;

	void Start(){
		Cursor.SetCursor (Default,Vector2.zero,CursorMode.Auto);
	}
	void OnMouseExit(){
		Cursor.SetCursor (Default,Vector2.zero,CursorMode.Auto);
	}
	void OnMouseOver(){
		Cursor.SetCursor (Over,Vector2.zero,CursorMode.Auto);
	}
	public void OnUIEnter(){
		Cursor.SetCursor (OverUI,Vector2.zero,CursorMode.Auto);
	}
	public void OnUIExit(){
		Cursor.SetCursor (Default,Vector2.zero,CursorMode.Auto);
	}

}
