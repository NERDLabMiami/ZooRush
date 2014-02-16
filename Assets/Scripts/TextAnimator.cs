using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
	private bool animating, clear;
	public bool finished;
	private string text;
	public TextMesh textMesh;
	private int currentChar;
	private Dialog dialog;

	void Start ()
	{
		animating = clear = finished = false;
		dialog = transform.parent.GetComponent<Dialog> ();
	}
	
	void FixedUpdate ()
	{
		if (animating) {
			if (clear) {
				StartCoroutine (Animate ());
			}
		}
	}

	public void AnimateText (string text)
	{
		this.text = text;
		textMesh.text = "";
		currentChar = 0;
		animating = true;
		clear = true;
	}

	private IEnumerator Animate ()
	{
		clear = false;
		yield return new WaitForSeconds (0.05f);
		textMesh.text = text.Substring (0, currentChar);
		if (currentChar < text.Length) {
			currentChar++;
		} else {
			animating = false;
			finished = true;
			dialog.stopSpeaking ();
		}
		clear = true;
		
	}

}
