using UnityEngine;
using System.Collections;

/** Handles all character movements and triggers.
 * @author Ebtissam Wahman
 */ 
public class PlayerControls : MonoBehaviour
{
	private Vector2 currentSpeed;
	public Vector2 speed;
	public Vector2 maxSpeed;

	private float xMovement;
	private float yMovement;

	private float movementForce = 30f;

	private Animator animate;

	void Start ()
	{
		animate = GetComponent<Animator> ();
		currentSpeed = rigidbody2D.velocity;
		speed = new Vector2 (7f, 0f);
		maxSpeed.x = 5f;
		maxSpeed.y = 3f;
		rigidbody2D.velocity = speed;
	}

	void FixedUpdate ()
	{
		currentSpeed = rigidbody2D.velocity;
		Debug.Log (currentSpeed.x);
		yMovement = Input.GetAxis ("Vertical");
		animate.SetTrigger ("Run");
		if (Mathf.Abs (currentSpeed.y) < maxSpeed.y) {
			rigidbody2D.AddForce (Vector2.up * yMovement * movementForce);
		} else {
			rigidbody2D.AddForce (Vector2.up * yMovement);
		}
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