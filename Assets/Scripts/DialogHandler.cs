using UnityEngine;
using System.Collections;

public class DialogHandler : MonoBehaviour
{
	private DialogTrigger dialog;
	
	public LayerMask layerMask ;
	
	private CameraFollow cameraFollower;
	
	void Start ()
	{
		cameraFollower = FindObjectOfType<CameraFollow> ();
	}
	
	void FixedUpdate ()
	{	
		if (cameraFollower.cameraSettled) {
			RaycastHit2D detected = Physics2D.Raycast (transform.position, Vector2.up, 250f, layerMask);
			if (detected.collider != null) {
				detected.collider.GetComponent<DialogTrigger> ().openDialog ();
			}
		}
	}

	public void forceDialog (DialogTrigger dialogReceived)
	{
		dialogReceived.openDialog ();
	}
}
