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

	public delegate void StateBasedAction ();
	public static event StateBasedAction StartLevel;
	public static event StateBasedAction Intro;
	public static event StateBasedAction Play;
	public static event StateBasedAction Paused;
	public static event StateBasedAction PauseToPlay;
	public static event StateBasedAction Transition;
	public static event StateBasedAction EndLevel;
	
	void Start ()
	{
		currentState = (int)GameState.StartLevel;
		if (StartLevel != null) {
			StartLevel ();
		}
	}

	public static void requestIntro ()
	{
		if (currentState == (int)GameState.StartLevel) {
			currentState = (int)GameState.Intro;
			if (Intro != null) {
				Intro ();
			}
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
					if (PauseToPlay != null) {
						PauseToPlay ();
					}
				} else {
					currentState = (int)GameState.Play;
					if (Play != null) {

						Play ();
					}
					GameObject.FindObjectOfType<CameraFollow> ().cameraFollowEnabled = true;
				}
			}
		}
	}

	public static void requestPause ()
	{
		if (currentState != (int)GameState.EndLevel && currentState != (int)GameState.Transition) {
			currentState = (int)GameState.Paused;
			if (Paused != null) {
				Paused ();
			}
		}
	}

	public static void requestTransition ()
	{
		if (currentState == (int)GameState.Play || currentState == (int)GameState.Paused) {
			currentState = (int)GameState.Transition;
			if (Transition != null) {
				Transition ();
			}
		}
	}

	public static void requestEndLevel ()
	{
		if (currentState == (int)GameState.Transition) {
			currentState = (int)GameState.EndLevel;
			if (EndLevel != null) {
				EndLevel ();
			}
		} else {
			currentState = (int)GameState.Transition;
			if (Transition != null) {
				Transition ();
			}
		}
	}

	public static void resetState ()
	{
		currentState = (int)GameState.StartLevel;
	}
}
