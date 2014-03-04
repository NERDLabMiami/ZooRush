using UnityEngine;
using System.Collections;

public class StartButton : Button
{
	protected override void action ()
	{
		GetComponent<Animator> ().SetTrigger ("Open");
	}

	public void closeStartScreen ()
	{
		GameStateMachine.requestIntro ();
		Destroy (transform.parent.gameObject);
	}
}
