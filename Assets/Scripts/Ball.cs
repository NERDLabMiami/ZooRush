using UnityEngine;
using System.Collections;

public class Ball : ObjectModel
{
	public AudioClip clip;
	private GameObject player;

	void Start ()
	{
		GetComponentInChildren<CollisionDetect> ().objectModel = this;
		audioController = GameObject.FindObjectOfType<AudioController> ();

	}

	void Update ()
	{
		if (player != null) {
			if (transform.position.x > player.transform.position.x + 40f) {
				destroyObstacle ();
			} else {
				if (40f < Mathf.Abs (transform.position.x - Camera.main.transform.position.x)) {
					destroyObstacle ();
				}
			}
		}
	}

	protected override void resetOtherValues ()
	{

	}

	public override void collisionDetected ()
	{
	}

	public override void interactWithCharacter (Collider2D character)
	{
		player = character.gameObject;
		audioController.objectInteraction (clip);
	}

	private void stopMoving ()
	{
		rigidbody2D.velocity = new Vector2 (0, 0);
	}
}
