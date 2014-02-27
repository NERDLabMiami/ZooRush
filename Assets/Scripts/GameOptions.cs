using UnityEngine;
using System.Collections;
using System.Globalization;

/** Handler for Game Options Menu.
 * @author: Ebtissam Wahman
 */ 

public class GameOptions : MonoBehaviour
{	
	private GameObject[] options;
	
	public GameObject MusicButton;
	public GameObject SoundButton;
	private GameObject Back;
	
	private SpriteRenderer charSelect;
	private string[] characterNames = {"David", "Lisa", "Christina","Zane"};
	public Sprite[] characters;
	private int charIndex;
	private int charMaxIndex = 0;
	
	private bool musicValue;
	private bool soundValue;
	
	
	void Start ()
	{

		Back = GameObject.Find ("Text - Back");
		charSelect = GameObject.Find ("Sprite - Character").GetComponent<SpriteRenderer> ();

		//Enable all Text Options
		TextOption[] textOptions = GameObject.FindObjectsOfType<TextOption> ();
		foreach (TextOption textOption in textOptions) {
			textOption.optionEnabled = true;
		}

		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetInt ("Music", 1);
		}
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetInt ("Sound", 1);
		}
		
		if (!PlayerPrefs.HasKey ("Character Selected")) { //Set up default character
			PlayerPrefs.SetInt ("Character Selected", 0);
		} 
		
		//Set the limit based on which characters are unlocked
		for (int i = 0; i < characterNames.Length; i++) {
			if (PlayerPrefs.HasKey (characterNames [i])) {
				if (PlayerPrefs.GetString (characterNames [i]).Equals ("true")) {
					charMaxIndex++;
				}
			} else {
				if (i > 1) {
					PlayerPrefs.SetString (characterNames [i], "false");
				} else {
					PlayerPrefs.SetString (characterNames [i], "true");
					charMaxIndex++;
				}
			}
		}
		
		musicValue = (PlayerPrefs.GetInt ("Music") == 1);
		soundValue = (PlayerPrefs.GetInt ("Sound") == 1);

		if (musicValue) {
			MusicButton.GetComponent<ToggleButton> ().Activate ();
		} else {
			MusicButton.GetComponent<ToggleButton> ().Deactivate ();
		}
		
		if (soundValue) {
			SoundButton.GetComponent<ToggleButton> ().Activate ();
		} else {
			SoundButton.GetComponent<ToggleButton> ().Deactivate ();
		}
		
		options = new GameObject[] {MusicButton, SoundButton, charSelect.gameObject, Back};
	}

	void FixedUpdate ()
	{
		if (PlayerPrefs.GetInt ("Music") != 0) {
			audio.mute = false;
		} else {
			audio.mute = true;
		}
		musicValue = (PlayerPrefs.GetInt ("Music") == 1);
		soundValue = (PlayerPrefs.GetInt ("Sound") == 1);

		PlayerPrefs.SetInt ("Music", MusicButton.GetComponent<ToggleButton> ().activated ? 1 : 0);
		PlayerPrefs.SetInt ("Sound", SoundButton.GetComponent<ToggleButton> ().activated ? 1 : 0);

	}

	void Update ()
	{
		for (int i = 0; i < charMaxIndex; i++) {
			if (PlayerPrefs.GetInt ("Character Selected") == i) {
				charSelect.sprite = characters [i];
				charIndex = i;
			}
		}
		
		foreach (GameObject option in options) {
			if (InputManager.touching && InputManager.pointerTouch.collider.name.Equals (option.name)) {
				if (InputManager.enter) {
					changeValue (option);
				}
			}
		}
		
	}
	
	void changeValue (GameObject option)
	{
//		if (option.name.Contains ("Music")) {
//			if (musicValue.Equals ("ON")) {
//				PlayerPrefs.SetString ("Music", "OFF");
//			} else {
//				PlayerPrefs.SetString ("Music", "ON");
//			}
//		} else {
//			if (option.name.Contains ("Sound")) {
//				if (soundValue.Equals ("ON")) {
//					PlayerPrefs.SetString ("Sound", "OFF");
//				} else {
//					PlayerPrefs.SetString ("Sound", "ON");
//				}
//			} else {
		if (option.name.Contains ("Character")) {
			if (charIndex == charMaxIndex - 1) {
				PlayerPrefs.SetInt ("Character Selected", 0);
			} else {
				PlayerPrefs.SetInt ("Character Selected", charIndex + 1);
			}
		}
//			}
//		}
	}
}
