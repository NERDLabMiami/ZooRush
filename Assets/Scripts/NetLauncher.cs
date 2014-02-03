using UnityEngine;
using System.Collections;

/** Launches A net at the animal being captured.
 * 
 * @author Ebtissam Wahman
 */ 

public class NetLauncher : MonoBehaviour
{
	public bool launchEnabled; //Indicates if it is possible to launch a net 
	public Rigidbody2D prefab; //The net prefab that will be instantiated
	
	private LevelGUIController levelGUI; //Pointer the the throw alert dialog box
	private bool throwAlertDisplayed;
	private Animal animal;	//Pointer to the animal class
	private float speed;	//Speed at which the net will be launched on the x-axis
	private bool firing;	//Indicates if the character is currently firing a net
	private int throwCount; //number of tries that have been made 
	
	void Start ()
	{	
		speed = 30f;	
		firing = false;
		throwCount = 0;
		animal = GameObject.FindObjectOfType<Animal> ();
		levelGUI = GameObject.FindObjectOfType<LevelGUIController> ();
	}
	
	void Update ()
	{
		if (launchEnabled) {
			if (!throwAlertDisplayed) {
				throwAlertDisplayed = levelGUI.displayThrowAlert ();
			}
		} else {
			if (throwAlertDisplayed) {
				throwAlertDisplayed = levelGUI.removeThrowAlert ();
			}
		}
	}
	
	void FixedUpdate ()
	{
		if (!InputManager.enter) {
			firing = false;
		} else {
			if (!firing && launchEnabled && throwCount < 3) {
				firing = true;
				fire ();
			}	
		}
		if (throwCount >= 3 && !animal.caught) { //Pauses character momentarily and resets the netthrow count
			GameObject.FindObjectOfType<PlayerControls> ().resetSpeed ();
			throwCount = 0;
		}
	}
	
	/** 
	*	Instatiates a new net object and applies a velocity in the +x-direction
	*/
	private void fire ()
	{
		Rigidbody2D netInstance = Instantiate (prefab, transform.position, prefab.transform.rotation) as Rigidbody2D;
		netInstance.velocity = new Vector2 (speed, 0f);
		throwCount += 1;
	}
	
}
