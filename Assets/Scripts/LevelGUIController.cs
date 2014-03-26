using UnityEngine;
using System.Collections;

public class LevelGUIController : MonoBehaviour
{
	private SceneManager sceneManager;
	private Animal animalControl;
	private ScoreKeeper scoreKeeper;


	public bool stopwatchActive;
	private GameObject throwAlert;
	private GameObject stopwatch;
	private GameObject startMenu;
	private StopwatchController stopwatchController;

	public GameObject[] menuPrefabs;
	public GameObject stopWatchObject;
	public SpriteRenderer screenDimmer;
	private bool isTutorial;

	private bool starDisplay1;
	private bool starDisplay2;
	private bool scoreDisplayed;
	private bool timeCountDown;
	private bool timeWait;
	private int time;
	private int timeCounter;
	private int infections;
	private int infectionCounter;
	private int powerUps;
	private int starCount;
	private int stars;
//	private int nextAnimalIndex;

	void Start ()
	{
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		animalControl = GameObject.FindObjectOfType<Animal> ();
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();


		stopwatchActive = false;

		//menuPrefabs[0] is the start screen by default
		startMenu = GameObject.Instantiate (menuPrefabs [0]) as GameObject;
		startMenu.transform.parent = transform;
		startMenu.transform.localPosition = new Vector3 (0, 0, 0);

		scoreDisplayed = false;
		timeCountDown = false;
		timeWait = true;
		starDisplay1 = false;
		starDisplay2 = false;
		stars = 3;
		timeCounter = 0;
		infectionCounter = 0;

//		GameObject animal = GameObject.FindGameObjectWithTag ("animal");

//		for (int i = 0; i < sceneManager.animals.Length; i++) {
//			if (sceneManager.animals [i].name.Contains (animal.name)) {
//				if (i == sceneManager.animals.Length - 1) {
//					nextAnimalIndex = 0;
//				} else {
//					nextAnimalIndex = i + 1;
//				}
//			}
//		}
	}
	
	void Update ()
	{
		if (InputManager.escape) {
			if (GameStateMachine.currentState == (int)GameStateMachine.GameState.Play) {
				pauseMenu ();
			}
		} else {
			switch (GameStateMachine.currentState) {
			case (int)GameStateMachine.GameState.EndLevel:
				if (sceneManager.fainted) {
					NextSceneHandler.fainted ();
				}
				if (animalControl.caught) {
					if (!scoreDisplayed) {
						StartCoroutine (displayScore ());
					}
				}
				break;

			}
		}
	}

//	void FixedUpdate ()
//	{
//		if (GameStateMachine.currentState == (int)GameStateMachine.GameState.EndLevel) {
//			if (timeCountDown) {
//				if (timeWait) {
//					StartCoroutine (TimeWait ());
//				} else {
//					if (timeCounter < time) {
//						StartCoroutine (starDisplay (stars));
//						StartCoroutine (timeCountUpMethod ());
//					} else {
//						timeCountDown = false;
//						if (!starDisplay1) {
//							if (time - sceneManager.targetTimeVar > 0) {
//								stars--;
//							}
//							if (time - (sceneManager.multiplier1 * sceneManager.targetTimeVar) > 0) {
//								stars--;
//							}
//							if (time - (sceneManager.multiplier2 * sceneManager.targetTimeVar) > 0) {
//								stars--;
//							}
//							if (stars > 0) {
//								if (scoreKeeper.totalInfectionsTouched () > 0) {
//									stars--;
//								}
//							}
//
//							StartCoroutine (starDisplay (stars));
//						} else {
//							if (infectionCounter < infections) {
//								StartCoroutine (infectionCountUp ());
//							} else {
//								if (!starDisplay2) {
//									if (stars > 0) {
//										if (infections > powerUps) {
//											stars--;
//										}
//									}
//									StartCoroutine (starDisplay (stars));
//								}
//							}
//						}
//					}
//				}
//			}
//		}
//	}

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
			GameStateMachine.requestPause ();
			GameObject pauseMenu = Instantiate (menuPrefabs [3]) as GameObject;
			pauseMenu.transform.parent = transform;
			pauseMenu.transform.localPosition = Vector3.zero;
		}

	}

