using UnityEngine;
using System.Collections;

/* For elements that trigger a bouce back reaction.
 *
 *	@author Ebtissam Wahman
 */
public class CollisionDetect : MonoBehaviour
{	
	public bool isInfection;
	public bool isPowerUp;
	public bool canMove;

	private bool moved;
	private bool signalSent; //if the pain bar has been notified of this collision

	void Start ()
	{
		signalSent = false;
		moved = false;

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.Equals (GameObject.FindGameObjectWithTag ("character"))) {
			if (isPowerUp) {
				if (!signalSent) {
					signalSent = true;
					GameObject.FindObjectOfType<PainBar> ().GetComponent<PainBar> ().objectInteraction (transform.parent.gameObject);
					if (transform.parent.gameObject.name.Contains ("Pill")) {
						GameObject.FindObjectOfType<AudioHandler> ().playSound ("PILL");
					}
				}
				other.GetComponent<PlayerControls> ().flash ();
				Obstacle parent = transform.parent.GetComponent<Obstacle> ();
				parent.collisionDetected ();
			} else {
				if (other.transform.position.y < -2.5f) {
					other.rigidbody2D.AddForce (new Vector2 (-350f, 50f));
				} else {
					other.rigidbody2D.AddForce (new Vector2 (-350f, -50f));
				}
				if (isInfection) {
					if (!signalSent) {
						signalSent = true;
						GameObject.FindObjectOfType<PainBar> ().GetComponent<PainBar> ().objectInteraction (transform.parent.gameObject);
						GameObject.FindObjectOfType<AudioHandler> ().playSound ("INFECTION");

					}
					Obstacle parent = transform.parent.GetComponent<Obstacle> ();
					parent.collisionDetected ();
					other.GetComponent<PlayerControls> ().resetSpeed ();

				} else {
					other.GetComponent<PlayerControls> ().resetSpeed ();

				}
			}
		}

	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (canMove) {
			transform.parent.rigidbody2D.AddForce (new Vector2 (200f, 0f));
			gameObject.collider2D.isTrigger = true;
			transform.parent.collider2D.enabled = false;
		} 
	}
}