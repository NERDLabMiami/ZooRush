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
		animator.SetTrigger ("Found");
		open ();
	}

	public void open ()
	{
		displaying = true;
		animator.SetTrigger ("Opened");
		animator.SetBool ("Speak", true);
		string displayText;
		if (text [0] [0] == '+') {
			displayText = text [0].Substring (1);
		} else {
			displayText = text [0];
		}
		currentTextIndex = 1;
		if (text.Length > 1 && text [1] [0] == '+') {
			displayText += "\n" + text [1].Substring (1);
			currentTextIndex = 2;
			if (text.Length > 2 && text [2] [0] == '+') {
				displayText += "\n" + text [2].Substring (1);
				currentTextIndex = 3;
			}
		}
		textAnimator.AnimateText (displayText);
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
			currentTextIndex = currentTextIndex + 2;
			if (text.Length > currentTextIndex + 2 && text [currentTextIndex + 2] [0] == '+') {
				displayText += "\n" + text [currentTextIndex + 2].Substring (1);
				currentTextIndex = currentTextIndex + 3;
			}
		}
		GetComponentInChildren<TextAnimator> ().AnimateText (displayText);
		clicked = false;
	}

	private void close ()
	{
		GetComponentInChildren<TextMesh> ().text = "";
		animator.SetTrigger ("Close");
	}

	private void destroy ()
	{
		animator.SetTrigger ("Disable");
		displaying = false;
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
