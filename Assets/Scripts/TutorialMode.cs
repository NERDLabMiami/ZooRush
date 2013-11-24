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
					StartCoroutine (introSequence ());
				}
			}
		}
	}
	
	private string[] introDialogScript;
	
	private IEnumerator introSequence ()
	{	
		int i = 0;
		
		while(i < introDialogScript.Length){
			
			if(InputManager.enter){
				i++;
			}
		}
		
		yield return;
		sceneManager.isPlaying = true;
	}
	  
}
