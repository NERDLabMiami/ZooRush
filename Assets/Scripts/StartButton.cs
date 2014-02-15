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
//		GameObject.FindObjectOfType<SceneManager> ().startLevel ();
		GameStateMachine.requestIntro ();
		Destroy (transform.parent.gameObject);
	}
}
