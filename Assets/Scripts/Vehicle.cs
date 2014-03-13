using UnityEngine;
using System.Collections;

public class Vehicle : ObjectModel
{
	public bool isCar;
	public AudioClip clip;
 
	private bool stopped;
	void Start ()
	{
		GetComponentInChildren<CollisionDetect> ().objectModel = this;
		audioController = GameObject.FindObjectOfType<AudioController> ();
		if (transform.localScale.x > 0) { //if poisitive
			rigidbody2D.velocity = new Vector2 (8f, 0);
		} else {
			rigidbody2D.velocity = new Vector2 (-9.5f, 0);
		}
		stopped = false;

		if (isCar) {
			GetComponent<Animator> ().SetInteger ("Car", Random.Range (0, 3));
			GetComponent<Animator> ().SetTrigger ("Change");
		}

	}
	
	void Update ()
	{
		if (!stopped) {
			if (GameStateMachine.currentState == (int)GameStateMachine.GameState.PauseToPlay) {
				if (transform.localScale.x > 0) { //if positive
					rigidbody2D.velocity = new Vector2 (8f, 0);
				} else {
					rigidbody2D.velocity = new Vector2 (-9.5f, 0);
				}
			} else if (GameStateMachine.currentState == (int)GameStateMachine.GameState.Paused) {
				rigidbody2D.velocity = new Vector2 (0f, 0);
			}
		}
	}

	protected override void resetOtherValues ()
	{
		
	}

	public override void collisionDetected ()
	{
		stopMoving ();
		if (isCar) {
			GameObject.FindObjectOfType<PainIndicator> ().setPoints (90);
		} else {
			GameObject.FindObjectOfType<PainIndicator> ().addPoints (40);
		}
	}
	
	public override void interactWithCharacter (GameObject character)
	{
		if (clip != null) {
			audioController.objectInteraction (clip);
		}
		float speed = 250f;
		if (isCar) {
			speed = 750f;
		}
		character.GetComponent<PlayerControls> ().pushAway (speed, transform.localScale.x < 0);

	}
	
	private void stopMoving ()
	{
		rigidbody2D.velocity = new Vector2 (0, 0);
		stopped = true;
	}
}
