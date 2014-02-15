using UnityEngine;
using System.Collections;
/** Script to make the camera follow the character.
 * @author: Ebtissam Wahman
 * 
 */ 
public class CameraFollow : MonoBehaviour
{
	private GameObject character;
	private NetLauncher netLauncher;

	public bool cameraSettled;
	private bool adjustToLaunchPosition;
	private bool adjustToStartPosition;

	void Start ()
	{
		character = GameObject.FindGameObjectWithTag ("character");
		netLauncher = GameObject.FindObjectOfType<NetLauncher> ();
		adjustToLaunchPosition = false;
		adjustToStartPosition = false;
		cameraSettled = false;
	}
	
	void LateUpdate ()
	{
		if (character == null) {
			character = GameObject.FindGameObjectWithTag ("character");
		}
		if (GameStateMachine.currentState != (int)GameStateMachine.GameState.StartLevel) {
			if (netLauncher.launchEnabled && !adjustToLaunchPosition) {
				transform.localPosition = Vector3.Lerp (transform.localPosition, 
				new Vector3 (character.transform.localPosition.x + 3.5f, transform.localPosition.y, transform.localPosition.z), 
				3f * Time.deltaTime);
				cameraSettled = false;
				if (transform.localPosition.x <= character.transform.localPosition.x + 3.48f) {
					adjustToLaunchPosition = true;
					cameraSettled = true;
				}
			} else {
				if (!adjustToLaunchPosition) {
					if (!adjustToStartPosition) {
						transform.localPosition = Vector3.Lerp (transform.localPosition,
					                                        new Vector3 (character.transform.localPosition.x + 5f, transform.localPosition.y, transform.localPosition.z),
					                                        3f * Time.deltaTime);
						cameraSettled = false;
						if (transform.localPosition.x <= character.transform.localPosition.x + 4.9f) {
							adjustToStartPosition = true;
							cameraSettled = true;
						}
					} else {
						transform.localPosition = new Vector3 (character.transform.localPosition.x + 5f, transform.localPosition.y, transform.localPosition.z);
					}
				} else {
					transform.localPosition = new Vector3 (character.transform.localPosition.x + 3.55f, transform.localPosition.y, transform.localPosition.z);
				}
			}
		}
	}
}
