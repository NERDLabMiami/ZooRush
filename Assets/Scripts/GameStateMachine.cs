using UnityEngine;
using System.Collections;

/**
 * Controls the Game's current state
 */ 
public class GameStateMachine : MonoBehaviour
{

	enum GameState
	{
		Intro,
		Play,
		Paused,
		Transition,
		EndLevel}
	;

	static int currentState;

	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public static void RequestPlay(){

	}

	public static void RequestPause(){

	}

	public static void RequestAnimationTransition(){

	}

	public static void Request
}
