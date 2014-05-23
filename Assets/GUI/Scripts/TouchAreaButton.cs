using UnityEngine;
using System.Collections;

public class TouchAreaButton : Button
{

		public override void onPressDown ()
		{
				//NOP
		}

		public override void onPressUp ()
		{
				buttonAction ();
		}

		public override void reset ()
		{
				//NOP
		}

		void FixedUpdate ()
		{
				transform.position = (otherButtonClass as GameObject).transform.position;
		}
}

