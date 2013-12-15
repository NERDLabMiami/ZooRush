using UnityEngine;
using System.Collections;

public class DialogHandler : MonoBehaviour
{
	private bool displaying;
	private bool found;
	private DialogTrigger dialog;
	private int dialogIndex;
	
	public LayerMask layerMask ;
	
	private SceneManager sceneManager;
	private CameraFollow cameraFollower;
	
	void Start ()
	{
		displaying = false;
		found = false;
		sceneManager = FindObjectOfType<SceneManager> ();
		cameraFollower = FindObjectOfType<CameraFollow> ();
		dialogIndex = 0;
	}
	
	void Update ()
	{	
		if (cameraFollower.cameraSettled) {
			RaycastHit2D detected = Physics2D.Raycast (transform.position, Vector2.up, 250f, layerMask);
			if (detected.collider != null) {
//				Debug.Log (detected.collider.name);
				if (detected.collider.gameObject.GetComponent<DialogTrigger> () != null) { // Dialog Box Found
					found = true;
					dialog = detected.collider.GetComponent<DialogTrigger> ();
					sceneManager.isPlaying = false;
				}
			}
			if (!displaying && found) {
				if (!dialog.isTriggered) {
					StartCoroutine (waitToDisplay (dialog.waitTime));
				} else {
					displaying = true;
				}
			
			} else {
				if (displaying && found) {
					if (!dialog.isDialogFinished ()) {
						displayDialog ();
					} else {
						closeDialog ();
						sceneManager.isPlaying = true;
						displaying = false;
					}
					if (InputManager.enter) {
						dialog.next ();
					}
				}
			}
		}
	}
	
	private IEnumerator waitToDisplay (float time)
	{
		yield return new WaitForSeconds (time);
		displaying = true;
	}
	
	private void displayDialog ()
	{
		dialog.openDialog ();
	}
	
	private void closeDialog ()
	{
		dialog.closeDialog ();
	}
}
