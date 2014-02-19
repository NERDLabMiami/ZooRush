using UnityEngine;
using System.Collections;

public class Dialog : Button
{
	private string[] text;

	private bool displaying;
	private int currentTextIndex;
	private Animator animator;
	public TextAnimator textAnimator;

	void Start ()
	{
		base.Start ();
		displaying = false;
		animator = GetComponent<Animator> ();
	}

	public void stopSpeaking ()
	{
		animator.SetBool ("Speak", false);
	}

	public void activateDialog (string[] dialogText)
	{
		text = dialogText;
		currentTextIndex = 0;
		animator.SetTrigger ("Found");
		open ();
	}

	public void open ()
	{
		Debug.Log ("OPEN");
		displaying = true;
		animator.SetTrigger ("Opened");
		textAnimator.gameObject.SetActive (true);
		next ();

	}

	private void next ()
	{
		animator.SetBool ("Speak", true);
		string displayText;
		if (text [currentTextIndex] [0] == '+') {
			displayText = text [currentTextIndex].Substring (1);
		} else {
			displayText = text [currentTextIndex];
		}
		if (text.Length > currentTextIndex + 1 && text [currentTextIndex + 1] [0] == '+') {
			displayText += "\n" + text [currentTextIndex + 1].Substring (1);
			currentTextIndex = currentTextIndex + 1;
			if (text.Length > currentTextIndex + 2 && text [currentTextIndex + 2] [0] == '+') {
				displayText += "\n" + text [currentTextIndex + 2].Substring (1);
				currentTextIndex = currentTextIndex + 1;
			}
		}
		while (textAnimator.animating)
			;
		textAnimator.AnimateText (displayText);
		clicked = false;
	}

	private void close ()
	{
		GetComponentInChildren<TextMesh> ().text = "";
		animator.SetTrigger ("Close");
	}

	private void destroy ()
	{
		text = null;
		animator.SetTrigger ("Disable");
		displaying = false;
		clicked = false;
		GameStateMachine.requestPlay ();
	}

	protected override void action ()
	{
		Debug.Log ("CLICK");
		if (displaying) {
			Debug.Log ("YUP");
			if (currentTextIndex < text.Length) {
				next ();
			} else {
				close ();
			}
		}
	}

}
