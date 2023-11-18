using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ClickImage : MonoBehaviour {

	//public GameObject createdObj;
	private bool fcreated = false;
	void OnMouseDown() {
	
		print ("ClickImage....");
		//GameManager	mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager>();
		//this.gameObject.
		//this.gameObject
		//mGameManager.CreateObject (this.gameObject);
		GameObject newobj = Instantiate (this.gameObject);
		newobj.name = "new1";
		//position.z = 0;
		//position = Input.mousePosition;
		//newobj.AddComponent <ClickImage>();
		Destroy(this.gameObject.GetComponent<ClickImage>());

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//	if (EventSystemManager.currentSystem.IsPointerOverEventSystemObject ()) {
			if (Input.GetMouseButtonDown (0) && fcreated == false) {
				//fcreated = true;
				///print ("ClickImage 2....");
				//GameManager	mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager>();
				//mGameManager.CreateObject (createdObj);
		
			}
			if (Input.GetMouseButtonUp (0) && fcreated == true) {
				//fcreated = false;
			}
		//}
	}
}
