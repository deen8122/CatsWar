using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
//namespace Completed
//{

public class GameGUI : MonoBehaviour {
		public static GameGUI instance = null;     //Allows other scripts to call functions from SoundManager.    
	    public GameObject pausedDialog;
		public GameObject wonDialog;
		public GameObject lostDialog;
	//	public GameObject selectObjectsMenu;
		public GameObject playGameButton;
		private bool isPlaying = false;
	    private GameManager	mGameManager = null;
	private bool btnBinoklActive = true;

	public GameObject BinoklGameButton;
	private Color C ;

		
	public void UpdateBinokl(){
		if(mGameManager.count_bonusPricel <=0){
			btnBinoklActive = true;
			BinoklGameButton.GetComponent<Button>().interactable = false;
		}
		else {
			btnBinoklActive = false;
			BinoklGameButton.GetComponent<Button>().interactable = true;
		}
		SetBinokl();

	}
	public void interactableBinokl(){	
			BinoklGameButton.GetComponent<Button>().interactable = true;	
	}
	public void SetBinokl(){

		if(mGameManager.count_bonusPricel <=0 && btnBinoklActive== false){
			return;
		}
		if(mGameManager.count_bonusPricel <=0 && btnBinoklActive== true){

		}
		if( btnBinoklActive){
			btnBinoklActive = false;
			C.a = 0.4f;
		}
		else {
			C.a = 1;
			btnBinoklActive = true;
		}
		BinoklGameButton.GetComponent<Image>().color = C;
		mGameManager.setBinoklActive(btnBinoklActive);

	}
    public void HideAll(){
			pausedDialog.SetActive(false);
			wonDialog.SetActive(false);
			lostDialog.SetActive (false);
		}
	public void ShowPausedMenu(){

		pausedDialog.SetActive(true);
		Time.timeScale = 0;
	}



    public void StartPlayGame(){
			//selectObjectsMenu.SetActive (false);
			playGameButton.SetActive (false);
			Time.timeScale = 1;
			isPlaying = true;
		    
		    mGameManager.PlayGameMode();
		}


	public void HidePausedMenu(){

		
		pausedDialog.SetActive(false);
		Time.timeScale = 1;
			if (isPlaying == true) {
				Time.timeScale = 1;
			}
		
		
	}
	public void PlayerWon(){
		print ("PlayerWon");
		wonDialog.SetActive(true);
	}
	public void PlayerLost(){
		print ("PlayerLost");
		//wonDialog.SetActive(true);
		lostDialog.SetActive (true);
	}

	// Use this for initialization
	void Start () {
			print ("Start");
			HideAll ();
			mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager>();
		    C = new Color(1, 1, 1, 0.5f);
		    SetBinokl();
	}
		/*
	 * 
	 *Перезагрузка уровня! 
	 */
		public void RealoadGame(){
			this.HideAll ();
			print ("RealoadGame");
			Application.LoadLevel (Application.loadedLevel);
			
		}
	public void NextLevel(){
		this.HideAll ();
		print ("NextLevel");
		GameManager	mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager>();
		Application.LoadLevel (mGameManager.nextleve);
		
	}
		//В ГЛАВНОЕ МЕНЮ
		public void GoToMainMenu(){
			this.gameObject.SetActive (false);
			Application.LoadLevel("StartScene");
		}
	// Update is called once per frame
	void Update () {
	
	}


	public void CreateObject(GameObject ob){
		 
		GameManager	mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager>();
		//ob2 = ob;
		mGameManager.CreateObject (ob);
	}
	public void CreateObject2(GameObject ob){
		ob.SetActive (false);

	}
	/*
	void Awake(){
		wonDialog = GameObject.FindGameObjectWithTag ("dialog_won");
		lostDialog = GameObject.FindGameObjectWithTag ("dialog_lost");
		pausedDialog= GameObject.FindGameObjectWithTag ("dialog_pause");
	}
	*/
}
//}
