using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour
{
	public bool isTriggered;
	public float waitTime;
		
	private bool dialogOver;
	private GameObject dialogBox;
	private TextMesh[] text;
	
	void Start ()
	{
		dialogOver = false;
		dialogBox = GetComponentInChildren<SpriteRenderer> ().gameObject;
		dialogBox.renderer.enabled = false;
		text = GetComponentsInChildren<TextMesh> ();
		foreach (TextMesh tex in text) {
			tex.renderer.enabled = false;
		}
	}
	
	void Update ()
	{
	
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
