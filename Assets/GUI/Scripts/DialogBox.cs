using UnityEngine;
using System.Collections;

public class DialogBox : MonoBehaviour, OtherButtonClass
{

		public string[] dialog;
		public TextAnimator textAnimator;
		private int index;
		public Button dialogButton;
		public Renderer[] dialogBoxRenderers;
		private bool activated;

		void Start ()
		{
				index = 0;
				dismiss ();
		}

		void OnEnable ()
		{
				GameState.dialogCalled += dialogStateCalled;
				GameState.dialogDismissed += leavingDialogState;
		}

		void OnDisable ()
		{
				GameState.dialogCalled -= dialogStateCalled;
				GameState.dialogDismissed -= leavingDialogState;

		}

		private void dialogStateCalled ()
		{
				Debug.Log ("Dialog State Called");
				activated = true;
				index = 0;
				next ();
		}

		private void leavingDialogState ()
		{
				Debug.Log ("Dismissing Dialog");
				dismiss ();
		}

		public void next ()
		{
				if (index == 0) {
						display ();
				}
				textAnimator.startAnimation (dialog [index]);
				index++;
		}

		public void display ()
		{
				foreach (Renderer sprite in dialogBoxRenderers) {
						sprite.enabled = true;
				}
				dialogButton.enableButton ();

		}

		public void dismiss ()
		{
				foreach (Renderer sprite in dialogBoxRenderers) {
						sprite.enabled = false;
				}
				dialogButton.disableButton ();
				activated = false;
		}

		public void otherButtonAction (Button thisButton = null)
		{
				if (activated) {
						if (index < dialog.Length) {
								next ();
						} else {
								GameState.requestPlay ();
						}
				}
		}


}
