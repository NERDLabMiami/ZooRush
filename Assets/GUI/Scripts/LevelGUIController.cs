using UnityEngine;
using System.Text;
using System.Collections;

public class LevelGUIController : MonoBehaviour
{
	private SceneManager sceneManager;
	private ScoreKeeper scoreKeeper;

	private GameObject throwAlert;
	private bool throwAlertDisplayed;
	private GameObject stopwatch;
	private GameObject startMenu;
	private StopwatchController stopwatchController;

	public GameObject[] menuPrefabs;
	public GameObject stopWatchObject;
	public SpriteRenderer screenDimmer;

	private bool scoreDisplayed;


	void Start ()
	{
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
		//menuPrefabs[0] is the start screen by default
		startMenu = GameObject.Instantiate (menuPrefabs [0]) as GameObject;
		startMenu.transform.parent = transform;
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
				GameObject.FindObjectOfType<LevelGUIController> ().timeOutMenu ();
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
				pauseMenu ();
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
		if (throwAlert == null) {
			//menuPrefabs[1] is the throw alert dialog box by default
			throwAlert = Instantiate (menuPrefabs [1]) as GameObject;
			throwAlert.transform.parent = transform;
			throwAlert.transform.localPosition = Vector3.zero;
		}
		return true;
	}

	/***
	 * @returns false if the there is no throw alert dialog present
	 */ 
	public bool removeThrowAlert ()
	{
		if (throwAlert != null) {
			Destroy (throwAlert);
		}
		return false;
	}

	public void timeOutMenu ()
	{
		if (GameObject.FindGameObjectWithTag ("menu") == null) {
			dimScreen ();
			GameObject timeOutMenu = Instantiate (menuPrefabs [5]) as GameObject;
			timeOutMenu.transform.parent = transform;
			timeOutMenu.transform.localPosition = Vector3.zero;
		}
	}

	public void pauseMenu ()
	{
		if (GameObject.FindGameObjectWithTag ("menu") == null) {
			GameObject pauseMenu = Instantiate (menuPrefabs [3]) as GameObject;
			pauseMenu.transform.parent = transform;
			pauseMenu.transform.localPosition = Vector3.zero;
			GameState.requestPause ();
		}
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
	
		GameObject menu = GameObject.Find (menuPrefabs [2].name);
		if (menu == null) {
			menu = Instantiate (menuPrefabs [2]) as GameObject;
			menu.transform.parent = transform;
			menu.transform.localPosition = new Vector3 (0f, 0f, 0f);
		}

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
