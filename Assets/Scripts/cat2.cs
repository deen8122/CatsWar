using UnityEngine;
using System.Collections;
using Assets.Scripts;


public class cat2 : MonoBehaviour {
	public float Health = 70f;
	public Sprite spr2 ;
	public Sprite spr3 ;
	private float initHealth = 0;

	private GameManager mGameManager=null;
	public bool isEnemy = true;
	public bool isDie = false;
	public bool  lookRight =true;
	public GameObject rot = null;

	public GameObject health1 = null;
	public GameObject health2 = null;
	public GameObject health3 = null;
	public GameObject Strelka = null;
	public GameObject Hat = null;
	public bool isLock = false;
	private AudioSource asMeow = null;
	private bool f_in_hat = false;
	public int catType = 0;
	private bool f_one_check= false;

	//-------------------------------------------------------------------------------------
	//вызывается при начале выстрела.
	public void StartShoot(){
		print ("StartShoot catType="+catType);
		if(catType == 1){
			f_one_check= true;
			GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range (-5, 5), Random.Range (0, 6) + 6);
			this.gameObject.layer = LayerMask.NameToLayer("enemy_bomb");
			//GetComponent<CircleCollider2D> ().isTrigger = true;
		}
	}
	/*
	 * Анимация кота который должен стрелять.
	 */




	public void AnimateSelected(){

	}
	public void AnimateBombCollision(){
		//GameObject rot = transform.FindChild("rot").gameObject;
		if(rot !=null){
			int deen_rot_noize = Animator.StringToHash("deen_rot_noize");
			//int runStateHash = Animator.StringToHash("Base Layer.Run");
			//rot.GetComponent<Animator>().Play(deen_rot_noize);
			//rot.GetComponent<Animator>().SetTrigger(deen_cat_mouth_ouu);
		}

		//this.GetComponent<AnimationClip>()
		//this.gameObject
	}
	//----------------
	public void AnimateStartShoot(){
		
	}
	//-------------------
	public void AnimateEndShoot(){

	}
	//---------------
	public void AnimateHappy(){

	}
	//-------------------
	public void AnimateDie(){

	}
	//---------------------------------------------------------------------------------


    void Update(){

		if(f_one_check){
			if(GetComponent<Rigidbody2D>().velocity.y<0){
				this.gameObject.layer = LayerMask.NameToLayer("enemy");
				//GetComponent<CircleCollider2D> ().isTrigger = false;
			}
		}
	}

	void Start(){
		initHealth = Health;
		if(GameObject.FindGameObjectWithTag ("gm")!=null)
		mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager>();

		asMeow = GetComponent<AudioSource>();
		if(asMeow!=null){
		float randomPitch = Random.Range(0.8f, 1.8f);
		asMeow.pitch = randomPitch;
		}
		if(Strelka!=null){
		Strelka.SetActive(false);
		GetComponent<Fire>().strleka = Strelka;

		}


		if(this.Hat !=null){
			this.f_in_hat = true;
		
		}
		if(this.lookRight == false){
			SetLookRight(lookRight);
		}

	}
    public void SetLookRight(bool f){
		this.lookRight = f;
		if(f == false){
			this.transform.localScale=new Vector3(-1,1,1);
			this.GetComponent<Fire>().napravlenie = -1;
		}
		else {
			this.transform.localScale=new Vector3(1,1,1);
			this.GetComponent<Fire>().napravlenie = 1;

		}
	}
	//----------------------------------------------------
	void OnCollisionEnter2D(Collision2D col)
	{
		if(isLock)return;
		if(isDie)return;
		if (GameManager.CurrentGameState == GameState.CREATE_OBJ) {
			return;
		}
		if(catType == 1 && f_one_check){
			f_one_check = false;
			if(this.transform.position.x < mGameManager.playerCatPositionX){
				SetLookRight(false);
			}else {
				SetLookRight(true);
			}
		}
		if(col.gameObject.tag == "lift_platform"){ return;}
		if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;
		if (mGameManager == null)
			return;
	


		float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;

		if(col.gameObject.tag == "tomato"){
			SpecialEffectsHelper.Instance.CatEffect(transform.position);
			//SoundManager.instance.RandomizeSfx(destroySound1);
			if(asMeow!=null& SoundManager.instance.soundOn){
			if(!asMeow.isPlaying){

				asMeow.Play();
			}
			}
			if(this.f_in_hat == true){
				this.f_in_hat = false;
				Hat.GetComponent<Rigidbody2D>().isKinematic = false;
				Hat.GetComponent<PolygonCollider2D>().isTrigger = false;
				return;
			}

		}

		AnimateBombCollision();
		Health -= damage;
		if (Health < initHealth / 1.5) {
			if(health1!=null)health1.SetActive(true);
		}
		if (Health < initHealth / 2) {
			if(health2!=null)health2.SetActive(true);
		}
		if (Health < initHealth / 2.2) {
			if(health3!=null)health3.SetActive(true);
		}


		if (Health <= 0) {

			SpecialEffectsHelper.Instance.CatDieEffect(transform.position);

			this.isDie = true;
			this.GetComponent<Fire>().isFired = true;
			if(isEnemy) {
				mGameManager.ENEMY_CAT_COL -- ;
			}
			else mGameManager.PLAYER_CAT_COL--;
			Vector3 pos = transform.position;
			this.transform.position  = new Vector3(-1000,-1000,0);
			this.tag="Untagged";
			if(asMeow!=null& SoundManager.instance.soundOn){
				Destroy(this.gameObject , asMeow.clip.length);
			}
			else Destroy(this.gameObject , 1f);
			mGameManager.CatDie(pos);


			//Destroy (this.gameObject);
		}

		/*
		if (mGameManager.ENEMY_CAT_COL <= 0) {

			GameManager.CurrentGameState = GameState.Won;
			mGameManager.PlayerWon();
		}
		if (mGameManager.PLAYER_CAT_COL <= 0) {
			
			GameManager.CurrentGameState = GameState.Lost;
			mGameManager.PlayerLost();
		}
		*/
	}
	

}
