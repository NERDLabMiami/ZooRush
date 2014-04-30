using UnityEngine;
using System.Collections;

public class EndlessAnimal : OtherButtonClass
{

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
	}

	public override void otherButtonAction (Button thisButton)
	{
		GameState.currentState = GameState.States.Launch;

		GameObject.FindObjectOfType<NetLauncher> ().throwNet ();
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
