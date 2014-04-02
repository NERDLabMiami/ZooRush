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
	
	private LevelGUIController levelGUI; //Pointer to the throw alert dialog box
	private SceneManager sceneManager; //Pointer to the level's scene manager
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
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
	}
	
	void Update ()
	{
		launchEnabled = enableLaunch ();
		if (launchEnabled) {
			if (!throwAlertDisplayed) {
				throwAlertDisplayed = levelGUI.displayThrowAlert ();
				GameObject.FindObjectOfType<CameraFollow> ().moveCameraToCharacterOffset (3.5f);
				GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("AHA!");
			}
		} else {
			if (throwAlertDisplayed) {
				throwAlertDisplayed = levelGUI.removeThrowAlert ();
				throwCount = 0;
				GameObject.FindObjectOfType<CameraFollow> ().moveCameraToCharacterOffset (5f);
			}
		}

		if (!InputManager.enter) {
			firing = false;
		} else {
			if (!firing && throwAlertDisplayed && throwCount < 3) {
				firing = true;
				fire ();
			}	
		}
		
		if (throwCount >= 3 && !animal.caught) { //Pauses character momentarily and resets the netthrow count
			animal.getAway ();
			GameObject.FindObjectOfType<PlayerControls> ().resetSpeed (1f);
			launchEnabled = false;
		}
	}
	
	/** 
	*	Instatiates a new net object and applies a velocity in the +x-direction
	*/
	private void fire ()
	{
		GameObject.FindObjectOfType<AudioController> ().objectInteraction (clip);
		Rigidbody2D netInstance = Instantiate (prefab, transform.position, prefab.transform.rotation) as Rigidbody2D;
		netInstance.velocity = new Vector2 (speed, 0f);
		throwCount += 1;
	}

	public void disableLaunch ()
	{
		launchEnabled = false;
	}

	public bool enableLaunch ()
	{
		switch (GameState.currentState) {
		case GameState.States.Play:
		case GameState.States.Transition:
			if (sceneManager && sceneManager.currentDistanceDiff < sceneManager.distanceDiffMin) {
				return true;
			}
			return false;
		default:
			return false;
		}
	}
	
}
