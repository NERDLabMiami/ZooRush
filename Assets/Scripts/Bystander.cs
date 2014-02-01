using UnityEngine;
using System.Collections;

public class Bystander : ObjectModel
{
	private bool touched;
	public AudioClip clip;
	public Sprite reaction;

	void Start ()
	{
		touched = false;
		GetComponentInChildren<CollisionDetect> ().objectModel = this;
	}
	
	void Update ()
	{
	
	}

	protected override void resetOtherValues ()
	{
		
	}

	public override void collisionDetected ()
	{
		if (reaction != null) {
			GetComponent<SpriteRenderer> ().sprite = reaction;
		}
	}
	public override void interactWithCharacter (Collider2D character)
	{
		if (!touched) {
			GameObject.FindObjectOfType<AudioController> ().objectInteraction (clip);
			if (character.transform.position.y < -2.5f) {
				character.rigidbody2D.AddForce (new Vector2 (-350f, 50f));
			} else {
				character.rigidbody2D.AddForce (new Vector2 (-350f, -50f));
			}
			character.GetComponent<PlayerControls> ().resetSpeed ();
			touched = true;
		}
	}

}
