using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{
	private GameObject[] optionsObjects;
	private GameObject volume;
	private GameObject music;
	private GameObject sound;
	private GameObject characterChange;
	private GameObject back;
	
	private GameObject currentOption;
	int currentOptionIndex;
	
	/** The Option Selection Algorithm:
	 *	Step 1: Highlight all confirmed choices (ON/OFF, slider position)
	 *			- Go through each optionObject
	 *			- In each option, check PlayerPrefs for current value
	 *			- Highlight the corresponding button based on current value
	 *	Step 2: Highlight current menu option
	 			Mouse/Touch Input:
	 			- Check if the mouse is currently over a game option
	 			
	 			Keyboard Input:
	 			- Check the currently selected game option's index 
	 *	Step 3: 
	 *
	 */
	
	void Start ()
	{
		if (!PlayerPrefs.HasKey ("Music")) { // set default values for music and sound
			PlayerPrefs.SetString ("Music", "ON");
		}
		
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetString ("Sound", "ON");
		}
		if (!PlayerPrefs.HasKey ("Volume")) {
			PlayerPrefs.SetFloat ("Volume", 1.0f);
		}
		
		if (!PlayerPrefs.HasKey ("Current Option")) {
			PlayerPrefs.SetInt ("Current Option", 0);
		}
		
		volume = GameObject.Find ("Volume");
		music = GameObject.Find ("Music");
		sound = GameObject.Find ("Sound");
		characterChange = GameObject.Find ("Change Character");
		back = GameObject.Find ("Back");
		
		optionsObjects = new GameObject[] {volume,music,sound,characterChange,back};
		
		currentOptionIndex = PlayerPrefs.GetInt ("Current Option");
	}
	

	void Update ()
	{
		//STEP 1:
		foreach (GameObject option in optionsObjects) { // Displays the current values for Game optioons 
			//TODO Put Volume slider code here
			if (option.name.Equals ("Music") || option.name.Equals ("Sound")) {
				TextMesh[] textMeshes = option.GetComponentsInChildren<TextMesh> ();
				string value = PlayerPrefs.GetString (option.name); // Get the settings value for this option
				foreach (TextMesh text in textMeshes) { // go through the text objects
					if (text.text.Equals (value)) {
						text.color = Color.yellow;
					} else {
						text.color = Color.white;
					}
				}
			}
		}

		//STEP 2:
		//MOUSE/TOUCH INPUT:
		if (InputManager.touching) {
			GameObject selectedObject = InputManager.pointerTouch.collider.gameObject;
			if (currentOption != null) {
				if (currentOption.GetComponent<SpriteRenderer> () != null) {
					currentOption.GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
			currentOption = selectedObject;
			if (currentOption.GetComponent<SpriteRenderer> () != null) {
				currentOption.GetComponent<SpriteRenderer> ().enabled = true;
			}
		}
		//KEYBOARD INPUT:
		if (InputManager.up || InputManager.down) {

		}
	}
	
	
}
