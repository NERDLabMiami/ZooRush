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
			currentCharacter = PlayerPrefs.GetInt ("Character Selected");
		} else {
			currentCharacter = 0;
			PlayerPrefs.SetInt ("Character Selected", currentCharacter);
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
