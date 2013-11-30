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
	private int i;
	SceneManager sceneManager;
	
	private GameObject tutText;
	private GameObject objectScanner;
	
	void Start ()
	{
		sceneManager = GetComponent<SceneManager> ();
		tutorialEnabled = sceneManager.tutEnabled;
		dialogPresent = false;
		introDialog = false;
		infectionDialog = false;
		powerUpDialog = false;
		painBarDialog = false;
		i = 0;
		tutText = GameObject.Find ("GUI - Tutorial Text");
		objectScanner = GameObject.Find ("Tutorial - Object Scanner");
		tutText.GetComponent<GUIText> ().text = "";
		tutText.GetComponentInChildren<GUITexture> ().enabled = false;
	}
	
	void Update ()
	{
		if (tutorialEnabled) {
			RaycastHit2D detected = Physics2D.Raycast (objectScanner.transform.position, Vector2.up);
			//First check for intro dialog
			//Second check for pain dialog
			//Then check for the presence of any other objects
			if (!introDialog && !dialogPresent) {
				Vector3 ratio = new Vector3 (0.5f, 0.5f);
				ratio.x -= 0.2f;
				ratio.y -= 0.07f;
				ratio.z = tutText.transform.position.z;
				tutText.transform.position = ratio;
				tutText.GetComponentInChildren<GUITexture> ().enabled = true;
				sceneManager.isPlaying = false;
				introSequence ();
			} else {
				if (!painBarDialog && !dialogPresent) {
					GameObject painBar = GameObject.Find ("Pain Icon");
					Vector3 objectScreenPoint = camera.WorldToScreenPoint (painBar.transform.position);
					Vector3 ratio = new Vector3 ((objectScreenPoint.x / Screen.width), (objectScreenPoint.y / Screen.height));
					ratio.x -= 0.2f;
					ratio.y -= 0.07f;
					ratio.z = tutText.transform.position.z;
					tutText.transform.position = ratio;
					tutText.GetComponentInChildren<GUITexture> ().enabled = true;
					sceneManager.isPlaying = false;
					painBarSequence ();
				} else {
					if (detected.collider != null && !dialogPresent) {
						if (detected.collider.name.Contains ("Infection")) {
							Vector3 objectScreenPoint = camera.WorldToScreenPoint (detected.transform.position);
							Vector3 ratio = new Vector3 ((objectScreenPoint.x / Screen.width), (objectScreenPoint.y / Screen.height));
							ratio.x -= 0.1f;
							ratio.y += 0.15f;
							ratio.z = tutText.transform.position.z;
							if (!infectionDialog) {
								tutText.transform.position = ratio;
								tutText.GetComponentInChildren<GUITexture> ().enabled = true;
								sceneManager.isPlaying = false;
								infectionSequence ();
							}
						}
					
						if (detected.collider.name.Contains ("Power Up")) {
							Vector3 objectScreenPoint = camera.WorldToScreenPoint (detected.transform.position);
							Vector3 ratio = new Vector3 ((objectScreenPoint.x / Screen.width), (objectScreenPoint.y / Screen.height));
							ratio.x -= 0.1f;
							ratio.y += 0.15f;
							ratio.z = tutText.transform.position.z;
							if (!powerUpDialog) {
								tutText.transform.position = ratio;
								tutText.GetComponentInChildren<GUITexture> ().enabled = true;
								sceneManager.isPlaying = false;
								powerUpSequence ();
							}
						}
					}
				}
			}
		}

	}

	private string[] introDialogScript = {
		"Welcome to Zoo Rush,","move your character by pressing the \n up and down keys"
	};

	private string[] painBarDialogScript = {
		"This is your crisis meter,","as you keep running, you exert \nyourself and build up pain,","When the meter fills up you enter \ncrisis mode!"
	};

	private string[] infectionDialogScript = {
		"This is an infection,","touching one slows you down \nand causes pain","Try your best to avoid them!"
	};

	private string[] powerUpDialogScript = {
		"This is a power up,","touching one reduces your crisis \nmeter and makes you healthier.","Try your best to collect them!"
	};
	
	private void introSequence ()
	{	
		tutText.GetComponent<GUIText> ().text = introDialogScript [i];
		if (InputManager.enter) {
			i++;
		}
		if (i >= introDialogScript.Length) {
			tutText.GetComponent<GUIText> ().text = "";
			tutText.GetComponentInChildren<GUITexture> ().enabled = false;
			dialogPresent = true;
			sceneManager.isPlaying = true;
			introDialog = true;
			i = 0;
			dialogPresent = false;
		}

	}

	private void painBarSequence ()
	{	
		tutText.GetComponent<GUIText> ().text = painBarDialogScript [i];
		if (InputManager.enter) {
			i++;
		}
		if (i >= painBarDialogScript.Length) {
			tutText.GetComponent<GUIText> ().text = "";
			tutText.GetComponentInChildren<GUITexture> ().enabled = false;
			dialogPresent = true;
			sceneManager.isPlaying = true;
			painBarDialog = true;
			i = 0;
			dialogPresent = false;
		}
		
	}

	private void infectionSequence ()
	{	
		tutText.GetComponent<GUIText> ().text = infectionDialogScript [i];
		if (InputManager.enter) {
			i++;
		}
		if (i >= infectionDialogScript.Length) {
			tutText.GetComponent<GUIText> ().text = "";
			tutText.GetComponentInChildren<GUITexture> ().enabled = false;
			dialogPresent = true;
			sceneManager.isPlaying = true;
			infectionDialog = true;
			i = 0;
			dialogPresent = false;
		}
		
	}

	private void powerUpSequence ()
	{	
		tutText.GetComponent<GUIText> ().text = powerUpDialogScript [i];
		if (InputManager.enter) {
			i++;
		}
		if (i >= powerUpDialogScript.Length) {
			tutText.GetComponent<GUIText> ().text = "";
			tutText.GetComponentInChildren<GUITexture> ().enabled = false;
			dialogPresent = true;
			sceneManager.isPlaying = true;
			powerUpDialog = true;
			i = 0;
			dialogPresent = false;
		}
		
	}
	  
}
