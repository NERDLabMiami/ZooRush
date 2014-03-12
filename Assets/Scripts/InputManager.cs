using UnityEngine;
using System.Collections;

/** The Input Manager acts as the platform independent interface between the game and the input method the user is emplyoing.
 * @author: Ebtissam Wahman
 */ 
public class InputManager : MonoBehaviour
{

	private float xInput;
	private float yInput;
	private float confirm;

	private Ray origin;
	public static RaycastHit pointerTouch;

	/** The Input manager keeps track of the previous input in order to provide 
	 * a trigger based input system.
	 * 
	 * An input is only read once until the user lets go of the key or resets 
	 * the position of the joystick or removes their finger from the screen.
	 * 
	 */ 

	private  bool prevUp;
	private  bool prevDown;
	private  bool prevLeft;
	private  bool prevRight;
	private  bool prevEnter;
	
//	public float xDelta;
	public float yDelta;
	
	public static bool up;
	public static bool down;
	public static bool left;
	public static bool right;
	public static bool enter;
	public static bool touching;
	public static bool escape;

	void Awake ()
	{
		prevUp = false;
		prevDown = false;
		prevLeft = false;
		prevRight = false;
		prevEnter = false;
		up = false;
		down = false;
		left = false;
		right = false;
		enter = false;
		escape = false;
		if (Input.touchCount > 0) {
			origin = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
		} else {
			origin = Camera.main.ScreenPointToRay (Input.mousePosition);
		}
		
		touching = Physics.Raycast (origin, out pointerTouch);
		Input.simulateMouseWithTouches = true;
	}

	void Start ()
	{
		prevUp = false;
		prevDown = false;
		prevLeft = false;
		prevRight = false;
		prevEnter = false;
		up = false;
		down = false;
		left = false;
		right = false;
		enter = false;
		escape = false;
		if (Input.touchCount > 0) {
			origin = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
		} else {
			origin = Camera.main.ScreenPointToRay (Input.mousePosition);
		}
		
		touching = Physics.Raycast (origin, out pointerTouch);
		Input.simulateMouseWithTouches = true;
		
	}
	
	void Update ()
	{
		if (Input.touchCount > 0) {
			origin = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
		} else {
			origin = Camera.main.ScreenPointToRay (Input.mousePosition);
		}

//		xDelta = (Input.GetAxis ("Mouse X") > 0) ? 1 : ((Input.GetAxis ("Mouse X") < 0) ? -1 : 0);
		yDelta = (Input.GetAxis ("Mouse Y") > 0) ? 1 : ((Input.GetAxis ("Mouse Y") < 0) ? -1 : 0);
//		Debug.Log ("X: " + xDelta + "\nY: " + yDelta);
		touching = Physics.Raycast (origin, out pointerTouch);
		
		xInput = Input.GetAxis ("Horizontal");
		yInput = Input.GetAxis ("Vertical");
		confirm = Input.GetAxis ("Fire1");
		escape = Input.GetKey (KeyCode.Escape);

		changeValue (ref prevLeft, ref left, xInput < 0);
		changeValue (ref prevRight, ref right, xInput > 0);
		changeValue (ref prevUp, ref up, yInput > 0);
		changeValue (ref prevDown, ref down, yInput < 0);
		changeValue (ref prevEnter, ref enter, confirm > 0);
	}
	
	private void changeValue (ref bool previous, ref bool current, bool pressed)
	{
		if (pressed) {//input activated
			if (!previous) { // if the input is being leaned on
				previous = true;
				current = true;
			} else { // else keep track of the input but do not echo
				previous = true;
				current = false;
			}
		} else {// otherwise keep track that the input is not active
			previous = false;
			current = false;
		}
	}

	private void checkValues ()
	{
		Debug.Log (origin);
		Debug.Log ("Up: " + up);
		Debug.Log ("Down: " + down);
		Debug.Log ("Left: " + left);
		Debug.Log ("Right: " + right);
		Debug.Log ("Enter: " + enter);
	}
}
