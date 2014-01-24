using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour
{
	public bool tutOnly;
	
	public string[] textDisplay;
	private string[] formatted;
	public int currentTextIndex;
	
	public bool isTriggered;
	public float waitTime;
	private bool dialogOver;
	private GameObject dialogBox;
	
	private TextMesh[] text;
	private int mainTextIndex;
	
	void Start ()
	{	
		dialogOver = false;
		currentTextIndex = -1;
		dialogBox = GetComponentInChildren<SpriteRenderer> ().gameObject;
		dialogBox.renderer.enabled = false;
		text = GetComponentsInChildren<TextMesh> ();
		for (int i = 0; i < text.Length; i++) {
			text [i].renderer.enabled = false;
			if (text [i].name.Contains ("Line 1")) {
				mainTextIndex = i;
			}
		}
		next ();
	}

	public void next ()
	{	
		if (currentTextIndex < textDisplay.Length - 1) {
			currentTextIndex++;
			Debug.Log ("LINE 1");
			if (textDisplay [currentTextIndex] [0].Equals ('+')) {
				text [mainTextIndex].text = textDisplay [currentTextIndex].Substring (1);
			} else {
				text [mainTextIndex].text = textDisplay [currentTextIndex];
			}
			if (currentTextIndex + 1 < textDisplay.Length) {
				if (textDisplay [currentTextIndex + 1] [0].Equals ('+')) {
					Debug.Log ("LINE 2");
					text [mainTextIndex].text += "\n" + textDisplay [currentTextIndex + 1].Substring (1);
					currentTextIndex++;
					if (currentTextIndex + 1 < textDisplay.Length) {
						
						if (textDisplay [currentTextIndex + 1] [0].Equals ('+')) {
							Debug.Log ("LINE 3");
							text [mainTextIndex].text += "\n" + textDisplay [currentTextIndex + 1].Substring (1);
							currentTextIndex++;
						}
					}
				}
			}
		} else {
			dialogOver = true;
			closeDialog ();
		}
	}

	private IEnumerator animateText (TextMesh textMesh, string fullText, float Delay)
	{
		int currentChar = 0;
		while (true) {
			if (currentChar < fullText.Length)
				textMesh.text += fullText [currentChar++];
			yield return new WaitForSeconds (Delay);
		}
	}
	
	public void openDialog ()
	{
		dialogBox.renderer.enabled = true;
		foreach (TextMesh tex in text) {
			tex.renderer.enabled = true;
		}
	}
	
	public void closeDialog ()
	{
		dialogBox.renderer.enabled = false;
		foreach (TextMesh tex in text) {
			tex.renderer.enabled = false;
		}
	}
	
	public bool isDialogFinished ()
	{
		return dialogOver;
	}
	
	public void isDialogFinished (bool yes)
	{
		dialogOver = yes;
	}
}
