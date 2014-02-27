using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
	public bool animating;
	private string text;
	public TextMesh textMesh;

	void Start ()
	{
		animating = false;
	}
	
	public void AnimateText (string displayText)
	{
		this.text = displayText;
		textMesh.text = "";
		animating = true;
		StartCoroutine (Animate ());

	}

	private IEnumerator Animate ()
	{
		for (int i = 1; i <= text.Length; i++) {
			textMesh.text = text.Substring (0, i);
			yield return new WaitForSeconds (0.05f);
		}

		animating = false;
	}

}
