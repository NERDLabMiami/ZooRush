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

	private NetLauncher netLauncher;
	private Animal animal;

	public string characterName;

	public Sprite[] faceIcons;
	
	void Start ()
	{
		animate = GetComponent<Animator> ();
		netLauncher = FindObjectOfType<NetLauncher> ();
		animal = FindObjectOfType<Animal> ();
		speed = new Vector2 (7f, 0f);
		maxSpeed = 4f;
		changeSpeed = false;
	}

	void FixedUpdate ()
	{
		if (GameStateMachine.currentState == (int)GameStateMachine.GameState.Play) {
			if (Input.GetMouseButton (0) && Camera.main.pixelRect.Contains (Input.mousePosition)) {
				yMovement = (Input.GetAxis ("Mouse Y") > 0) ? 1 : ((Input.GetAxis ("Mouse Y") < 0) ? -1 : 0);
			} else {
				yMovement = Input.GetAxis ("Vertical");
			}
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, yMovement * maxSpeed * PlayerPrefs.GetFloat ("Sensitivity", 1));
		} else {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0f);
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
			if (rigidbody2D.velocity.x > 0) {
				currentSpeed = rigidbody2D.velocity;
				Debug.Log (currentSpeed);
			}
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
		if (!changeSpeed) {
			rigidbody2D.velocity = thespeed;
		}
	}

	public void resetSpeed (float time = 0.3f)
	{
		rigidbody2D.velocity = new Vector2 (0f, 0f);
		StartCoroutine (waitToResume (time));
	}

	private IEnumerator waitToResume (float time)
	{
		changeSpeed = true;
		animate.SetTrigger ("Flash");
		yield return new WaitForSeconds (time);
		rigidbody2D.velocity = currentSpeed;
		changeSpeed = false;
	}
}