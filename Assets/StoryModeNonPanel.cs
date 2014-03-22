using UnityEngine;
using System.Collections;

public class StoryModeNonPanel : TouchHandler
{

	public CharacterSpeech speech;
	public Character character;
	private int speechIndex;
	private bool clicked;
	private string[] currentDialog;
	public BackgroundCharacterController bgCharCreator;
	
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
		}

	}

	private void nextSpeech ()
	{
		if (++speechIndex < currentDialog.Length) {
			speech.SpeechBubbleDisplay (currentDialog [speechIndex]);
		} else {
			Debug.Log ("END OF DIALOG");
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
		if (currentDialog == dialog1) {
			clicked = true;
			Debug.Log ("CUE ANIMAL APPEARANCE");
			bgCharCreator.createInstance ();
		}
	}

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
		yield return new WaitForSeconds (0.2f);
		clicked = false;
	}
}
