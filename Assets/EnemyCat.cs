using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class EnemyCat : MonoBehaviour {
	//public AnimationClip mAnimationClip;
	
	//-------------------------------------------------------------------------------------
	void Start(){

		float rand = Random.Range (0.1f, 1.5f);
		//mAnimationClip.frameRate = rand;
	//	Animation anim = GetComponent<Animation>();
		//anim.clip.frameRate = rand;
		Animator a = GetComponent<Animator>();
		a.speed = rand;
		//anim.AddClip(mAnimationClip,"mAnimationClip");


	}

	
	
}
