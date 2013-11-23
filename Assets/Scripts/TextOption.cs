using UnityEngine;
using System.Collections;

public class TextOption : MonoBehaviour
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
			Application.LoadLevel ("Game Options");

		}

		if (gameObject.name.Contains ("Back")) {
			if (PlayerPrefs.HasKey ("Last Scene") && !PlayerPrefs.GetString ("Last Scene").Equals (Application.loadedLevelName)) {
				Application.LoadLevel (PlayerPrefs.GetString ("Last Scene"));
			} else {
				Application.LoadLevel ("Splash");
			}
		}

	}
	
}
