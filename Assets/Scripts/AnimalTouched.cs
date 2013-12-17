using UnityEngine;
using System.Collections;

public class AnimalTouched : MonoBehaviour
{
	public bool touched;
	
	void Start ()
	{
		touched = false;
	}
	
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
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
