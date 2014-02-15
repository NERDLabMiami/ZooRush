using UnityEngine;
using System.Collections;

public class ImmovableObject : ObjectModel
{
	private CollisionDetect collisionDetect;
	void Start ()
	{
		collisionDetect = GetComponentInChildren<CollisionDetect> ();
		collisionDetect.objectModel = this;
	}
	
	void Update ()
	{
	
	}

	protected override void resetOtherValues ()
	{
		
	}

	public override void collisionDetected ()
	{
	}
	
	public override void interactWithCharacter (Collider2D character)
	{
		Debug.Log ("I Feel ya!");
		if (character.transform.localPosition.y < -3f) {
			character.rigidbody2D.AddForce (new Vector2 (-350f, 50f));
		} else {
			character.rigidbody2D.AddForce (new Vector2 (-350f, -50f));
		}
		character.GetComponent<PlayerControls> ().resetSpeed ();
		collisionDetect.signalSent = false;
	}
}
