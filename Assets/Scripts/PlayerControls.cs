using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{

	private Vector2 currentSpeed;
	public Vector2 speed;
	public Vector2 maxSpeed;

	private float xMovement;
	private float yMovement;
	

	private bool forward;

	private float movementForce = 30f;

	private Animator animate;
	// Use this for initialization
	void Start ()
	{
		animate = GetComponent<Animator> ();
		forward = true;
		currentSpeed = rigidbody2D.velocity;
		speed = new Vector2 (7f, 0f);
		maxSpeed.x = 5f;
		maxSpeed.y = 3f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//rigidbody2D.velocity = speed;
		currentSpeed = rigidbody2D.velocity;
		//Debug.Log (currentSpeed.x);
		xMovement = Input.GetAxis ("Horizontal");
		yMovement = Input.GetAxis ("Vertical");
		
		if (Mathf.Abs (currentSpeed.x) > 0.5f || Mathf.Abs (currentSpeed.y) > 0.5f) {
			animate.SetTrigger ("Run");
		} else {
			animate.SetTrigger ("Idle");
			rigidbody2D.velocity = new Vector2 (0f, 0f);
		}

		if (forward && currentSpeed.x < 0) {
			Vector3 temp = transform.localScale;
			temp.x *= -1f;
			transform.localScale = temp;
			forward = false;
		} else {
			if (!forward && currentSpeed.x > 0) {
				Vector3 temp = transform.localScale;
				temp.x *= -1f;
				transform.localScale = temp;
				forward = true; 
			}
		}
		if (Mathf.Abs (currentSpeed.x) < maxSpeed.x) {
			rigidbody2D.AddForce (Vector2.right * xMovement * movementForce);
		} else {
			rigidbody2D.AddForce (Vector2.right * xMovement * 15f);
		}
		
		if (Mathf.Abs (currentSpeed.y) < maxSpeed.y) {
			rigidbody2D.AddForce (Vector2.up * yMovement * movementForce);
		} else {
			rigidbody2D.AddForce (Vector2.up * yMovement * 15f);
		}

	}
}
