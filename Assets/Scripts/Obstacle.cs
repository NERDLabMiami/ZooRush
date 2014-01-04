using UnityEngine;
using System.Collections;
/** Handles all collsion based interactions with the character and 
 * the object the script is attached to.
 *
 *	@author Ebtissam Wahman
 */ 
public class Obstacle : MonoBehaviour
{
	private bool inFront; // Is the character in front or behind the obstacle?
	public bool canMove; // Cane this object move on it's own or based on physics?
	public Vector2 speed;
	private Renderer[] sprites; //All sprites that are a child of the object
	private PlayerControls player;
	
	private bool inFrontOverride;

	void Start ()
	{
		sprites = transform.GetComponentsInChildren<Renderer> ();
		player = GameObject.FindObjectOfType<PlayerControls> ();
		inFrontOverride = false;
	}

	void Update ()
	{
		if (canMove) {
			rigidbody2D.velocity = speed;
		}
		if (!inFrontOverride) {
			if (inFront) {
				foreach (Renderer sprite in sprites) {
					if (!sprite.name.Contains ("Ground Shadow") && sprite.sortingLayerName != "Obstacles-Behind") {
						sprite.sortingLayerName = "Obstacles-Behind";
					}
				}
			} else {
				foreach (Renderer sprite in sprites) {
					if (!sprite.name.Contains ("Ground Shadow") && sprite.sortingLayerName != "Obstacles-InFront") {
						sprite.sortingLayerName = "Obstacles-InFront";
					}
				}
			}
		} else {
			foreach (Renderer sprite in sprites) {
				if (!sprite.name.Contains ("Ground Shadow") && sprite.sortingLayerName != "Obstacles-Behind") {
					sprite.sortingLayerName = "Obstacles-Behind";
				}
			}
		}
		if (canMove) {
			if (transform.position.x > GameObject.FindObjectOfType<PlayerControls> ().transform.position.x + 40f) {
				destroyObstacle ();
			} else {
				if (40f < Mathf.Abs (transform.position.x - Camera.main.transform.position.x)) {
					destroyObstacle ();
				}
			}
		}
	}
	
	public void setBehind ()
	{
		inFrontOverride = true;
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject == player.gameObject) {
			inFront = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject == player.gameObject) {
			inFront = false;
		}
	}

	public void collisionDetected ()
	{
		GetComponent<Animator> ().SetTrigger ("Flash");
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

	public void stopMoving ()
	{
		canMove = false;
		rigidbody2D.velocity = new Vector2 (0, 0);
	}
}
