using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		//print ("trigger");
		//isStopped = true;
		if (other.gameObject.tag == "tomato") {
			other.gameObject.GetComponent<Bomb>().SetStopped();
			//StartCoroutine (BombStoped());
		}

	}

    void OnTriggerEnter2D2(Collider2D col)
    {
        //destroyers are located in the borders of the screen
        //if something collides with them, the'll destroy it
      //  string tag = col.gameObject.tag;
      //  if(tag == "tomato" || tag == "Pig" || tag == "Brick")
     //   {
          //  Destroy(col.gameObject);
     //   }
    }
}
