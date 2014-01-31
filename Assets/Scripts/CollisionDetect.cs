using UnityEngine;
using System.Collections;

/* For elements that trigger a bouce back reaction.
 *
 *	@author Ebtissam Wahman
 */
public class CollisionDetect : MonoBehaviour
{	
//	public bool isInfection;
//	public bool isPowerUp;
//	public bool canMove;
//	public bool touchOnce;

	private bool touched;
	private bool signalSent; //if the pain bar has been notified of this collision

	public ObjectModel objectModel;

	void Start ()
	{
		signalSent = false;
		touched = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.Equals (GameObject.FindGameObjectWithTag ("character"))) {
			if (!signalSent) {
				objectModel.interactWithCharacter (other);
//				other.GetComponent<PlayerControls> ().flash ();
				objectModel.collisionDetected ();
				signalSent = true;
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
			if (!signalSent) {
				objectModel.interactWithCharacter (coll.collider);
				objectModel.collisionDetected ();
				signalSent = true;
			}
		}
	}
	
}