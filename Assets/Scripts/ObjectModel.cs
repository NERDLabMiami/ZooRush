using UnityEngine;
using System.Collections;

/** Contains basic information about the object as well as any 
 * data it may need in order to interact with other game elements.
 * @author Ebtissam Wahman
 */ 
public class ObjectModel : MonoBehaviour
{	
	public string objectType;
	public AudioClip[] soundClip;
	public bool moves;
	public Vector2 speed;
	public bool reactiveAnimation;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (moves) {
			rigidbody2D.velocity = speed;
			if (transform.position.x > GameObject.FindObjectOfType<PlayerControls> ().transform.position.x + 40f) {
				destroyObstacle ();
			} else {
				if (40f < Mathf.Abs (transform.position.x - Camera.main.transform.position.x)) {
					destroyObstacle ();
				}
			}
		}
		
	}

	public void collisionDetected ()
	{
		GetComponent<Animator> ().SetTrigger ("Flash");
	}

	public void stopMoving ()
	{
		moves = false;
		rigidbody2D.velocity = new Vector2 (0, 0);
	}

	public void resetState ()
	{
		if (!gameObject.activeSelf) {
			gameObject.SetActive (true);
		}
		if (GetComponent<Animator> () != null) {
			GetComponent<Animator> ().SetTrigger ("Reset");
		}
		if (GetComponentInChildren<CollisionDetect> () != null) {
			GetComponentInChildren<CollisionDetect> ().resetTouch ();
		}
	}

	private void destroyObstacle ()
	{
		StartCoroutine (GameObject.FindObjectOfType<SceneRepeater> ().DestroyObstacle (gameObject));
	}

	public void interactWithCharacter ()
	{
		if (soundClip != null) {

			GameObject.FindObjectOfType<AudioController> ().objectInteraction (soundClip);

		}
	}
}
