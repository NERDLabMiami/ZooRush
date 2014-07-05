using UnityEngine;
using System.Collections;

/** Handles all character movements and triggers.
 * @author Ebtissam Wahman
 */ 
public class PlayerControls : MonoBehaviour
{
	private Vector2 speed;
	private const float xSpeed = 7f;
	private Vector2 currentSpeed;
	private float maxYSpeed;

	private bool changeSpeed;
	private float yMovement;
	
	public Animator animate;
	public Animal animal;

	void Awake ()
	{
		if (animal == null) {
			animal = GameObject.FindObjectOfType<Animal> ();
		}
		speed = new Vector2 (7f, 0f);
		maxYSpeed = 4f;
		changeSpeed = false;
	}

	void OnEnable ()
	{
		GameState.StateChanged += OnStateChanged;
	}
	
	void OnDisable ()
	{
		GameState.StateChanged -= OnStateChanged;
	}
	
	private void OnStateChanged ()
	{
		switch (GameState.currentState) {
		case GameState.States.Play:
			StartCoroutine (waitToResume (0.1f));
			break;
		case GameState.States.Launch:
			setSpeed (true);
			break;
		default:
			rigidbody2D.Sleep ();
			rigidbody2D.velocity = Vector2.zero;
			animate.speed = 0;
			break;
		}
	}

	void FixedUpdate ()
	{
		if (Input.GetMouseButton (0)) {
			yMovement = (Input.GetAxis ("Mouse Y") > 0) ? 1 : ((Input.GetAxis ("Mouse Y") < 0) ? -1 : 0);
		} else {
			yMovement = Input.GetAxis ("Vertical");
		}
		if (GameState.checkForState (GameState.States.Play) || GameState.checkForState (GameState.States.Launch)) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, yMovement * maxYSpeed * PlayerPrefs.GetFloat ("Sensitivity", 1));
		} else {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0f);
		}
	}

	void Update ()
	{
		if (GameState.checkForState (GameState.States.Play)) {
			setSpeed ();
		}
		if (GameState.checkForState (GameState.States.Launch)) {
			setSpeed (true);
		}
	}

	public void flash ()
	{
		animate.SetTrigger ("Flash");
	}

	public void pushAway (float addedSpeed, bool left)
	{
		changeSpeed = true;
		rigidbody2D.velocity = Vector2.zero;
		if (left) {
			rigidbody2D.AddForce (new Vector2 (-addedSpeed, 0));
		} else {
			rigidbody2D.AddForce (new Vector2 (addedSpeed * 1.5f, 0));
		}
		StartCoroutine (waitToResume (0.5f));
	}

	public void pushAway (Vector2 addedSpeed, bool left)
	{
		changeSpeed = true;
		rigidbody2D.velocity = Vector2.zero;
//		Debug.Log ("Push Away Called");
		if (left) {
			rigidbody2D.AddForce (new Vector2 (-addedSpeed.x, addedSpeed.y));
		} else {
			rigidbody2D.AddForce (new Vector2 (addedSpeed.x * 1.5f, addedSpeed.y));
		}
		StartCoroutine (waitToResume (0.5f));
	}

	public void speedUp ()
	{
		rigidbody2D.AddForce (new Vector2 (150f, 0));
		StartCoroutine (rampUP ());
	}


	public void setSpeed (bool followAnimal = false)
	{
		if (animal && !changeSpeed) {
			if (followAnimal) {
				rigidbody2D.velocity = animal.speed;
			} else {
				rigidbody2D.velocity = speed;
			}
			animate.speed = 1;
		}
	}

	public void resetSpeed (float time = 0.3f)
	{
		rigidbody2D.velocity = Vector2.zero;
		StartCoroutine (waitToResume (time));
	}

	private IEnumerator rampUP ()
	{
		changeSpeed = true;
        
		float velocity = 0;
		float nextXVelocity;
		while (!GameState.checkForState(GameState.States.Launch) && rigidbody2D.velocity.x < 9.9f) {
			nextXVelocity = Mathf.SmoothDamp (rigidbody2D.velocity.x, 10, ref velocity, 0.2f);
			rigidbody2D.velocity = new Vector2 (nextXVelocity, rigidbody2D.velocity.y);
			yield return new WaitForFixedUpdate ();
		}
		StartCoroutine (waitToResume (0.1f));
	}

	private IEnumerator waitToResume (float time)
	{
		changeSpeed = true;
		animate.SetTrigger ("Flash");
		yield return new WaitForSeconds (time);
		while (!GameState.checkForState(GameState.States.Play) && !GameState.checkForState(GameState.States.Launch)) {
			yield return new WaitForFixedUpdate ();
		}
		changeSpeed = false;
	}
}