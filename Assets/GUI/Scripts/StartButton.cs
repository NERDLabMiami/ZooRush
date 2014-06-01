using UnityEngine;
using System.Collections;

public class StartButton : OtherButtonClass
{
		public Animator animator;
		private Vector3 originalPosition;

		void Awake ()
		{
				originalPosition = transform.parent.position;
		}

		public void closeStartScreen ()
		{
				GameState.requestIntro ();
				transform.parent.transform.position = originalPosition;
		}

		public override void otherButtonAction (Button thisButton)
		{
				animator.SetTrigger ("Open");
		}
}
