using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {
	//public int test = 0;
	public static GameState CurrentGameState = GameState.Playing;

	[HideInInspector]
	public bool isEnemy = false;
	Vector2 pointTo;
	private GameObject[] enemyGO;
	private GameObject MainCamera;
	private float SpeedMovingCamera = 2;
	private bool CameraToLeft = true;
	public string nextleve = "test";
	public static bool f_bonusPricel = true;
	public int count_bonusPricel =0;
	[HideInInspector]
	public int ENEMY_CAT_COL = 1;
	[HideInInspector]
	public int PLAYER_CAT_COL = 1;
	[HideInInspector]
	public int PLAYER_SHOOT =1;
	[HideInInspector]
	public int ENEMY_SHOOT = 1;
	[HideInInspector]
	private int CURRENT_SHOTER = 0;
	private GameObject Sel = null;
	public LineRenderer TrajectoryLineRenderer;
	private int DiedCatsCol = 0;
	private int DiedCatsCol2 = 0;
	[HideInInspector]
	public float playerCatPositionX;

	/*
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 */
	public void CatFireEvent(){
		if (!isEnemy){
		    minusBinokl();
		}
	}
	//---------------------------------------
	public void setBinoklActive(bool f){
		f_bonusPricel = f;
	}
	/*
	 * добавлнеи жизни	
	 */
	public void plusLife(){
		GameObject[] playeryGO = GameObject.FindGameObjectsWithTag("player_cats");
		for (int i = 0; i < playeryGO.Length; i++) {
			if(playeryGO[i].gameObject == null) continue;
			playeryGO[i].gameObject.GetComponent<cat2>().Health+=40;
			//SpecialEffectsHelper.Instance.CatPlusLifeEffect(playeryGO[i].transform.position);		
		}
	}

	public void plusBinokl(int col){
		count_bonusPricel+=col;
		//f_bonusPricel = true;
		GameObject.FindGameObjectWithTag ("GIU_BINOKL_TEXT").GetComponent<Text>().text =""+count_bonusPricel;
		GameObject.FindGameObjectWithTag ("GameCanvas").GetComponent<GameGUI>().interactableBinokl();
		//GameObject.FindGameObjectWithTag ("GameCanvas").GetComponent<GameGUI>().UpdateBinokl();//(true);
	}
	public void minusBinokl(){
		if(!f_bonusPricel) return;
		count_bonusPricel--;
		if(count_bonusPricel <=0){
			GameObject.FindGameObjectWithTag ("GameCanvas").GetComponent<GameGUI>().UpdateBinokl();//(true);
			f_bonusPricel = false;
		}
		GameObject.FindGameObjectWithTag ("GIU_BINOKL_TEXT").GetComponent<Text>().text =""+count_bonusPricel;
	}
	public bool OnMouseDown(Vector3 mousePos){
		if(GameManager.CurrentGameState == GameState.PressedObj){
			print ("Обьект выбран, нет необходимтости искать...");
			return false;
		}
		float R = 1;
		float distance = 0;
		GameObject[] playeryGO = GameObject.FindGameObjectsWithTag("player_cats");

		for (int i = 0; i < playeryGO.Length; i++) {
			if(playeryGO[i].gameObject == null) continue;
			if(playeryGO[i].gameObject.GetComponent<Fire>().isFired == false){

				distance = Vector2.Distance(mousePos, playeryGO[i].gameObject.transform.position);
//				print ("mousePos="+mousePos.ToString()+" playeryGO="+playeryGO[i].gameObject.transform.position.ToString()+ " distance="+distance);
				if(distance < R){
					R = distance;
					Sel = playeryGO[i];
				}
			}
		
		}
		if(Sel !=null) {

			Sel.GetComponent<Fire>().TouchDown();
			return true;
		}else return false;
	}
	public void onMouseUp(){
		if(Sel!=null){
		Sel.GetComponent<Fire>().TouchUp();
	    Sel = null;
		}
	}

	public void EnemyStartShoot(){
		print ("1 ----------------------------------------------------------------------------EnemyStartShoot -------------->ENEMY_CAT_COL="+ENEMY_CAT_COL);
		isEnemy = true;
		ENEMY_SHOOT =unlockBombs ("enemy_cats");
		ENEMY_CAT_COL = ENEMY_SHOOT;

		EnemyShoot ();
	}
	
	private void EnemyShoot(){


		print ("2 ---------------EnemyShoot ------------------------------------------------------------------->ENEMY_CAT_COL="+ENEMY_SHOOT);
		CURRENT_SHOTER = 0;
		enemyGO = GameObject.FindGameObjectsWithTag("enemy_cats");
		if (enemyGO.Length == 0)
			return;
		int i2 = 0;
		print ("EnemyShoot ------------------------------------------------------------------->enemyGO.Length="+enemyGO.Length);
		int xol = 0;
		for (int i = 0; i < enemyGO.Length; i++) {
			if(enemyGO[i].gameObject == null) continue;
			xol++;
			if(enemyGO[i].gameObject.GetComponent<Fire>().isFired == false){
				CURRENT_SHOTER = i;
				i2=1;
				break;
			}
		}
		print ("EnemyShoot ------xol="+xol+"-----------i2="+i2+"---->CURRENT_SHOTER="+CURRENT_SHOTER);
		if(i2 == 0){
			PlayerStartShoot();
			return;
		}
		enemyGO [CURRENT_SHOTER].GetComponent<cat2>().isLock=true;
		StartCoroutine (AnimateCameraToStartPosition(enemyGO [CURRENT_SHOTER],1));
		StartCoroutine (WaitANDshoor(enemyGO [CURRENT_SHOTER]));

	}
	private void EnemyShoot2(){
		print ("=====================================>EnemyShoot ENEMY_CAT_COL="+ENEMY_SHOOT);
		CURRENT_SHOTER = 0;
		enemyGO = GameObject.FindGameObjectsWithTag("enemy_cats");
		if (enemyGO.Length == 0)
			return;
		int i2 = 0;
		for (int i = 0; i < enemyGO.Length; i++) {
			if(enemyGO[i].gameObject == null) continue;
			if(enemyGO[i].gameObject.GetComponent<Fire>().isFired == false){
				CURRENT_SHOTER = i;
				enemyGO [CURRENT_SHOTER].GetComponent<cat2>().isLock=true;
				StartCoroutine (AnimateCameraToStartPosition(enemyGO [CURRENT_SHOTER],0.5f));
				StartCoroutine (WaitANDshoor(enemyGO [CURRENT_SHOTER]));
				i2=1;
				//break;
			}
			
		}
		
		if(i2 == 0){
			PlayerStartShoot();
			return;
			
		}
		ENEMY_SHOOT = -1;
		//enemyGO [CURRENT_SHOTER].GetComponent<cat2>().isLock=true;
		//StartCoroutine (AnimateCameraToStartPosition(enemyGO [CURRENT_SHOTER],1));
		//StartCoroutine (WaitANDshoor(enemyGO [CURRENT_SHOTER]));
		
	}

	IEnumerator WaitANDshoor(GameObject o) {
		yield return new WaitForSeconds(2f);
		print ("WaitANDshoor --------------------->GameObject="+o.ToString());
		o.GetComponent<cat2> ().StartShoot ();
		o.GetComponent<Fire> ().TouchDown ();
		Vector3 pos = o.transform.position;
		o.GetComponent<Fire> ().SetEnemyAttack (pos);
		o.GetComponent<Fire> ().SetPosAttackPosition (pos);
		float randX = Random.Range (0.7f, 2);
		float randY = Random.Range (0, 1.0f);
		//for (int i = 0; i < 15; i++) {
			pos.x+=3*randX;
			pos.y-=randY;
			// WaitForSeconds(1);
			//StartCoroutine(WaitFOR());
			//yield return new WaitForSeconds(1);
			o.GetComponent<Fire> ().SetPosAttackPosition (pos);
		//}
		o.GetComponent<Fire> ().TouchUp ();
		//o.GetComponent<cat2>().isLock=false;
	}
	//-------------------------------------
	IEnumerator WaitFOR() {
		print(Time.time);
		yield return new WaitForSeconds(0.5f);
		print(Time.time);
	}
	IEnumerator ArrowShow(Vector3 pos) {
		yield return new WaitForSeconds(1f);
		SpecialEffectsHelper.Instance.ArrowShow(pos);
	}
	//
	public void PlayerStartShoot(){		
		print ("PlayerStartShoot ----PLAYER_CAT_COL="+PLAYER_CAT_COL);
		isEnemy = false;
		PLAYER_SHOOT=unlockBombs ("player_cats");	
		PLAYER_CAT_COL = PLAYER_SHOOT;
		PlayerShoot ();
	}

	private Vector3 selected_cat_pos = new Vector3(0,0,0);
	private void  PlayerShoot(){
		DiedCatsCol=0;
		
		print ("PlayerShoot ----PLAYER_CAT_COL="+PLAYER_SHOOT);
		GameObject[] playeryGO = GameObject.FindGameObjectsWithTag("player_cats");
		if (playeryGO.Length == 0)
			return;
		int i2 = 0;
		for (int i = 0; i < playeryGO.Length; i++) {
			if(playeryGO[i].gameObject == null) continue;
			if(playeryGO[i].gameObject.GetComponent<Fire>().isFired == false){
				i2=1;
				playeryGO[i].GetComponent<cat2>().isLock=true;
				//SpecialEffectsHelper.Instance.ArrowShow(playeryGO[i].transform.position);
				//StartCoroutine (ArrowShow( playeryGO[i].transform.position ));
				selected_cat_pos = playeryGO[i].transform.position;
				playerCatPositionX = playeryGO[i].transform.position.x;
				if(GameManager.CurrentGameState != GameState.PressedObj)
				StartCoroutine (AnimateCameraToStartPosition(playeryGO[i],1));

				break;
			}


		}
		if(i2 == 0){
			BombStoped();
			
		}


	}

	public void CatDie(Vector3 diePos){
		if (!isEnemy){
			DiedCatsCol++;
			if(DiedCatsCol>=2){
				DiedCatsCol = 0;
				this.GetComponent<BonusGenerator>().CreateBonusBinokl(diePos);
				int rand =  Random.Range(1,3);
				if(rand==1){
					this.GetComponent<BonusGenerator>().CreateBonusLife(diePos);

				}


			}
			DiedCatsCol2++;
			if(DiedCatsCol2 > 5){
				DiedCatsCol2=0;
				this.GetComponent<BonusGenerator>().CreateBonusLife(diePos);
			}


		}
		//		
		print ("CatDie");
		GameObject[] playeryGO = GameObject.FindGameObjectsWithTag("player_cats");
		if (playeryGO.Length == 0){
			GameManager.CurrentGameState = GameState.Lost;
			PlayerLost();
			return;
		}
		playeryGO = GameObject.FindGameObjectsWithTag("enemy_cats");
		if (playeryGO.Length == 0){
			GameManager.CurrentGameState = GameState.Won;
			PlayerWon();
			return;
		}
	}
     //ВЫЗЫВАЕТСЯ КОГДА БОМБА ОСТАНОВИЛАСЬ
	public void BombStoped(){
		if(GameManager.CurrentGameState == GameState.Won||GameManager.CurrentGameState == GameState.Lost)return;
		/*
		 *
		 *
		 */
		print ("BombStoped");
		if(TrajectoryLineRenderer!=null) TrajectoryLineRenderer.SetVertexCount(0);
		if (isEnemy) {

			ENEMY_SHOOT --;
			print ("   BombStoped ENEMY   ENEMY_SHOOT="+ENEMY_SHOOT);
			CURRENT_SHOTER++;
			if (ENEMY_SHOOT <= 0) {
				CURRENT_SHOTER = 0;

				PlayerStartShoot();
			}
			else {
				EnemyShoot();
			}
	
		} else {
			PLAYER_SHOOT--;
			if (PLAYER_SHOOT <= 0) {
				EnemyStartShoot();
			}else {

				PlayerShoot();
			}
		}        
	}

	// Use this for initialization

	void Start () {
		MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>().enabled = true;
		Time.timeScale = 1;
	//	BuildGameMode ();
		PlayGameMode ();
		//Добавляем количество бонусов для БИНОКЛЯ
		int bonus_binokl_col = PlayerPrefs.GetInt("bonus_binokl_col");
		plusBinokl(bonus_binokl_col);
	}
	Vector3	position;

	public void CreateObject(GameObject ob){
		print ("CreateObject");
		//position  = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		GameObject newobj = Instantiate (ob);
		newobj.AddComponent <ClickImage>();
		//newobj.transform.position = position;

	}
	void BuildGameMode(){
		CurrentGameState = GameState.CREATE_OBJ;
		Time.timeScale = 0;
	}
	public void PlayGameMode(){
		PLAYER_CAT_COL=unlockBombs ("player_cats");
		ENEMY_CAT_COL =unlockBombs ("enemy_cats");
		StartGame ();
		PlayerStartShoot ();
	}

	void StartGame(){	
		GameObject[] enemyGO2 = GameObject.FindGameObjectsWithTag("draggable_objects");
		for (int i = 0; i < enemyGO2.Length; i++) {
			if(enemyGO2[i].gameObject == null) continue;
			Destroy(enemyGO2[i].gameObject.GetComponent<Dragabble>());

		}
		 enemyGO2 = GameObject.FindGameObjectsWithTag("player_cats");
		for (int i = 0; i < enemyGO2.Length; i++) {
			if(enemyGO2[i].gameObject == null) continue;
			Destroy(enemyGO2[i].gameObject.GetComponent<Dragabble>());
			
		}
	}




	void EventStopAnimToSartPos(){
	//	if(selected_cat_pos!=null)
	//	StartCoroutine (ArrowShow( selected_cat_pos ));

	}
	// Update is called once per frame
	void Update () {
		switch (CurrentGameState)
		{
			//case GameState.CameraMoveToPlayer:
			//	break;
			
		case GameState.CAMERA_MOVING:


			MainCamera.transform.position=
				new Vector3(MainCamera.transform.position.x+SpeedMovingCamera ,MainCamera.transform.position.y,MainCamera.transform.position.z) ;
			if(CameraToLeft == true && ( (MainCamera.transform.position.x ) < pointTo.x )){
				CurrentGameState = GameState.Playing;
			//	EventStopAnimToSartPos();
			}
			if(CameraToLeft == false && ( (MainCamera.transform.position.x) > pointTo.x )){
				CurrentGameState = GameState.Playing;
				//EventStopAnimToSartPos();
			}
			break;

		}
	}




	private IEnumerator AnimateCameraToStartPosition(GameObject cat , float waitsecond)
	{

		yield return new WaitForSeconds(waitsecond);
		float duration = Vector2.Distance(MainCamera.transform.position, cat.transform.position) / 10f;
		if (duration == 0.0f) duration = 0.1f;
		pointTo = cat.transform.position;
//		print ("AnimateCameraToStartPosition pointTo="+pointTo.ToString()+" MainCamera="+MainCamera.transform.position.ToString());
		int direction = 1;
		if (pointTo.x < MainCamera.transform.position.x) {
			CameraToLeft = true;
			if(isEnemy == false ){
				pointTo.x+=3;
			}else {
				pointTo.x-=3;	
			}
			//pointTo.x+=3;
			SpeedMovingCamera = -duration;
			direction = -1;
		} else {
			CameraToLeft = false;
			if(isEnemy == false ){
				pointTo.x+=3;
			}else {
				pointTo.x-=3;	
			}
			direction = 1;
			SpeedMovingCamera = duration;
		}

		CurrentGameState = GameState.CAMERA_MOVING;



	}
	private IEnumerator CameraZoom( float waitsecond)
	{
		yield return new WaitForSeconds(waitsecond);

		CurrentGameState = GameState.CAMERA_MOVING;
		
		
	}


	//
	private int unlockBombs(string tag){
	//	if()
		print ("->unlockBombs - "+tag);
		GameObject[] arr = GameObject.FindGameObjectsWithTag(tag);
		for (int i = 0; i < arr.Length; i++) {
			if(arr[i].gameObject!=null){
				arr[i].gameObject.GetComponent<Fire>().isFired = false;
			}
			
		}
		return arr.Length;
	}





	public void NextGameStart(){

	}

	public void PlayerWon(){
        GameGUI	mGameGUI = GameObject.FindGameObjectWithTag ("GameCanvas").GetComponent<GameGUI>();
		mGameGUI.PlayerWon ();
		PlayerPrefs.SetInt("bonus_binokl_col",count_bonusPricel);
		PlayerPrefs.SetInt(nextleve,1);

	}
	public void PlayerLost(){
		//print ("PlayerLost");
		GameGUI	mGameGUI = GameObject.FindGameObjectWithTag ("GameCanvas").GetComponent<GameGUI>();
		mGameGUI.PlayerLost ();
		//wonDialog.SetActive(true);
		//lostDialog.SetActive (true);
	}



	

	//-----------------------------------------------


}
