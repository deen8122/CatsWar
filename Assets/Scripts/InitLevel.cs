using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InitLevel : MonoBehaviour {

	public string levelname="test";
	public Sprite passedSprite;
	// Use this for initialization
	void Start () {
	
		int level =PlayerPrefs.GetInt(levelname);
		print (level);
		if( level==0 ){
			this.GetComponent<Image>().sprite = passedSprite;
			this.GetComponent<Button>().interactable = false;
		}
		else {

		}

	}
	public void StartGame(){

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
