using UnityEngine;
using System.Collections;

public class StoryModeNonPanel : TouchHandler
{

	public CharacterSpeech speech;
	public Character character;
	public GameObject animalPrefab;
	private GameObject animal;
	private int speechIndex;
	private bool clicked;
	private string[] currentDialog;
	private bool readyForAnimal, readyToFadeOut, readyToStart;
	public SpriteRenderer dimScreen;
	public SpriteRenderer zooKeeper;
//	public GameObject SuburbsBG;
//	public GameObject ZooBG;
	public GameObject speechBubble;
	
	// Use this for initialization
	void Start ()
	{
		GameStateMachine.currentState = (int)GameStateMachine.GameState.PauseToPlay;
		character.gameObject.rigidbody2D.velocity = new Vector2 (8f, 0);
		speechIndex = 0;
		clicked = false;
		//assign current scene's dialog
		currentDialog = dialog1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameStateMachine.currentState == (int)GameStateMachine.GameState.PauseToPlay) {
			GameStateMachine.requestPlay ();
			GameObject.FindObjectOfType<CameraFollow> ().moveCameraToCharacterOffset (5f);
			speech.SpeechBubbleDisplay (currentDialog [speechIndex]);
			readyForAnimal = true;
		}
		if (animal != null) {
			if (animal.transform.position.x < Camera.main.transform.position.x && !inView (animal.renderer)) {
				Destroy (animal);
				speech.SpeechBubbleDisplay ("Uh oh... This doesn't look good...");
				readyToFadeOut = true;
				StartCoroutine (waitToResetTouch ());
			}
		}
	}

	private void nextSpeech ()
	{
		clicked = true;
		if (++speechIndex < currentDialog.Length) {
			speech.SpeechBubbleDisplay (currentDialog [speechIndex]);
			StartCoroutine (waitToResetTouch ());
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
			readyForAnimal = false;
			animal = Instantiate (animalPrefab, new Vector3 (transform.position.x + 20f, transform.position.y - 2.5f, transform.position.z), Quaternion.identity) as GameObject;
			animal.rigidbody2D.velocity = new Vector2 (-6, 0);
		}
		if (readyToFadeOut) {
			speech.hide ();
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
		while (dimScreen.color != Color.white) {
			dimScreen.color = Color.Lerp (dimScreen.color, Color.white, 1f * Time.deltaTime);
			yield return new WaitForFixedUpdate ();
		}

		Camera.main.transform.localPosition = new Vector3 (Camera.main.transform.localPosition.x, -57.25f, Camera.main.transform.localPosition.z);
		character.transform.localPosition = new Vector3 (character.transform.localPosition.x, -60.5f, character.transform.localPosition.z);

//		Instantiate (ZooBG, new Vector3 (SuburbsBG.transform.position.x, 2.3f, 0), Quaternion.identity);
//		Destroy (SuburbsBG);
//		GameObject.FindObjectOfType<SceneRepeater> ().recalculate ();

		while (dimScreen.color != Color.clear) {
			dimScreen.color = Color.Lerp (dimScreen.color, Color.clear, 1f * Time.deltaTime);
			yield return new WaitForFixedUpdate ();
		}

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
		StartCoroutine (waitToResetTouch ());
		readyToStart = true;
	}

	string ZooKeeperDialog = "Help! Help! All the animals have\nescaped their pens. They’re getting\naway!";

	public override void objectTouched ()
	{
		if (!clicked && Input.GetMouseButtonUp (0)) {
			nextSpeech ();
		}
	}
	public override void objectUntouched ()
	{

	}
	protected override IEnumerator waitToResetTouch ()
	{
		yield return new WaitForSeconds (0.5f);
		clicked = false;
	}
}
