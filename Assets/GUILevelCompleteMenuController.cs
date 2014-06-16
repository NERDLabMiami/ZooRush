using UnityEngine;
using System.Collections;

public class GUILevelCompleteMenuController : MonoBehaviour
{
	public GUITimeObject timeDisplay;
	public GUIInfectionCount infectionDisplay;
	public GUIStarDisplay starDisplay;

	public void activate ()
	{
		StartCoroutine ("startSequence");
	}

	private IEnumerator startSequence ()
	{
		activateTime ();
		while (!timeDisplay.finished) {
			yield return new WaitForFixedUpdate ();
		}
		timeDisplay.gameObject.SetActive (false);

		activateInfection ();
		while (!infectionDisplay.finished) {
			yield return new WaitForFixedUpdate ();
		}
		infectionDisplay.gameObject.SetActive (false);

		activateStar ();

	}

	private void activateTime ()
	{
		timeDisplay.gameObject.SetActive (true);
		//TODO Make below property associate with Game Score Keeper
		timeDisplay.StartTimer (88);
	}

	private void activateInfection ()
	{
		infectionDisplay.gameObject.SetActive (true);
		//TODO Make below property associate with Game Score Keeper

		infectionDisplay.startInfectionDisplay (10, new int[]{1,4,5});
	}

	private void activateStar ()
	{
		starDisplay.gameObject.SetActive (true);
		//TODO Make below property associate with Game Score Keeper

		starDisplay.activateStars (2);
	}
}
