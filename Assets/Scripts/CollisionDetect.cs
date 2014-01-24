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
	public bool touchOnce;

	private bool touched;
	private bool signalSent; //if the pain bar has been notified of this collision

	void Start ()
	{
		signalSent = false;
		touched = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.Equals (GameObject.FindGameObjectWithTag ("character"))) {
			if (isPowerUp) {
				if (!signalSent) {
					signalSent = true;
					GameObject.FindObjectOfType<PainIndicator> ().GetComponent<PainIndicator> ().objectInteraction (transform.parent.gameObject);
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
				if (touchOnce) {
					if (!touched) {
						if (other.transform.position.y < -2.5f) {
							other.rigidbody2D.AddForce (new Vector2 (-350f, 50f));
						} else {
							other.rigidbody2D.AddForce (new Vector2 (-350f, -50f));
						}
						Obstacle parent = transform.parent.GetComponent<Obstacle> ();
						parent.setBehind ();
						other.GetComponent<PlayerControls> ().resetSpeed ();
						touched = true;
					}
				} else {
					if (other.transform.position.y < -2.5f) {
						other.rigidbody2D.AddForce (new Vector2 (-350f, 50f));
					} else {
						other.rigidbody2D.AddForce (new Vector2 (-350f, -50f));
					}
				}
				if (gameObject.transform.parent.name.Contains ("Trash")) {
					gameObject.transform.parent.gameObject.GetComponent<Animator> ().SetTrigger ("Fall");
				}
				if (isInfection) {
					if (!signalSent) {
						signalSent = true;
						GameObject.FindObjectOfType<PainIndicator> ().GetComponent<PainIndicator> ().objectInteraction (transform.parent.gameObject);
						GameObject.FindObjectOfType<AudioHandler> ().playSound ("INFECTION");
					}
					Obstacle parent = transform.parent.GetComponent<Obstacle> ();
					parent.collisionDetected ();
					other.GetComponent<PlayerControls> ().resetSpeed ();
				} else {
					if (canMove) {
						transform.parent.GetComponent<Obstacle> ().stopMoving ();
						if (transform.parent.name.Contains ("Car") || transform.parent.name.Contains ("Truck") || transform.parent.name.Contains ("Van")) {
							GameObject.FindObjectOfType<SceneManager> ().hitByVehicle = true;
						}
						if (transform.parent.name.Contains ("Lawnmower")) {
							GameObject.FindObjectOfType<PainIndicator> ().objectInteraction (transform.parent.gameObject);
						}
					} 
					if (!touchOnce) {
						other.GetComponent<PlayerControls> ().resetSpeed ();
					}
				}
			}
		}
	}
	
	public void resetTouch ()
	{
		signalSent = false;
		touched = false;
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.name.Contains ("Character")) {
			if (transform.parent.name.Contains ("Ball")) {
				GameObject.FindObjectOfType<AudioHandler> ().playSound ("BALL");
			}
		}
	}
	
}