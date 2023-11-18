using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Fire : MonoBehaviour {
	public float stretchLimit = 1.0f;
	
	//public LineRenderer stringBack;
	//public LineRenderer stringFront;
	
	private SpringJoint2D spring;
	private bool clicked;
	private Transform slingshot;
	private Ray mouseRay;
	private Ray leftRay;
	private float stretchSquare;
	//	private float radius;
	private Vector2 velocityX;
	
	
	public GameObject  bombTomato2;
	private GameObject  bombTomato;
	private GameObject  bombTomato_2;
	private int ACTION = 0;
	private static int MOVE = 1;
	Vector3 pos1;
	public float ThrowSpeed = 100;
	private float distance = 1;
	//public SlingshotState mSlingshotState;
	public bool isEnemy = true;
	private LineRenderer TrajectoryLineRenderer;
	private bool fComputer = false;
	private Vector3 ENEMY_POSITION;
	public bool isFired = false;
	public Animator AnimatorFired;
	public GameObject strleka = null;
	private GameManager mGameManager=null;
	public int TYPE_CAT = 1;
	public int napravlenie = 1;
	void OnDestroy(){
		
		Destroy(bombTomato);
	}
	//private SlingshotState slingshotState;
	//------------------------------------------------
	void Start(){
		bombTomato = Instantiate (bombTomato2);
		bombTomato.transform.position = new Vector3 (-10,-1,0);
		bombTomato.SetActive (false);
		if (isEnemy) {
			bombTomato.layer = LayerMask.NameToLayer("enemy_bomb");
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("enemy_bomb"), LayerMask.NameToLayer("enemy"), true);
		} else {
			bombTomato.layer = LayerMask.NameToLayer("player_bomb");
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player_bomb"), LayerMask.NameToLayer("player"), true);
			
			
		}

		if( this.TYPE_CAT == 2){
			print("=================================================TouchDown type=2");
			bombTomato_2 = Instantiate (bombTomato2);
			bombTomato_2.transform.position = new Vector3 (-10,-1,0);
			bombTomato_2.SetActive (false);
			bombTomato_2.GetComponent<Bomb>().isEnableBombStop = false;
			if (isEnemy) {
				bombTomato_2.layer = LayerMask.NameToLayer("enemy_bomb_2");
				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("enemy_bomb_2"), LayerMask.NameToLayer("enemy"), true);
				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("enemy_bomb_2"), LayerMask.NameToLayer("enemy_bomb"), true);
			} 

		}

		if(strleka!=null)strleka.SetActive(false);
		Physics2D.IgnoreLayerCollision(bombTomato.layer, LayerMask.NameToLayer("ignore_bomb"), true);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("enemy_bomb"), LayerMask.NameToLayer("enemy_bomb"), true);
		mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager>();
		TrajectoryLineRenderer = mGameManager.TrajectoryLineRenderer;
		
	}
	public void TouchDown(){
		print("TouchDown");
		if (isFired == true) {
			return;
		}
		
	   //  GameState.CAMERA_MOVING:
			GameManager.CurrentGameState = GameState.PressedObj;
		ACTION = MOVE;	
		bombTomato.transform.position = this.transform.position;
		bombTomato.SetActive (true);
		GameManager.CurrentGameState = GameState.PressedObj;
		pos1 = gameObject.transform.position;
		bombTomato.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		

		if(this.TYPE_CAT == 2){
			print("=================================================TouchDown type=2");
			bombTomato_2.transform.position = this.transform.position;
			bombTomato_2.SetActive (true);
			bombTomato_2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

		}

	}
	
	
	public void SetEnemyAttack(Vector3 pos){
		fComputer = true;
		pos1 = pos;
		
	}
	public void SetPosAttackPosition(Vector3 pos){
		print ("SetPosAttackPosition");
		this.ENEMY_POSITION = pos;
		
	}
	public void TouchUp(){
		
		if (isFired == true) {
			// GetComponents<Animator>().
			return;
		}
		isFired = true;

		if(strleka!=null)strleka.SetActive(false);
		GetComponent<cat2>().isLock = false;	

		print ("TouchUp");
		GameManager.CurrentGameState = GameState.BOMB_FLYING;
		ACTION = 0;
		pos1 = gameObject.transform.position;
		Vector3 location;
		if (fComputer) {
			location = ENEMY_POSITION;
		} else {
			
			location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		location.z = 0;
		//----------------------
		//we will let the user pull the bird up to a maximum distance
		if (Vector3.Distance(location , pos1) > 0.1f)
		{
			//basic vector maths :)
			
			var maxPosition = (location - pos1).normalized * 0.1f + pos1;
			//var maxPosition = (location - pos1).normalized * 0.2f;
			bombTomato.transform.position = maxPosition;
		}
		else
		{
			bombTomato.transform.position = location;
		}
		distance = Vector3.Distance(pos1, location);
		
		Vector3 velocity = pos1 - bombTomato.transform.position;
		if (distance > 2)
			distance = 2;
		
		if(fComputer ) distance = 2;
		distance*=2*napravlenie;

		bombTomato.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y) * ThrowSpeed * distance;
		bombTomato.GetComponent<Bomb> ().OnThrow ( this.isEnemy);

		if( this.TYPE_CAT == 2){
			print("=======================================TouchUp type=2");
			bombTomato_2.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y+0.01f) * ThrowSpeed * distance;
		    bombTomato_2.GetComponent<Bomb> ().OnThrow ( this.isEnemy);
		}

		//StartCoroutine (SetActiveBullet ());
		
		//bombTomato.GetComponent<Collider2D> ().isTrigger = true;
		//if (!fComputer)
		CameraFollow.BirdToFollow = bombTomato.transform;
		fComputer = false;
		SoundManager.instance.ShootSoundPlay();
		mGameManager.CatFireEvent();
		//bombTomato.GetComponent<Rigidbody2D>().AddForce(new Vector2(100,100)) ;//= n * ThrowSpeed * distance;
	}
	
	
	IEnumerator SetActiveBullet() {
		
		yield return new WaitForSeconds(0.3f);
		bombTomato.GetComponent<CircleCollider2D> ().radius = 0.16f;
		//bombTomato.GetComponent<Collider2D> ().isTrigger = false;
	}
	void Update()
	{
		if (GameManager.CurrentGameState == GameState.CREATE_OBJ)
			return;
		if (ACTION == MOVE) {
				pos1 = gameObject.transform.position;
			Vector3 location;
			if (fComputer) {
				location = ENEMY_POSITION;
			} else {
				
				location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
			location.z = 0;
			
			//we will let the user pull the bird up to a maximum distance
			if (Vector3.Distance(location , pos1) > 0.2f)
			{
				//basic vector maths :)
				
				var maxPosition = (location - pos1).normalized * 0.2f + pos1;
				//var maxPosition = (location - pos1).normalized * 0.2f;
				bombTomato.transform.position = maxPosition;
			}
			else
			{
				bombTomato.transform.position = location;
			}
			
			distance = Vector3.Distance(pos1, location);
			
			if(strleka!=null){
				if(strleka!=null)strleka.SetActive(true);
				Vector3 mouse_pos = Input.mousePosition;
				Vector3 player_pos = Camera.main.WorldToScreenPoint(this.transform.position);
				
				mouse_pos.x = mouse_pos.x - player_pos.x;
				mouse_pos.y = mouse_pos.y - player_pos.y;
				
				Vector3 dir = location;
				dir.Normalize();
				
				float angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg+180;
				
				
				strleka.transform.rotation = Quaternion.Euler (new Vector3(0, 0, angle));
				//	strleka.transform.ro.RotateAround(pos1,location, angle);
				
			}
			//trleka.transform.rotation = Quaternion.LookRotation(pos1, bombTomato.transform.position);
			
			if (distance > 2)
				distance = 2;
			
			
			if(TrajectoryLineRenderer!=null && isEnemy==false){
				if(GameManager.f_bonusPricel == true){
					DisplayTrajectoryLineRenderer2(distance);
				}
			}
			//display projected trajectory based on the distance
			//Debug.Log (distance);
		}
		
	}
	
	void DisplayTrajectoryLineRenderer2(float distance)
	{
		//SetTrajectoryLineRenderesActive(true);
		Vector3 v2 = transform.position - bombTomato.transform.position;
		int segmentCount = 25;
		float segmentScale = 1;
		Vector2[] segments = new Vector2[segmentCount];
		
		// The first line point is wherever the player's cannon, etc is
		segments[0] = bombTomato.transform.position;
		
		// The initial velocity
		Vector2 segVelocity = new Vector2(v2.x, v2.y) * ThrowSpeed * distance;
		
		float angle = Vector2.Angle(segVelocity, new Vector2(1, 0));
		float time = segmentScale / segVelocity.magnitude;
		for (int i = 1; i < segmentCount; i++)
		{
			//x axis: spaceX = initialSpaceX + velocityX * time
			//y axis: spaceY = initialSpaceY + velocityY * time + 1/2 * accelerationY * time ^ 2
			//both (vector) space = initialSpace + velocity * time + 1/2 * acceleration * time ^ 2
			float time2 = i * Time.fixedDeltaTime * 5;
			segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
		}
		
		TrajectoryLineRenderer.SetVertexCount(segmentCount);
		for (int i = 0; i < segmentCount; i++)
			TrajectoryLineRenderer.SetPosition(i, segments[i]);
	}
	
	void OnMouseDown(){
		if (isEnemy == true) return;
		if (GameManager.CurrentGameState == GameState.CREATE_OBJ)
			return;
		this.TouchDown ();
	}
	void OnMouseUp(){
		if (isEnemy == true) return;
		if (GameManager.CurrentGameState == GameState.CREATE_OBJ)
			return;
		this.TouchUp ();
	}
	
	/*
	void Awake () {
		spring = GetComponent<SpringJoint2D> ();
		slingshot = spring.connectedBody.transform;
		
	}
	
	void Start () {
		StringSetup ();
		mouseRay = new Ray(slingshot.position, Vector3.zero);
		leftRay = new Ray(stringFront.transform.position, Vector3.zero);
		stretchSquare = stretchLimit * stretchLimit;
		CircleCollider2D circleColl = GetComponent<Collider2D>() as CircleCollider2D;
		radius = circleColl.radius;
		
	}
	
	void Update () {
		if (clicked)
			Dragging ();
		
		if (spring != null) {
			if (!GetComponent<Rigidbody2D>().isKinematic && velocityX.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude) {
				Destroy (spring);
				GetComponent<Rigidbody2D>().velocity = velocityX;
			}
			
			if (!clicked)
				velocityX = GetComponent<Rigidbody2D>().velocity;
			
			StringUpdate ();
			
		}
		else {
			stringBack.enabled = false;
			stringFront.enabled = false;
		}
	}
	
	void StringSetup () {
		stringBack.SetPosition (0, stringBack.transform.position);
		stringFront.SetPosition (0, stringFront.transform.position);
		stringBack.sortingOrder = 1;
		stringFront.sortingOrder = 5;
		stringBack.sortingLayerName = "Foreground";
		stringFront.sortingLayerName = "Foreground";
	}
	
	void OnMouseDown () {
		spring.enabled = false;
		clicked = true;
		
	}
	
	void OnMouseUp () {
		spring.enabled = true;
		GetComponent<Rigidbody2D>().isKinematic = false;
		clicked = false;
	}
	
	void Dragging () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 fromSlingshot = mousePos - slingshot.position;
		
		if (fromSlingshot.sqrMagnitude > stretchSquare) {
			mouseRay.direction = fromSlingshot;
			mousePos = mouseRay.GetPoint(stretchLimit);
		}
		
		mousePos.z = 0f;
		transform.position = mousePos;
	}
	
	void StringUpdate () {
		Vector2 projectile = transform.position - stringFront.transform.position;
		leftRay.direction = projectile;
		Vector3 hold = leftRay.GetPoint(projectile.magnitude + radius);
		stringBack.SetPosition(1, hold);
		stringFront.SetPosition(1, hold);
	}
	*/
	
	
}
