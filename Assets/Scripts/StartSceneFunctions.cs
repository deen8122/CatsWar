using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class StartSceneFunctions : MonoBehaviour {

	public GameObject  aboutPanel;
	public GameObject  howplay;
	public GameObject  MainPanel;
	public GameObject  SelectLevelPanel;

	public GameObject  LevelPanel1;
	public GameObject  LevelPanel2;
	public GameObject  LevelPanel3;
	private GameObject LevelPanelOpened;



	public GameObject MyCamera;
	private GameObject CurrentPanel = null;
	private Vector3 initPos;
	private bool SoundOn = true;
	private bool MusicOn = true;
	private int CurrentSelectPanel = 0;
	public Sprite levelPassed ;
	public Sprite levelNoPassed ;
	public bool isDebug = false;
	// Use this for initialization
	void Start () {

		if(isDebug){
			for(int i = 0; i < 45; i++){
			//	string name;
			//	name = i+"";
			//	PlayerPrefs.SetInt(name,1);
			}

		}
		LevelPanelOpened = LevelPanel1;
		//arrLevelPanel[0]=LevelPanel1;
		//arrLevelPanel[1]=LevelPanel2;
		//arrLevelPanel[2]=LevelPanel3;
		Time.timeScale = 1;
		aboutPanel.SetActive (false);
		//SelectLevelPanel.SetActive (false);
		MainPanel.SetActive (true);
		howplay.SetActive (false);
		CurrentPanel = MainPanel;
		//Camera.main.WorldToViewportPoint(pos)
		//initPos = MainPanel.transform.position;
		//initPos = Camera.main.WorldToViewportPoint(MainPanel.transform.position);
		initPos = new Vector3 (Camera.main.pixelWidth, Camera.main.pixelHeight, 0);
		initPos -= MainPanel.transform.position;

		//GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>()
		GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
		MyCamera.SetActive(true);
		//level Select
		//GameObject[] enemyGO = GameObject.FindGameObjectsWithTag("levelselect");
		//int level = PlayerPrefs.GetInt("level");
		//level++;
		//for (int i = 0; i < level; i++) {
		//	enemyGO[i].GetComponent<Image>().sprite = levelPassed;
		//}
		//PlayerPrefs.SetInt("level1",1);
	//	GameObject btn_sound = GameObject.Find("btn_sound").GetComponentInChildren<GameObject>();
		int sound = PlayerPrefs.GetInt("sound") ;
		if(sound == 0){
			PlayerPrefs.SetInt("sound",1);

		}

		if(sound==-1){
			//btn_sound_no
			GameObject.FindGameObjectWithTag("btn_sound_no").SetActive(true);
			SoundOn = false;
			this.GetComponent<AudioSource>().mute = true;
		}else {
			GameObject.FindGameObjectWithTag("btn_sound_no").SetActive(false);
			SoundOn = true;
			this.GetComponent<AudioSource>().mute = false;
		}
		print ("sound="+sound);
		//if(PlayerPrefs.GetInt("sound") == 1 ){

		int music = PlayerPrefs.GetInt("music") ;
		if(music == 0){
			PlayerPrefs.SetInt("music",1);
			
		}
		if(music==-1){
			//btn_sound_no
			GameObject.Find("btn_music_no").SetActive(true);
			//MainMusic.instance.Pause();
			MusicOn = false;

		}else {
			GameObject.Find("btn_music_no").SetActive(false);
			MainMusic.instance.Play();
			MusicOn = true;
		
		}
		print ("music="+music);
		CurrentSelectPanel = PlayerPrefs.GetInt("CurrentSelectPanel") ;
		print ("CurrentSelectPanel="+CurrentSelectPanel);
		SetPanelSL(CurrentSelectPanel);
		//SetPanelSL(CurrentSelectPanel);
		//	btn_sound.SetActive(false);
		//}else {
	//		btn_sound.SetActive(true);
		//}
	}
	

	public void BtnSound(){
		this.GetComponent<AudioSource>().Play();
	}
	public void Sound(GameObject go){
		if (SoundOn) {
			PlayerPrefs.SetInt("sound",-1);
			this.GetComponent<AudioSource>().mute = true;
			go.SetActive(true);
			SoundOn = false;
		} else {
			go.SetActive(false);
			PlayerPrefs.SetInt("sound",1);
			this.GetComponent<AudioSource>().mute = false;
			SoundOn = true;
		}
		BtnSound();
	}

	//
	public void Music(GameObject go){
		if (MusicOn) {
			go.SetActive(true);
			PlayerPrefs.SetInt("music",-1);
			MainMusic.instance.Pause();
			MusicOn = false;
		} else {
			PlayerPrefs.SetInt("music",1);
			go.SetActive(false);
			MainMusic.instance.Play();
			MusicOn = true;
		}
		BtnSound();
	}
	public void About(){
		SwitchPanel (aboutPanel);
	}
	public void MainPage(){
		//SelectLevelPanel.GetComponent<Animator> ().SetBool ("showLevelSelect",false);
		SwitchPanel (MainPanel);


	}
	public void ExitGame(){
		Application.Quit();
	}
	public void SelectLevel(){

		SelectLevelPanel.transform.localPosition = new Vector3 (0, 0, 0);
		SwitchPanel (SelectLevelPanel);

		//SelectLevelPanel.GetComponent<Animator> ().SetBool ("showLevelSelect",true);
	}

	public void StartGame(string level){
		print ("StartGame level="+level);
		//BtnSound();
		Application.LoadLevel(level);
		//SwitchPanel (SelectLevelPanel);
	}



	public void SwitchPanel(GameObject newP){
		print ("SwitchPanel");
		newP.SetActive (true);
		//newP.transform.position =initPos;
		CurrentPanel.SetActive (false);
		CurrentPanel = newP;
		BtnSound();

	}

	public void SelectPanelLeft(){
		CurrentSelectPanel--;
		SetPanelSL(CurrentSelectPanel);

	}
	public void SelectPanelRight(){
		CurrentSelectPanel++;


		SetPanelSL(CurrentSelectPanel);
		
	}
     void SetPanelSL(int index){
		BtnSound();
		if(index<0) {
			index = 2;
			CurrentSelectPanel = 2;
		}
		if(index>2) {
			index = 0;
			CurrentSelectPanel = 0;
		}
		PlayerPrefs.SetInt("CurrentSelectPanel",index);
		LevelPanelOpened.SetActive(false);
		if(index == 0){
			LevelPanelOpened = LevelPanel1;
		}
		if(index == 1){
			LevelPanelOpened = LevelPanel2;
		}
		if(index == 2){
			LevelPanelOpened = LevelPanel3;
		}
		LevelPanelOpened.SetActive(true);
	}
}
