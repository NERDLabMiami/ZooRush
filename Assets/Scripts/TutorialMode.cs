using UnityEngine;
using System.Collections;

public class TutorialMode : MonoBehaviour
{
	private bool tutorialEnabled;
	
	private bool dialogPresent;
	private bool infectionDialog;
	private bool powerUpDialog;
	private bool painBarDialog;
	private bool introDialog;
	private int i = 0;
	SceneManager sceneManager;
	
	void Start ()
	{
		sceneManager = GetComponent<SceneManager> ();
		tutorialEnabled = sceneManager.tutEnabled;
		dialogPresent = false;
		infectionDialog = false;
		powerUpDialog = false;
		painBarDialog = false;
		introDialog = false;
	}
	
	void Update ()
	{
		if (tutorialEnabled) {
			if (!dialogPresent) {
				//First check for intro dialog
				//Second check for pain dialog
				//Then check for the presence of any other objects
				
				if (!introDialog) {
					sceneManager.isPlaying = false;
					introSequence ();
				}
			}
		}
	}
	
	private string[] introDialogScript = {
		"This is an infection,","touching one slows you down and causes pain","Try your best to avoid them!"
	};
	
	private void introSequence ()
	{	
//		dialogPresent = true;

		
//		while (i < introDialogScript.Length) {
		Debug.Log (introDialogScript [i]);
		if (InputManager.enter) {
			i++;
		}
//		}
		if (i >= introDialogScript.Length) {
//			dialogPresent = false;
			dialogPresent = true;
			sceneManager.isPlaying = true;
			introDialog = true;
		}

	}
	  
}
