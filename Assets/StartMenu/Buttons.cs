using UnityEngine;
using System.Collections;

public delegate void ClickedEvent();

public class Buttons : MonoBehaviour {
	private Color prev_color;
	private ClickedEvent clickedEvent;

	public ClickedEvent ClickedEvent{
		get{return this.clickedEvent;}
		set{this.clickedEvent = value;}
	}

	void OnMouseDown(){
		prev_color = this.gameObject.guiTexture.color;
		this.gameObject.guiTexture.color = new Color(0,0,0,150);
	}

	void OnMouseUp(){
		this.gameObject.guiTexture.color = prev_color;
		clickedEvent();
	}
}
