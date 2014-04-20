using UnityEngine;
using System.Collections;

public class Bystander : ObjectModel
{
	public bool male;
	private bool touched;
	private AudioClip clip;
	private static AudioClip[] maleclips;
	private static AudioClip[] femaleclips;
	public Sprite reaction;
	private Sprite original;

	void Awake ()
	{
		maleclips = Resources.LoadAll<AudioClip> ("Sounds/Ows/Male");
		femaleclips = Resources.LoadAll<AudioClip> ("Sounds/Ows/Female");
	}

	new void Start ()
	{
		base.Start ();

		touched = false;
		audioController = GameObject.FindObjectOfType<AudioController> ();
		GetComponentInChildren<CollisionDetect> ().objectModel = this;
		original = GetComponent<SpriteRenderer> ().sprite;
		if (male) {
			clip = maleclips [Random.Range (0, maleclips.Length)];
		} else {
			clip = femaleclips [Random.Range (0, maleclips.Length)];
		}
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
			transform.localPosition = new Vector3 (transform.localPosition.x + 1f, transform.localPosition.y, transform.localPosition.z);
			character.GetComponent<PlayerControls> ().pushAway (50, true);
			touched = true;
		}
	}

}
