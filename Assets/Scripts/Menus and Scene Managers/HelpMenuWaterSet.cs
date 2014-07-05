using UnityEngine;
using System.Collections;

public class HelpMenuWaterSet : HelpMenuSet
{
		public PowerUp[] waterBottles;
	
		public override void activate ()
		{
				activated = true;
				transform.parent = Camera.main.transform;
				GameState.requestPause ();
				transform.localPosition = Vector3.zero;
				transform.parent = null;
				StartCoroutine ("resumeMovement");
		}
	
		public override void dismiss ()
		{
				activated = false;
				transform.parent = null;
				transform.position = originalPosition;
		}
	
		public override void reset ()
		{
		
		}

		private IEnumerator resumeMovement ()
		{
				yield return new WaitForSeconds (1);
				GameState.requestPlay ();
		}
}
