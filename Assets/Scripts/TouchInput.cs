/** 
 * Code by Sebastian Lague for touch interfaces.
 * contact:  seblague@yahoo.com
 */ 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour
{

	public LayerMask touchInputMask;

	private List<GameObject> touchList = new List<GameObject> ();
	private GameObject[] touchesOld;
	private RaycastHit hit;


	void Update ()
	{


#if UNITY_EDITOR
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
			
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();

			
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray,out hit)) {
				
				GameObject recipient = hit.transform.gameObject;
				Debug.Log(recipient.name);
				touchList.Add(recipient);
				
				if (Input.GetMouseButtonDown(0)) {
					recipient.SendMessage("OnTouchDown",hit.point,SendMessageOptions.DontRequireReceiver);
				}
				if (Input.GetMouseButtonUp(0)) {
					recipient.SendMessage("OnTouchUp",hit.point,SendMessageOptions.DontRequireReceiver);
				}
				if (Input.GetMouseButton(0)) {
					recipient.SendMessage("OnTouchStay",hit.point,SendMessageOptions.DontRequireReceiver);
				}
				
			}

			foreach (GameObject g in touchesOld) {
				if (!touchList.Contains(g)) {
					g.SendMessage("OnTouchExit",hit.point,SendMessageOptions.DontRequireReceiver);
				}
			}
		}


#endif


		if (Input.touchCount > 0) {

			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo (touchesOld);
			touchList.Clear ();

			foreach (Touch touch in Input.touches) {

				Ray ray = camera.ScreenPointToRay (touch.position);

				if (Physics.Raycast (ray, out hit, touchInputMask)) {

					GameObject recipient = hit.transform.gameObject;
					touchList.Add (recipient);

					if (touch.phase == TouchPhase.Began) {
						recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Ended) {
						recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
						recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Canceled) {
						recipient.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
					}

				}

			}
			foreach (GameObject g in touchesOld) {
				if (!touchList.Contains (g)) {
					g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}

	}
}
