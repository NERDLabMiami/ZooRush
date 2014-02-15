using UnityEngine;
using System.Collections;

/** Data and functions specific to Animal objects.
 * 
 * @author Ebtissam Wahman
 */ 
public class Animal : MonoBehaviour
{

	public bool caught; //Indicator for whehter the Animal has been caught by the player
	public Vector2 speed; //Current speed of the animal object
	public Sprite animalIcon; //Icon used in the distance meter

	private Animator animator; //Animator for the animal's running sprites

	void Start ()
	{
		animator = GetComponent<Animator> (); //assigns the pointer to the animator component
		caught = false; //default value for whether the animal has been caught
		transform.parent.rigidbody2D.velocity = new Vector2 (0f, 0f);
		GameObject.Find ("Animal Icon").GetComponent<SpriteRenderer> ().sprite = animalIcon; //Changes the icon in the distance meter
	}
	
	void Update ()
	{
		animator.SetFloat ("Speed", transform.parent.rigidbody2D.velocity.x); //Tells the animator state machine what the current speed value is
		switch (GameStateMachine.currentState) {
		case (int)GameStateMachine.GameState.Intro:
		case (int)GameStateMachine.GameState.Play:
			setSpeed ();
			break;
		case (int)GameStateMachine.GameState.PauseToPlay:
			StartCoroutine (waitToResume (0.1f));
			break;
		default:
			transform.parent.rigidbody2D.velocity = new Vector2 (0f, 0f);
			break;
		}
	}

	/**
	 * Sets the speed to the animal's standard running speed.
	 */ 
	public void setSpeed ()
	{
		transform.parent.rigidbody2D.velocity = speed; //assigns the rigidbody component the desired velocity
	}

	/** 
	 * Randomly changes the y-axis value of the animal object.
	 */
	public void changeY ()
	{
		int moveChance = Random.Range (1, 101);
		if (moveChance % 100 == 0) {//1% chance
			float yForce = Random.Range (-150f, 150f);
			transform.parent.rigidbody2D.AddForce (new Vector2 (0, yForce));
		}
	}
	
	/** Resumes the default speed of the animal.
	* @param time the wait time before resetting the speed of the animal
	*/
	
	private IEnumerator waitToResume (float time)
	{
		yield return new WaitForSeconds (time);
		transform.parent.rigidbody2D.velocity = speed;
		GameStateMachine.requestPlay ();
	}
}
