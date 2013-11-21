using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour
{
	Color optionUnselected = Color.white;
	Color optionSelected = Color.yellow;
	
	void OnMouseEnter ()
	{	
		GetComponent<TextMesh> ().color = optionSelected;

	}

	void OnMouseExit ()
	{
		GetComponent<TextMesh> ().color = optionUnselected;
	}

	void OnMouseUp ()
	{
		if (gameObject.name.Contains ("Options")) {
			PlayerPrefs.SetString ("Last Scene", Application.loadedLevelName);
			Application.LoadLevel ("Options");

		}

		if (gameObject.name.Contains ("Back")) {
			if (PlayerPrefs.HasKey ("Last Scene") && !PlayerPrefs.GetString ("Last Scene").Equals (Application.loadedLevelName)) {
				Application.LoadLevel (PlayerPrefs.GetString ("Last Scene"));
			} else {
				Application.LoadLevel ("Splash");
			}
		}

		if (gameObject.name.Contains ("Change Character")) {
			PlayerPrefs.SetString ("Last Scene", Application.loadedLevelName);
			Application.LoadLevel ("Character Select");

		}
	}
	
}
