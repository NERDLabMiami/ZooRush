using UnityEngine;
using System.Collections;

public class PlaySound : ObjectModel
{
	public AudioClip clip;
	private bool touched;


	void Start ()
	{
		touched = false;
		audioController = GameObject.FindObjectOfType<AudioController> ();
		GetComponentInChildren<CollisionDetect> ().objectModel = this;

	}
	
	void Update ()
	{
	
	}

	protected override void resetOtherValues ()
	{
		touched = false;
	}
	public override void collisionDetected ()
	{

	}
	public override void interactWithCharacter (Collider2D character)
	{
		if (!touched) {
			audioController.objectInteraction (clip);
			touched = true;
		}
	}

}
