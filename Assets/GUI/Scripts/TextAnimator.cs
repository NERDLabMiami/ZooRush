using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour
{

	public TextMesh text;
	private string fullText;
	private bool animating;

	void Start ()
	{
		animating = false;
//		startAnimation ();
	}

	private void textSetUp (string textToDisplay)
	{
		if (textToDisplay.Length > 0) {
			fullText = textToDisplay;
		} else {
			fullText = text.text;
		}
		text.text = "";
	}
	
	private IEnumerator animate ()
	{
		animating = true;
		int index = 0;
		while (!text.text.Equals(fullText)) {
			text.text = fullText.Substring (0, ++index);
			yield return new WaitForFixedUpdate ();
		}
		animating = false;
	}

	public void startAnimation (string textToDisplay = "")
	{
		if (!animating) {
			textSetUp (textToDisplay);
			StartCoroutine (animate ());
		} else {
			StartCoroutine (waitForAnimatingToFinishThenAnimate (textToDisplay));
		}
	}

	private IEnumerator waitForAnimatingToFinishThenAnimate (string textToDisplay)
	{
		while (animating) {
			yield return new WaitForSeconds (0.15f);
		}
		startAnimation (textToDisplay);
	}

}
