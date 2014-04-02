using UnityEngine;
using System.Collections;

public class ImmovableObjectWithReaction : ObjectModel
{
	public string Reaction;
	private bool touched;

	new void Start ()
	{
		base.Start ();
		touched = false;
	}

	protected override void resetOtherValues ()
	{
		touched = false;
		if (gameObject.GetComponent<Animator> () != null) {
			gameObject.GetComponent<Animator> ().SetTrigger ("Reset");

		}
	}

	public override void collisionDetected ()
	{
	}
	
	public override void interactWithCharacter (GameObject character)
	{
		if (!touched) {
			gameObject.GetComponent<Animator> ().SetTrigger (Reaction);
		}
		touched = true;
	}
}
