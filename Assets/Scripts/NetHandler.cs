using UnityEngine;
using System.Collections;

/** Handles net-animal interaction
* @author Ebtissam Wahman
*/
public class NetHandler : MonoBehaviour
{

	void Update ()
	{
		if (transform.position.y < Camera.main.transform.position.y - 10f) { //if completely out of view of the camera
			Destroy (gameObject); //destroy this net instance
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{//on interaction with another object with a trigger collider
		if (other.gameObject.GetComponentInChildren<Animal> () != null) { //if that object is an animal
			StartCoroutine (interact (other.gameObject));
			other.gameObject.GetComponentInChildren<Animal> ().caught = true;
		}
	}
	
	private IEnumerator interact (GameObject obj)
	{
		GetComponent<SpriteRenderer> ().enabled = false; //stop rendering net
		SpriteRenderer[] animalSprites = obj.GetComponentsInChildren<SpriteRenderer> (); //all sprites under the animal object
		yield return new WaitForSeconds (0.1f); //delay before rendering full net
		foreach (SpriteRenderer sprite in animalSprites) {
			if (sprite.name.Equals ("Net")) { //activate only the net sprite
				sprite.enabled = true;
			}
		}
		Destroy (gameObject); //destroy the net
	}
}
