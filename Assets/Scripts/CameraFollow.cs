using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	private GameObject character;
	private NetLauncher netLauncher;
	private SceneManager sceneManager;

	void Start ()
	{
		character = GameObject.FindGameObjectWithTag ("character");
		netLauncher = GameObject.FindObjectOfType<NetLauncher> ();
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
	}
	
	void Update ()
	{
		if (!sceneManager.levelStartWait) {
			if (netLauncher.launchEnabled) {
				transform.localPosition = Vector3.Lerp (transform.localPosition, 
				new Vector3 (character.transform.localPosition.x + 3.5f, transform.localPosition.y, transform.localPosition.z), 
				3f * Time.deltaTime);
			} else {
				transform.localPosition = Vector3.Lerp (transform.localPosition,
				new Vector3 (character.transform.localPosition.x + 5f, transform.localPosition.y, transform.localPosition.z),
				3f * Time.deltaTime);
			}
		}
	}
}
