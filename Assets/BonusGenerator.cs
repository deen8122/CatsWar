using UnityEngine;
using System.Collections;

public class BonusGenerator : MonoBehaviour {

	public GameObject bonusBinokl;

	public GameObject bonusLife;
	public void CreateBonusBinokl(Vector3 position){
		Instantiate (bonusBinokl,position , Quaternion.identity);

	}
	public void CreateBonusLife(Vector3 position){
		Instantiate (bonusLife,position , Quaternion.identity);
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
