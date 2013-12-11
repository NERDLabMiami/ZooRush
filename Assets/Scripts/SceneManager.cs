using UnityEngine;
using System.Collections;


/** Main controller for most level-based events.
 * 
 * @author: Ebtissam Wahman
 */ 
public class SceneManager : MonoBehaviour
{
	public string NextSceneName;
	public float distanceDiffMin;
	public float currentDistanceDiff;
	public float waitTime;
	public int levelNumber;
	public float targetTimeVar;
	public float multiplier1;
	public float multiplier2;
	public GameObject[] menus;
	private GameObject screenDimmer;
	
	private PlayerControls playerControl;
	private ScoreKeeper scoreKeeper;
	private Animal animalControl;

	private GameObject character;
	private GameObject animal;
	private GameObject painBar;
	private GameObject timeIndicator;
	private NetLauncher netLauncher;
	
	public bool levelStartWait;
	public bool isPlaying;
	public bool tutEnabled;
	public bool fainted;
		
	void Start ()
	{
		isPlaying = true;
		levelStartWait = true;
		fainted = false;
		playerControl = GameObject.FindObjectOfType<PlayerControls> ();
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
		animalControl = GameObject.FindObjectOfType<Animal> ();
		netLauncher = GameObject.FindObjectOfType<NetLauncher> ();
		character = GameObject.FindGameObjectWithTag ("character");
		animal = GameObject.FindGameObjectWithTag ("animal");
		
		screenDimmer = GameObject.Find ("GUI - Level Dimmer");
		
		distanceDiffMin = 6.5f;
		currentDistanceDiff = Mathf.Abs (animal.transform.position.x - character.transform.position.x);
		painBar = GameObject.Find ("Pain Bar");
		timeIndicator = GameObject.Find ("GUI - Time");
//		if (!PlayerPrefs.HasKey ("Tutorial")) {
//			PlayerPrefs.SetString ("Tutorial", "true");
//		}
//		tutEnabled = ((PlayerPrefs.GetString ("Tutorial").Equals ("true")) ? true : false);
	}
	
	void Update ()
	{
		currentDistanceDiff = Mathf.Abs (animal.transform.localPosition.x - character.transform.localPosition.x);
		if (levelStartWait) {
			if (currentDistanceDiff > 18f) {
				levelStartWait = false;
			}
			
		} else {
			if (isPlaying) {
				if (animalControl.caught) {
					isPlaying = false;
					StartCoroutine (displayScore ());
				} else {
					if (currentDistanceDiff < distanceDiffMin) {
						playerControl.setSpeed (animalControl.speed);
						netLauncher.launchEnabled = true;
					} else {
						netLauncher.launchEnabled = false;
					}
					if (fainted) {
						isPlaying = false;
						StartCoroutine (displayFainted ());
					} else {
						if (currentDistanceDiff < distanceDiffMin) {
							playerControl.setSpeed (animalControl.speed);
							netLauncher.launchEnabled = true;
						} else {
							netLauncher.launchEnabled = false;
						}
					}
					
				}
			}
		}
	}
	
	private IEnumerator displayFainted ()
	{
		yield return new WaitForSeconds (0.1f);
		dimScreen ();
		
		GameObject menu = GameObject.Find (menus [1].name);
		if (menu == null) {
			menu = Instantiate (menus [1]) as GameObject;
			menu.transform.parent = Camera.main.transform;
			menu.transform.localPosition = new Vector3 (0f, 0f, 10f);
		}
		
	}

	private IEnumerator displayScore ()
	{
		yield return new WaitForSeconds (1f);
		dimScreen ();
		
		int[] scores = scoreKeeper.getScore ();
		scoreStore (scores);
		GameObject menu = GameObject.Find (menus [2].name);
		if (menu == null) {
			menu = Instantiate (menus [2]) as GameObject;
			menu.transform.parent = Camera.main.transform;
			menu.transform.localPosition = new Vector3 (0f, 0f, 10f);
		}
		
		TextMesh[] menuText = menu.GetComponentsInChildren<TextMesh> ();
		foreach (TextMesh text in menuText) {
			if (text.name.Contains ("Red")) {
				text.text = "" + scores [0];
			}
			if (text.name.Contains ("Yellow")) {
				text.text = "" + scores [1];
			}
			if (text.name.Contains ("Green")) {
				text.text = "" + scores [2];
			}
			if (text.name.Contains ("Water")) {
				text.text = "" + scores [3];
			}
			if (text.name.Contains ("Time")) {
				int minutes = scores [5] / 60;
				int seconds = scores [5] % 60;
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
				text.text = timeText;
			}
		}
		
		GameObject pill = GameObject.Find ("Menu - Pill Icon");
		if (scores [4] == 1) {
			pill.GetComponent<SpriteRenderer> ().color = Color.grey;
		} else {
			pill.GetComponent<SpriteRenderer> ().color = Color.white;
		}

		if (scores [6] > 0) {//# of stars > 0
			Animator[] stars = GetComponentsInChildren<Animator> ();
			foreach (Animator star in stars) {
				if (star.name.Contains ("Star 1")) {
					if (scores [6] >= 1) {
						star.SetTrigger ("Activate");
					}
				}
				if (star.name.Contains ("Star 2")) {
					if (scores [6] >= 2) {
						star.SetTrigger ("Activate");
					}
				}
				if (star.name.Contains ("Star 3")) {
					if (scores [6] >= 3) {
						star.SetTrigger ("Activate");
					}
				}
			}
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

	private void dimScreen ()
	{
		timeIndicator.SetActive (false);
		painBar.SetActive (false);
		screenDimmer.GetComponent<SpriteRenderer> ().enabled = true;
	}
	
	private void lightScreen ()
	{
		timeIndicator.SetActive (true);
		painBar.SetActive (true);
		screenDimmer.GetComponent<SpriteRenderer> ().enabled = false;
	}

	private void unlockLevel ()
	{
		PlayerPrefs.SetString (NextSceneName, "true");
	}

}


