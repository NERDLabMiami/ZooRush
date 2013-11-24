using UnityEngine;
using System.Collections;

/** Handles all character movements and triggers.
 * @author Ebtissam Wahman
 */ 
public class PlayerControls : MonoBehaviour
{
	private Vector2 currentSpeed;
	private Vector2 speed;
	public Vector2 maxSpeed;

	private float xMovement;
	private float yMovement;

	private float movementForce = 30f;

	private Animator animate;
	private SceneManager sceneManager;

	private bool pause;
	private bool prevPause;

	void Start ()
	{
		animate = GetComponent<Animator> ();
		sceneManager = FindObjectOfType<SceneManager> ();
		pause = !sceneManager.isPlaying;
		prevPause = pause;
		currentSpeed = rigidbody2D.velocity;
		speed = new Vector2 (7f, 0f);
		maxSpeed.x = 5f;
		maxSpeed.y = 3f;
//		rigidbody2D.velocity = speed;
	}

	void FixedUpdate ()
	{
		currentSpeed = rigidbody2D.velocity;
		yMovement = Input.GetAxis ("Vertical");
		animate.SetTrigger ("Run");
		if (Mathf.Abs (currentSpeed.y) < maxSpeed.y) {
			rigidbody2D.AddForce (Vector2.up * yMovement * movementForce);
		} else {
			rigidbody2D.AddForce (Vector2.up * yMovement);
		}
	}

	void Update ()
	{
		if (!sceneManager.isPlaying) {
			if (!prevPause) { // if the input is being leaned on
				prevPause = true;
				pause = true;
				rigidbody2D.velocity = new Vector2 (0f, 0f);
			} else { // else keep track of the input but do not echo
				prevPause = true;
				pause = false;
				StartCoroutine (waitToResume (0.1f));
			}
		} else {// otherwise keep track that the input is not active
			prevPause = false;
			pause = false;
		}
	}

	public void flash ()
	{
		animate.SetTrigger ("Flash");
	}
	
	public void setSpeed ()
	{
		rigidbody2D.velocity = speed;
	}
	
	public void setSpeed (Vector2 thespeed)
	{
		rigidbody2D.velocity = thespeed;
	}

	public void resetSpeed ()
	{
		rigidbody2D.velocity = new Vector2 (0f, 0f);
		StartCoroutine (waitToResume (0.3f));
	}

	private IEnumerator waitToResume (float time)
	{
		animate.SetTrigger ("Flash");
		yield return new WaitForSeconds (time);
		rigidbody2D.velocity = speed;
	}
}