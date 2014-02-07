using UnityEngine;
using System.Collections;

public class PowerUp : ObjectModel
{
	public AudioClip clip; //Audio played when interacting with this object
	private PainIndicator painIndicator;
	public int painPoints; // pills = 75 , water bottles = 25
	public bool isPill;

	private bool interacted;
	private int pillCount;

	void Start ()
	{
		interacted = false;
		painIndicator = GameObject.FindObjectOfType<PainIndicator> ();
		if (GetComponentInChildren<CollisionDetect> () != null) {
			GetComponentInChildren<CollisionDetect> ().objectModel = this;
		}
		audioController = GameObject.FindObjectOfType<AudioController> ();
	}

	void OnMouseUp ()
	{
		if (isPill) {
			pillCount = PlayerPrefs.GetInt ("PILLS");
			if (pillCount > 0) {
				pillCount--;
				PlayerPrefs.SetInt ("PILLS", pillCount);
				GameObject.FindObjectOfType<SceneManager> ().updatePillCount ();
				painIndicator.subtractPoints (painPoints);
			}
		}
	}

	protected override void resetOtherValues ()
	{
		interacted = false;
		GetComponent<Animator> ().SetTrigger ("Reset");
		GetComponentInChildren<CollisionDetect> ().resetTouch ();
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
		if (!interacted) {
			addToScore ();
			painIndicator.subtractPoints (painPoints);
			audioController.objectInteraction (clip);
			interacted = true;
		}
	}
}
