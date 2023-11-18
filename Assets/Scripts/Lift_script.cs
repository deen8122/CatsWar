using UnityEngine;
using System.Collections;

public class Lift_script : MonoBehaviour {

	public float speed = 1;
	SliderJoint2D door;
	JointMotor2D doorMotor;
	void OnTriggerEnter2D(Collider2D col){
		print ("trigger->"+col.gameObject.tag);
		if(col.gameObject.tag != "lift_bottom" ){
			doorMotor.motorSpeed =speed;
		}
		if(col.gameObject.tag != "lift_top" ){
			doorMotor.motorSpeed =-speed;
		}
		door.motor = doorMotor;
	}

	// Use this for initialization
	void Start () {
		door = gameObject.GetComponent<SliderJoint2D>();
		//we will use the jointMotor2D to add speed and later set it back to the SliderJoint2D's 
		//motor as setting the motor speed of the sliderJoint2D doesn't work.
		doorMotor = door.motor;
		//Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("no_collision"), LayerMask.NameToLayer("enemy"), true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
