using UnityEngine;
using System.Collections;

public class GUILevelCompleteMenuController : MonoBehaviour
{
		public GUITimeObject timeDisplay;
		public GUIInfectionCount infectionDisplay;
		public GUIStarDisplay starDisplay;
		public ScoreKeeper scoreKeeper;
		private int[] score;

		public void activate ()
		{
				score = scoreKeeper.getScore ();
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
				timeDisplay.StartTimer (score [5]);
		}

		private void activateInfection ()
		{
				infectionDisplay.gameObject.SetActive (true);
				int totalInfections = score [0] + score [1] + score [2];
				infectionDisplay.startInfectionDisplay (totalInfections, new int[]{score [0],score [1],score [2]});
		}

		private void activateStar ()
		{
				starDisplay.gameObject.SetActive (true);
				starDisplay.activateStars (score [6]);
		}
}
