using UnityEngine;
using System.Collections;

public class StoryModeSpriteHandler : MonoBehaviour
{
	public Sprite[] sprites;
	private int currentCharacter;
	public bool isOtherCharacter;
	
	void Awake ()
	{
		changeSprites ();
	}

	public void changeSprites ()
	{
		currentCharacter = PlayerPrefs.GetInt ("Character Selected", 0);
		
		if (!isOtherCharacter) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [currentCharacter];
		} else {
			int characterIndex = currentCharacter;
			while (characterIndex == currentCharacter) {
				characterIndex = Random.Range (0, sprites.Length);
			}
			gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [characterIndex];
		}
	}
	
}
