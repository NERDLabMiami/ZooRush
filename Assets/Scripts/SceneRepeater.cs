using UnityEngine;
using System.Collections;

public class SceneRepeater : MonoBehaviour
{
	private GameObject[] sceneObjects;
	float sceneWidth;

	void Start ()
	{
		sceneObjects = GameObject.FindGameObjectsWithTag ("repeatable");
		GameObject rightmostObject = sceneObjects [0];
		GameObject leftmostObject = sceneObjects [0];
		
		foreach (GameObject element in sceneObjects) {
			if (element.transform.position.x > rightmostObject.transform.position.x) {
				rightmostObject = element;
			}
			if (element.transform.position.x < leftmostObject.transform.position.x) {
				leftmostObject = element;
			}
		}
		sceneWidth = rightmostObject.transform.position.x - leftmostObject.transform.position.x;
		sceneWidth += (rightmostObject.GetComponent<SpriteRenderer> ().sprite.bounds.extents.x);
		sceneWidth += (leftmostObject.GetComponent<SpriteRenderer> ().sprite.bounds.extents.x * 1.75f);
	}
	
	void Update ()
	{
		foreach (GameObject element in sceneObjects) {
			if (element.transform.position.x < Camera.main.transform.position.x - 15f) {

				element.transform.position = new Vector3 (
														element.transform.position.x + sceneWidth,
														element.transform.position.y,
														element.transform.position.z);
			}
		}
	}
}
