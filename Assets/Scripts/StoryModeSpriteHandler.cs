using UnityEngine;
using System.Collections;

public class StoryModeSpriteHandler : MonoBehaviour
{
	public Sprite[] sprites;
	private int currentCharacter;
	private string[] characters = {"David", "Lisa", "Christina","Zane"};
	
	public bool isOtherCharacter;
	
	void Awake ()
	{
		if (PlayerPrefs.HasKey ("Character Selected")) {
			string character = PlayerPrefs.GetString ("Character Selected");
			for (int i = 0; i < characters.Length; i++) {
				if (characters [i].Equals (character)) {
					currentCharacter = i;
				}
			}
		} else {
			currentCharacter = 0;
		}
		if (!isOtherCharacter) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [currentCharacter];
		} else {
			int characterIndex = currentCharacter;
			while (characterIndex == currentCharacter) {
				characterIndex = Random.Range (0, characters.Length);
			}
			gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [characterIndex];
		}
	}
	
}
