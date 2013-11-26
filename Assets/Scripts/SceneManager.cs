using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
	public float waitTime;
	public int levelNumber;
	
	private PlayerControls playerControl;
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
		animalControl = GameObject.FindObjectOfType<Animal> ();
		//TODO Make object finding better and less name dependent
		character = GameObject.Find ("BoyZoo");
		animal = GameObject.Find ("Animal - Tortoise");
//		if (!PlayerPrefs.HasKey ("Tutorial")) {
//			PlayerPrefs.SetString ("Tutorial", "true");
//		}
//		tutEnabled = ((PlayerPrefs.GetString ("Tutorial").Equals ("true")) ? true : false);
	}
	
	void Update ()
	{
		if (levelStartWait) {
			StartCoroutine (wait (waitTime));
		}
		if (isPlaying) {
//			Time.timeScale = 1f;
			if (animalControl.caught) {
				isPlaying = false;
				Debug.Log ("CAUGHT!");
				playerControl.rigidbody2D.velocity = new Vector2 (0f, 0f);
				character.GetComponent<Animator> ().SetTrigger ("Idle");
				animalControl.transform.parent.rigidbody2D.velocity = new Vector2 (0f, 0f);
			} else {
				if (Mathf.Abs (animal.transform.position.x - character.transform.position.x) < 8f) {
					playerControl.setSpeed (animalControl.speed);
					NetLauncher.launchEnabled = true;
				} else {
					NetLauncher.launchEnabled = false;
				}
			}
		} //else {
//			Time.timeScale = 0f;
//		}
	}
	
	private IEnumerator wait (float time)
	{
		levelStartWait = false;
		yield return new WaitForSeconds (time);
		playerControl.setSpeed ();
	}
}


