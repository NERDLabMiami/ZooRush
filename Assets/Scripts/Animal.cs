using UnityEngine;
using System.Collections;

/** Data and functions specific to Animal objects.
 * 
 * @author Ebtissam Wahman
 */ 
public class Animal : MonoBehaviour
{

	public bool caught; //Indicator for whehter the Animal has been caught by the player
	public Vector2 speed; //Current speed of the animal object
	public Sprite animalIcon; //Icon used in the distance meter
	public AudioClip audioClip; // Animal audio sound clip

	private Animator animator; //Animator for the animal's running sprites
	private AudioSource audioSource; //Audio Source that plays sound clip
	private Rigidbody2D animalPhysics; //The rigid body component that controls our animal's physics

	void OnEnable ()
	{
		GameStateMachine.Intro += OnIntro;
		GameStateMachine.Paused += OnPause;
		GameStateMachine.PauseToPlay += OnPauseToPlay;
		GameStateMachine.Play += OnPlay;
	}
	
	
	void OnDisable ()
	{
		GameStateMachine.Intro -= OnIntro;
		GameStateMachine.Paused -= OnPause;
		GameStateMachine.PauseToPlay -= OnPauseToPlay;
		GameStateMachine.Play -= OnPlay;
	}

	private void OnIntro ()
	{
		setSpeed ();
	}

	private void OnPause ()
	{
		Debug.Log ("ON PAUSE CALLED");
		animator.SetTrigger ("Stop");
		animalPhysics.velocity = Vector2.zero;
		if (audioSource) {
			if (audioSource.isPlaying) {
				audioSource.Pause ();
			}
		}
	}

	private void OnPauseToPlay ()
	{
		Debug.Log ("ON PAUSE TO PLAY CALLED");

		StartCoroutine (waitToResume (0.1f));
	}
	
	private void OnPlay ()
	{
		Debug.Log ("ON PLAY CALLED");
		setSpeed ();
		if (audioSource) { //start audio playback
			if (!audioSource.isPlaying) {
				audioSource.Play ();
			}
		}
	}

	void Start ()
	{
		if (PlayerPrefs.GetInt ("Sound") != 0) { // sound is enabled, we will add a sound source for the animal
			audioSource = gameObject.AddComponent<AudioSource> (); //adding the sound source
			audioSource.playOnAwake = false; //disable playing on instatiation
			audioSource.clip = audioClip; 
		}

		animator = GetComponent<Animator> (); //assigns the pointer to the animator component
		caught = false; //default value for whether the animal has been caught
		animalPhysics = transform.parent.rigidbody2D; //setting the pointer to our rigid body physics controller
		animalPhysics.velocity = Vector2.zero; //we set the initial velocity to 0
		GameObject.Find ("Animal Icon").GetComponent<SpriteRenderer> ().sprite = animalIcon; //Changes the icon in the distance meter
	}
	
	/**
	 * Sets the speed to the animal's standard running speed.
	 */ 
	public void setSpeed ()
	{
		animator.SetTrigger ("Go");
		animalPhysics.velocity = speed; //assigns the rigidbody component the desired velocity
		if (Random.Range (0, 101) == 37) {//.01% chance per frame of moving the animal up or down
			Vector2 randomY = new Vector2 (0, ((Random.Range (0, 2) == 1) ? -1 : 1) * Random.Range (600, 751));
			animalPhysics.AddForce (randomY);
		}
	}

	public void getAway ()
	{
		Debug.Log ("GET AWAY CALLEd");
		StartCoroutine (tempChangeToSpeed (speed.x + 3f));
	}

	private IEnumerator tempChangeToSpeed (float tempSpeed)
	{
		animalPhysics.velocity = new Vector2 (tempSpeed, animalPhysics.velocity.y);
		yield return new WaitForSeconds (1.5f);
		setSpeed ();
	}

	/** Resumes the default speed of the animal after a delay.
	* @param time the wait time before resetting the speed of the animal
	*/
	
	private IEnumerator waitToResume (float time)
	{
		yield return new WaitForSeconds (time);
		animalPhysics.velocity = speed;
		GameStateMachine.requestPlay ();
	}

	public void touched ()
	{

	}

	public void rotate ()
	{
		StartCoroutine (rotateRoutine ());
		StartCoroutine (flash ());
	}

	private IEnumerator rotateRoutine ()
	{
		float velocity = 0;

		while (transform.localEulerAngles.z < 3) {
			float nextR = Mathf.SmoothDamp (transform.localEulerAngles.z, 3.1f, ref velocity, 0.2f);
			transform.eulerAngles = new Vector3 (0, 0, nextR);
			yield return new WaitForSeconds (0.1f);
		}

		while (transform.localEulerAngles.z > 0.05f) {
			float nextR = Mathf.SmoothDamp (transform.localEulerAngles.z, 0f, ref velocity, 0.2f);
			transform.eulerAngles = new Vector3 (0, 0, nextR);
			yield return new WaitForSeconds (0.1f);
		}

		while (transform.localEulerAngles.z < 3) {
			float nextR = Mathf.SmoothDamp (transform.localEulerAngles.z, 3.1f, ref velocity, 0.2f);
			transform.eulerAngles = new Vector3 (0, 0, nextR);
			yield return new WaitForSeconds (0.1f);
		}

		while (transform.localEulerAngles.z > 0.05f) {
			float nextR = Mathf.SmoothDamp (transform.localEulerAngles.z, 0f, ref velocity, 0.2f);
			transform.eulerAngles = new Vector3 (0, 0, nextR);
			yield return new WaitForSeconds (0.1f);
		}

		transform.eulerAngles = new Vector3 (0, 0, 0);

	}

	private IEnumerator flash ()
	{
		float velocity = 0;
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();

		while (sprite.color.a > 0.1f) {
			float newAlpha = Mathf.SmoothDamp (sprite.color.a, 0, ref velocity, 0.1f);
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
			yield return new WaitForSeconds (0.1f);
		}

		while (sprite.color.a < 0.9f) {
			float newAlpha = Mathf.SmoothDamp (sprite.color.a, 1, ref velocity, 0.1f);
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
			yield return new WaitForSeconds (0.1f);
		}

		while (sprite.color.a > 0.1f) {
			float newAlpha = Mathf.SmoothDamp (sprite.color.a, 0, ref velocity, 0.1f);
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
			yield return new WaitForSeconds (0.1f);
		}

		while (sprite.color.a < 0.9f) {
			float newAlpha = Mathf.SmoothDamp (sprite.color.a, 1, ref velocity, 0.1f);
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
			yield return new WaitForSeconds (0.1f);
		}
		while (sprite.color.a > 0.1f) {
			float newAlpha = Mathf.SmoothDamp (sprite.color.a, 0, ref velocity, 0.1f);
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
			yield return new WaitForSeconds (0.1f);
		}
		
		while (sprite.color.a < 0.9f) {
			float newAlpha = Mathf.SmoothDamp (sprite.color.a, 1, ref velocity, 0.1f);
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
			yield return new WaitForSeconds (0.1f);
		}
		while (sprite.color.a > 0.1f) {
			float newAlpha = Mathf.SmoothDamp (sprite.color.a, 0, ref velocity, 0.1f);
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
			yield return new WaitForSeconds (0.1f);
		}
		
		while (sprite.color.a < 0.9f) {
			float newAlpha = Mathf.SmoothDamp (sprite.color.a, 1, ref velocity, 0.1f);
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
			yield return new WaitForSeconds (0.1f);
		}

		sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, 1);

	}
	
}
