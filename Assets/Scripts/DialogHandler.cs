using UnityEngine;
using System.Collections;

public class DialogHandler : MonoBehaviour
{
	public bool displaying;
	public bool found;
	private DialogTrigger dialog;
	
	public LayerMask layerMask ;
	
	private SceneManager sceneManager;
	private CameraFollow cameraFollower;
	
	void Start ()
	{
		sceneManager = FindObjectOfType<SceneManager> ();
		cameraFollower = FindObjectOfType<CameraFollow> ();
	}
	
	void FixedUpdate ()
	{	
		if (cameraFollower.cameraSettled) {
			RaycastHit2D detected = Physics2D.Raycast (transform.position, Vector2.up, 250f, layerMask);
			if (detected.collider != null) {
				if (dialog == null || detected.collider.gameObject != dialog.gameObject) { // Dialog Box Found
					dialog = detected.collider.GetComponent<DialogTrigger> ();
					if (dialog.tutOnly) {
						if (sceneManager.tutEnabled) {
							GameStateMachine.requestPause ();
							dialog.openDialog ();
						} 

					} else {
						GameStateMachine.requestPause ();
						dialog.openDialog ();
					}
				}
			}
		}
	}

	public void forceDialog (DialogTrigger dialogReceived)
	{
		dialog = dialogReceived;
		if (dialog.tutOnly) {
			if (sceneManager.tutEnabled) {
				GameStateMachine.requestPause ();
				dialog.openDialog ();
			} 
			
		} else {
			GameStateMachine.requestPause ();
			dialog.openDialog ();
		}
	}
}
