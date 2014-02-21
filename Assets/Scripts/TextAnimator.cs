using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
	public bool animating, finished, forceStop;
	private string text;
	public TextMesh textMesh;

	void Start ()
	{
		animating = finished = forceStop = false;
	}
	
	public void AnimateText (string displayText)
	{
		this.text = displayText;
		textMesh.text = "";
		forceStop = false;
		animating = true;
		StartCoroutine (Animate ());

	}

	private IEnumerator Animate ()
	{
		foreach (char letter in text.ToCharArray()) {
			Debug.Log ("Animating is " + animating);
			if (forceStop) {
				yield return 0;
			}
			textMesh.text += letter;
			yield return new WaitForSeconds (0.05f);
		}
		Debug.Log ("YUP");
		if (!textMesh.text.Equals (text)) {
			textMesh.text = text;
		}
		finished = true;
		animating = false;
	}

	public void forceFinish ()
	{
		Debug.Log ("OTAY");
		forceStop = true;
	}
}
