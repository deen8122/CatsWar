using UnityEngine;
using System.Collections;
using Assets.Scripts;
public class Brick : MonoBehaviour
{

	void Start(){
		initHealth = Health;
		mAudioSource = GetComponent<AudioSource>();
	}

    void OnCollisionEnter2D(Collision2D col)
    {


        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;
		if(col.gameObject.tag == "lift_platform")return;
        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
        if (damage >= 10) {
			//GetComponent<AudioSource>().clip = destroySound1;

	
		}


        Health -= damage;
		//print (Health);
		if (Health < initHealth / 2) {
			if(spr2!=null)this.gameObject.GetComponent<SpriteRenderer>().sprite = spr2;
		}
		if (Health < initHealth / 3) {
			if(spr3!=null)this.gameObject.GetComponent<SpriteRenderer>().sprite = spr3;
		}
		/*
		 * 
		 * gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load("img1", typeof(Sprite)) as Sprite;
		 */
		if(col.gameObject.tag == "tomato"){
			//SpecialEffectsHelper.Instance.CatEffect(transform.position);
			
			//SoundManager.instance.RandomizeSfx(destroySound1);
			if(!mAudioSource.isPlaying && SoundManager.instance.soundOn){
				float randomPitch = Random.Range(0.8f, 1.8f);
				mAudioSource.pitch = randomPitch;
				mAudioSource.volume = damage/10;
				mAudioSource.Play();
			}
			
		}else {
			if( SoundManager.instance.soundOn && ( damage > 10) ){
				float randomPitch = Random.Range(0.8f, 1.8f);
				mAudioSource.pitch = randomPitch;
				mAudioSource.volume = damage/(2*10);
				mAudioSource.Play();
			}

		}
		//print ("briks col isBall="+isBall);

		if(!mAudioSource.isPlaying && SoundManager.instance.soundOn && isBall){
			//float randomPitch = Random.Range(0.8f, 1.8f);
			//mAudioSource.pitch = randomPitch;
			print ("briks col isBall="+isBall);
			mAudioSource.volume = damage+0.1f;
			mAudioSource.Play();

		}

        //if health is 0, destroy the block
        if (Health <= 0) {
			if(isIce){

				SpecialEffectsHelper.Instance.bb_set_pos (
					Constants.BOX_TYPE_ICE,
					this.transform.position,col.gameObject.GetComponent<Rigidbody2D>().velocity);
			}else if(!isBall) {
				SpecialEffectsHelper.Instance.bb_set_pos (
					Constants.BOX_TYPE_BLOCK,
					this.transform.position,col.gameObject.GetComponent<Rigidbody2D>().velocity);
			}


			SpecialEffectsHelper.Instance.IceBreakEffect(transform.position);
			SpecialEffectsHelper.Instance.CatDieEffect(transform.position);
			//SoundManager.instance.RandomizeSfx(destroySound2);
			//this.gameObject.SetActive(false);
			this.transform.position  = new Vector3(-1000,-1000,0);
			Destroy(this.gameObject , mAudioSource.clip.length);
			return;
		}




    }

    public float Health = 70f;
	public Sprite spr2 ;
	public Sprite spr3 ;
	private float initHealth = 0;
	private AudioSource mAudioSource;
	public bool isBall = false;
	public bool isIce = false;


    //wood sound found in 
    //https://www.freesound.org/people/Srehpog/sounds/31623/
}
