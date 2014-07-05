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
		}

		public void SpeechBubbleDisplay (string text, bool stay = false)
		{
				animator.SetTrigger ("Show");
				speechBubbleText.text = text;
				if (!stay) {
						if (animator != null) {
								StartCoroutine ("fadeOutDelay");
						}
				
				}
		}

		private IEnumerator fadeOutDelay ()
		{
				yield return new WaitForSeconds (1.5f);
				animator.SetTrigger ("FadeOut");
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
