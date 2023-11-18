using UnityEngine;
using System.Collections;
using Assets.Scripts;
public class Bomb : MonoBehaviour {
	private GameManager mGameManager;
	private bool isEnemy = false;
	private bool isStopped = true;
	public bool isEnableBombStop = true;



	public void  SetStopped(){
		if (isStopped)
			return;
		isStopped = true;
		StartCoroutine (BombStoped());
	}
	void FixedUpdate()
	{
		if (isStopped)
			return;
		//if we've thrown the bird
		//and its speed is very small
		if (GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= Constants.MinVelocity)
		{
			isStopped = true;
			StartCoroutine (BombStoped());
		}
	}

	IEnumerator BombStoped() {	
		yield return new WaitForSeconds(0.5f);
		this.gameObject.SetActive(false);
		if(isEnableBombStop){
			mGameManager.BombStoped();
		}

		//this.gameObject.transform.position=new Vector3(-1000,1000,this.gameObject.transform.position.z);
	}



	void OnCollisionEnter2D(Collision2D col)
	{
		//if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;
		//if (col.gameObject.tag == "ground")
		//	return;
		if (isStopped)
			return;
	//	print ("collision");
		isStopped = true;
	
				// 'Splosion!
		SpecialEffectsHelper.Instance.SnowEffect(transform.position);

		StartCoroutine (BombStoped());

	}
	
	public void OnThrow(bool isenemy)
	{
		this.isEnemy = isenemy;
		isStopped = false;
		//play the sound
		//GetComponent<AudioSource>().Play();
		//show the trail renderer
		//GetComponent<TrailRenderer>().enabled = true;
		//allow for gravity forces
		//GetComponent<Rigidbody2D>().isKinematic = false;
		//make the collider normal size
		//GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusNormal;
		//State = BirdState.Thrown;
	}
	
	IEnumerator DestroyAfter(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Destroy(gameObject);
	}


	void OnCollisionEnter2D2(Collision2D col)
	{
		if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

		if (isEnemy) {
			if (col.gameObject.tag == "enemy_cats" || col.gameObject.tag == "enemy_objects")
			{
				return;
			}

		} else {

			if (col.gameObject.tag == "player_cats" || col.gameObject.tag == "player_objects")
			{
				return;
			}

		}


	}



	// Use this for initialization
	void Start(){
	
		mGameManager= GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
/*
 * 
 * 
 * public class Bird : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //trailrenderer is not visible until we throw the bird
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";
        //no gravity at first
        GetComponent<Rigidbody2D>().isKinematic = true;
        //make the collider bigger to allow for easy touching
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusBig;
        State = BirdState.BeforeThrown;
    }



    void FixedUpdate()
    {
        //if we've thrown the bird
        //and its speed is very small
        if (State == BirdState.Thrown &&
            GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= Constants.MinVelocity)
        {
            //destroy the bird after 2 seconds
            StartCoroutine(DestroyAfter(2));
        }
    }

    public void OnThrow()
    {
        //play the sound
        GetComponent<AudioSource>().Play();
        //show the trail renderer
        GetComponent<TrailRenderer>().enabled = true;
        //allow for gravity forces
        GetComponent<Rigidbody2D>().isKinematic = false;
        //make the collider normal size
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusNormal;
        State = BirdState.Thrown;
    }

    IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public BirdState State
    {
        get;
        private set;
    }
}
 * */
