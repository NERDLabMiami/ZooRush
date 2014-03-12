using UnityEngine;
using System.Collections;

public class CharacterSpeech : MonoBehaviour
{
	public  GameObject speechBubble;
	private  TextMesh speechBubbleText;
	private Animator animator;

	void Start ()
	{
		speechBubbleText = speechBubble.GetComponentInChildren<TextMesh> ();
		animator = speechBubble.GetComponent<Animator> ();
		hide ();
	}

	public void SpeechBubbleDisplay (string text)
	{
		show ();
		speechBubbleText.text = text;
		animator.SetTrigger ("Hide");
		StartCoroutine (waitForDisappear ());
	}

	private  IEnumerator waitForDisappear ()
	{
		SpriteRenderer thisRenderer = speechBubble.GetComponent<SpriteRenderer> ();
		while (thisRenderer.color.a > 0.05f) {
			yield return new WaitForSeconds (0.1f);
		}
		hide ();
		speechBubble.GetComponent<Animator> ().SetTrigger ("Reset");
	}

	private void hide ()
	{
		changeAlpha (0);
	}

	private void show ()
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
