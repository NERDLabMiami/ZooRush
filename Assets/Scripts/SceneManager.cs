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
				int[] scores = scoreKeeper.getScore ();
				foreach (int score in scores) {
					Debug.Log (score);
				}
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
}


