using UnityEngine;
using System.Collections;
using Assets.Scripts;
//[RequireComponent(typeof(BoxCollider2D))]
public class Dragabble : MonoBehaviour {
        private Vector3 screenPoint;
		private Vector3 offset;
	    private int mouseAction = 0;
	    private static int MOVE = 1;
		
		void OnMouseDown() {
		mouseAction = 0;
			
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
		
		void OnMouseDrag()
		{
		mouseAction = MOVE;
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
		}
	void OnMouseUp(){
		//gameObject.GetComponent<Rigidbody2D> ()
		//transform.rotation = Quaternion.Euler(0, 0, 0);
	}
	void Update(){
		if (mouseAction == MOVE) {
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}

	}
}
