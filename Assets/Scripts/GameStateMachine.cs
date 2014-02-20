using UnityEngine;
using System.Collections;

/**
 * Controls the Game's current state
 */ 
public class GameStateMachine : MonoBehaviour
{

	public enum GameState
	{
		StartLevel,
		Intro,
		Play,
		Paused,
		PauseToPlay,
		Transition,
		EndLevel}
	;

	public static int currentState;

	void Start ()
	{
		currentState = (int)GameState.StartLevel;
	}

	public static void requestIntro ()
	{
		if (currentState == (int)GameState.StartLevel) {
			currentState = (int)GameState.Intro;
		}
	}

	/**
	 * Receives request to change the gameplay state to "In Play"
	 */ 
	public static void requestPlay ()
	{
		if (currentState != (int)GameState.Play) {
			if (currentState != (int)GameState.EndLevel && currentState != (int)GameState.Transition) {
				if (currentState != (int)GameState.PauseToPlay) {
					currentState = (int)GameState.PauseToPlay;
				} else {
					currentState = (int)GameState.Play;
					GameObject.FindObjectOfType<CameraFollow> ().cameraFollowEnabled = true;
				}
			}
		}
	}

	public static void requestPause ()
	{
		if (currentState != (int)GameState.EndLevel && currentState != (int)GameState.Transition) {
			currentState = (int)GameState.Paused;
		}
	}

	public static void requestTransition ()
	{
		if (currentState == (int)GameState.Play || currentState == (int)GameState.Paused) {
			currentState = (int)GameState.Transition;
		}
	}

	public static void requestEndLevel ()
	{
		if (currentState == (int)GameState.Transition) {
			currentState = (int)GameState.EndLevel;
		} else {
			currentState = (int)GameState.Transition;
		}
	}

	public static void resetState ()
	{
		currentState = (int)GameState.StartLevel;
	}
}
