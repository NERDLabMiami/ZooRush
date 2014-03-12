using UnityEngine;
using System.Collections;

/** Handles all character movements and triggers.
 * @author Ebtissam Wahman
 */ 
public class PlayerControls : MonoBehaviour
{
	private Vector2 speed;
	private Vector2 currentSpeed;
	private float maxSpeed;


	private bool changeSpeed;
	private float yMovement;
	
	private Animator animate;
	private SceneManager sceneManager;
	private InputManager inputManager;
	private NetLauncher netLauncher;
	private Animal animal;

	public string characterName;

	public Sprite[] faceIcons;

	private float sensitivity;

	void Start ()
	{
		animate = GetComponent<Animator> ();
		sceneManager = FindObjectOfType<SceneManager> ();
		inputManager = FindObjectOfType<InputManager> ();
		netLauncher = FindObjectOfType<NetLauncher> ();
		animal = FindObjectOfType<Animal> ();
		speed = new Vector2 (7f, 0f);
		maxSpeed = 4f;
		changeSpeed = false;
		sensitivity = PlayerPrefs.GetFloat ("Sensitivity", 1);
	}

	void FixedUpdate ()
	{
		if (GameStateMachine.currentState == (int)GameStateMachine.GameState.Play) {
			if (Input.GetMouseButton (0)) {
				yMovement = inputManager.yDelta;
			} else {
				yMovement = Input.GetAxis ("Vertical");
			}
			if (sceneManager.isPlaying) {
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, yMovement * maxSpeed * sensitivity);
			} else {
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0f);
			}
		}
	}

	void Update ()
	{
		switch (GameStateMachine.currentState) {
		case (int)GameStateMachine.GameState.Play:
			if (netLauncher.launchEnabled) {
				setSpeed (animal.speed);
			} else {
				setSpeed ();
			}
			currentSpeed = rigidbody2D.velocity;
			break;
		case (int)GameStateMachine.GameState.PauseToPlay:
			StartCoroutine (waitToResume (0.1f));
			animate.StopPlayback ();
			break;
		default:
			rigidbody2D.velocity = new Vector2 (0f, 0f);
			animate.StartPlayback ();
			break;
		}
	}

	public void flash ()
	{
		animate.SetTrigger ("Flash");
	}
	
	public void setSpeed ()
	{
		if (!changeSpeed) {
			rigidbody2D.velocity = speed;
		}
	}

	public void pushAway (float speed, bool left)
	{
		changeSpeed = true;
		rigidbody2D.velocity = new Vector2 (0f, 0f);
		Debug.Log ("Push Away Called");
		if (left) {
			rigidbody2D.AddForce (new Vector2 (-speed, 0));
		} else {
			rigidbody2D.AddForce (new Vector2 (speed * 1.5f, 0));
		}
		StartCoroutine (waitToResume (0.5f));
	}

	public void pushAway (Vector2 speed, bool left)
	{
		changeSpeed = true;
		rigidbody2D.velocity = new Vector2 (0f, 0f);
		Debug.Log ("Push Away Called");
		if (left) {
			rigidbody2D.AddForce (new Vector2 (-speed.x, speed.y));
		} else {
			rigidbody2D.AddForce (new Vector2 (speed.x * 1.5f, speed.y));
		}
		StartCoroutine (waitToResume (0.5f));
	}


	public void setSpeed (Vector2 thespeed)
	{
		rigidbody2D.velocity = thespeed;
	}

	public void resetSpeed (float time = 0.3f)
	{
		rigidbody2D.velocity = new Vector2 (0f, 0f);
		StartCoroutine (waitToResume (time));
	}

	private IEnumerator waitToResume (float time)
	{
		animate.SetTrigger ("Flash");
		yield return new WaitForSeconds (time);
		rigidbody2D.velocity = currentSpeed;
		changeSpeed = false;
	}
}