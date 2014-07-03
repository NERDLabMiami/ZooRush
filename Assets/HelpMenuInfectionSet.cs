using UnityEngine;
using System.Collections;

public class HelpMenuInfectionSet : HelpMenuSet
{
		public Infection[] infections;

		public override void activate ()
		{
				transform.parent = Camera.main.transform;
				GameState.requestPause ();
				transform.localPosition = Vector3.zero;
				transform.parent = null;
				StartCoroutine ("resumeMovement");
		}

		public override void dismiss ()
		{
				transform.position = originalPosition;
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
}

