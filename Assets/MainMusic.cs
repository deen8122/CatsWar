using UnityEngine;
using System.Collections;

public class MainMusic : MonoBehaviour {
	public static MainMusic instance = null;
	// Use this for initialization
	public AudioSource audio;
	void Start () {
		if(instance!=null){

			Destroy(gameObject);
			Debug.Log ("One Music Destroyed");
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
		//if(GlobalOptions.isSound()){
		int music = PlayerPrefs.GetInt("music") ;
		if(music==1)audio.Play();
		else audio.Pause();
		//}
	}
	
	public void Pause(){
		this.gameObject.SetActive(false);
		audio.Pause();

	}
	
	public void Play(){

		this.gameObject.SetActive(true);
		if(!audio.isPlaying)audio.Play();
	}
}
