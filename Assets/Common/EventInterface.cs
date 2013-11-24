using UnityEngine;
using System.Collections;
using LuaInterface;

public class EventInterface : MonoBehaviour {
	private Lua LuaEngine;
	private string textQueue;
	private Vector3[] CharaPoints;

	private GameObject[] CharaSprites;
	private GameObject Frame,Back;
	private GameObject Sentence,Title;

	const int CHARA_WIDTH = 455;
	const int CHARA_HEIGHT = 730;
	const int FRAME_HEIGHT = 200;
	const int FRAME_WIDTH = 800;
	const int LEFT=0,CENTER=1,RIGHT=2;


	// For Debug
	private Buttons eventButton;
	
	void Start () {
		this.gameObject.SetActive(false);

		CharaPoints = new Vector3[]{
			new Vector3(CHARA_WIDTH/2,CHARA_HEIGHT/2,2),				//left
			new Vector3(Screen.width/2,CHARA_HEIGHT/2,2),				//center
			new Vector3(Screen.width - CHARA_WIDTH/2,CHARA_HEIGHT/2,2)	//right
		};
		CharaSprites = new GameObject[3];
		textQueue = "";

		// debug code
		eventButton = transform.Find("/EventButton").GetComponent<Buttons>();
		eventButton.ClickedEvent += this.StartEventWrapper;
	}

	void StartEvent(int eventNum){
		LuaEngine = new Lua();
		InstantiateTexts ();
		InstantiateFrame("Frame1");

		ChangeCharacter("Chara1","",LEFT);
		ChangeCharacter("Chara2","",RIGHT);
		AddText("fueeeeeeeeeeeee");
		ChangeTitle ("Nyaa:");
		ChangeBack("Back1");

		this.gameObject.SetActive(true);
	}
	
	public void EndEvent(){
		ClearBack ();
		ClearTexts();
		ClearFrame ();
		ClearCharacters();
		this.gameObject.SetActive(false);
	}

	void InstantiateTexts(){
		var vec = new Vector3();
		vec.Set(0.18f,0.16f,1);
		Sentence = (GameObject)Instantiate(Resources.Load("Sentence"),vec,Quaternion.identity);
		vec.Set(0.18f,0.20f,1);
		Title = (GameObject)Instantiate(Resources.Load("Title"),vec,Quaternion.identity);
	}

	void InstantiateFrame(string frameName){
		var vec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2,FRAME_HEIGHT/2,1));
		Frame = (GameObject)Instantiate(Resources.Load(frameName),vec,Quaternion.identity);
	}

	void InstantiateChara(string charaName,int position){
		var vec = Camera.main.ScreenToWorldPoint(CharaPoints[position]);
		vec.z = 1;
		CharaSprites[position] = (GameObject)Instantiate(Resources.Load(charaName),vec,Quaternion.identity);
	}
	
	void InstantiateBack(string backName){
		var worldVec = new Vector3(0,0,3);
		Back = (GameObject)Instantiate(Resources.Load(backName),worldVec,Quaternion.identity);
	}
	
	// debug code
	public void StartEventWrapper(){
		StartEvent(0);
		this.eventButton.gameObject.SetActive(false);
		transform.Find("/StartEvent").gameObject.SetActive(false);
	}

	public void ChangeCharacter(string name,string face,int position){
		if(CharaSprites[position]!=null)
			Destroy(CharaSprites[position]);
		InstantiateChara(name,position);
	}

	public void ChangeTitle(string title){
		Title.guiText.text = title;
	}

	public void ChangeBack(string name){
		InstantiateBack("Back1");
	}

	public void AddText(string text){
		Sentence.guiText.text = "";
		textQueue = text;
	}

	public void ClearTexts(){
		Destroy(Sentence);
		Destroy(Title);
	}

	public void ClearBack(){
		if(Back!=null)Destroy(Back);
	}

	public void ClearFrame(){
		if(Frame!=null)Destroy(Frame);
	}

	public void ClearCharacters(){
		foreach(GameObject chara in CharaSprites){
			if(chara!=null)Destroy (chara);
		}
	}

	public void ProceedText(){
		if(Sentence!=null){
			Sentence.guiText.text += textQueue[0];
			textQueue = textQueue.Remove(0,1);
		}
	}

	void Update () {
		if(textQueue.Length > 0)ProceedText();
	}
}
