using UnityEngine;
using System.Collections;

public class Infection : ObjectModel
{
	public AudioClip clip;
	private PainIndicator painIndicator;
	public int painPoints;
	public string infectionType;

	private bool stopWatchActive;
	private bool interacted;



	void Start ()
	{
		interacted = false;
		painIndicator = GameObject.FindObjectOfType<PainIndicator> ();
		GetComponentInChildren<CollisionDetect> ().objectModel = this;
		audioController = GameObject.FindObjectOfType<AudioController> ();

	}
	
	void Update ()
	{
	
	}

	protected override void resetOtherValues ()
	{
		interacted = false;
		GetComponent<Animator> ().SetTrigger ("Reset");
		GetComponentInChildren<CollisionDetect> ().resetTouch ();
	}

	public void addToScore ()
	{
		GameObject.FindObjectOfType<ScoreKeeper> ().addToCount (infectionType);
	}
	public override void collisionDetected ()
	{
		GetComponent<Animator> ().SetTrigger ("Flash");

	}
	public override void interactWithCharacter (GameObject character)
	{
		if (!interacted) {
			GameObject.FindObjectOfType<AudioController> ().objectInteraction (clip);
			addToScore ();
			painIndicator.addPoints (painPoints);
			if (character.transform.position.y < -2.5f) {
				character.rigidbody2D.AddForce (new Vector2 (-350f, 50f));
			} else {
				character.rigidbody2D.AddForce (new Vector2 (-350f, -50f));
			}
			character.GetComponent<PlayerControls> ().resetSpeed ();
			GameObject.FindObjectOfType<LevelGUIController> ().displayStopwatch (infectionType);
		}
	}

}