//	private IEnumerator displayEndlessScore ()
//	{
//		int[] scores = scoreKeeper.getScore ();
//		time = scores [5];
//		yield return new WaitForSeconds (0.1f);
//		
//		dimScreen ();
//		if (GameObject.Find ("GUI Menu - Endless Mode(Clone)") == null) {
//			GameObject menu = Instantiate (menuPrefabs [4]) as GameObject;
//			menu.transform.parent = transform;
//			menu.transform.localPosition = new Vector3 (0f, 0f, 0f);
//			GameObject.Find ("Menu - Time Value").GetComponent<TextMesh> ().text = TimeText (time);
//			GameObject.Find ("Menu - Animals Value").GetComponent<TextMesh> ().text = "" + scores [7];
//		}
//	}

	private IEnumerator displayScore ()
	{
		scoreDisplayed = true;
		yield return new WaitForSeconds (1f);
		dimScreen ();
		
		int[] scores = scoreKeeper.getScore ();
		scoreStore (scores);
		time = scores [5];
		infections = scores [0] + scores [1] + scores [2];
		powerUps = scores [3] + scores [4];
		GameObject menu = GameObject.Find (menuPrefabs [2].name);
		if (menu == null) {
			menu = Instantiate (menuPrefabs [2]) as GameObject;
			menu.transform.parent = transform;
			menu.transform.localPosition = new Vector3 (0f, 0f, 0f);
		}
		
		TextMesh[] menuText = menu.GetComponentsInChildren<TextMesh> ();
		foreach (TextMesh text in menuText) {
			if (text.name.Contains ("Next Level")) {
				text.gameObject.GetComponent<TextOption> ().levelName = sceneManager.NextSceneName;
			}
			
			if (text.name.Contains ("Infect Count")) {
				text.text = "" + infections;
			}
		}
		
		Animator[] stars = GetComponentsInChildren<Animator> ();
		foreach (Animator star in stars) {
			if (star.name.Contains ("Star 1")) {
				star.SetTrigger ("Activate");
			}
			if (star.name.Contains ("Star 2")) {
				star.SetTrigger ("Activate");
			}
			if (star.name.Contains ("Star 3")) {
				star.SetTrigger ("Activate");
			}
		}
		
		starCount = scores [6];
		TextMesh timeTextMesh = GameObject.Find ("Menu - Time").GetComponent<TextMesh> ();
		timeTextMesh.text = TimeText (time);

		StartCoroutine (starDisplay (starCount));

		unlockLevel ();
	}

	private IEnumerator starDisplay (int starNumber)
	{
		if (time <= 0) {
			if (starDisplay1 == false) {
				starDisplay1 = true;
			} else {
				starDisplay2 = true;
			}
		}
		yield return new WaitForSeconds (1f);
		
		if (starNumber > 0) {//# of stars > 0
			Animator[] stars = GetComponentsInChildren<Animator> ();
			foreach (Animator star in stars) {
				if (star.name.Contains ("Star 1")) {
					if (starCount >= 1) {
						star.SetTrigger ("Activate");
					} else {
						star.SetTrigger ("Deactivate");
					}
				}
				if (star.name.Contains ("Star 2")) {
					if (starCount >= 2) {
						star.SetTrigger ("Activate");
					} else {
						star.SetTrigger ("Deactivate");
					}
				}
				if (star.name.Contains ("Star 3")) {
					if (starCount >= 3) {
						star.SetTrigger ("Activate");
					} else {
						star.SetTrigger ("Deactivate");
					}
				}
			}
		}
	}

//	private IEnumerator TimeWait ()
//	{
//		yield return new WaitForSeconds (1.5f);
//		timeWait = false;
//	}
	
//	private IEnumerator timeCountUpMethod ()
//	{
//		TextMesh timeTextMesh = GameObject.Find ("Menu - Time").GetComponent<TextMesh> ();
//		timeTextMesh.text = TimeText (timeCounter);
//		yield return new WaitForSeconds (0.1f);
//		timeCounter += 1;
//	}

	private string TimeText (int timeVal)
	{
		int minutes = timeVal / 60;
		int seconds = timeVal % 60;
		string timeText = "";
		if (minutes % 100 <= 9 && minutes <= 99) {
			timeText = "0" + minutes;
		} else {
			if (minutes <= 99) {
				timeText = "" + minutes;
			} else {
				timeText = "99";
			}
		}
		
		timeText += ":";
		
		if (seconds % 100 <= 9 && minutes <= 100f) {
			timeText += "0" + seconds;
		} else {
			if (minutes <= 100f) {
				timeText += "" + seconds;
			} else {
				timeText += "59+";
			}
		}
		
		return timeText;
	}

	private IEnumerator infectionCountUp ()
	{
		infections += 1;
		yield return new WaitForSeconds (0.3f);
		GameObject.Find ("Menu - Infect Count").GetComponent<TextMesh> ().text = "" + infections;
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
