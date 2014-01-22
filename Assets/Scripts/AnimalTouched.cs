using UnityEngine;
using System.Collections;

/** Controller for The Animal Object that handles touches made by the user.
 * 
 * @author Ebtissam Wahman
 */
public class AnimalTouched : MonoBehaviour
{
	public bool touched; //Indicator for whether the animal has been touched
	
	void Start ()
	{
		touched = false; //by default the animal starts off as untouched
	}
	
	void Update ()
	{
		if (Input.GetMouseButton (0)) { //If the mouse button has been pressed/touch input received
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.collider.gameObject == gameObject) {
					touched = true;
				}
			}
		} else {
			touched = false;
		}
	}
}
