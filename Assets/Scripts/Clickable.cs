using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.collider.gameObject == gameObject) {
					GameObject.FindObjectOfType<PainIndicator> ().objectInteraction (gameObject);
				}
			}
		}
	}
}