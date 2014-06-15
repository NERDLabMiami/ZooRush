using UnityEngine;
using System.Collections;

public class GUIStarDisplay : MonoBehaviour
{

		public Animator[] starAnimators;
		public bool finished;

		void Start ()
		{
//				activateStars (2);
		}
		
		public void activateStars (int stars)
		{
				for (int i = 0; i < stars; i++) {
						starAnimators [i].SetTrigger ("Activate");
				}
				StartCoroutine ("declareFinished");
		}
		
		private IEnumerator declareFinished ()
		{
				yield return new WaitForSeconds (0.15f);
				finished = true;
		}
}
