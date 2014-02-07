using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour
{
	public bool tutOnly;
	public bool opened;
	
	public string[] textDisplay;
	private string[] formatted;
	public int currentTextIndex;
	
	public bool dialogOver;
	private GameObject dialogBox;

	private TextMesh[] text;
	private int mainTextIndex;


	private SceneManager sceneManager;
	private DialogHandler dialogHandler;
	void Start ()
	{	
		dialogOver = false;
		opened = false;
		currentTextIndex = -1;
		dialogBox = GetComponentInChildren<SpriteRenderer> ().gameObject;
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		dialogHandler = GameObject.FindObjectOfType<DialogHandler> ();
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
			if (textDisplay [currentTextIndex] [0].Equals ('+')) {
				text [mainTextIndex].text = textDisplay [currentTextIndex].Substring (1);
			} else {
				text [mainTextIndex].text = textDisplay [currentTextIndex];
			}
			if (currentTextIndex + 1 < textDisplay.Length) {
				if (textDisplay [currentTextIndex + 1] [0].Equals ('+')) {
					text [mainTextIndex].text += "\n" + textDisplay [currentTextIndex + 1].Substring (1);
					currentTextIndex++;
					if (currentTextIndex + 1 < textDisplay.Length) {
						
						if (textDisplay [currentTextIndex + 1] [0].Equals ('+')) {
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
		if (!opened) {
			dialogBox.renderer.enabled = true;
			foreach (TextMesh tex in text) {
				tex.renderer.enabled = true;
			}
			opened = true;
			if (GameObject.FindObjectOfType<StopwatchController> () != null) {
				GameObject.FindObjectOfType<StopwatchController> ().pauseStopwatch ();
			}
		}

	}
	
	private void closeDialog ()
	{
		dialogBox.renderer.enabled = false;
		foreach (TextMesh tex in text) {
			tex.renderer.enabled = false;
		}
		sceneManager.isPlaying = true;
		dialogHandler.displaying = false;
		dialogHandler.found = false;
		if (GameObject.FindObjectOfType<StopwatchController> () != null) {
			GameObject.FindObjectOfType<StopwatchController> ().resumeStopwatch ();
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
