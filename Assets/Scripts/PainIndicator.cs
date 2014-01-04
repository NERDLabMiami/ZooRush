using UnityEngine;
using System.Collections;

public class PainIndicator : MonoBehaviour
{

	public float painRate;
	public float painPoints;

	private Sprite[] healthFaces;
	private SpriteRenderer sprite;
	private ScoreKeeper scoreKeeper;
	private SceneManager sceneManager;
	private AudioHandler audioHandler;
	private Animator animator;
	
	void Start ()
	{
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		audioHandler = GameObject.FindObjectOfType<AudioHandler> ();
		animator = GetComponent<Animator> ();
		painPoints = 0f;
		painRate = 3.5f;
		healthFaces = GameOptions.FindObjectOfType<PlayerControls> ().faceIcons;
		sprite = GetComponent<SpriteRenderer> ();
		sprite.sprite = healthFaces [0];
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
		animator.SetFloat ("PainPoints", painPoints);
		animator.SetBool ("Playing", sceneManager.isPlaying);
		if (painPoints < 33f) { // Change to normal face
			if (!sprite.sprite.Equals (healthFaces [0])) {
				sprite.sprite = healthFaces [0];
			}
		}
		if (painPoints > 33f && painPoints < 75f) { // Change to discomfort face
			if (!sprite.sprite.Equals (healthFaces [1])) {
				sprite.sprite = healthFaces [1];		
			}	
		}
		if (painPoints > 75f) { // Change to pain face
			if (!sprite.sprite.Equals (healthFaces [2])) {
				sprite.sprite = healthFaces [2];
			}
			if (sceneManager.isPlaying) {
				audioHandler.playSound ("HARDSICKLOOP");
			}
		}
	}

	public void objectInteraction (GameObject obj)
	{
		if (obj.name.Contains ("Doctor") || obj.name.Contains ("First Aid")) {
			painPoints = 0f;
		} else {
			
			if (obj.name.Contains ("Infection")) {
				if (obj.name.Contains ("Red")) {
					painPoints += 35f;
					
				} else {
					if (obj.name.Contains ("Yellow")) {
						painPoints += 20f;
						//player.decrementSpeed (0.15f * player.maxSpeed.x); // Slows character down by 15% of normal speed
					} else { // infection is green
						painPoints += 5f;
						//player.decrementSpeed (0.05f * player.maxSpeed.x); // Slows character down by 5% of normal speed
					}
				}
				
			} else {
				if (obj.name.Contains ("Water Bottle")) {
					painPoints -= 25f;
				} else {
					if (obj.name.Contains ("Pill") && !scoreKeeper.pillBottleUsed ()) {
						audioHandler.playSound ("PILL");
						obj.GetComponent<Animator> ().SetTrigger ("Open");
						obj.GetComponent<SpriteRenderer> ().color = Color.gray;
						scoreKeeper.addToCount (obj);
						painPoints -= 75f;
					} else {
						Debug.Log ("ERROR - Only one Pill Bottle Per Level");
					}
					if (obj.name.Contains ("Lawnmower")) {
						painPoints += 40f;
					}
				}
			}
		}
		scoreKeeper.addToCount (obj);
	}
}
