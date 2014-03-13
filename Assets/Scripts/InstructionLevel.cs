using UnityEngine;
using System.Collections;

public class InstructionLevel : MonoBehaviour
{

	private int instructionIndex;
	public GameObject left;
	public GameObject right;
	public Camera[] slides;
	private RaycastHit2D[] results = new RaycastHit2D[1]; //array of objects touched by the ray

	void Start ()
	{
		instructionIndex = 0;
	}

	void Update ()
	{
		if (instructionIndex == 0) {
			left.SetActive (false);
		} else {
			left.SetActive (true);
		}

		if (instructionIndex == slides.Length - 1) {
			right.SetActive (false);
		} else {
			right.SetActive (true);
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = camera.ScreenPointToRay (Input.mousePosition); //create a ray going from the mouse into the z-direction
			if (Physics2D.GetRayIntersectionNonAlloc (ray, results) > 0) { //if we've hit at least one collider
				foreach (RaycastHit2D element in results) { //iterate through the colliders we've touched
					if (element.transform == left.transform) {
						moveCameras (true);
					}
					if (element.transform == right.transform) {
						moveCameras (false);
					}
				}
			}
		}
	}

	private void moveCameras (bool left)
	{
		if (left && instructionIndex > 0) {
			instructionIndex = instructionIndex - 1;
			foreach (Camera slide in slides) {
				Rect slideRect = slide.rect;
				slideRect.x++;
				slide.rect = slideRect;
			}
		} else {
			if (!left && instructionIndex < slides.Length - 1) {
				instructionIndex = instructionIndex + 1;
				foreach (Camera slide in slides) {
					Rect slideRect = slide.rect;
					slideRect.x--;
					slide.rect = slideRect;
				}
			}
		}



	}
}
