using UnityEngine;
using System.Collections;

/** Main controller for most level-based events.
 * 
 * @author: Ebtissam Wahman
 */ 
public class SceneManager : MonoBehaviour
{
	public string NextSceneName; 		//Filename of the next scene 
	public float distanceDiffMin; 		//Minimum distance needed between character and animal
	public float currentDistanceDiff; 	//Current distance between character and animal

	private float timeOutDistance; //Distance at which it is almost impossible to catch the animal
	private bool timedOut;
	public float targetTimeVar;
	public float multiplier1;
	public float multiplier2;

	private Animal animalControl;
	private GameObject character;
	private GameObject animal;
	private CameraFollow cameraFollow;

	public bool isEndless;
	public bool isPlaying;
	public bool pauseAudio;
	public bool tutEnabled;
	public bool fainted;


	private int nextAnimalIndex;
	
	public GameObject[] animals;
	
	void Start ()
	{
		timeOutDistance = GameObject.FindObjectOfType<SceneRepeater> ().getSceneWidth () * 0.90f;
		Debug.Log ("TimeOutDistnace is : " + timeOutDistance);
		cameraFollow = GameObject.FindObjectOfType<CameraFollow> ();
		timedOut = false; 

		GameStateMachine.resetState ();
		isPlaying = true;
		pauseAudio = false;
		fainted = false;

		character = GameObject.FindGameObjectWithTag ("character");
		animal = GameObject.FindGameObjectWithTag ("animal");
		Application.targetFrameRate = 30;

		for (int i = 0; i < animals.Length; i++) {
			if (animals [i].name.Contains (animal.name)) {
				if (i == animals.Length - 1) {
					nextAnimalIndex = 0;
				} else {
					nextAnimalIndex = i + 1;
				}
			}
		}

		animalControl = GameObject.FindObjectOfType<Animal> ();
		distanceDiffMin = 7f;
		currentDistanceDiff = Mathf.Abs (animal.transform.position.x - character.transform.position.x);
		if (tutEnabled) {
			gameObject.AddComponent<TutorialConditionalDialogController> ();
		}
	}


	
	void Update ()
	{
		if (GameStateMachine.currentState != (int)GameStateMachine.GameState.StartLevel) {
			currentDistanceDiff = Mathf.Abs (animal.transform.localPosition.x - character.transform.localPosition.x);
		}

		switch (GameStateMachine.currentState) {
		case (int)GameStateMachine.GameState.Intro:
			if (currentDistanceDiff > 25f) {
				animal.transform.localPosition = new Vector3 (animal.transform.localPosition.x + 25f, animal.transform.localPosition.y, animal.transform.localPosition.z);
				GameStateMachine.requestPlay ();
				GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("Ok! Let's go!");
				cameraFollow.moveCameraToCharacterOffset (5f);
			}
			break;
		case (int)GameStateMachine.GameState.Play:
			if (currentDistanceDiff > timeOutDistance) {
				timedOut = true;
				GameStateMachine.requestPause ();
			}
			break;
		case (int)GameStateMachine.GameState.Paused:
			if (animalControl.caught) {
				GameStateMachine.requestTransition ();
			}
			if (timedOut) {
				if (cameraFollow.cameraSettled) {
					cameraFollow.moveCameraToCharacterOffset (currentDistanceDiff + 5f);
					GameStateMachine.requestTransition ();
				}
			}
			break;
		case (int)GameStateMachine.GameState.Transition:
			if (animalControl.caught) {
				GameObject.FindObjectOfType<PainKiller> ().savePillCount ();
				GameStateMachine.requestEndLevel ();
			} else {
				if (timedOut && cameraFollow.cameraSettled) {
					if (objInView (animalControl.gameObject.renderer)) {
						animalControl.setSpeed ();
					} else {
						animalControl.speed = Vector2.zero;
						GameObject.FindObjectOfType<LevelGUIController> ().timeOutMenu ();
					}
				}
			}
			break;
		case (int) GameStateMachine.GameState.EndLevel:
			
			break;
		default:
			break;
		}
	}

	private bool objInView (Renderer obj)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
	}


	public void updatePillCount (int pillCount)
	{
		string theCount = "x" + pillCount;
		TextMesh pillCountText = GameObject.Find ("Pill Count").GetComponentInChildren<TextMesh> ();
		pillCountText.text = theCount;
	}
	
	private void changeAnimal ()
	{//TODO Bring this back into effect for Endless mode
		Vector3 position = character.transform.position;
		position.x -= 15f;
		Destroy (animal);
		animal = Instantiate (animals [nextAnimalIndex], position, Quaternion.identity) as GameObject;
		nextAnimalIndex = (nextAnimalIndex + 1) % animals.Length;
		animalControl = GameObject.FindObjectOfType<Animal> ();
		animalControl.setSpeed ();
	}

}


