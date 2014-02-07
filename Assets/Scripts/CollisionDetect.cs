using UnityEngine;
using System.Collections;

/* For elements that trigger a bouce back reaction.
 *
 *	@author Ebtissam Wahman
 */
public class CollisionDetect : MonoBehaviour
{	
	public bool signalSent; //if the pain bar has been notified of this collision

	public ObjectModel objectModel;

	void Start ()
	{
		signalSent = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.Equals (GameObject.FindGameObjectWithTag ("character"))) {
			if (!signalSent) {
				objectModel.interactWithCharacter (other);
				objectModel.collisionDetected ();
				signalSent = true;
			}
		}
	}
	
	public void resetTouch ()
	{
		signalSent = false;
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.name.Contains ("Character")) {
			if (!signalSent) {
				objectModel.interactWithCharacter (coll.collider as Collider2D);
				objectModel.collisionDetected ();
				signalSent = true;
			}
		}
	}
	
}