using UnityEngine;
using System.Collections;
using Assets.Scripts;

/// <summary>
/// Found in 
/// http://unity3d.com/pt/learn/tutorials/modules/beginner/platform-specific/pinch-zoom
/// Contains both perspective and orthographic stuff, in this 2D game we'll
/// be using only the orthographic one
/// </summary>
public class CameraPinchToZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.1f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.1f;        // The rate of change of the orthographic size in orthographic mode.
	private Vector3 posd = new Vector3(0,0,0);

	private float lastZoomDistance = float.PositiveInfinity; // remember that this should   be reset to infinite on touch end
	private float maxZoomOut = 9;
	private float maxZoomIn = 3;
	private float zoomSpeed = 0.5f;
	

	void LateUpdate() {
		if (GameManager.CurrentGameState == GameState.CAMERA_ZOOMING) {
			GameManager.CurrentGameState = GameState.CAMERA_MOVING;
		}
	}
    void Update()
    {

		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if(GetComponent<Camera>().orthographicSize < maxZoomOut){
				GetComponent<Camera>().orthographicSize+=orthoZoomSpeed;
			}

			//transform.position.x = 500;
			//transform.position.y = Mathf.Lerp(maximumY, minimumY, Time.time);
			//transform.position.z = Mathf.Lerp(transform.position.z, transform.position.z + zoomSpeed, Time.time);
			//print("Wheel Forward" + transform.position);
			f_resize = true;
		}
		
		// Mouse wheel moving backwards
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			f_resize = true;
			if(GetComponent<Camera>().orthographicSize > maxZoomIn){
				GetComponent<Camera>().orthographicSize-=orthoZoomSpeed;
			}

			//transform.position.x = 500;
			//transform.position.y = Mathf.Lerp(minimumY, maximumY, Time.time);
		//	transform.position.z = Mathf.Lerp(transform.position.z, transform.position.z - zoomSpeed, Time.time);
			//print("Wheel Backward" + transform.position);
		}
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
			f_resize = true;
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


			//float Xd = (transform.position.x - touchZeroPrevPos.x)*orthoZoomSpeed;
			//posd.x = Xd;
			//posd.y = transform.position.y;
			//posd.z = transform.position.z;
			//transform.position = posd;
            // If the camera is orthographic...
            if (GetComponent<Camera>().orthographic)
            {

                // ... change the orthographic size based on the change in distance between the touches.
                GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed/3;

                // Make sure the orthographic size never drops below zero.
				GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, maxZoomIn, maxZoomOut);
            }
            else //perspective
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
            }


 

        }

		if( f_resize){
			//GameManager.CurrentGameState = GameState.CAMERA_ZOOMING;
			Vector3 bottomLeft = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0,transform.position.y,-transform.position.z));

			GetComponent<CameraMove>().minY  = transform.position.y + (y2 - bottomLeft.y);
			GetComponent<CameraMove>().maxY=4.5f+ GetComponent<CameraMove>().minY ;
			if(bottomLeft.y < y2)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + (y2 - bottomLeft.y), transform.position.z);

			}
			if(bottomLeft.y > y2+1f)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
				
			}
			//y2 = transform.position.y;
			f_resize = false;
		}
    }


	public float constZ;
	private float y2;
	private bool f_resize = false;
	void Start(){

		y2 = transform.position.y;
		Vector3 bottomLeft = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0,0.2f,-transform.position.z));
		y2 = bottomLeft.y - transform.position.y+0.5f;
		print ("constZ="+constZ+" y="+GetComponent<Camera>().transform.position.y+" size="+GetComponent<Camera>().orthographicSize);


	}



	/*
	 * 
	 * 	void Update2() {
		
		if (Input.touchCount == 2) {
			
			Vector2 touch0, touch1;
			float distance;
			Vector2 pos = new Vector2(transform.position.x, transform.position.y);
			touch0 = Input.GetTouch(0).position - pos;
			touch1 = Input.GetTouch(1).position - pos;
			
			Vector2 zoomCenter = (touch0 + touch1) / 2;
			
			distance = Vector2.Distance(touch0, touch1);
			if(lastZoomDistance == float.PositiveInfinity) {
				lastZoomDistance = distance;
			} else {
				if(distance > lastZoomDistance && GetComponent<Camera>().orthographicSize + zoomSpeed <= maxZoomOut) {
					GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + zoomSpeed;
					// Assuming script is attached to camera - otherwise, change the transform.position to the camera object
					transform.position = Vector3.Lerp(transform.position, zoomCenter, Time.deltaTime);
				} else if(distance < lastZoomDistance && GetComponent<Camera>().orthographicSize - zoomSpeed >= maxZoomIn) {
					GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - zoomSpeed;
					transform.position = Vector3.Lerp(transform.position, zoomCenter, Time.deltaTime);
				}
			}
			lastZoomDistance = distance;
		}
		
	}
	 */ 
}