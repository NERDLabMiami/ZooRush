using UnityEngine;
using System.Collections;

/** Data and functions specific to Animal objects.
 * 
 * @author Ebtissam Wahman
 */ 
public class StoryAnimal : Animal, OtherButtonClass
{	
		public Sprite animalIcon; //Icon used in the distance meter
		
		void OnEnable ()
		{
				GameState.StateChanged += OnStateChanged;
		}
	
		void OnDisable ()
		{
				GameState.StateChanged -= OnStateChanged;
		}

		private void OnStateChanged ()
		{
				switch (GameState.currentState) {
				case GameState.States.Play:
						OnPauseToPlay ();
						break;
				case GameState.States.Intro:
						OnIntro ();
						break;
				case GameState.States.Launch:
						touchZone.enableButton ();
						return; //skip the rest of the function
				default:
						OnPause ();
						break;
				}
				touchZone.disableButton ();
		}

		private void OnIntro ()
		{
				setSpeed ();
		}

		private void OnPause ()
		{
				animator.SetTrigger ("Stop");
				transform.parent.rigidbody2D.velocity = Vector2.zero;
				if (audioSource) {
						if (audioSource.isPlaying) {
								audioSource.Pause ();
						}
				}
		}

		private void OnPauseToPlay ()
		{
				StartCoroutine (waitToResume (0.1f));
		}
	
		private void OnPlay ()
		{
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

				//assigns the pointer to the animator component
				caught = false; //default value for whether the animal has been caught
				transform.parent.rigidbody2D.velocity = Vector2.zero; //we set the initial velocity to 0
				GameObject.Find ("Animal Icon").GetComponent<SpriteRenderer> ().sprite = animalIcon; //Changes the icon in the distance meter
		}
	
		/**
	 * Sets the speed to the animal's standard running speed.
	 */ 
		public void setSpeed ()
		{
				animator.SetTrigger ("Go");
				transform.parent.rigidbody2D.velocity = speed; //assigns the rigidbody component the desired velocity
				if (Random.Range (0, 101) == 37) {//.01% chance per frame of moving the animal up or down
						Vector2 randomY = new Vector2 (0, ((Random.Range (0, 2) == 1) ? -1 : 1) * Random.Range (600, 751));
						transform.parent.rigidbody2D.AddForce (randomY);
				}
		}

		public override void getAway ()
		{
				Debug.Log ("GET AWAY CALLED");
				StartCoroutine (tempChangeToSpeed (speed.x + 3f));
		}

		private IEnumerator tempChangeToSpeed (float tempSpeed)
		{
				transform.parent.rigidbody2D.velocity = new Vector2 (tempSpeed, transform.parent.rigidbody2D.velocity.y);
				yield return new WaitForSeconds (1.15f);
				setSpeed ();
		}

		/** Resumes the default speed of the animal after a delay.
	* @param time the wait time before resetting the speed of the animal
	*/
	
		private IEnumerator waitToResume (float time)
		{
				yield return new WaitForSeconds (time);
				transform.parent.rigidbody2D.velocity = speed;
				animator.SetTrigger ("Go");
		}

		public void otherButtonAction (Button thisButton)
		{
				GameObject.FindObjectOfType<NetLauncher> ().throwNet ();
		}

}
