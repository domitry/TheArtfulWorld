using UnityEngine;
using System.Collections;

public class GUController : MonoBehaviour {
	void Start () {
		Buttons start_button = transform.Find("/StartButton").GetComponent<Buttons>();
		Buttons load_button = transform.Find("/LoadButton").GetComponent<Buttons>();
		Buttons end_button = transform.Find("/EndButton").GetComponent<Buttons>();
		start_button.ClickedEvent += ()=>{Application.LoadLevel("NewGame");};
		load_button.ClickedEvent += ()=>{Application.LoadLevel("LoadGame");};
		end_button.ClickedEvent += ()=>{Application.Quit();};
	}
}
