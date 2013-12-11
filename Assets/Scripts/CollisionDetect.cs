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

	private bool signalSent; //if the pain bar has been notified of this collision

	void Start ()
	{
		signalSent = false;
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
					} else {
						if (transform.parent.gameObject.name.Contains ("Water Bottle")) {
							GameObject.FindObjectOfType<AudioHandler> ().playSound ("WATERDRINK");
						}
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
					if (canMove) {
						transform.parent.GetComponent<Obstacle> ().stopMoving ();
						if (transform.parent.name.Contains ("Car")) {
							GameObject.FindObjectOfType<SceneManager> ().fainted = true;
						}
						if (transform.parent.name.Contains ("Lawnmower")) {
							GameObject.FindObjectOfType<PainBar> ().objectInteraction (transform.parent.gameObject);
						}
					} 
					other.GetComponent<PlayerControls> ().resetSpeed ();

				}
			}
		}

	}
}