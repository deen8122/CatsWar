using UnityEngine;
using System.Collections;
using Assets.Scripts;
public class CameraFollow : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
		// mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		 

		//private const float maxCameraX = 13;
        StartingPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
		if (GameManager.CurrentGameState == GameState.BOMB_FLYING)
        {
            if (BirdToFollow != null) //bird will be destroyed if it goes out of the scene
            {
                var birdPosition = BirdToFollow.transform.position;
			
				float x = Mathf.Clamp(birdPosition.x , minCameraX, maxCameraX);
                //camera follows bird's x position
				if(offsetX==0){
					StartingPosition = transform.position;
					offsetX = x- StartingPosition.x;
				}
				//if(birdPosition.x> StartingPosition.x){
				transform.position = new Vector3(x - offsetX , StartingPosition.y, StartingPosition.z);
				//}
                
            }
            else {
				StartingPosition = transform.position;
				IsFollowing = false;
				offsetX = 0;
			}
                
        }
		else {
			//mainCamera.GetComponents<CameraMove>().minX;
			StartingPosition = transform.position;
			IsFollowing = false;
			offsetX = 0;
		}
    }

    [HideInInspector]
    public Vector3 StartingPosition;
	//GameObject mainCamera;

    private const float minCameraX = -2;
    private const float maxCameraX = 13;
	private float offsetX=0;
    //[HideInInspector]
    public bool IsFollowing = true;
   // [HideInInspector]
    public static Transform BirdToFollow;
}
