using UnityEngine;
using System.Collections;

public class PainIndicator : MonoBehaviour
{
	public AudioClip clip;
	public float painRate;
	public float painPoints;

	private Sprite[] healthFaces;
	private SpriteRenderer sprite;
	private AudioController audioController;
	private SceneManager sceneManager;
	private Animator animator;
	
	void Start ()
	{
		audioController = GameObject.FindObjectOfType<AudioController> ();
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		animator = GetComponent<Animator> ();
		painPoints = 0f;
		painRate = 3.5f;
		healthFaces = GameOptions.FindObjectOfType<PlayerControls> ().faceIcons;
		sprite = GetComponent<SpriteRenderer> ();
		sprite.sprite = healthFaces [0];
	}

	void FixedUpdate ()
	{
		if (GameStateMachine.currentState == (int)GameStateMachine.GameState.Play) {
			painPoints += (Time.deltaTime * painRate);
		}
	}
	
	void Update ()
	{
		if (painPoints >= 100f) {
			painPoints = 100f;
			sceneManager.fainted = true;
			GameStateMachine.requestEndLevel ();
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
				audioController.objectInteraction (clip);
			}
		}
	}

	public void subtractPoints (int points)
	{
		painPoints -= points;
	}

	public void addPoints (int points)
	{
		painPoints += points;
	}

	public void objectInteraction (GameObject obj)
	{
		if (obj.name.Contains ("Doctor") || obj.name.Contains ("First Aid")) {
			painPoints = 0f;
		} 
//		scoreKeeper.addToCount (obj);
	}
}
