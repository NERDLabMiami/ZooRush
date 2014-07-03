using UnityEngine;
using System.Collections;

public class CharacterSpeech : MonoBehaviour
{
		public  GameObject speechBubble;
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
				SpriteRenderer thisRenderer = speechBubble.GetComponent<SpriteRenderer> ();
				while (thisRenderer.color.a > 0.05f) {
						yield return new WaitForSeconds (0.1f);
				}
				hide ();
				if (animator != null) {
						animator.SetTrigger ("Reset");
				}
		}

		public void hide ()
		{
				changeAlpha (0);
		}

		public void show ()
		{
				changeAlpha (1);
		}

		private void changeAlpha (int alpha)
		{
				Color color;

				SpriteRenderer spriteRend = speechBubble.GetComponentInChildren<SpriteRenderer> ();
				color = spriteRend.color;
				color.a = alpha;
				spriteRend.color = color;

				color = speechBubbleText.color;
				color.a = alpha;
				speechBubbleText.color = color;
		}
}
