using UnityEngine;
using System.Collections;

public class LevelGUIController : MonoBehaviour
{
	private SceneManager sceneManager;

	public bool stopwatchActive;
	private GameObject throwAlert;
	private GameObject stopwatch;
	private GameObject startMenu;
	private StopwatchController stopwatchController;

	public GameObject[] menuPrefabs;
	public GameObject stopWatchObject;
	public GameObject screenDimmer;

	void Start ()
	{
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		stopwatchActive = false;

		//menuPrefabs[0] is the start screen by default
		startMenu = GameObject.Instantiate (menuPrefabs [0]) as GameObject;
		startMenu.transform.parent = transform;
		startMenu.transform.localPosition = new Vector3 (0, 0, 10);
	}
	
	void Update ()
	{
	
	}

	/**
	 * @returns true if a stopwatch object is present;
	 */ 
	public void displayStopwatch (string infectionType)
	{
		if (stopwatch == null) {
			stopwatch = Instantiate (stopWatchObject) as GameObject;
			stopwatch.transform.parent = transform;
			stopwatch.transform.localPosition = new Vector3 (0, 0, 10);
			stopwatchController = stopwatch.GetComponent<StopwatchController> ();
		}
		stopwatchController.receiveInteraction (infectionType);
	}

	public void removeStopwatch ()
	{
		if (stopwatch != null) {
			stopwatchController.stopStopwatch ();
		}
	}

	/**
	 * @returns true if a throw alert dialog is present, false otherwise
	 */ 
	public bool displayThrowAlert ()
	{
		if (throwAlert == null) {
			//menuPrefabs[1] is the throw alert dialog box by default
			throwAlert = Instantiate (menuPrefabs [1]) as GameObject;
			throwAlert.transform.parent = transform;
			throwAlert.transform.localPosition = new Vector3 (0, 0, 10);
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
}
