using UnityEngine;
using System.Collections;

public class PowerUp : ObjectModel
{
	public AudioClip clip; //Audio played when interacting with this object
	private PainIndicator painIndicator;
	private ScoreKeeper scoreKeeper; //pointer to score keeper
	private CollisionDetect collisionDetect; //pointer to the collision detector

	private Animator animator;
	public int painPoints; // pills = 75 , water bottles = 25

	private bool interacted;
	private int pillCount;

	void Start ()
	{
		interacted = false;
		painIndicator = GameObject.FindObjectOfType<PainIndicator> ();
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();

		animator = GetComponent<Animator> ();
		collisionDetect = GetComponentInChildren<CollisionDetect> ();
		if (collisionDetect) {
			collisionDetect.objectModel = this;
		}
		audioController = GameObject.FindObjectOfType<AudioController> ();
	}


	protected override void resetOtherValues ()
	{
		interacted = false;
		animator.SetTrigger ("Reset");
		collisionDetect.resetTouch ();
	}

	public void addToScore ()
	{
		scoreKeeper.addToCount ("Water Bottle");
	}

	public override void collisionDetected ()
	{
		animator.SetTrigger ("Flash");
	}

	public override void interactWithCharacter (Collider2D character)
	{
		if (!interacted) {
			addToScore ();
			painIndicator.subtractPoints (painPoints);
			audioController.objectInteraction (clip);
			interacted = true;
		}
	}
}
