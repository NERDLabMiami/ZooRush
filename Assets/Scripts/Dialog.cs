using UnityEngine;
using System.Collections;

public class Dialog : Button
{
	private string[] text;

	private bool displaying;
	private int currentTextIndex;
	private Animator animator;
	public TextAnimator textAnimator;
	private Renderer[] renderers;

	new void Start ()
	{
		base.Start ();
		displaying = false;
		animator = GetComponent<Animator> ();
		renderers = GetComponentsInChildren<Renderer> ();
		hide ();
	}

	private void hide ()
	{
		foreach (Renderer rend in renderers) {
			rend.enabled = false;
		}
	}

	private void show ()
	{
		foreach (Renderer rend in renderers) {
			rend.enabled = true;
		}
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
//		Debug.Log ("OPEN");
		displaying = true;
		textAnimator.gameObject.SetActive (true);
		animator.SetTrigger ("Opened");
//		show ();
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
			if (text.Length > currentTextIndex + 1 && text [currentTextIndex + 1] [0] == '+') {
				displayText += "\n" + text [currentTextIndex + 1].Substring (1);
				currentTextIndex = currentTextIndex + 1;
			}
		}
		
		textAnimator.AnimateText (displayText);
		stopSpeaking ();
		currentTextIndex++;
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

//		hide ();
		GameStateMachine.requestPlay ();
	}

	protected override void action ()
	{
		if (displaying & !textAnimator.animating) {
			if (currentTextIndex < text.Length) {

				next ();
			} else {
				close ();
			}
		}
		clicked = false;
	}

}
