using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine;

// Создание экземпляра частиц
public class SpecialEffectsHelper : MonoBehaviour
{
	// Синглтон
	public static SpecialEffectsHelper Instance;
	

	public ParticleSystem mSnowEffect;
	public ParticleSystem mCatEffect;
	public GameObject arrowdown;

	public ParticleSystem iceBreakEffect;
	public ParticleSystem catDieEffect;


	public ParticleSystem clifeDieEffect;
//	public GameObject _shooter_cat;
//	private GameObject shooter_cat;

	public GameObject block_breakble;
	public GameObject ice_block_breakble;
	private GameObject[] bb_block;
	private GameObject[] bb_iceblock;
	private void init_BB(){
		bb_block = new GameObject[4];
		for (int i= 0; i < 4; i++) {
			bb_block[i] = Instantiate (block_breakble);
			bb_block[i].transform.position = new Vector3(-10,0,0);
			bb_block[i].SetActive(false);
		}
		bb_iceblock = new GameObject[4];
		for (int i= 0; i < 4; i++) {
			bb_iceblock [i] = Instantiate (ice_block_breakble);
			bb_iceblock [i].transform.position = new Vector3 (-10, 0, 0);
			bb_iceblock[i].SetActive(false);
		}

	}
	public void bb_set_pos(int type , Vector3 pos, Vector3 force){
		force /= 2;
		GameObject[] bb;
		if (type == Constants.BOX_TYPE_BLOCK) {
			bb = bb_block;
		} else {
			bb = bb_iceblock;
		}
		for (int i =0; i < bb.Length; i++) {
			if(bb[i].activeSelf==false && bb[i].transform.position.x==-10){
				bb[i].SetActive(true);
				bb[i].transform.position = pos;
				Rigidbody2D[] c;
				c = bb[i].GetComponentsInChildren<Rigidbody2D>();
				for(int j = 0; j < c.Length; j++){
					c[j].velocity = new Vector2(force.x , force.y-j);

				}
				StartCoroutine (bb_hide(bb[i]));
			    break;
			}
		}

	}
	IEnumerator  bb_hide(GameObject go){
		yield return new WaitForSeconds(2f);
		Transform[] c;
		c = go.GetComponentsInChildren<Transform>();
		for(int j = 0; j < c.Length; j++){
			c[j].position = new Vector2(0, 0);
			
		}
		go.transform.position = new Vector3(-10,0,0);
		go.SetActive(false);
	}


	void Awake()
	{
		// регистрация синглтона
		if (Instance != null)
		{
			Debug.LogError("Несколько экземпляров SpecialEffectsHelper!");
		}
		
		Instance = this;
		init_BB ();

	}
	void Start(){

		//shooter_cat = Instantiate (_shooter_cat);
	}
	public void BonusGetEffect(Vector3 position)
	{
		instantiate(catDieEffect, position);
	}
	// Создать взрыв в данной точке
	public void CatDieEffect(Vector3 position)
	{
		instantiate(catDieEffect, position);
	}
	public void CatPlusLifeEffect(Vector3 position)
	{
		instantiate(clifeDieEffect, position);
	}
	// Создать взрыв в данной точке
	public void IceBreakEffect(Vector3 position)
	{
		//print ("CatEffect");
		//instantiate(iceBreakEffect, position);
	}

	public void ArrowShow(Vector3 pos){
		//print ("ArrowShow");
		//shooter_cat.SetActive(true);
		//pos.y+=1;
		//shooter_cat.transform.position = pos;
		//StartCoroutine (,1));

	}
	// Создать взрыв в данной точке
	public void CatEffect(Vector3 position)
	{
		//print ("CatEffect");
		instantiate(mCatEffect, position);
	}
	// Создать взрыв в данной точке
	public void SnowEffect(Vector3 position)
	{
		instantiate(mSnowEffect, position);
	}
	// Создать взрыв в данной точке
	public void Explosion(Vector3 position)
	{
		// Дым над водой
		//instantiate(smokeEffect, position);
		
		// да-даам
		
		// Огонь в небе
		//instantiate(fireEffect, position);
	}
	
	// Создание экземпляра системы частиц из префаба
	private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
	{
		ParticleSystem newParticleSystem = Instantiate(
			prefab,
			position,
			Quaternion.identity
			) as ParticleSystem;
		
		// Убедитесь, что это будет уничтожено
		Destroy(
			newParticleSystem.gameObject,
			newParticleSystem.startLifetime
			);
		
		return newParticleSystem;
	}
}

