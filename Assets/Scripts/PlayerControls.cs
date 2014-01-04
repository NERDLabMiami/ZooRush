using UnityEngine;
using System.Collections;

/** Handles all character movements and triggers.
 * @author Ebtissam Wahman
 */ 
public class PlayerControls : MonoBehaviour
{
	private Vector2 speed;
	private float minSpeed;
	public Vector2 maxSpeed;

	private float yMovement;
	
	private Animator animate;
	private SceneManager sceneManager;
	private InputManager inputManager;
	private NetLauncher netLauncher;

	private bool play;
	private bool prevPlay;

	public string characterName;
	private bool changingSpeed;

	public Sprite[] faceIcons;

	void Start ()
	{
		animate = GetComponent<Animator> ();
		sceneManager = FindObjectOfType<SceneManager> ();
		inputManager = FindObjectOfType<InputManager> ();
		netLauncher = FindObjectOfType<NetLauncher> ();
		play = sceneManager.isPlaying;
		prevPlay = play;
		speed = new Vector2 (7f, 0f);
		maxSpeed.x = 5f;
		maxSpeed.y = 4f;
		minSpeed = 1f;
	}

	void FixedUpdate ()
	{
		if (Input.GetMouseButton (0)) {
			yMovement = inputManager.yDelta;
		} else {
			yMovement = Input.GetAxis ("Vertical");
		}
		if (sceneManager.isPlaying) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, yMovement * maxSpeed.y);
		} else {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0f);
		}
	}

	void Update ()
	{
		//Tracking for the paused or played state
		prevPlay = play;
		if (sceneManager.isPlaying && !sceneManager.levelStartWait) {
			play = true;
			if (rigidbody2D.velocity.x < speed.x && !changingSpeed && !netLauncher.launchEnabled) {
				setSpeed ();
			}
		} else {// otherwise keep track that the input is not active
			play = false;
		}
		if (!prevPlay && play) { //our previous state is the paused state, we are now going into the play state
			StartCoroutine (waitToResume (0.1f));
			animate.StopPlayback ();
		} else { // our previous state is the play state
			if (!play) {//we need to move into the paused state
				rigidbody2D.velocity = new Vector2 (0f, 0f);
				animate.StartPlayback ();
				Debug.Log ("STOPPED");
			}
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
		changingSpeed = true;
		rigidbody2D.velocity = new Vector2 (0f, 0f);
		StartCoroutine (waitToResume (0.3f));
	}
	public void decrementSpeed (float value)
	{
//		Debug.Log ("DECREMENTING SPEED");
		if (rigidbody2D.velocity.x - value >= minSpeed) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x - value, rigidbody2D.velocity.y);
		} else {
			rigidbody2D.velocity = new Vector2 (minSpeed, rigidbody2D.velocity.y);
		}
	}
	private IEnumerator waitToResume (float time)
	{
		animate.SetTrigger ("Flash");
		yield return new WaitForSeconds (time);
		rigidbody2D.velocity = speed;
		changingSpeed = false;
	}
}