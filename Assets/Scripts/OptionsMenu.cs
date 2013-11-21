using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{
	private GameObject[] Options;
	private GameObject[] sound;
	private GameObject[] music;
	private GameObject selectedOption;

	void Start ()
	{
		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetString ("Music", "On");
		}
		
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetString ("Sound", "On");
		}
		
		Options = GameObject.FindGameObjectsWithTag ("option");
		selectedOption = GameObject.Find ("Text - Back");
		foreach (GameObject option in Options) {
			if (selectedOption.Equals (option)) {
				option.GetComponent<TextMesh> ().color = Color.yellow;
			}
		}
	}

	void Update ()
	{
		if (InputManager.touching) {
			foreach (GameObject option in Options) {
				if (InputManager.pointerTouch.collider.Equals (option.collider2D)) {
					selectedOption = option;
				}
			}
			if (InputManager.enter) {
				selectThisObject (InputManager.pointerTouch.collider.gameObject);
			}
		}
		highlightSelection ();
	}
	
	void highlightThisObject (GameObject thisObject)
	{
		thisObject.GetComponent<TextMesh> ().color = Color.yellow;
	}
	
	void selectThisObject (GameObject thisObject)
	{
		highlightThisObject (thisObject);
		if (thisObject.name.Contains ("Music On")) {
			PlayerPrefs.SetString ("Music", "On");
		}
		if (thisObject.name.Contains ("Music Off")) {
			PlayerPrefs.SetString ("Music", "Off");
		}
		if (thisObject.name.Contains ("Sound On")) {
			PlayerPrefs.SetString ("Sound", "On");
		}
		if (thisObject.name.Contains ("Sound Off")) {
			PlayerPrefs.SetString ("Sound", "Off");
		}
		if (thisObject.name.Contains ("Back")) {
			if (PlayerPrefs.HasKey ("Last Scene") && !PlayerPrefs.GetString ("Last Scene").Equals (Application.loadedLevelName)) {
				Application.LoadLevel (PlayerPrefs.GetString ("Last Scene"));
			} else {
				Application.LoadLevel ("Splash");
			}
		}
	}
	
	
	void highlightSelection ()
	{
		foreach (GameObject option in Options) {
			if (!selectedOption.Equals (option)) {
				option.GetComponent<TextMesh> ().color = Color.white;
			} else {
				highlightThisObject (option);
			}
		}
	}
}
