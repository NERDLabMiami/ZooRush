using UnityEngine;
using System.Collections;

/* For elements that trigger a bouce back reaction.
 *
 *	@author Ebtissam Wahman
 */
public class CollisionDetect : MonoBehaviour
{	
	public bool isInfection;

	void OnTriggerEnter2D (Collider2D other)
	{

		if (other.gameObject.name == "BoyZoo") {
			if (other.transform.position.y < -2.5f) {
				other.rigidbody2D.AddForce (new Vector2 (-350f, 50f));
			} else {
				other.rigidbody2D.AddForce (new Vector2 (-350f, -50f));
			}
			if (isInfection) {
				Obstacle parent = transform.parent.GetComponent<Obstacle> ();
				parent.collisionDetected ();
			}
			other.GetComponent<PlayerControls> ().resetSpeed ();
		}

	}

}