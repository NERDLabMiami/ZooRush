using UnityEngine;
using System.Collections;

/** Contains basic information about the object as well as any 
 * data it may need in order to interact with other game elements.
 * @author Ebtissam Wahman
 */ 
public abstract class ObjectModel : MonoBehaviour
{	

	protected AudioController audioController;
	protected CollisionDetect collisionDetect;

	public abstract void collisionDetected ();

	protected void Start ()
	{
		audioController = GameObject.FindObjectOfType<AudioController> ();
		collisionDetect = GetComponentInChildren<CollisionDetect> ();
		if (collisionDetect) {
			collisionDetect.objectModel = this;
		}
	}

	public void resetState ()
	{
		resetOtherValues ();
		if (!gameObject.activeSelf) {
			gameObject.SetActive (true);
		}
	}

	protected abstract void resetOtherValues ();

	protected void destroyObstacle ()
	{
		GameObject.FindObjectOfType<SceneRepeater> ().DestroyObstacle (gameObject);
	}

	public abstract void interactWithCharacter (GameObject character);

}
