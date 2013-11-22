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
			other.rigidbody2D.AddForce (new Vector2 (-350f, -50f)); //TODO make variable based on current y-location (upwards if close to bottom, downwards if close to top
			if (isInfection) {
				Obstacle parent = transform.parent.GetComponent<Obstacle> ();
				parent.collisionDetected ();
			}
			other.GetComponent<PlayerControls> ().resetSpeed ();
		}

	}

}