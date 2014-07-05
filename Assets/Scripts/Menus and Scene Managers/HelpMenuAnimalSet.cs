using UnityEngine;
using System.Collections;

public class HelpMenuAnimalSet : HelpMenuSet
{
		public AnimalStory animal;
		public SpriteRenderer netRenderer;
		private bool callledDismiss;

		void OnEnable ()
		{
				animal.speed = Vector2.zero;
		}

		public override void activate ()
		{
				activated = true;
				callledDismiss = false;
				transform.parent = Camera.main.transform;
				transform.localPosition = Vector3.zero;
				animal.setSpeed ();
				GameState.requestLaunch ();
				transform.parent = null;
				
		}
	
		public override void dismiss ()
		{
				activated = false;
				transform.position = originalPosition;
				GameState.currentState = GameState.States.Pause;
				GameState.requestPlay ();
				GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("What would you\nlike help with?", true);
		}
	
		public override void reset ()
		{
				animal.caught = false;
				netRenderer.enabled = false;
		}

		void Update ()
		{
				if (!callledDismiss && animal.caught) {
						callledDismiss = true;
						StartCoroutine ("startDismissal");
				}
		}

		private IEnumerator startDismissal ()
		{
				yield return new WaitForSeconds (1.5f);
				dismiss ();
		}
}