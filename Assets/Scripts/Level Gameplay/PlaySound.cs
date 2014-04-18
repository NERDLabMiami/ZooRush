using UnityEngine;
using System.Collections;

public class PlaySound : ObjectModel
{
	public AudioClip clip;
	private bool touched;


	new void Start ()
	{
		base.Start ();

		touched = false;
		audioController = GameObject.FindObjectOfType<AudioController> ();
	}

	protected override void resetOtherValues ()
	{
		touched = false;
	}
	public override void collisionDetected ()
	{

	}
	public override void interactWithCharacter (GameObject character)
	{
		if (!touched) {
			audioController.objectInteraction (clip);
			touched = true;
		}
	}

}
