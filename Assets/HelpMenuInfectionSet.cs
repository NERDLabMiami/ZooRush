using UnityEngine;
using System.Collections;

public class HelpMenuInfectionSet : HelpMenuSet
{
		public Infection[] infections;
		private bool interacted;

		public override void activate ()
		{
				activated = true;
				interacted = false;
				transform.parent = Camera.main.transform;
				GameState.requestPause ();
				transform.localPosition = Vector3.zero;
				transform.parent = null;
				StartCoroutine ("resumeMovement");
		}

		public override void dismiss ()
		{
				activated = false;
				transform.position = originalPosition;
				GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("What would you\nlike help with?", true);

		}

		public override void reset ()
		{
				foreach (Infection infection in infections) {
						infection.resetState ();
				}
		}

		private IEnumerator resumeMovement ()
		{
				yield return new WaitForSeconds (1);
				GameState.requestPlay ();
		}

		private IEnumerator startDismissal ()
		{
				yield return new WaitForSeconds (1.5f);
				dismiss ();
		}

		void Update ()
		{
				if (activated && !interacted) {
						foreach (Infection infection in infections) {
								if (infection.touched ()) {
										interacted = true;
								}
						}
						if (interacted) {
								StartCoroutine ("startDismissal");
						}
				}
		}
}

