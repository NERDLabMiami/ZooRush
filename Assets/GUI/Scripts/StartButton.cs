using UnityEngine;
using System.Collections;

public class StartButton : OtherButtonClass
{

	public void closeStartScreen ()
	{
		Destroy (transform.parent.gameObject);
		GameState.requestIntro ();
	}

	public override void otherButtonAction (Button thisButton)
	{
		GetComponent<Animator> ().SetTrigger ("Open");
	}
}
