using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour
{
	
	private Vector2 currentMousePosition;

	private bool mouseDown;

	private RaycastHit2D[] results = new RaycastHit2D[1]; //array of objects touched by the ray

	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //create a ray going from the mouse into the z-direction
		if (Physics2D.GetRayIntersectionNonAlloc (ray, results) > 0) { //if we've hit at least one collider
			foreach (RaycastHit2D element in results) { //iterate through the colliders we've touched
				if (element.transform != null && element.transform.gameObject.Equals (gameObject)) {
					if (!mouseDown && Input.GetMouseButtonDown (0)) {
						StartCoroutine (waitForMouseUp ());
					}
				}
			}
		}
	}

	private IEnumerator waitForMouseUp ()
	{
		mouseDown = true;
		while (!Input.GetMouseButtonUp(0)) {
			currentMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			currentMousePosition.y = transform.position.y;
			transform.position = currentMousePosition;
			yield return new WaitForEndOfFrame ();
		}
	}

}
