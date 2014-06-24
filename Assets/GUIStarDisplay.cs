using UnityEngine;
using System.Collections;

public class GUIStarDisplay : MonoBehaviour
{
		public Animator[] starAnimators;
		public bool finished = false;
		public AudioModel audioPlayer;
		private int starCount;
		
		public void activateStars (int stars)
		{
				starCount = stars;
				StartCoroutine ("launchStars");
		}

		private IEnumerator launchStars ()
		{
				int starIndex = 0;
				while (starCount >0) {
						starAnimators [starIndex].SetTrigger ("Activate");
						if (AudioModel.sound) {
								audio.Play ();
						}
						starIndex++;
						starCount--;
						yield return new WaitForSeconds (0.5f);
				}
				StartCoroutine ("declareFinished");
		}
		
		private IEnumerator declareFinished ()
		{
				yield return new WaitForSeconds (0.15f);
				finished = true;
		}
}
