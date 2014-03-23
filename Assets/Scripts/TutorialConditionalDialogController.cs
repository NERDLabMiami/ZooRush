﻿using UnityEngine;
using System.Collections;

public class TutorialConditionalDialogController : MonoBehaviour
{
	private bool stopWatchExplained;
	private bool crisisExplained;
	private bool animalExplained;

	NetLauncher netLauncher;
	
	void Start ()
	{
		netLauncher = GameObject.FindObjectOfType<NetLauncher> ();
		if (Application.loadedLevelName.Contains ("Tutorial")) {
			stopWatchExplained = false;
			crisisExplained = false;
			animalExplained = false;
		} else {
			stopWatchExplained = crisisExplained = animalExplained = true;
		}
	}
	
	void Update ()
	{

		if (!stopWatchExplained) {
			if (GameObject.FindObjectOfType<StopwatchController> () != null) {
				stopWatchExplained = createDialog (stopwatchText);
			}
		}
		if (!crisisExplained) {
			if (GameObject.FindObjectOfType<PainIndicator> ().painPoints >= 75f) {
				crisisExplained = createDialog (crisisText);
			}
		}
		if (!animalExplained) {
			if (netLauncher.launchEnabled) {
				animalExplained = createDialog (animalText);
			}
		}

	}

	private bool createDialog (string[] text)
	{
		if (GameStateMachine.currentState != (int)GameStateMachine.GameState.Paused) {
			GameObject DialogTrigger = new GameObject ("Dialog Trigger", typeof(DialogTrigger));
			DialogTrigger.GetComponent<DialogTrigger> ().textDisplay = text;
			GameObject.FindObjectOfType<DialogHandler> ().forceDialog (DialogTrigger.GetComponent<DialogTrigger> ());
			return true;
		}
		return false;
	}

	private string[] stopwatchText = {
		"Look out! You just got",
		"+an infection! Get to",
		"+the hospital before it",
		"+gets worse!"
	};

	private string[] crisisText = {
		"You're about to have a",
		"+crisis! Staying",
		"+hydrated and taking",
		"painkillers can help",
		"+you manage your",
		"+condition."
	};

	private string[] animalText = {
		"When you're close to",
		"+an animal, you can",
		"+throw a net by tapping",
		"+on it. Make sure you",
		"+position yourself",
		"+correctly or you might",
		"+end up missing!"
	};
}
