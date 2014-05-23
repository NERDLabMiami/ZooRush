using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour, OtherButtonClass
{

		public void closeStartScreen ()
		{
				Destroy (transform.parent.gameObject);
				GameState.requestIntro ();
		}

		public void otherButtonAction (Button thisButton)
		{
				GetComponent<Animator> ().SetTrigger ("Open");
		}
}
