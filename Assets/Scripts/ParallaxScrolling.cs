using UnityEngine;
using System.Collections;
using Assets.Scripts;
public class ParallaxScrolling : MonoBehaviour {

	private Vector3 initLocaL;
	private float initZoom;
	void Resize2()
	{
		SpriteRenderer sr=GetComponent<SpriteRenderer>();
		if(sr==null) return;
		
		transform.localScale=initLocaL;
		
		float width=sr.sprite.bounds.size.x;
		float height=sr.sprite.bounds.size.y;
		
		
		float worldScreenHeight=Camera.main.orthographicSize +initZoom;
		float worldScreenWidth=worldScreenHeight/Screen.height*Screen.width +initZoom;
		
		Vector3 xWidth = transform.localScale;
		xWidth.x=worldScreenWidth * width ;
		transform.localScale=xWidth;
		//transform.localScale.x = worldScreenWidth / width;
		Vector3 yHeight = transform.localScale;
		yHeight.y=worldScreenHeight *height;
		transform.localScale=yHeight;
		//transform.position = new Vector3(0,0,0);
		//transform.localScale.y = worldScreenHeight / height;
		
	}

	private SpriteRenderer sr;
	void Resize()
	{
		 sr=GetComponent<SpriteRenderer>();
		if(sr==null) return;

		transform.localScale=initLocaL;
		float worldScreenHeight=Camera.main.orthographicSize;
		Vector3 xWidth = transform.localScale;
		xWidth.x=worldScreenHeight / initZoom ;

		transform.localScale=xWidth;
		//transform.localScale.x = worldScreenWidth / width;
		Vector3 yHeight = transform.localScale;
		yHeight.y=worldScreenHeight / initZoom ;

		transform.localScale=yHeight;
		

		
	}
	// Use this for initialization
	void Start () {
        camera = Camera.main;
        previousCameraTransform = camera.transform.position;
		initLocaL = this.gameObject.transform.localScale;
		initZoom  = Camera.main.orthographicSize/initLocaL.x;
		SpriteRenderer sr=GetComponent<SpriteRenderer>();
		print ("initZoom= "+initZoom+" w= "+ sr.sprite.bounds.size.x);
	}

    Camera camera;
	
	/// <summary>
	/// similar tactics just like the "CameraMove" script
	/// </summary>
	void LateUpdate () {
		if (true|| GameManager.CurrentGameState == GameState.CAMERA_MOVING|| GameManager.CurrentGameState == GameState.CAMERA_ZOOMING)
		{
			Resize();
		}
        Vector3 delta = camera.transform.position - previousCameraTransform;
		//delta.y = 0; 
		delta.z = 0;
        transform.position += delta / ParallaxFactor;


        previousCameraTransform = camera.transform.position;
	}

    public float ParallaxFactor;

    Vector3 previousCameraTransform;

    ///background graphics found here:
    ///http://opengameart.org/content/hd-multi-layer-parallex-background-samples-of-glitch-game-assets
}
