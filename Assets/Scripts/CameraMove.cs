using UnityEngine;
using System.Collections;
using Assets.Scripts;


public class CameraMove : MonoBehaviour
{
	
	public float minX = -2;
	public float maxX = 13.36336f;
	public float minY = 0;
	public float maxY = 4.5f;
	private GameManager mGameManager=null;
	private bool fTouched = false;
	void Start(){
		mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager>();
	}
	// Update is called once per frame
	void Update()
	{
	
		if (Input.GetMouseButtonDown(0))
		{
			//print ("OnMouseDown"+this.transform.position.ToString());
			//GameManager.
			//camera.ScreenToWorldPoint
			Camera camera = GetComponent<Camera>();
			//fTouched = true;
			//camera.ScreenToWorldPoint
			//	mGameManager.OnMouseDown(camera.ScreenToWorldPoint(this.transform.position));
			fTouched = mGameManager.OnMouseDown(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
		if (Input.GetMouseButtonUp(0)&& fTouched == true)
		{
			fTouched = false;
			mGameManager.onMouseUp();
		}
		if( fTouched == true) return;

			if (Input.GetMouseButtonUp(0) && GameManager.CurrentGameState == GameState.CAMERA_ZOOMING)
			{
				GameManager.CurrentGameState = GameState.Playing;
			}
		
		
		if (GameManager.CurrentGameState == GameState.CAMERA_ZOOMING) {
			return;
		}
		if (GameManager.CurrentGameState == GameState.CAMERA_MOVING) {
			return;
		}
		if (GameManager.CurrentGameState == GameState.BOMB_FLYING) {
			if (Input.GetMouseButtonDown(0))
			{
				GameManager.CurrentGameState =GameState.Playing;
			}
		}
		if (GameManager.CurrentGameState == GameState.Playing)

		{
			//drag start
			if (Input.GetMouseButtonDown(0) )
			{

			
				//print (GameManager.CurrentGameState);

				timeDragStarted = Time.time;
			//	dragSpeed = 0f;
				previousPosition = Input.mousePosition;
			}
			//we calculate time difference in order for the following code
			//NOT to run on single taps ;)
			else if (Input.GetMouseButton(0) )
			{
				//find the delta of this point with the previous frame
				Vector3 input = Input.mousePosition;
				float deltaX = (previousPosition.x - input.x)  * dragSpeed;
				float deltaY = (previousPosition.y - input.y) * dragSpeed;
				//clamp the values so that we drag within limits
				float newX = Mathf.Clamp(transform.position.x + deltaX,minX, maxX);
				float newY = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
				//move camera
			
				transform.position = new Vector3(
					newX,
					newY,
					transform.position.z);
				
				previousPosition = input;
				//some small acceleration ;)
				//if(dragSpeed < 0.01f) dragSpeed += 0.002f;
			}
		}
	}
	
	private float dragSpeed = 0.01f;
	private float timeDragStarted;
	private Vector3 previousPosition = Vector3.zero;
	
	//public SlingShot SlingShot;
}
