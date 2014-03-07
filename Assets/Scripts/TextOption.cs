using UnityEngine;
using System.Collections;

/** Handles visuals for text in options.
 * @author: Ebtissam Wahman
 */ 
public class TextOption : TouchHandler
{
	private Color optionUnselected; //Text color when not selected
	private Color optionSelected = Color.yellow; //Text color when selected
	public bool isLevelOption; //Indicator if the option loads a level
	public string levelName; //The name of the scene the option loads
	private bool clicked; //indicates if we've registered a click

	void Start ()
	{
		optionUnselected = GetComponent<TextMesh> ().color; //sets initial color as default color
		clicked = false;
	}
	
	public override void objectTouched ()
	{
		GetComponent<TextMesh> ().color = optionSelected;
		if (!clicked && Input.GetMouseButtonUp (0)) {
			clicked = true;
			if (levelName.Equals ("Reset")) {
				PlayerPrefs.DeleteAll ();
			} else {
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
					if (gameObject.name.Contains ("Resume")) {
						GameStateMachine.requestPlay ();
						Destroy (transform.parent.gameObject);
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
			StartCoroutine (waitToResetTouch ());
		}
	}
	public override void objectUntouched ()
	{
		GetComponent<TextMesh> ().color = optionUnselected;
	}
	protected override IEnumerator waitToResetTouch ()
	{
		yield return new WaitForSeconds (0.2f);
		clicked = false;
	}
	
}
