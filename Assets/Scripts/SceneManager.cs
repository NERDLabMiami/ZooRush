using UnityEngine;
using System.Collections;

/** Main controller for most level-based events.
 * 
 * @author: Ebtissam Wahman
 */ 
public class SceneManager : MonoBehaviour
{
	public bool startPressed;
	public string NextSceneName; 		//Filename of the next scene 
	public float distanceDiffMin; 		//Minimum distance needed between character and animal
	public float currentDistanceDiff; 	//Current distance between character and animal
	public int levelNumber;				
	public float targetTimeVar;
	public float multiplier1;
	public float multiplier2;
	
	public GameObject[] menus;
	private GameObject screenDimmer;
	
	private PlayerControls playerControl;
	private AudioController audioController;
	private ScoreKeeper scoreKeeper;
	private Animal animalControl;
	private GameObject character;
	private GameObject animal;
	private NetLauncher netLauncher;
	
	public bool isEndless;
	public bool levelStartWait;
	public bool isPlaying;
	public bool pauseAudio;
	public bool tutEnabled;
	public bool fainted;
	public bool hitByVehicle;
	
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
	private int nextAnimalIndex;
	
	public GameObject[] animals;
	
	void Start ()
	{
		startPressed = false;
		isPlaying = true;
		pauseAudio = false;
		levelStartWait = true;
		fainted = false;
		hitByVehicle = false;
		scoreDisplayed = false;
		timeCountDown = false;
		timeWait = true;
		starDisplay1 = false;
		starDisplay2 = false;
		stars = 3;
		timeCounter = 0;
		infectionCounter = 0;
		character = GameObject.FindGameObjectWithTag ("character");
		animal = GameObject.FindGameObjectWithTag ("animal");
		for (int i = 0; i < animals.Length; i++) {
			if (animals [i].name.Contains (animal.name)) {
				if (i == animals.Length - 1) {
					nextAnimalIndex = 0;
				} else {
					nextAnimalIndex = i + 1;
				}
			}
		}
		
		playerControl = GameObject.FindObjectOfType<PlayerControls> ();
		audioController = GameObject.FindObjectOfType<AudioController> ();
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
		animalControl = GameObject.FindObjectOfType<Animal> ();
		netLauncher = GameObject.FindObjectOfType<NetLauncher> ();
		
		screenDimmer = GameObject.Find ("GUI - Level Dimmer");
		
		distanceDiffMin = 6.5f;
		currentDistanceDiff = Mathf.Abs (animal.transform.position.x - character.transform.position.x);
		updatePillCount ();
	}
	
	void Update ()
	{
		if (startPressed) {
			currentDistanceDiff = Mathf.Abs (animal.transform.localPosition.x - character.transform.localPosition.x);
			if (levelStartWait) {
				if (currentDistanceDiff > 25f) {
					animal.transform.localPosition = new Vector3 (animal.transform.localPosition.x + 25f, animal.transform.localPosition.y, animal.transform.localPosition.z);
					levelStartWait = false;
				}	
			} else {
				if (isPlaying) {
					if (animalControl.caught) {
						isPlaying = false;
						if (isEndless) {
							StartCoroutine (resetSceneEndlessMode ());
						} else {
							audioController.pauseAudio ();
							if (!scoreDisplayed) {
								StartCoroutine (displayScore ());
							}
						}
					} else {
						if (GameObject.FindObjectOfType<StopwatchController> () == null) {
							if (currentDistanceDiff < distanceDiffMin) {
								playerControl.setSpeed (animalControl.speed);
								netLauncher.launchEnabled = true;
							} else {
								netLauncher.launchEnabled = false;
							}
						} else {
							netLauncher.launchEnabled = false;
						}
						if (fainted) {
							isPlaying = false;
							pauseAudio = true;
							if (isEndless) {
								StartCoroutine (displayEndlessScore ());
							} else {
								NextSceneHandler.fainted ();
							}
						} else {
							if (hitByVehicle) {
								isPlaying = false;
								pauseAudio = true;
								if (isEndless) {
									StartCoroutine (displayEndlessScore ());
								} else {
									NextSceneHandler.hitByCar ();
								}
							} else {
								if (GameObject.FindObjectOfType<StopwatchController> () == null) {
									if (currentDistanceDiff < distanceDiffMin) {
										playerControl.setSpeed (animalControl.speed);
										netLauncher.launchEnabled = true;
									} else {
										netLauncher.launchEnabled = false;
									}
								} else {
									netLauncher.launchEnabled = false;
								}
								if (netLauncher.launchEnabled) {
									playerControl.setSpeed (animalControl.speed);
								}
							}
						}	
					}
				} else {
					netLauncher.launchEnabled = false;
				}
			}
		}
	}
	
