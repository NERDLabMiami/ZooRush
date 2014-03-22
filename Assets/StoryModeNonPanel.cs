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
