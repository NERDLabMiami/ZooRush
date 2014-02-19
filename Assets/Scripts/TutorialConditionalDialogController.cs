using UnityEngine;
using System.Collections;

public class TutorialConditionalDialogController : MonoBehaviour
{
	private bool stopWatchExplained;
	private bool animalExplained;
	private bool crisisExplained;

	
	void Start ()
	{
//		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		stopWatchExplained = false;

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

	}

	private bool createDialog (string[] text)
	{
		if (GameStateMachine.currentState != (int)GameStateMachine.GameState.Paused) {
			GameObject DialogTrigger = new GameObject ("Dialog Trigger", typeof(DialogTrigger));
			DialogTrigger.GetComponent<DialogTrigger> ().textDisplay = text;
			return GameObject.FindObjectOfType<DialogHandler> ().forceDialog (DialogTrigger.GetComponent<DialogTrigger> ());
		}
		return false;
	}

	private string[] stopwatchText = {
		"Oh no! You've been",
		"+exposed to bad bacteria,",
		"+be sure to get to a doctor",
		"+before it turns into",
		"+a bad infection!"
	};

	private string[] crisisText = {
		"You're having a crisis!",
		"+If you're in a pinch, some",
		"+painkillers can help prevent",
		"+you from fainting!"
	};
}
