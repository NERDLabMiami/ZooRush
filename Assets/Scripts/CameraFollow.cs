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

	void Start ()
	{
		character = GameObject.FindGameObjectWithTag ("character");
		netLauncher = GameObject.FindObjectOfType<NetLauncher> ();
	}
	
	void Update ()
	{
		if (netLauncher.launchEnabled) {
			transform.localPosition = new Vector3 (character.transform.localPosition.x + 15f, transform.localPosition.y, transform.localPosition.z);
		} else {
			transform.localPosition = new Vector3 (character.transform.localPosition.x + 5f, transform.localPosition.y, transform.localPosition.z);
		}
	}
}
