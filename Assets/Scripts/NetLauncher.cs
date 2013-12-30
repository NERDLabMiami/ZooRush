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
	
	private GameObject throwAlert; //Pointer the the throw alert dialog box
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
		throwAlert = GameObject.Find ("Throw Alert");
	}
	
	void Update ()
	{
		if (launchEnabled) {
			if (!throwAlert.activeSelf) { //enalbe the throw alert dialog box
				throwAlert.SetActive (true);
			}
		} else {
			if (throwAlert.activeSelf) { //disable the throw alert dialog box
				throwAlert.SetActive (false);
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
