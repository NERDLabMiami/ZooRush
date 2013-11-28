using UnityEngine;
using System.Collections;

public class SceneRepeater : MonoBehaviour
{
	private SpriteRenderer[] sceneObjects;
	private GameObject[] sceneThings;
	float sceneWidth;

	void Start ()
	{
		sceneObjects = FindObjectsOfType<SpriteRenderer> ();
		sceneThings = GameObject.FindGameObjectsWithTag ("repeatable");
		SpriteRenderer rightmostObject = sceneObjects [0];
		SpriteRenderer leftmostObject = sceneObjects [0];
		
		foreach (SpriteRenderer element in sceneObjects) {
			if (element.transform.position.x > rightmostObject.transform.position.x) {
				rightmostObject = element;
			}
			if (element.transform.position.x < leftmostObject.transform.position.x) {
				leftmostObject = element;
			}
		}
		sceneWidth = rightmostObject.transform.position.x - leftmostObject.transform.position.x;
		sceneWidth += (rightmostObject.sprite.bounds.extents.x);
		sceneWidth += (leftmostObject.sprite.bounds.extents.x * 1.75f);
	}
	
	void Update ()
	{
		foreach (GameObject element in sceneThings) {
			if (element != null) {
				if (element.transform.position.x < Camera.main.transform.position.x - 15f) {

					element.transform.position = new Vector3 (
														element.transform.position.x + sceneWidth,
														element.transform.position.y,
														element.transform.position.z);
				}
			}
		}
	}
}
