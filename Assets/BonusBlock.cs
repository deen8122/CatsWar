using UnityEngine;
using System.Collections;

public class BonusBlock : MonoBehaviour {


	public int bonusType = 0;
	private GameManager	mGameManager = null;
	private bool f_block = true;
	void OnCollisionEnter2D(Collision2D col)
	{
		if(f_block) return;
		if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;
		if(col.gameObject.tag != "tomato" || mGameManager.isEnemy == true){
			return;
		}

		if(bonusType==0){
			SpecialEffectsHelper.Instance.BonusGetEffect(transform.position);
			mGameManager.plusBinokl(3);
		}
		// Прибавляем жизнь
		if(bonusType==1){
			SpecialEffectsHelper.Instance.CatPlusLifeEffect(transform.position);
			mGameManager.plusLife();
		}

		    SpecialEffectsHelper.Instance.CatEffect(transform.position);
			this.transform.position  = new Vector3(-1000,-1000,0);
			Destroy(this.gameObject);

	
		
		
		
		
	}
	IEnumerator unsetBlock() {

		yield return new WaitForSeconds(2f);
		f_block = false;
	}
	// Use this for initialization
	void Start () {
		f_block = true;
		StartCoroutine (unsetBlock());
		mGameManager = GameObject.FindGameObjectWithTag ("gm").GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
