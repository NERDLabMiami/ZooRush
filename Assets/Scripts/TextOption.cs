using UnityEngine;
using System.Collections;

/** Handles visuals for text in options.
 * @author: Ebtissam Wahman
 */ 
public class TextOption : MonoBehaviour
{
	private Color optionUnselected; //Text color when not selected
	private Color optionSelected = Color.yellow; //Text color when selected
	public bool optionEnabled; //Indicator if the text is selectable
	public bool isLevelOption; //Indicator if the option loads a level
	public string levelName; //The name of the scene the option loads

	void Start ()
	{
		optionUnselected = GetComponent<TextMesh> ().color; //sets initial color as default color
	}

	void OnMouseEnter ()
	{	
		if (optionEnabled) {
			GetComponent<TextMesh> ().color = optionSelected;
		}

	}

	void OnMouseExit ()
	{
		if (optionEnabled) {
			GetComponent<TextMesh> ().color = optionUnselected;
		}
	}

	void OnMouseUp ()
	{
		if (optionEnabled) {
			if (isLevelOption) {
				if (gameObject.name.Contains ("Retry")) {
					NextSceneHandler.nextLevel (Application.loadedLevelName);
				} else {
					if (gameObject.name.Contains ("Quit") || gameObject.name.Contains ("Main")) {
						Application.LoadLevel ("Splash");
					} else {
						if (gameObject.name.Contains ("Next")) {
							NextSceneHandler.nextLevel (levelName);
						} 
					}
				}
				
			} else {
//				if (gameObject.name.Contains ("Play")) {
//					if (PlayerPrefs.HasKey ("Levels Unlocked")) {
//						if (PlayerPrefs.GetInt ("Levels Unlocked") > 1) {
//							Application.LoadLevel ("Level Select");
//						} else {
//							StoryModeHandler.NextSceneName = "Level1-Tutorial";
//							Application.LoadLevel ("IntroScene");
//						}
//					} else {
//						StoryModeHandler.NextSceneName = "Level1-Tutorial";
//						Application.LoadLevel ("IntroScene");
//					}
//				}
//				if (gameObject.name.Contains ("Options")) {
//					PlayerPrefs.SetString ("Last Scene", Application.loadedLevelName);
//					Application.LoadLevel ("Game Options");
//				}

				if (gameObject.name.Contains ("Back")) {
					if (PlayerPrefs.HasKey ("Last Scene") && !PlayerPrefs.GetString ("Last Scene").Equals (Application.loadedLevelName)) {
						Application.LoadLevel (PlayerPrefs.GetString ("Last Scene"));
					} else {
						Application.LoadLevel ("Splash");
					}
				}
			}
		}

	}
	
}
