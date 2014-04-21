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
	public bool timedOut;
	public float targetTimeVar;
	public float multiplier1;
	public float multiplier2;

	private Animal animalControl;
	private GameObject character;
	private GameObject animal;
	private CameraFollow cameraFollow;

	public bool isPlaying;
	public bool pauseAudio;
	private bool tutEnabled;
	public bool fainted;
	
	void Start ()
	{
		timeOutDistance = GameObject.FindObjectOfType<SceneRepeater> ().getSceneWidth () * 0.90f;
		cameraFollow = GameObject.FindObjectOfType<CameraFollow> ();
		timedOut = false; 
		tutEnabled = PlayerPrefs.GetInt ("TUTORIAL", 0) == 0;

		isPlaying = true;
		pauseAudio = false;
		fainted = false;
		character = GameObject.FindGameObjectWithTag ("character");
		animal = GameObject.FindGameObjectWithTag ("animal");
		animalControl = GameObject.FindObjectOfType<Animal> ();
		distanceDiffMin = 7f;
		currentDistanceDiff = Mathf.Abs (animal.transform.position.x - character.transform.position.x);
		if (tutEnabled) {
			gameObject.AddComponent<TutorialConditionalDialogController> ();
		}
		GameState.requestStart ();
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
		case GameState.States.Intro:
			StartCoroutine (introSequence ());
			break;
		case GameState.States.Transition:
			if (animalControl.caught) {
				GameState.requestWin ();
			}
			if (fainted) {
				GameState.requestLose ();
			}
			if (timedOut) {
				if (cameraFollow.cameraSettled) {
					cameraFollow.moveCameraToCharacterOffset (currentDistanceDiff + 5f);
					StartCoroutine (timeOutSequence ());
				}
			}
			break;
		case GameState.States.Win:
			if (tutEnabled) {
				PlayerPrefs.SetInt ("TUTORIAL", 1);
			}
			GameObject.FindObjectOfType<PainKiller> ().savePillCount ();
			BreadCrumbs.nextScene = NextSceneName;
			break;
		default:
			break;
		}
	}

	private IEnumerator introSequence ()
	{
		while (currentDistanceDiff < 25f) {
			yield return new WaitForFixedUpdate ();
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
			if (currentDistanceDiff < distanceDiffMin) {
				GameState.requestLaunch ();
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


