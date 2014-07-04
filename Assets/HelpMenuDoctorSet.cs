using UnityEngine;
using System.Collections;

public class HelpMenuDoctorSet : HelpMenuSet
{
		public Building doctorOffice;

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
		
		}
}
