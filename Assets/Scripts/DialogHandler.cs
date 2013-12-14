using UnityEngine;
using System.Collections;

public class DialogHandler : MonoBehaviour
{
	private bool displaying;
	private bool found;
	private DialogTrigger[] dialog;
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
		dialog = new DialogTrigger[4];
	}
	
	void Update ()
	{	
		if (cameraFollower.cameraSettled) {
			RaycastHit2D detected = Physics2D.Raycast (transform.position, Vector2.up, 250f, layerMask);
			if (detected.collider != null) {
//				Debug.Log (detected.collider.name);
				if (detected.collider.gameObject.GetComponent<DialogTrigger> () != null) { // Dialog Box Found
					found = true;
					dialog [dialogIndex] = detected.collider.GetComponent<DialogTrigger> ();
					sceneManager.isPlaying = false;
				}
			}
			if (!displaying && found) {
				if (!dialog [dialogIndex].isTriggered) {
					StartCoroutine (waitToDisplay (dialog [dialogIndex].waitTime));
				} else {
					displaying = true;
					dialogIndex++;
				}
			
			} else {
				if (displaying && found) {
					if (!dialog [dialogIndex].isDialogFinished ()) {
						displayDialog ();
					} else {
						closeDialog ();
						sceneManager.isPlaying = true;
						displaying = false;
						dialogIndex--;
					}
					if (InputManager.enter) {
						dialog [dialogIndex].isDialogFinished (true);
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
		dialog [dialogIndex].openDialog ();
	}
	
	private void closeDialog ()
	{
		dialog [dialogIndex].closeDialog ();
	}
}
