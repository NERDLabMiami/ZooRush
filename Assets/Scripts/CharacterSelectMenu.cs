using UnityEngine;
using System.Collections;

public class CharacterSelectMenu : MonoBehaviour
{
	private GameObject[] characterOptions;
	private GameObject currentCharacter;
	private GameObject backButton;
	private int selection;
	private int maxSelectable = 0;
	private GameObject selectedCharacter;
	private string[] characters = {"Main Boy", "Main Girl", "Hispanic Girl"};
	public Sprite[] characterSprites;
	private bool inputDetected = false;
	private bool backSelected = false;
	
	void Start ()
	{
		//Initialize the unlocked characters on first opening
		for (int i = 0; i < characters.Length; i++) {
			if (!PlayerPrefs.HasKey (characters [i])) {
				if (i < 2) {
					PlayerPrefs.SetString (characters [i], "true");
				} else {
					PlayerPrefs.SetString (characters [i], "false");
				}
			}
		}

		//Set the limit based on which characters are unlocked
		for (int i = 0; i < characters.Length; i++) {
			if (PlayerPrefs.GetString (characters [i]).Equals ("true")) {
				maxSelectable++;
			}
		}


		if (!PlayerPrefs.HasKey ("Character Selected")) { //Set up default character
			PlayerPrefs.SetString ("Character Selected", characters [0]);
			selection = 0;
		} else { //Load previous character selection
			string character = PlayerPrefs.GetString ("Character Selected");
			for (int i = 0; i < characters.Length; i++) {
				if (characters [i].Equals (character)) {
					selection = i;
				}
			}
		}

		characterOptions = GameObject.FindGameObjectsWithTag ("option");
		selectedCharacter = GameObject.Find ("Selected Character");
		selectedCharacter.GetComponent<SpriteRenderer> ().sprite = characterSprites [selection];
		backButton = GameObject.Find ("Text - Back");

		foreach (GameObject character in characterOptions) {
			if (character.name.Contains (characters [selection])) {
				currentCharacter = character;
			}
		}
	}
	
	void FixedUpdate ()
	{
		HighlightCurrent ();

		float yInput = Input.GetAxis ("Vertical");
		float xInput = Input.GetAxis ("Horizontal");
		float enter = Input.GetAxis ("Fire1");

		if (yInput > 0 || xInput < 0) { //up\left button pushed
			if (!inputDetected) {
				inputDetected = true;
				StartCoroutine (changeCharacter (true));
			}
		} else {
			if (yInput < 0 || xInput > 0) { //down button pushed
				if (!inputDetected) {
					inputDetected = true;
					StartCoroutine (changeCharacter (false));
				}
			}
		}
		if (enter > 0) {
			PlayerPrefs.SetString ("Character Selected", characters [selection]);
		}
	}

	private void HighlightCurrent ()
	{
		for (int i = 0; i < characters.Length; i++) {
			if (characterOptions [i].Equals (currentCharacter)) {
				SpriteRenderer[] sprites = characterOptions [i].GetComponentsInChildren<SpriteRenderer> ();
				foreach (SpriteRenderer sp in sprites) {
					if (sp.gameObject.name.Equals ("Fill")) {
						sp.color = Color.yellow;
					}
				}
			} else {
				SpriteRenderer[] sprites = characterOptions [i].GetComponentsInChildren<SpriteRenderer> ();
				foreach (SpriteRenderer sp in sprites) {
					if (sp.gameObject.name.Equals ("Fill")) {
						if (PlayerPrefs.HasKey (characters [i])) {
							if (PlayerPrefs.GetString (characters [i]).Equals ("true")) {
								sp.color = Color.white;
							} else {
								sp.color = Color.gray;
							}
						} else {
							sp.color = Color.gray;
						}
					}
					if (sp.gameObject.name.Contains ("Character")) {
						if (PlayerPrefs.HasKey (characters [i])) {
							if (PlayerPrefs.GetString (characters [i]).Equals ("true")) {
								sp.color = Color.white;
							} else {
								sp.color = Color.black;
							}
						} else {
							sp.color = Color.black;
						}
					}
				}
			}
		}
		if (backSelected) {
			backButton.GetComponent<TextMesh> ().color = Color.yellow;
		}
	}

	private IEnumerator changeCharacter (bool up)
	{
		if (up) {
			if (selection > 0) {
				selection--;
				backSelected = false;
			}
		} else {
			if (selection < maxSelectable - 1) {
				selection++;
				backSelected = false;
			} else { //go to back button
				backSelected = true;
			}
		}

		if (backSelected) {
			backButton.GetComponent<TextMesh> ().color = Color.yellow;
		} else {
			backButton.GetComponent<TextMesh> ().color = Color.white;
			foreach (GameObject option in characterOptions) {
				if (option.name.Contains (characters [selection])) {
					currentCharacter = option;
				}
			}
			selectedCharacter.GetComponent<SpriteRenderer> ().sprite = characterSprites [selection];
		}
		yield return new WaitForSeconds (0.3f);
		inputDetected = false;
	}
}
