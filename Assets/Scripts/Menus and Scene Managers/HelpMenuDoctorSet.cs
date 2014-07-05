using UnityEngine;
using System.Collections;

public class HelpMenuDoctorSet : HelpMenuSet
{
	public Building doctorOffice;

	public override void activate ()
	{
		activated = true;
		transform.parent = Camera.main.transform;
		GameState.requestPause ();
		transform.localPosition = Vector3.zero;
		transform.parent = null;
		StartCoroutine ("resumeMovement");

	}
	
	public override void dismiss ()
	{
		activated = false;
		transform.position = originalPosition;
	}
	
	public override void reset ()
	{
		doctorOffice.resetState ();
	}

	private IEnumerator resumeMovement ()
	{
		yield return new WaitForSeconds (1);
		GameState.requestPlay ();
	}
}
