using UnityEngine;
using System.Collections;

public class PowerUp : ObjectModel
{
	public AudioClip clip; //Audio played when interacting with this object
	private PainIndicator painIndicator;
	public int painPoints; // pills = 75 , water bottles = 25
	public bool isPill;

	void Start ()
	{
		painIndicator = GameObject.FindObjectOfType<PainIndicator> ();
	}
	
	void Update ()
	{
	
	}

	protected override void resetOtherValues ()
	{
		
	}

	public void addToScore ()
	{
		if (isPill) {
			GameObject.FindObjectOfType<ScoreKeeper> ().addToCount ("Pill");
		} else {
			GameObject.FindObjectOfType<ScoreKeeper> ().addToCount ("Water Bottle");
		}
	}

	public override void collisionDetected ()
	{
		if (isPill) {

		} else {
			GetComponent<Animator> ().SetTrigger ("Flash");
		}
	}

	public override void interactWithCharacter (Collider2D character)
	{
		addToScore ();
		painIndicator.subtractPoints (painPoints);
		GameObject.FindObjectOfType<AudioController> ().objectInteraction (clip);
	}
}
