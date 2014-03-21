using UnityEngine;
using System.Collections;
/**
 * Model for Vehicle Objects such as cars and lawnmowers.
 * @author: Ebby Wahman
 */ 
public class Vehicle : ObjectModel
{
	public bool isCar; //if the vehicle is a car,van, or truck
	public AudioClip clip; //audio clip that plays when the Character collides with this vehicle.
 
	new void Start ()
	{
		base.Start ();

		setSpeed ();

		if (isCar) {
			GetComponent<Animator> ().SetInteger ("Car", Random.Range (0, 3));
			GetComponent<Animator> ().SetTrigger ("Change");
		}
	}

	void OnEnable ()
	{
		GameStateMachine.Paused += OnPause;
		GameStateMachine.PauseToPlay += OnPauseToPlay;
	}
	
	
	void OnDisable ()
	{
		GameStateMachine.Paused -= OnPause;
		GameStateMachine.PauseToPlay -= OnPauseToPlay;
	}

	void OnPause ()
	{
		stopMoving ();
	}

	void OnPauseToPlay ()
	{
		setSpeed ();
	}

	void Update ()
	{	

		if (transform.localScale.x > 0) { //facing right
			if (transform.position.x > Camera.main.transform.position.x && !inView ()) {
				GameObject.FindObjectOfType<SceneRepeater> ().DestroyObstacle (gameObject);
			}
		} else { //facing left
			if (transform.position.x < Camera.main.transform.position.x && !inView ()) {
				GameObject.FindObjectOfType<SceneRepeater> ().DestroyObstacle (gameObject);
			}
		}
	}

	private bool inView ()
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, renderer.bounds);
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

	private void setSpeed ()
	{
		if (transform.localScale.x > 0) { //if facing right
			rigidbody2D.velocity = new Vector2 (8f, 0);
		} else {//facing left
			rigidbody2D.velocity = new Vector2 (-9.5f, 0);
		}
	}
	
	private void stopMoving ()
	{
		rigidbody2D.velocity = Vector2.zero;
	}
}
