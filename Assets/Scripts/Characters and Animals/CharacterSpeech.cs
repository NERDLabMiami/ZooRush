using UnityEngine;
using System.Collections;

public class CharacterSpeech : MonoBehaviour
{
		public  GameObject speechBubble;
		public SpriteRenderer speechBubbleRenderer;
		public  TextMesh speechBubbleText;
		public Animator animator;

		void Awake ()
		{
				if (speechBubbleText == null) {
						speechBubbleText = speechBubble.GetComponentInChildren<TextMesh> ();
				}
				if (animator == null) {
						animator = speechBubble.GetComponent<Animator> ();
				}
				if (speechBubbleRenderer == null) {
						speechBubble.GetComponent<SpriteRenderer> ();
				}
				hide ();
		}

		public void SpeechBubbleDisplay (string text, bool stay = false)
		{
				show ();
				speechBubbleText.text = text;
				if (!stay) {
						if (animator != null) {
								animator.SetTrigger ("Hide");
						}
				
						StartCoroutine (waitForDisappear ());
				}
		}

		private  IEnumerator waitForDisappear ()
		{
				while (speechBubbleRenderer.color.a > 0.05f) {
						yield return new WaitForSeconds (0.1f);
				}
				hide ();
				if (animator != null) {
						animator.SetTrigger ("Reset");
				}
		}

		public void hide ()
		{
				Debug.Log ("HIDE CALLED");
				changeAlpha (0);
		}

		public void show ()
		{
				Debug.Log ("SHOW CALLED");
				changeAlpha (1);
		}

		private void changeAlpha (int alpha)
		{
				if (alpha == 1) {
						speechBubbleRenderer.color = Color.white;
				} else {
						speechBubbleRenderer.color = Color.clear;
				}

				Color color = speechBubbleText.color;
				color.a = alpha;
				speechBubbleText.color = color;
		}
}
