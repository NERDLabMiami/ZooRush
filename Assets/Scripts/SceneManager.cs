using UnityEngine;
using System.Collections;


/** Main controller for most level-based events.
 * 
 * @author: Ebtissam Wahman
 */ 
public class SceneManager : MonoBehaviour
{
	public float distanceDiffMin;
	public float currentDistanceDiff;
	public float waitTime;
	public int levelNumber;
	public float targetTime;
	
	public GameObject[] menus;
	private GameObject screenDimmer;
	
	private PlayerControls playerControl;
	private ScoreKeeper scoreKeeper;
	private Animal animalControl;

	private GameObject character;
	private GameObject animal;
	
	public bool levelStartWait;
	public bool isPlaying;
	public bool tutEnabled;
	
	void Start ()
	{
		isPlaying = true;
		levelStartWait = true;
		playerControl = GameObject.FindObjectOfType<PlayerControls> ();
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
		animalControl = GameObject.FindObjectOfType<Animal> ();
		//TODO Make object finding better and less name dependent
		character = GameObject.FindGameObjectWithTag ("character");
		animal = GameObject.FindGameObjectWithTag ("animal");
		
		screenDimmer = GameObject.Find ("GUI - Level Dimmer");
		
		distanceDiffMin = 8f;
		currentDistanceDiff = Mathf.Abs (animal.transform.position.x - character.transform.position.x);
//		if (!PlayerPrefs.HasKey ("Tutorial")) {
//			PlayerPrefs.SetString ("Tutorial", "true");
//		}
//		tutEnabled = ((PlayerPrefs.GetString ("Tutorial").Equals ("true")) ? true : false);
	}
	
	void Update ()
	{
		currentDistanceDiff = Mathf.Abs (animal.transform.localPosition.x - character.transform.localPosition.x);
		if (levelStartWait) {
			StartCoroutine (wait (waitTime));
		}
		if (isPlaying) {
			if (animalControl.caught) {
				isPlaying = false;
				Debug.Log ("CAUGHT!");
				displayScore ();
				playerControl.rigidbody2D.velocity = new Vector2 (0f, 0f);
				character.GetComponent<Animator> ().SetTrigger ("Idle");
				animalControl.transform.parent.rigidbody2D.velocity = new Vector2 (0f, 0f);
			} else {
				if (currentDistanceDiff < distanceDiffMin) {
					playerControl.setSpeed (animalControl.speed);
					NetLauncher.launchEnabled = true;
				} else {
					NetLauncher.launchEnabled = false;
				}
			}
		}
	}
	
	private IEnumerator wait (float time)
	{
		levelStartWait = false;
		yield return new WaitForSeconds (time);
		playerControl.setSpeed ();
	}

	private void displayScore ()
	{
		int[] scores = scoreKeeper.getScore ();
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
		
	}
	
	private void unlockLevel ()
	{
		string levelName = "Level " + (levelNumber + 1);
		PlayerPrefs.SetString (levelName, "true");
	}

}


