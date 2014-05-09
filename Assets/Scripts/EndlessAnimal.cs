using UnityEngine;
using System.Collections;

public class EndlessAnimal : OtherButtonClass
{
		public EndlessSceneManager sceneManager;
		public bool caught; //Indicator for whehter the Animal has been caught by the player
		public Vector2 speed; //Current speed of the animal object
		public AudioClip audioClip; // Animal audio sound clip
		public Button touchZone;
		public Animator animator; //Animator for the animal's running sprites
		private AudioSource audioSource; //Audio Source that plays sound clip

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
//			OnPauseToPlay ();
						break;
				case GameState.States.Intro:
//			OnIntro ();
						break;
				case GameState.States.Launch:
						touchZone.enableButton ();
						return; //skip the rest of the function
				default:
//			OnPause ();
						break;
				}
				touchZone.disableButton ();
		}

		void Start ()
		{
				if (PlayerPrefs.GetInt ("Sound", 1) != 0) { // sound is enabled, we will add a sound source for the animal
						audioSource = gameObject.AddComponent<AudioSource> (); //adding the sound source
						audioSource.playOnAwake = false; //disable playing on instatiation
						audioSource.clip = audioClip; 
				}
		
				//assigns the pointer to the animator component
				caught = false; //default value for whether the animal has been caught
				rigidbody2D.velocity = Vector2.zero; //we set the initial velocity to 0
				StartCoroutine (randomYMovementTimer ());
		}

		public override void otherButtonAction (Button thisButton)
		{
				GameState.currentState = GameState.States.Launch;

				GameObject.FindObjectOfType<NetLauncher> ().throwNet ();
		}

		// Update is called once per frame
		void FixedUpdate ()
		{
				if (caught) {
						Debug.Log ("CHANGING FROM CAUGHT TO UNCAUGHT");
						GameObject.FindObjectOfType<NetLauncher> ().resetNets ();
						sceneManager.addToAnimalsCaught ();
						sceneManager.introduceAnimal ();
						caught = false;
				}
		}

		private IEnumerator randomYMovementTimer ()
		{
//		Debug.Log ("Timer Called");

				float waitTime = Random.Range (0.5f, 1.5f);
				float timePassed = 0;
				while (timePassed < waitTime) {
						timePassed += Time.deltaTime;
						yield return new WaitForFixedUpdate ();
				}
				randomYVelocityChange ();
		}

		private void randomYVelocityChange ()
		{
				int yVelocity = Random.Range (0, 3);
				yVelocity *= (Random.Range (0, 2) == 0) ? 1 : -1;

				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, yVelocity);
//		Debug.Log ("Random Y Change Called for " + yVelocity + " speed.");

				resetSpeed ();

		}

		private void resetSpeed ()
		{
				if (!caught) {
//						Debug.Log ("Reset Speed Change Called");
        
						if (sceneManager && !sceneManager.failed) {
								StartCoroutine (randomYMovementTimer ());
						}
				}
				//else we break out of this loop MWAHAHAHA
		}

		public void getAway ()
		{
				Debug.Log ("GET AWAY CALLED");
				sceneManager.failed = true;
				rigidbody2D.isKinematic = true;
				rigidbody2D.velocity = new Vector2 (7, 0);
				StartCoroutine (sceneManager.callEndMenu ());
		}

}
