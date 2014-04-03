using UnityEngine;
using System.Collections;

public class DialogScanner : MonoBehaviour
{
	private DialogTrigger dialog;
	
	public LayerMask layerMask ;
	
	private CameraFollow cameraFollower;
	
	void Start ()
	{
		cameraFollower = FindObjectOfType<CameraFollow> ();
		if (GameObject.FindObjectOfType<DialogTrigger> () == null) {
			Destroy (gameObject);
		}
	}
	
	void FixedUpdate ()
	{	
		if (cameraFollower.cameraSettled) {
			RaycastHit2D detected = Physics2D.Raycast (transform.position, Vector2.up, 250f, layerMask);
			if (detected.collider != null) {
				dialog = detected.collider.GetComponent<DialogTrigger> ();
				dialog.openDialog ();
			}
		}
	}
	
	public void forceDialog (DialogTrigger dialogReceived)
	{
		dialogReceived.openDialog ();
	}
}
