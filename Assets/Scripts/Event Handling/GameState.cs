using UnityEngine;
using System;
using System.Collections;

public static class GameState : System.Object
{

	public static States currentState;

	public enum States
	{
		Start,
		Intro,
		Play,
		Dialog,
		Launch,
		Transition,
		Win,
		Lose
	}
	;

	public delegate void StateChange ();
	public static event StateChange StateChanged;
	public static event StateChange dialogCalled;
	public static event StateChange dialogDismissed;
	public static event StateChange launchCalled;
	public static event StateChange launchDismissed;

	private static void callStateChanged ()
	{
		if (StateChanged != null) {
			StateChanged ();
		}
	}

	private static void callDialog ()
	{
		if (dialogCalled != null) {
			dialogCalled ();
		}
	}

	private static void callDialogDismissed ()
	{
		if (dialogDismissed != null) {
			dialogDismissed ();
		}
	}

	private static void callLaunch ()
	{
		if (launchCalled != null) {
			launchCalled ();
		}
	}
	
	private static void callLaunchDismissed ()
	{
		if (launchDismissed != null) {
			launchDismissed ();
		}
	}

	public static bool requestStart ()
	{
		currentState = States.Start;
		callStateChanged ();
		return true;
	} 

	public static bool requestIntro ()
	{
		if (currentState == States.Start) {
			currentState = States.Intro;
			callStateChanged ();
			return true;
		} 

		Debug.Log ("Incorrect Request in State Flow");
		return false;
	}

	public static bool requestPlay ()
	{
		switch (currentState) {
		case States.Dialog:
			callDialogDismissed ();
			break;
		case States.Intro:
			break;
		case States.Launch:
			callLaunchDismissed ();
			break;
		default:
			Debug.Log ("Incorrect Request in State Flow");
			return false;
		}
		currentState = States.Play;
		callStateChanged ();
		return true;
	}

	public static bool requestDialog ()
	{
		if (currentState == States.Play) {
			currentState = States.Dialog;
			callStateChanged ();
			callDialog ();
			return true;
		} 
		Debug.Log ("Incorrect Request in State Flow");
		return false;
	}

	public static bool requestLaunch ()
	{
		if (currentState == States.Play) {
			currentState = States.Launch;
			callStateChanged ();
			callLaunch ();
			return true;
		} 
		Debug.Log ("Incorrect Request in State Flow");
		return false;
	}

	public static bool requestTransition ()
	{
		if (currentState == States.Play || currentState == States.Launch) {
			currentState = States.Transition;
			callStateChanged ();
			return true;
		} 
		Debug.Log ("Incorrect Request in State Flow");
		return false;
	}

	public static bool requestWin ()
	{
		if (currentState == States.Transition) {
			currentState = States.Win;
			callStateChanged ();
			return true;
		} 
		Debug.Log ("Incorrect Request in State Flow");
		return false;
	}

	public static bool requestLose ()
	{
		if (currentState == States.Transition) {
			currentState = States.Lose;
			callStateChanged ();
			return true;
		} 
		Debug.Log ("Incorrect Request in State Flow");
		return false;
	}

}
