using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{
	private GameObject[] Options;
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
		selectedOption = Options [0];
	}

	void Update ()
	{
		foreach (GameObject option in Options) {
			if (InputManager.pointerTouch.collider.Equals (option.collider2D)) {
				option.GetComponent<TextMesh> ().color = Color.yellow;
			} else {
				if (!selectedOption.Equals (option)) {
					option.GetComponent<TextMesh> ().color = Color.white;
				}
			}
		}
	}
}