	void FixedUpdate ()
	{
		if (startPressed) {
			if (timeCountDown) {
				if (timeWait) {
					StartCoroutine (TimeWait ());
				} else {
					if (timeCounter < time) {
						StartCoroutine (starDisplay (stars));
						StartCoroutine (timeCountUpMethod ());
					} else {
						timeCountDown = false;
						if (!starDisplay1) {
							if (time - targetTimeVar > 0) {
								stars--;
							}
							if (time - (multiplier1 * targetTimeVar) > 0) {
								stars--;
							}
							if (time - (multiplier2 * targetTimeVar) > 0) {
								stars--;
							}
							StartCoroutine (starDisplay (stars));
						} else {
							if (infectionCounter < infections) {
								StartCoroutine (infectionCountUp ());
							} else {
								if (!starDisplay2) {
									if (stars > 0) {
										if (infections > powerUps) {
											stars--;
										}
									}
									StartCoroutine (starDisplay (stars));
								}
							}
						}
					}
				}
			}
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
	
	private IEnumerator infectionCountUp ()
	{
		infections += 1;
		yield return new WaitForSeconds (0.3f);
		GameObject.Find ("Menu - Infect Count").GetComponent<TextMesh> ().text = "" + infections;
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

	public void startLevel ()
	{
		startPressed = true;
		animalControl.setSpeed ();
	}
	
	private IEnumerator TimeWait ()
	{
//		Debug.Log ("Waiting");
		yield return new WaitForSeconds (1.5f);
		timeWait = false;
	}
	
	private IEnumerator timeCountUpMethod ()
	{
		TextMesh timeTextMesh = GameObject.Find ("Menu - Time").GetComponent<TextMesh> ();
		timeTextMesh.text = TimeText (timeCounter);
		yield return new WaitForSeconds (0.1f);
		timeCounter += 1;
	}
	
	private IEnumerator displayEndlessScore ()
	{
		int[] scores = scoreKeeper.getScore ();
		time = scores [5];
		yield return new WaitForSeconds (0.1f);
		
		dimScreen ();
		if (GameObject.Find ("GUI Menu - Endless Mode(Clone)") == null) {
			GameObject menu = GameObject.Find (menus [1].name);
			if (menu == null) {
				menu = Instantiate (menus [3]) as GameObject;
				menu.transform.parent = Camera.main.transform;
				menu.transform.localPosition = new Vector3 (0f, 0f, 10f);
				GameObject.Find ("Menu - Time Value").GetComponent<TextMesh> ().text = TimeText (time);
				GameObject.Find ("Menu - Animals Value").GetComponent<TextMesh> ().text = "" + scores [7];
			}
			
		}
	}

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
		GameObject menu = GameObject.Find (menus [2].name);
		if (menu == null) {
			menu = Instantiate (menus [2]) as GameObject;
			menu.transform.parent = Camera.main.transform;
			menu.transform.localPosition = new Vector3 (0f, 0f, 10f);
		}
		
		TextMesh[] menuText = menu.GetComponentsInChildren<TextMesh> ();
		foreach (TextMesh text in menuText) {
			if (text.name.Contains ("Next Level")) {
				text.gameObject.GetComponent<TextOption> ().levelName = NextSceneName;
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
		unlockLevel ();
		timeCountDown = true;
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

	private void dimScreen ()
	{
		screenDimmer.GetComponent<SpriteRenderer> ().enabled = true;
	}
	
	private void lightScreen ()
	{
		screenDimmer.GetComponent<SpriteRenderer> ().enabled = false;
	}

	private void unlockLevel ()
	{
		PlayerPrefs.SetString (NextSceneName, "true");
		if (PlayerPrefs.HasKey ("Levels Unlocked")) {
			int val = PlayerPrefs.GetInt ("Levels Unlocked");
			val++;
			PlayerPrefs.SetInt ("Levels Unlocked", val);	 
		} else {
			PlayerPrefs.SetInt ("Levels Unlocked", 2);	 
		}
	}
	
	private void changeAnimal ()
	{
		Vector3 position = character.transform.position;
		position.x -= 15f;
		Destroy (animal);
		animal = Instantiate (animals [nextAnimalIndex], position, Quaternion.identity) as GameObject;
		nextAnimalIndex = (nextAnimalIndex + 1) % animals.Length;
		animalControl = GameObject.FindObjectOfType<Animal> ();
		animalControl.setSpeed ();
	}
	
	private IEnumerator resetSceneEndlessMode ()
	{
		yield return new WaitForSeconds (1f);
		lightScreen ();
		changeAnimal ();
		levelStartWait = true;
		isPlaying = true;
	}
}


