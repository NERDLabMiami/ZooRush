using UnityEngine;
using System.Collections;


/** Controller for the character Pain Bar.
 * @author: Ebtissam Wahman
 */ 
public class PainBar : MonoBehaviour
{
	public float painRate; // using 3.5f atm
	public float painPoints;
	public float maxPainBarSize;
	public Sprite[] healthStates;

	private ScoreKeeper scoreKeeper;
	private SceneManager sceneManager;
	private AudioHandler audioHandler;
	
	void Start ()
	{
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		audioHandler = GameObject.FindObjectOfType<AudioHandler> ();
		transform.localScale = new Vector3 (0f, transform.localScale.y, transform.localScale.z);
		painPoints = 0f;
		maxPainBarSize = 0.32f;
		if (painRate <= 0) {
			painRate = 3.5f;
		}
	}
	
	void FixedUpdate ()
	{
		if (sceneManager.isPlaying && !sceneManager.levelStartWait) {
			painPoints += (Time.deltaTime * painRate);
		}
	}
	
	void Update ()
	{
		if (painPoints >= 100f) {
			painPoints = 100f;
			sceneManager.fainted = true;
		}
		if (painPoints < 0) {
			painPoints = 0;
		}

		GetComponent<Animator> ().SetFloat ("Pain", painPoints);
		
		if (painPoints <= 100f) { // Change health bar sized based on current pain points
			transform.localScale = new Vector3 ((painPoints / 100f) * maxPainBarSize, 
			                                    transform.localScale.y, 
			                                    transform.localScale.z);
		}
		if (painPoints < 33f) { // Change health to Green
			GetComponent<SpriteRenderer> ().sprite = healthStates [0];
		}
		if (painPoints > 33f && painPoints < 75f) { // Change health to Yellow
			GetComponent<SpriteRenderer> ().sprite = healthStates [1];
		}
		if (painPoints > 75f) { // Change health to Red
			GetComponent<SpriteRenderer> ().sprite = healthStates [2];
			audioHandler.playSound ("HARDSICKLOOP");
		}
	}

	public void objectInteraction (GameObject obj)
	{
		if (obj.name.Contains ("Doctor") || obj.name.Contains ("First Aid")) {
			painPoints = 0f;
		} else {
		
			if (obj.name.Contains ("Infection")) {
				PlayerControls player = GameObject.FindObjectOfType<PlayerControls> ();
				if (obj.name.Contains ("Red")) {
					painPoints += 35f;

				} else {
					if (obj.name.Contains ("Yellow")) {
						painPoints += 20f;
						player.decrementSpeed (0.15f * player.maxSpeed.x); // Slows character down by 15% of normal speed
					} else { // infection is green
						painPoints += 5f;
						player.decrementSpeed (0.05f * player.maxSpeed.x); // Slows character down by 5% of normal speed
					}
				}

			} else {
				if (obj.name.Contains ("Power Up")) {
					if (obj.name.Contains ("Water Bottle")) {
						painPoints -= 25f;
					} else {
						if (!scoreKeeper.pillBottleUsed ()) {
							painPoints -= 75f;
						} else {
							Debug.Log ("ERROR - Only one Pill Bottle Per Level");
						}

					}
				} else {
					if (obj.name.Contains ("Lawnmower")) {
						painPoints += 40f;
					}
				}
			}
		}
		scoreKeeper.addToCount (obj);
	}
}
