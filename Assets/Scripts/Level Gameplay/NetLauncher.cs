using UnityEngine;
using System.Collections;

/** Launches A net at the animal being captured.
 * 
 * @author Ebtissam Wahman
 */ 

public class NetLauncher : MonoBehaviour
{
	public AudioClip clip;
	public bool launchEnabled; //Indicates if it is possible to launch a net 
	public Rigidbody2D prefab; //The net prefab that will be instantiated
	
	private Animal animal;	//Pointer to the animal class
	private float speed;	//Speed at which the net will be launched on the x-axis
	private bool firing;	//Indicates if the character is currently firing a net
	private int throwCount; //number of tries that have been made 
	private int currentNetIndex;
	public Rigidbody2D[] nets; //pool of nets to save instatiating while firing

	void Start ()
	{	
		speed = 30f;	
		firing = false;
		throwCount = 0;
		animal = GameObject.FindObjectOfType<Animal> ();
		currentNetIndex = 0;
		nets = new Rigidbody2D[5];
		for (int i = 0; i < nets.Length; i++) {
			Rigidbody2D net = Instantiate (prefab, (Vector2.zero - 8 * Vector2.up), prefab.transform.rotation) as Rigidbody2D;
			nets [i] = net;
		}
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
		case GameState.States.Launch:
			throwCount = 0;
			break;
		default:
			//NOP;
			break;
		}
	}

	public void throwNet ()
	{
		if (GameState.checkForState (GameState.States.Launch)) {
			if (!firing && throwCount < 3) {
				firing = true;
				fire ();
			}
			if (throwCount >= 3 && animal && !animal.caught) { //Pauses character momentarily and resets the netthrow count
				animal.getAway ();
				GameState.requestPlay ();
			}
		}
	}

	private IEnumerator resetFiringState ()
	{
		yield return new WaitForSeconds (0.1f);
		firing = false;
	}

	/** 
	*	Instatiates a new net object and applies a velocity in the +x-direction
	*/
	private void fire ()
	{
//		Debug.Log ("FIRE CALLED");
		GameObject.FindObjectOfType<AudioController> ().objectInteraction (clip);
		Rigidbody2D netInstance = nets [currentNetIndex];
		netInstance.transform.position = transform.position;
		netInstance.isKinematic = false;
		netInstance.velocity = new Vector2 (speed, 0f);
		throwCount += 1;
		currentNetIndex = (currentNetIndex + 1) % nets.Length;
		StartCoroutine (resetFiringState ());
	}

}
