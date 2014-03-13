using UnityEngine;
using System.Collections;

/** Contains basic information about the object as well as any 
 * data it may need in order to interact with other game elements.
 * @author Ebtissam Wahman
 */ 
public abstract class ObjectModel : MonoBehaviour
{	

	protected AudioController audioController;

	public abstract void collisionDetected ();


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
		StartCoroutine (GameObject.FindObjectOfType<SceneRepeater> ().DestroyObstacle (gameObject));
	}

	public abstract void interactWithCharacter (GameObject character);

}
