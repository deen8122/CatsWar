using UnityEngine;
using System.Collections;

public class Scale : MonoBehaviour {
	public GameObject respawnPrefab;
	public GameObject[] respawns;
	void OnMouseDown(){
		//Destroy (gameObject);
		//print ("1");
		gameObject.transform.localScale += new Vector3(0.2F, 0.2F, 0);
	}
	void OnMouseUp(){
		//Destroy (gameObject);

		//GameManager gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();
		//gm.test++;
		//print ("val.test="+gm.test);
		gameObject.transform.localScale -= new Vector3(0.2F, 0.2F, 0);
	
		//respawnPrefab.transform.position = gameObject.transform.position;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			//Destroy (gameObject);
			//gameObject.transform.position = new Vector3(0, 0, 0);;
			//print ("2");
		}

	}
}
