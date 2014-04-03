using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{

	private GameObject obj1, obj2;
	private bool isMainCamera;
	private bool down;

	void Start ()
	{
		Input.simulateMouseWithTouches = true;
		isMainCamera = camera == Camera.main;
	}

	private RaycastHit2D[] results = new RaycastHit2D[1]; //array of objects touched by the ray

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			down = true;
			getObject ();
		}
		if (Input.GetMouseButtonUp (0)) {
			down = false;
			getObject ();
		}
	}
		
	private void getObject ()
	{
		if (!down) {
			if (obj1 != null) {
				UserTouchable touchable = obj1.GetComponent<UserTouchable> ();
				if (touchable != null) {
					touchable.reset ();
				}
			}
			if (obj2 != null) {
				UserTouchable touchable = obj2.GetComponent<UserTouchable> ();
				if (touchable != null) {
					touchable.reset ();
				}
			}
		}

		if (!isMainCamera) {
			cameraRaySearch (camera, ref obj1);
		}
		cameraRaySearch (Camera.main, ref obj2);

	}

	private void cameraRaySearch (Camera cam, ref GameObject objectReceived)
	{
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);

		Debug.DrawRay (ray.origin, ray.direction * 10, Color.red);

		if (Physics2D.GetRayIntersectionNonAlloc (ray, results) > 0) { //if we've hit at least one collider
			foreach (RaycastHit2D element in results) { //iterate through the colliders we've touched
				objectReceived = element.transform.gameObject;
				if (objectReceived != null) {
					processObject (objectReceived);
				}
			}
		}
	}

		
	private void processObject (GameObject objectTouched)
	{
		UserTouchable touchInterface = objectTouched.GetComponentInChildren<UserTouchable> ();
		if (touchInterface != null) {
			if (down) {
				touchInterface.onPressDown ();
			} else {
				touchInterface.onPressUp ();
			}
		}
	}
}
