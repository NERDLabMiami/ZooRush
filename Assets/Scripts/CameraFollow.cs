using UnityEngine;
using System.Collections;


/** Script to make the camera follow the character.
 * @author: Ebtissam Wahman
 * 
 */ 
public class CameraFollow : MonoBehaviour
{
	private GameObject character;

	public bool cameraFollowEnabled;
	public bool cameraSettled;
	public float characterOffset;
	
	void Start ()
	{
		character = GameObject.FindGameObjectWithTag ("character");
		cameraSettled = false;
	}

	void LateUpdate ()
	{
		if (GameStateMachine.currentState != (int)GameStateMachine.GameState.StartLevel) { //we are not in the intro gameplay state and need to move the camera
			if (cameraSettled) {
				transform.position = addXOffsetToCharacter (characterOffset);
			}
		}
	}

	public void moveCameraToCharacterOffset (float offset)
	{
		StartCoroutine (moveCameraToXPosition (offset));
	}

	private IEnumerator moveCameraToXPosition (float offset)
	{
		float velocity = 0;
		cameraSettled = false;
		characterOffset = offset;
		float currentDistance = transform.position.x - character.transform.position.x;

		while (currentDistance >= offset) {
			currentDistance = transform.position.x - character.transform.position.x;
			float nextX = Mathf.SmoothDamp (transform.position.x, character.transform.position.x + offset, ref velocity, 0.2f);
			transform.position = new Vector3 (nextX, transform.position.y, transform.position.z);
			yield return new WaitForEndOfFrame ();
		}
		cameraSettled = true;
	}

	private Vector3 addXOffsetToCharacter (float offset)
	{
		return new Vector3 (character.transform.position.x + offset, transform.position.y, transform.position.z);
	}

}
