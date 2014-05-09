using UnityEngine;
using System.Collections;

/** Handles net-animal interaction
* @author Ebtissam Wahman
*/
public class NetHandler : MonoBehaviour
{
	public bool collided;

	void Start ()
	{
		collided = false;
	}

	void Update ()
	{
		if (transform.position.y < Camera.main.transform.position.y - 10f) { //if completely out of view of the camera
			Destroy (gameObject); //destroy this net instance
		}
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		collided = true;
		//on interaction with another object with a trigger collider
		if (coll.gameObject.name.Contains ("Animal")) { //if that object is an animal
			StartCoroutine (interact (coll.gameObject));
			coll.gameObject.GetComponentInChildren<Animal> ().caught = true;
			GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("Gotcha!");
			GameState.requestTransition ();
		} else {
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.isKinematic = true;
			GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("I Missed!");
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
