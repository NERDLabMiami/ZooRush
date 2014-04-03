using UnityEngine;
using System.Text;
using System.Collections;

public class LevelGUIController : MonoBehaviour
{
	private SceneManager sceneManager;
	private ScoreKeeper scoreKeeper;

	public GameObject startMenu;
	public GameObject throwAlert;
	public GameObject pauseMenu;
	public GameObject timeOutMenu;
	private bool throwAlertDisplayed;
	private GameObject stopwatch;
	private StopwatchController stopwatchController;

	public GameObject levelCompleteMenu;
	public GameObject stopWatchObject;
	public SpriteRenderer screenDimmer;

	private bool scoreDisplayed;


	void Start ()
	{
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
		startMenu.transform.localPosition = new Vector3 (0, 0, 0);
		scoreDisplayed = false;
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
			if (!throwAlertDisplayed) {
				throwAlertDisplayed = displayThrowAlert ();
				GameObject.FindObjectOfType<CameraFollow> ().moveCameraToCharacterOffset (3.5f);
				GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("AHA!");
			}
			return; //skip the rest of the code
		case GameState.States.Lose:
			if (sceneManager.timedOut) {
				GameObject.FindObjectOfType<LevelGUIController> ().callTimeOutMenu ();
			} 
			if (sceneManager.fainted) {
				NextSceneHandler.fainted ();
			}
			break;
		case GameState.States.Win:
			if (!scoreDisplayed) {
				StartCoroutine (displayScore ());
			}
			break;
		default:
			break;
		}
		if (throwAlertDisplayed) {
			throwAlertDisplayed = removeThrowAlert ();
			GameObject.FindObjectOfType<CameraFollow> ().moveCameraToCharacterOffset (5f);
		}
	}
	
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			if (GameState.currentState == GameState.States.Play) {
				callPauseMenu ();
			}
		} 
	}

	private void dimScreen ()
	{
		screenDimmer.enabled = true;
	}
	
	private void lightScreen ()
	{
		screenDimmer.enabled = false;
	}

	/**
	 * @returns true if a stopwatch object is present;
	 */ 
	public void displayStopwatch (string infectionType)
	{
		if (!netCurrentlyInMotion () || !GameObject.FindObjectOfType<Animal> ().caught) {
			if (stopwatch == null) {
				stopwatch = Instantiate (stopWatchObject) as GameObject;
				stopwatch.transform.parent = transform;
				stopwatch.transform.localPosition = new Vector3 (0, 0, 0);
				stopwatchController = stopwatch.GetComponent<StopwatchController> ();
			}
			stopwatchController.receiveInteraction (infectionType);
		}
	}

	public void removeStopwatch ()
	{
		if (stopwatch != null) {
			stopwatchController.stopStopwatch ();
		}
	}

	private bool netCurrentlyInMotion ()
	{
		NetHandler[] nets = GameObject.FindObjectsOfType<NetHandler> ();
		if (nets != null) {
			foreach (NetHandler net in nets) {
				if (!net.collided) {
					return true;
				}
			}
		}
		return false;
	}

	/**
	 * @returns true if a throw alert dialog is present, false otherwise
	 */ 
	public bool displayThrowAlert ()
	{
		if (GameObject.FindObjectOfType<StopwatchController> () != null) {
			return false;
		}
		throwAlert.transform.localPosition = Vector3.zero;
		return true;
	}

	/***
	 * @returns false if the there is no throw alert dialog present
	 */ 
	public bool removeThrowAlert ()
	{
		throwAlert.transform.localPosition = Vector2.up * 23;
		return false;
	}

	public void callTimeOutMenu ()
	{
		dimScreen ();
		timeOutMenu.transform.localPosition = Vector3.zero;
	}

	public void callPauseMenu ()
	{
		pauseMenu.transform.localPosition = Vector3.zero;
		GameState.requestPause ();
	}

	public void dismissPauseMenu ()
	{
		pauseMenu.transform.localPosition = 23 * Vector3.up;
		GameState.requestPlay ();
	}

	private IEnumerator displayScore ()
	{
		scoreDisplayed = true;
		scoreStore (scoreKeeper.getScore ());

		float timePassed = 0;
		while (timePassed < 1) {
			timePassed += Time.deltaTime;
			yield return new WaitForFixedUpdate ();
		}
		dimScreen ();
	
		levelCompleteMenu.transform.localPosition = new Vector3 (0f, 0f, 0f);
		levelCompleteMenu.GetComponent<LevelCompleteMenu> ().activate ();

		unlockLevel ();
	}

	private void scoreStore (int[] score)
	{
		if (PlayerPrefs.HasKey (Application.loadedLevelName + "Stars")) {
			int currentHigh = PlayerPrefs.GetInt (Application.loadedLevelName + "Stars");
			if (currentHigh < score [6]) {
				PlayerPrefs.SetInt (Application.loadedLevelName + "Stars", score [6]);
				PlayerPrefs.SetInt (Application.loadedLevelName + "RedInfections", score [0]);
				PlayerPrefs.SetInt (Application.loadedLevelName + "YellowInfections", score [1]);
				PlayerPrefs.SetInt (Application.loadedLevelName + "GreenInfections", score [2]);
				PlayerPrefs.SetInt (Application.loadedLevelName + "WaterBottles", score [3]);
				PlayerPrefs.SetInt (Application.loadedLevelName + "PillBottle", score [4]);
				PlayerPrefs.SetInt (Application.loadedLevelName + "Time", score [5]);
			}
		} else {
			PlayerPrefs.SetInt (Application.loadedLevelName + "Stars", score [6]);
			PlayerPrefs.SetInt (Application.loadedLevelName + "RedInfections", score [0]);
			PlayerPrefs.SetInt (Application.loadedLevelName + "YellowInfections", score [1]);
			PlayerPrefs.SetInt (Application.loadedLevelName + "GreenInfections", score [2]);
			PlayerPrefs.SetInt (Application.loadedLevelName + "WaterBottles", score [3]);
			PlayerPrefs.SetInt (Application.loadedLevelName + "PillBottle", score [4]);
			PlayerPrefs.SetInt (Application.loadedLevelName + "Time", score [5]);
		}
	}

	private void unlockLevel ()
	{
		PlayerPrefs.SetInt (sceneManager.NextSceneName, 1);
		if (PlayerPrefs.HasKey ("Levels Unlocked")) {
			int val = PlayerPrefs.GetInt ("Levels Unlocked");
			val++;
			PlayerPrefs.SetInt ("Levels Unlocked", val);	 
		} else {
			PlayerPrefs.SetInt ("Levels Unlocked", 2);	 
		}
	}
}
