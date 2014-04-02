using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{

	private GameObject obj;

	void Start ()
	{
		Input.simulateMouseWithTouches = true;
	}

	private RaycastHit2D[] results = new RaycastHit2D[1]; //array of objects touched by the ray

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			getObject (true);
		}
		if (Input.GetMouseButtonUp (0)) {
			getObject (false);
		}
	}
		
	private void getObject (bool down)
	{
		if (!down && obj != null) {
			UserTouchable touchable = obj.GetComponent<UserTouchable> ();
			if (touchable != null) {
				touchable.reset ();
			}
		}
		Ray ray = camera.ScreenPointToRay (Input.mousePosition); //create a ray going from the mouse into the z-direction
		if (Physics2D.GetRayIntersectionNonAlloc (ray, results) > 0) { //if we've hit at least one collider
			foreach (RaycastHit2D element in results) { //iterate through the colliders we've touched
				obj = element.transform.gameObject;
				if (obj != null) {
					processObject (obj, down);
				}
			}
		}
	}
		
	private void processObject (GameObject objectTouched, bool pressDown)
	{
		Debug.Log (objectTouched.name + " touched.");
		UserTouchable touchInterface = objectTouched.GetComponent<UserTouchable> ();
		if (touchInterface != null) {
			if (pressDown) {
				objectTouched.GetComponent<UserTouchable> ().onPressDown ();
			} else {
				objectTouched.GetComponent<UserTouchable> ().onPressUp ();
			}
		}
	}
}
