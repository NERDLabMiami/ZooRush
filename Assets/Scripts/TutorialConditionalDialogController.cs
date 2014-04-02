using UnityEngine;
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
		if (GameState.currentState != GameState.States.Dialog && GameState.currentState != GameState.States.Pause) {
			GameObject.FindObjectOfType<DialogBox> ().dialog = text;
			GameState.requestDialog ();
			return true;
		}
		return false;
	}

	private string[] stopwatchText = {
		"Look out! You just got\nan infection! Get to\nthe hospital before it\ngets worse!"
	};

	private string[] crisisText = {
		"You're about to have a\ncrisis!", 
		"Staying hydrated and\ntaking painkillers can\n help you manage your\ncondition."
	};

	private string[] animalText = {
		"When you're close to\nan animal, you can\nthrow a net by tapping\non it.",
		"Make sure you\nposition yourself\ncorrectly or you might\nend up missing!"
	};
}
