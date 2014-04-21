using UnityEngine;
using System.Collections;

public class Ball : ObjectModel
{
	public AudioClip clip;
	private GameObject player;
	public Transform[] shadows;
	private Vector3[] distanceDiff;
	public SpriteRenderer sprite;

	private bool shadowDisplayed;

	new void Start ()
	{
		base.Start ();
		shadowDisplayed = true;
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
		if (!rigidbody2D.velocity.Equals (Vector2.zero) && shadowDisplayed) {
			makeShadowDisappear ();
		}
		if (player != null) {
			if (!inView (sprite)) {
				destroyObstacle ();
			}
		}
	}

	private void makeShadowDisappear ()
	{
		foreach (Transform shadow in shadows) {
			shadow.renderer.enabled = false;
		}
		shadowDisplayed = false;
	}

	private bool inView (SpriteRenderer obj)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
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
		makeShadowDisappear ();
	}
//
//	private void stopMoving ()
//	{
//		rigidbody2D.velocity = new Vector2 (0, 0);
//	}
}
