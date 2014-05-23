using UnityEngine;
using System.Collections;

public class EndlessAnimal : OtherButtonClass
{
		public EndlessSceneManager sceneManager;
		public bool caught; //Indicator for whehter the Animal has been caught by the player
		public bool stopAllCoroutines;
		public Vector2 speed; //Current speed of the animal object
		public AudioClip audioClip; // Animal audio sound clip
		public Button touchZone;
		public Animator animator; //Animator for the animal's running sprites
		private AudioSource audioSource; //Audio Source that plays sound clip
		public Sprite[] animalBadges;
		public SpriteRenderer currentAnimalBadge;
		public Transform counterLocation;
		public SpriteRenderer[] renderers;

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
				stopAllCoroutines = false;
				rigidbody2D.velocity = Vector2.zero; //we set the initial velocity to 0
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
						stopAllCoroutines = true;
						Debug.Log ("CHANGING FROM CAUGHT TO UNCAUGHT");
						GameObject.FindObjectOfType<NetLauncher> ().resetNets ();
						//TODO: Add Animationg that changes animal into badge and adds it to the count
						StartCoroutine (transformIntoBadge ());
						//END OF TODO
						caught = false;
				}
		}
		
		private void disappear ()
		{
				foreach (SpriteRenderer rend in renderers) {
						rend.enabled = false;
				}
		}

		private void appear ()
		{
				foreach (SpriteRenderer rend in renderers) {
						rend.enabled = true;
				}
		}

		public void reset ()
		{
				currentAnimalBadge.enabled = false;
				currentAnimalBadge.transform.localPosition = new Vector3 (0, 1, 0);
				appear ();
		}
		
		private IEnumerator transformIntoBadge ()
		{
				Vector3 destinationSize = new Vector3 (0.25f, 0.25f, 1);
				Vector3 velocity = Vector3.zero;
				while (transform.localScale.x > .26f) {
						transform.localScale = Vector3.SmoothDamp (transform.localScale, destinationSize, ref velocity, 0.15f);
						yield return new WaitForFixedUpdate ();
				}
				currentAnimalBadge.sprite = animalBadges [(int)sceneManager.currentAnimal];
				currentAnimalBadge.enabled = true;
				disappear ();

				StartCoroutine (moveBadgeToCounter ());
		}

		private IEnumerator moveBadgeToCounter ()
		{
				Vector3 velocity = Vector3.zero;

				while (currentAnimalBadge.transform.position.y < (counterLocation.position.y - 0.01f)) {
						Debug.Log ("WAITING TO MOVE BADGE TO COUNTER");
						currentAnimalBadge.transform.position = Vector3.SmoothDamp (currentAnimalBadge.transform.position, counterLocation.position, ref velocity, 0.15f);
						yield return new WaitForFixedUpdate ();
				}
				
				sceneManager.addToAnimalsCaught ();
				currentAnimalBadge.enabled = false;
				stopAllCoroutines = false;
				sceneManager.introduceAnimal ();
		}
	
		
		private IEnumerator randomYMovementTimer ()
		{
//		Debug.Log ("Timer Called");

				float waitTime = Random.Range (0f, 0.5f);
				float timePassed = 0;
				while (timePassed < waitTime) {
						timePassed += Time.deltaTime;
						yield return new WaitForFixedUpdate ();
				}
				randomYVelocityChange ();
		}

		public void randomYVelocityChange ()
		{
				int yVelocity = Random.Range (0, 3);
				yVelocity *= (Random.Range (0, 2) == 0) ? 1 : -1;

				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, yVelocity);
//		Debug.Log ("Random Y Change Called for " + yVelocity + " speed.");

				resetSpeedY ();

		}

		private void resetSpeedY ()
		{
				if (!caught && !stopAllCoroutines) {
//						Debug.Log ("Reset Speed Change Called");
						if (sceneManager && !sceneManager.failed) {
								StartCoroutine (randomYMovementTimer ());
						}
				}
				//else we break out of this loop MWAHAHAHA
		}

		private IEnumerator randomXMovementTimer ()
		{
				//		Debug.Log ("Timer Called");
		
				float waitTime = Random.Range (0f, 0.5f);
				float timePassed = 0;
				while (timePassed < waitTime) {
						timePassed += Time.deltaTime;
						yield return new WaitForFixedUpdate ();
				}
				randomXVelocityChange ();
		}
	
		public void randomXVelocityChange ()
		{
				if (sceneManager && !sceneManager.failed) {
						float xVelocity = Random.Range (0, 1.1f);
		
						rigidbody2D.velocity = new Vector2 (xVelocity, rigidbody2D.velocity.y);
						//		Debug.Log ("Random Y Change Called for " + yVelocity + " speed.");
		
						resetSpeedX ();
				}
		}
	
		private void resetSpeedX ()
		{
				if (!caught && !stopAllCoroutines) {
						//						Debug.Log ("Reset Speed Change Called");
						if (sceneManager && !sceneManager.failed) {
								StartCoroutine (randomXMovementTimer ());
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
