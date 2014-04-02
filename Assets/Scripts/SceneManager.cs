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
	
//	public GameObject[] animals;
	
	void Start ()
	{
		timeOutDistance = GameObject.FindObjectOfType<SceneRepeater> ().getSceneWidth () * 0.90f;
//		Debug.Log ("TimeOutDistnace is : " + timeOutDistance);
		cameraFollow = GameObject.FindObjectOfType<CameraFollow> ();
		timedOut = false; 
		if (NextSceneName.Contains ("2-Zoo")) {
			if (PlayerPrefs.GetInt ("TUTORIAL", 0) == 0) {
				tutEnabled = true;
			}
		} else {
			tutEnabled = false;
		}
		GameState.requestStart ();
		isPlaying = true;
		pauseAudio = false;
		fainted = false;

		character = GameObject.FindGameObjectWithTag ("character");
		animal = GameObject.FindGameObjectWithTag ("animal");

//		for (int i = 0; i < animals.Length; i++) {
//			if (animals [i].name.Contains (animal.name)) {
//				if (i == animals.Length - 1) {
//					nextAnimalIndex = 0;
//				} else {
//					nextAnimalIndex = i + 1;
//				}
//			}
//		}

		animalControl = GameObject.FindObjectOfType<Animal> ();
		distanceDiffMin = 7f;
		currentDistanceDiff = Mathf.Abs (animal.transform.position.x - character.transform.position.x);
		if (tutEnabled) {
			gameObject.AddComponent<TutorialConditionalDialogController> ();
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
		case GameState.States.Pause:
			break;
		case GameState.States.Play:
			break;
		case GameState.States.Dialog:
			break;
		case GameState.States.Intro:
			StartCoroutine (introSequence ());
			break;
		case GameState.States.Transition:
			if (animalControl.caught) {
				GameState.requestWin ();
			}
			if (timedOut) {
				if (cameraFollow.cameraSettled) {
					cameraFollow.moveCameraToCharacterOffset (currentDistanceDiff + 5f);
					StartCoroutine (timeOutSequence ());
				}
			}
			break;
		case GameState.States.Win:
			if (NextSceneName.Contains ("2-Zoo")) {
				PlayerPrefs.SetInt ("TUTORIAL", 1);
			}
			GameObject.FindObjectOfType<PainKiller> ().savePillCount ();
			//TODO Add Score Display here
			break;
		case GameState.States.Lose:
			if (timedOut) {
				GameObject.FindObjectOfType<LevelGUIController> ().timeOutMenu ();
			}
			break;
		default:
			break;
		}
	}

	private IEnumerator introSequence ()
	{
		while (currentDistanceDiff < 25f) {
			yield return new WaitForEndOfFrame ();
		}
		animal.transform.localPosition = new Vector3 (animal.transform.localPosition.x + 25f, animal.transform.localPosition.y, animal.transform.localPosition.z);
		GameState.requestPlay ();
		if (!tutEnabled) {
			GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("Ok! Let's go!");
		} else {
			GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("The first day\nis the hardest.");
		}
		cameraFollow.moveCameraToCharacterOffset (5f);
	}

	private IEnumerator timeOutSequence ()
	{
		while (!cameraFollow.cameraSettled) {
			yield return new WaitForFixedUpdate ();
		}
		while (objInView (animalControl.gameObject.renderer)) {
			animalControl.setSpeed ();
			yield return new WaitForFixedUpdate ();
		}
		animalControl.speed = Vector2.zero;
		GameState.requestLose ();
	}
	
	void Update ()
	{
		if (!GameState.checkForState (GameState.States.Start)) {
			currentDistanceDiff = Mathf.Abs (animal.transform.localPosition.x - character.transform.localPosition.x);
		}
		if (GameState.checkForState (GameState.States.Play)) {
			if (currentDistanceDiff > timeOutDistance) {
				timedOut = true;
				GameState.requestTransition ();
			}
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

}


