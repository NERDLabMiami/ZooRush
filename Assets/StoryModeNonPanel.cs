using UnityEngine;
using System.Collections;

public class StoryModeNonPanel : MonoBehaviour
{

	private CharacterSpeech speech;
	private Character character;
	
	// Use this for initialization
	void Start ()
	{
		GameStateMachine.currentState = (int)GameStateMachine.GameState.PauseToPlay;
		character = GameObject.FindObjectOfType<Character> ();
		character.gameObject.rigidbody2D.velocity = new Vector2 (8f, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameStateMachine.currentState == (int)GameStateMachine.GameState.PauseToPlay) {
			GameStateMachine.requestPlay ();
			GameObject.FindObjectOfType<CameraFollow> ().moveCameraToCharacterOffset (5f);
		}
	}
}
