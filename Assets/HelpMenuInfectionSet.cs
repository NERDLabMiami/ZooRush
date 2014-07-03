using UnityEngine;
using System.Collections;

public class HelpMenuInfectionSet : HelpMenuSet
{
		public Infection[] infections;

		public override void activate ()
		{
				transform.localPosition = Vector3.zero;
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
}
