using UnityEngine;
using System.Collections;
/** Handles all collsion based interactions with the character and 
 * the object the script is attached to.
 *
 *	@author Ebtissam Wahman
 */ 
public class Obstacle : MonoBehaviour
{
	public bool isInfection; //If the obstacle is an infection
	private bool inFront; // Is the character in front or behind the obstacle?
	private SpriteRenderer[] sprites; //All sprites that are a child of the object
	

	void Start ()
	{
		sprites = transform.GetComponentsInChildren<SpriteRenderer> ();
	}


	void Update ()
	{
		if (inFront) {
			foreach (SpriteRenderer sprite in sprites) {
				if (sprite.sortingLayerName != "Obstacles-Behind") {
					sprite.sortingLayerName = "Obstacles-Behind";
				}
			}
		} else {
			foreach (SpriteRenderer sprite in sprites) {
				if (sprite.sortingLayerName != "Obstacles-InFront") {
					sprite.sortingLayerName = "Obstacles-InFront";
				}
			}
		}

	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		inFront = true;
	}

	void OnTriggerExit2D (Collider2D other)
	{
		inFront = false;
	}

	public void collisionDetected ()
	{
		GetComponent<Animator> ().SetTrigger ("Flash");
		GameObject.Find ("BoyZoo").GetComponent<Animator> ().SetTrigger ("Flash");
	}

	private void destroyObstacle ()
	{
		Destroy (gameObject);
	}

}
