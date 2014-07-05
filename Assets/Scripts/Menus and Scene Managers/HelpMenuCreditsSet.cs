using UnityEngine;
using System.Collections;

public class HelpMenuCreditsSet : HelpMenuSet
{

		public override void activate ()
		{
				activated = true;
				transform.localPosition = Vector3.zero;
		}
	
		public override void dismiss ()
		{
				activated = false;
				transform.position = originalPosition;
		}
	
		public override void reset ()
		{
//		throw new System.NotImplementedException ();
		}
}
