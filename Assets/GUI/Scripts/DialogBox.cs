using UnityEngine;
using System.Collections;

public class DialogBox : OtherButtonClass
{

	public string[] dialog;
	public TextAnimator textAnimator;
	private int index;
	public Renderer[] dialogBoxSprites;
	private bool activated;

	void OnEnable ()
	{
		GameState.dialogCalled += dialogStateCalled;
		GameState.dialogDismissed += leavingDialogState;
	}

	void OnDisable ()
	{
		GameState.dialogCalled -= dialogStateCalled;
		GameState.dialogDismissed -= leavingDialogState;

	}

	private void dialogStateCalled ()
	{
		Debug.Log ("Dialog State Called");
		activated = true;
		index = 0;
		next ();
	}

	private void leavingDialogState ()
	{
		Debug.Log ("Dismissing Dialog");
		dismiss ();
	}

	void Start ()
	{
		index = 0;
		dismiss ();
//		next ();
	}

	public void next ()
	{
		if (index == 0) {
			display ();
		}
		textAnimator.startAnimation (dialog [index]);
		index++;
	}

	public void display ()
	{
		foreach (Renderer sprite in dialogBoxSprites) {
			sprite.material.color = Color.white;
		}
	}

	public void dismiss ()
	{
		foreach (Renderer sprite in dialogBoxSprites) {
			sprite.material.color = Color.clear;
		}
		activated = false;
	}

	public override void otherButtonAction (Button thisButton = null)
	{
		if (activated) {
			if (index < dialog.Length) {
				next ();
			} else {
				GameState.requestPlay ();
			}
		}
	}


}
