using UnityEngine;
using System.Collections;

/**
 * GUI Controller for mouse/touch interaction between the user and game objects on screen.
 */ 
public class GUITouchController : MonoBehaviour
{
	private RaycastHit2D[] results = new RaycastHit2D[1]; //array of objects touched by the ray

	void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //create a ray going from the mouse into the z-direction
		if (Physics2D.GetRayIntersectionNonAlloc (ray, results) > 0) { //if we've hit at least one collider
			foreach (RaycastHit2D element in results) { //iterate through the colliders we've touched
				if (element.transform != null) {
					TouchHandler touchHandler = element.transform.gameObject.GetComponentInChildren<TouchHandler> (); //check if the object has a touch handler
					if (touchHandler) {
						touchHandler.objectTouched ();//send signal to object that it was touched
					}
				}
			}
		}
	}
}
