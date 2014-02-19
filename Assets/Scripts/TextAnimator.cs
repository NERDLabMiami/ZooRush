using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
	public bool animating;
	public bool finished;
	private string text;
	public TextMesh textMesh;
	private int currentChar;
	private Dialog dialog;

	void Start ()
	{
		animating = false;
		finished = false;
		dialog = transform.parent.GetComponent<Dialog> ();
	}


	public void AnimateText (string text)
	{
		this.text = text;
		textMesh.text = "";
		currentChar = 0;
		animating = true;
		StartCoroutine (Animate ());
	}

	private IEnumerator Animate ()
	{
		while (currentChar < text.Length) {
			textMesh.text += text [currentChar++];
			yield return new WaitForSeconds (0.05f);
		}

		finished = true;
		dialog.stopSpeaking ();
		animating = false;
	}

}
