using UnityEngine;
using System.Collections;

/** Repeats scene objects in order to create a seamless loop.
 * @author: Ebtissam Wahman
 */ 
public class SceneRepeater : MonoBehaviour
{
	private SpriteRenderer[] sceneObjects; //All sprites in the scene
	private GameObject[] sceneThings; //All repeatable objects in the scene
	float sceneWidth; //distance from the leftmost to the rightmost sprite in the Scene

	void Awake ()
	{
		calculate ();
	}
	
	void Update ()
	{
		if (GameState.checkForState (GameState.States.Play) || GameState.checkForState (GameState.States.Launch)) {
			foreach (GameObject element in sceneThings) {
				if (element != null) {
					if (element.transform.position.x < Camera.main.transform.position.x - 50f) {
						if (!element.activeSelf) {
							element.SetActive (true);
						}
						if (element.GetComponent<Building> () != null) {
							element.GetComponent<Building> ().resetState ();
						}
						if (element.GetComponent<ObjectModel> () != null) {
							element.GetComponent<ObjectModel> ().resetState ();
						}
					
						element.transform.position = new Vector3 (
						element.transform.position.x + sceneWidth,
						element.transform.position.y,
						element.transform.position.z);
					}
				}
			}
		}
	}

	public void calculate ()
	{
		sceneObjects = GameObject.FindObjectsOfType<SpriteRenderer> ();
		sceneThings = GameObject.FindGameObjectsWithTag ("repeatable");
		SpriteRenderer rightmostObject = sceneObjects [0];
		SpriteRenderer leftmostObject = sceneObjects [0];
		
		//find the leftmost and rightmost sprites in the scene
		foreach (SpriteRenderer element in sceneObjects) {
			if (element.transform.position.x > rightmostObject.transform.position.x) {
				rightmostObject = element;
			}
			if (element.transform.position.x < leftmostObject.transform.position.x) {
				leftmostObject = element;
			}
		}
		sceneWidth = rightmostObject.transform.position.x + (rightmostObject.transform.lossyScale.x * rightmostObject.sprite.bounds.extents.x)
			- (leftmostObject.transform.position.x - leftmostObject.transform.lossyScale.x * leftmostObject.sprite.bounds.extents.x);
	}

	public void DestroyObstacle (GameObject obj)
	{
		StartCoroutine (destroyer (obj));
	}
	
	private IEnumerator destroyer (GameObject obj)
	{
		yield return new WaitForSeconds (0.1f);
		if (obj.activeSelf) {
			obj.SetActive (false);
		}
	}

	public float getSceneWidth ()
	{
		return sceneWidth;
	}
}
