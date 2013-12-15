using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour
{
	public int size;
	
	public string[] textDisplay;
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
		currentTextIndex = 0;
		dialogBox = GetComponentInChildren<SpriteRenderer> ().gameObject;
		dialogBox.renderer.enabled = false;
		text = GetComponentsInChildren<TextMesh> ();
		for (int i = 0; i < text.Length; i++) {
			text [i].renderer.enabled = false;
			if (text [i].name.Contains ("Line 1")) {
				mainTextIndex = i;
				text [i].text = textDisplay [currentTextIndex];
			}
		}
	}
	
	public void next ()
	{	
		if (currentTextIndex < textDisplay.Length - 1) {
			currentTextIndex++;
			text [mainTextIndex].text = textDisplay [currentTextIndex];
		} else {
			dialogOver = true;
			closeDialog ();
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
