using UnityEngine;
using System.Collections;

public class HelpMenuSetPainKiller : HelpMenuSet
{
		public PainKiller painkiller;

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
//				throw new System.NotImplementedException ();
		}
}
