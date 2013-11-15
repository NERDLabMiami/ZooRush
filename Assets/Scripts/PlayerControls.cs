using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{

	private Vector2 currentSpeed;

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
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
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

		rigidbody2D.AddForce (Vector2.right * xMovement * movementForce);
		rigidbody2D.AddForce (Vector2.up * yMovement * movementForce);

	}
}
