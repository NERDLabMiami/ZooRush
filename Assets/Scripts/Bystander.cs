using UnityEngine;
using System.Collections;

public class Bystander : ObjectModel
{
	private bool touched;
	public AudioClip clip;
	public Sprite reaction;
	private Sprite original;

	void Start ()
	{
		touched = false;
		audioController = GameObject.FindObjectOfType<AudioController> ();
		GetComponentInChildren<CollisionDetect> ().objectModel = this;
		original = GetComponent<SpriteRenderer> ().sprite;
	}
	
	void Update ()
	{
	
	}

	protected override void resetOtherValues ()
	{
		GetComponent<SpriteRenderer> ().sprite = original;
		touched = false;
		GetComponentInChildren<CollisionDetect> ().signalSent = false;
	}

	public override void collisionDetected ()
	{
		if (reaction != null) {
			GetComponent<SpriteRenderer> ().sprite = reaction;
		}
	}
	public override void interactWithCharacter (GameObject character)
	{
		if (!touched) {
			audioController.objectInteraction (clip);
			character.GetComponent<PlayerControls> ().pushAway (50, true);
			touched = true;
		}
	}

}
