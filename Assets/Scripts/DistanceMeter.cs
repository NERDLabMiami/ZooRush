using UnityEngine;
using System.Collections;

/** Idicates the distance between the player and the animal by moving icons on a bar on screen.
 * 
 * @author: Ebtissam Wahman
 */ 
public class DistanceMeter : MonoBehaviour
{
	private GameObject characterIcon;
	private GameObject animalIcon;

	private float charIconStarting;

	private float maxLength;
	private float maxBounds;
	private float currentDistanceDiff;
	private float distanceDiffMin;

	void Start ()
	{	
		characterIcon = GameObject.Find ("Character Icon");
		animalIcon = GameObject.Find ("Animal Icon");

		charIconStarting = characterIcon.transform.localPosition.x;
		distanceDiffMin = GameObject.FindObjectOfType<SceneManager> ().distanceDiffMin;
		maxLength = Mathf.Abs (animalIcon.transform.localPosition.x 
			- characterIcon.transform.localPosition.x)
			- (1.1f * characterIcon.transform.localScale.x) 
			- (1.1f * animalIcon.transform.localScale.x);
		maxBounds = 50f + distanceDiffMin;
	}
	
	void Update ()
	{
		maxBounds = 50f + distanceDiffMin;
		currentDistanceDiff = GameObject.FindObjectOfType<SceneManager> ().currentDistanceDiff;
		distanceDiffMin = GameObject.FindObjectOfType<SceneManager> ().distanceDiffMin;
	
		if (currentDistanceDiff >= maxBounds) {
			characterIcon.transform.localPosition = new Vector3 (charIconStarting, characterIcon.transform.localPosition.y, characterIcon.transform.localPosition.z);
		} else {
			float ratio = Mathf.Abs (currentDistanceDiff - distanceDiffMin) / maxBounds;
			float add = maxLength * ratio;
			if (add < 0.01f) {
				add = 0;
			}
			characterIcon.transform.localPosition = new Vector3 (charIconStarting + maxLength - add, characterIcon.transform.localPosition.y, characterIcon.transform.localPosition.z);
		}

	}
	
}
