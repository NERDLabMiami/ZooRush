using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
	public bool animating, finished;
	private string text;
	public TextMesh textMesh;

	void Start ()
	{
		animating = finished = false;
	}
	
	public void AnimateText (string displayText)
	{
		this.text = displayText;
		textMesh.text = "";
		StartCoroutine (Animate ());
	}

	private IEnumerator Animate ()
	{
		animating = true;
		foreach (char letter in text.ToCharArray()) {
			textMesh.text += letter;
			yield return new WaitForSeconds (0.05f);
		}
		finished = true;
		animating = false;
	}
}
