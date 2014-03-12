using UnityEngine;
using System.Collections;

public class Ball : ObjectModel
{
	public AudioClip clip;
	private GameObject player;
	public Transform[] shadows;
	private Vector3[] distanceDiff;

	void Start ()
	{
		GetComponentInChildren<CollisionDetect> ().objectModel = this;
		audioController = GameObject.FindObjectOfType<AudioController> ();
		distanceDiff = new Vector3[shadows.Length];
		for (int i = 0; i < distanceDiff.Length; i++) {
			distanceDiff [i].x = shadows [i].position.x - transform.position.x;
			distanceDiff [i].y = shadows [i].position.y - transform.position.y;
			distanceDiff [i].z = shadows [i].position.z - transform.position.z;
		}

	}

	void Update ()
	{
		if (player != null) {
			for (int i = 0; i < distanceDiff.Length; i++) {
				shadows [i].position = transform.position + distanceDiff [i];
			}
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

	public override void interactWithCharacter (GameObject character)
	{
		player = character;
		audioController.objectInteraction (clip);
		rigidbody2D.AddTorque (10f);
	}
//
//	private void stopMoving ()
//	{
//		rigidbody2D.velocity = new Vector2 (0, 0);
//	}
}
