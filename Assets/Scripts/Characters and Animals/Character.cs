using UnityEngine;
using System.Collections;

/**
 *  Swaps out and intializes Character Grpahics at the start of the level.
 */ 
public class Character : MonoBehaviour
{
	
		public Animator faceSpriteAnimator;
		public Animator spriteAnimator;

		private int currentCharacter;
		private static string[] characterNames = {"David", "Lisa", "Christina","Zane"};
		public string characterName {
				get { 
						currentCharacter = PlayerPrefs.GetInt ("Character Selected", 0);
						return characterNames [currentCharacter];
				}
				set {
						for (int i = 0; i < characterNames.Length; i++) {
								if (characterNames [i].Equals (value)) {
										currentCharacter = i;
										PlayerPrefs.SetInt ("Character Selected", currentCharacter);
										changeCharacter ();
								}
						}
				}
		}

		void Awake ()
		{
				changeCharacter ();
		}

		public void changeCharacter ()
		{
				currentCharacter = PlayerPrefs.GetInt ("Character Selected", 0);

				if (faceSpriteAnimator) {
						faceSpriteAnimator.SetInteger ("Character Value", currentCharacter);
						faceSpriteAnimator.SetTrigger ("Change");
				}

				if (spriteAnimator) {
						spriteAnimator.SetInteger ("Character Value", currentCharacter);
						spriteAnimator.SetTrigger ("Change");
				}
				
		}
}
