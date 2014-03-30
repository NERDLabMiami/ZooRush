using UnityEngine;
using System.Collections;
using System.Text;

public class Dialog : ButtonOld
{
	private string[] text;

	private bool displaying;
	private int currentTextIndex;
	public SpriteRenderer[] renderers;
	public TextMesh textMeshDisplay;

	new void Start ()
	{
		base.Start ();
		hide ();
	}

	private void hide ()
	{
		foreach (SpriteRenderer rend in renderers) {
			rend.color = Color.clear;
		}
		displaying = false;
	}

	private void show ()
	{
		foreach (SpriteRenderer rend in renderers) {
			rend.color = Color.white;
		}
		displaying = true;
	}

	public void activateDialog (string[] dialogText)
	{
		if (!displaying) {
			text = dialogText;
			currentTextIndex = 0;
			next ();
			show ();
			GameStateMachine.requestPause ();
		}
	}
	
	private void next ()
	{
		StringBuilder displayText = new StringBuilder ();
		int nextIndex = currentTextIndex;

		for (int i = currentTextIndex; i < text.Length; i++) {
			if (i < currentTextIndex + 3) {
				if (text [i] [0] == '+') {
					displayText.Append (text [i].Substring (1));
				} else {
					displayText.Append (text [i]);
				}
				displayText.Append ("\n");
				nextIndex = i + 1;
			} else {
				break;
			}
		}
		currentTextIndex = nextIndex;
		textMeshDisplay.text = displayText.ToString ();
		StartCoroutine (waitToResetTouch ());
	}

	private void close ()
	{
		GetComponentInChildren<TextMesh> ().text = "";
		GameObject.FindObjectOfType<DialogHandler> ().closeDialog ();
		hide ();
		GameStateMachine.requestPlay ();
		StartCoroutine (waitToResetTouch ());
	}

	protected override void action ()
	{
		if (displaying) {
			if (currentTextIndex < text.Length) {
				Debug.Log ("Next Called");
				next ();
			} else {
				close ();
			}
		}
	}
}
