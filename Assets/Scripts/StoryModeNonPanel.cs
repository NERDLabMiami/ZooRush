using UnityEngine;
using System.Collections;

public class StoryModeNonPanel : OtherButtonClass
{

	public CharacterSpeech speech;
	public Character character;
	public GameObject animalPrefab;
	private GameObject animal;
	private int speechIndex;
	private string[] currentDialog;
	private bool readyForAnimal, readyToFadeOut, readyToStart;
	public SpriteRenderer dimScreen;
	public SpriteRenderer zooKeeper;
	public GameObject speechBubble;
	public Button continueButton;
	
	void Start ()
	{
		GameStateMachine.currentState = (int)GameStateMachine.GameState.PauseToPlay;
		character.gameObject.rigidbody2D.velocity = new Vector2 (8f, 0);
		speechIndex = 0;
		//assign current scene's dialog
		currentDialog = dialog1;
	}
	
	void Update ()
	{
		if (GameStateMachine.currentState == (int)GameStateMachine.GameState.PauseToPlay) {
			GameStateMachine.requestPlay ();
			CameraFollow cameraFollower = GameObject.FindObjectOfType<CameraFollow> ();
			cameraFollower.characterOffset = 5;
			cameraFollower.cameraFollowEnabled = true;
			cameraFollower.cameraSettled = true;
			speech.SpeechBubbleDisplay (currentDialog [speechIndex]);
			readyForAnimal = true;
		}
		if (animal != null) {
			if (animal.transform.position.x < Camera.main.transform.position.x && !inView (animal.renderer)) {
				Destroy (animal);
				speech.SpeechBubbleDisplay ("Uh oh... This doesn't look good...");
				continueButton.enableButton ();
				readyToFadeOut = true;
			}
		}
	}

	private void nextSpeech ()
	{
		if (++speechIndex < currentDialog.Length) {
			speech.SpeechBubbleDisplay (currentDialog [speechIndex]);
		} else {
			nextAction ();
		}
	}

	string[] dialog1 = {
		"Today is a big day. I get to start my\ndream job working in a zoo!",
		"I may have struggled all my life with\n" +
		"sickle-cell anemia.", 
		"But by paying attention to my body, I\n" +
		"have learned to live with it...",
		"And I certainly won't let it slow me\n" +
		"down today!\n"
	};

	private void nextAction ()
	{
		if (readyForAnimal) {
			speech.hide ();
			continueButton.disableButton ();
			readyForAnimal = false;
			animal = Instantiate (animalPrefab, new Vector3 (transform.position.x + 20f, transform.position.y - 2.5f, transform.position.z), Quaternion.identity) as GameObject;
			animal.rigidbody2D.velocity = new Vector2 (-6, 0);
		}
		if (readyToFadeOut) {
			speech.hide ();
			continueButton.disableButton ();
			StartCoroutine (fadeOut ());
			readyToFadeOut = false;
		}
		if (readyToStart) {
			PlayerPrefs.SetInt ("Level1-TutorialStory", 1); //Story has been seen, no need to replay
			NextSceneHandler.nextLevel ("Level1-Tutorial");
		}
	}

	private bool inView (Renderer obj)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
	}

	private IEnumerator fadeOut ()
	{
		while (dimScreen.color.a < 0.97f) {
			dimScreen.color = Color.Lerp (dimScreen.color, Color.white, 1f * Time.deltaTime);
			yield return new WaitForFixedUpdate ();
		}

		Camera.main.transform.localPosition = new Vector3 (Camera.main.transform.localPosition.x, -57.25f, Camera.main.transform.localPosition.z);
		character.transform.localPosition = new Vector3 (character.transform.localPosition.x, -60.5f, character.transform.localPosition.z);

		while (dimScreen.color.a > 0.1f) {
			dimScreen.color = Color.Lerp (dimScreen.color, Color.clear, 1f * Time.deltaTime);
			yield return new WaitForFixedUpdate ();
		}
		dimScreen.color = Color.clear;

		cueZooKeeper ();
	}

	private void cueZooKeeper ()
	{
		zooKeeper.color = Color.white;
		GameStateMachine.currentState = (int)GameStateMachine.GameState.Paused;
		character.gameObject.rigidbody2D.velocity = Vector2.zero;
		Destroy (character.gameObject.GetComponent<Animator> ());
		speechBubble.transform.localScale = new Vector3 (-speechBubble.transform.localScale.x, speechBubble.transform.localScale.y, speechBubble.transform.localScale.z);
		speechBubble.transform.localPosition = new Vector3 (13, 4, 0);

		GameObject textObject = speechBubble.GetComponentInChildren<TextMesh> ().gameObject;
		textObject.transform.localScale = new Vector3 (-textObject.transform.localScale.x, textObject.transform.localScale.y, textObject.transform.localScale.z);
		speech.SpeechBubbleDisplay (ZooKeeperDialog);
		continueButton.enableButton ();
		readyToStart = true;
	}

	string ZooKeeperDialog = "Help! Help! All the animals have\nescaped their pens. They’re getting\naway!";

	public override void otherButtonAction (Button thisButton)
	{
		nextSpeech ();
	}
}
