using UnityEngine;
using System.Collections;

public class ImmovableObject : ObjectModel
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
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
		if (character.transform.position.y < -2.5f) {
			character.rigidbody2D.AddForce (new Vector2 (-350f, 50f));
		} else {
			character.rigidbody2D.AddForce (new Vector2 (-350f, -50f));
		}
	}
}
