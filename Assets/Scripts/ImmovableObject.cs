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
		Vector2 speed;
		if (character.transform.localPosition.y < -3f) {
			speed = new Vector2 (650f, 1000f);
		} else {
			speed = new Vector2 (650f, -1000f);
		}
		Debug.Log ("Current Speed is: " + character.rigidbody2D.velocity.x);
		character.GetComponent<PlayerControls> ().pushAway (speed, true);
		Debug.Log ("Current Speed is: " + character.rigidbody2D.velocity.x);
		collisionDetect.signalSent = false;
	}
}
