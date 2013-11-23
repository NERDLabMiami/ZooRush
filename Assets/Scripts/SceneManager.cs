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
	
	public static bool levelStartWait;
	
	void Start ()
	{
		levelStartWait = true;
		playerControl = GameObject.FindObjectOfType<PlayerControls> ();
		animalControl = GameObject.FindObjectOfType<Animal> ();
		//TODO Make object finding better and less name dependent
		character = GameObject.Find ("BoyZoo");
		animal = GameObject.Find ("Animal - Tortoise");
	}
	
	void Update ()
	{
		if (levelStartWait) {
			StartCoroutine (wait (waitTime));
		} else {
			if (animalControl.caught) {
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
			
			
		}
	}
	
	private IEnumerator wait (float time)
	{
		levelStartWait = false;
		yield return new WaitForSeconds (time);
		playerControl.setSpeed ();
	}
}


