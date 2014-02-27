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

	public float targetTimeVar;
	public float multiplier1;
	public float multiplier2;

	private Animal animalControl;
	private GameObject character;
	private GameObject animal;

	public bool isEndless;
	public bool isPlaying;
	public bool pauseAudio;
	public bool tutEnabled;
	public bool fainted;


	private int nextAnimalIndex;
	
	public GameObject[] animals;
	
	void Start ()
	{
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
		distanceDiffMin = 10f;
		currentDistanceDiff = Mathf.Abs (animal.transform.position.x - character.transform.position.x);
		updatePillCount ();
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
			}
			break;
		case (int)GameStateMachine.GameState.Play:

			break;
		case (int)GameStateMachine.GameState.Paused:
			if (animalControl.caught) {
				GameStateMachine.requestTransition ();
			}
			break;
		case (int)GameStateMachine.GameState.Transition:
			if (animalControl.caught) {
				GameStateMachine.requestEndLevel ();
			}
			break;
		default:
			break;
		}
	}

	public void updatePillCount ()
	{
		string theCount = "x" + PlayerPrefs.GetInt ("PILLS");
		TextMesh[] pillCount = GameObject.Find ("Pill Count").GetComponentsInChildren<TextMesh> ();
		foreach (TextMesh texts in pillCount) {
			texts.text = theCount;
		}
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


