using UnityEngine;
using System.Collections;

public class ImmovableObject : ObjectModel
{

	protected override void resetOtherValues ()
	{
		
	}

	public override void collisionDetected ()
	{
	}
	
	public override void interactWithCharacter (GameObject character)
	{
		Vector2 speed;
		if (character.transform.localPosition.y < -3f) {
			speed = new Vector2 (650f, 1000f);
		} else {
			speed = new Vector2 (650f, -1000f);
		}
		character.GetComponentInChildren<PlayerControls> ().pushAway (speed, true);
		collisionDetect.signalSent = false;
	}
}
