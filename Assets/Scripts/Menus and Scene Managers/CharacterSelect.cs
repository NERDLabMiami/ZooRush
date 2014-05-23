using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour, OtherButtonClass
{
		private GameObject[] options;
		public Button leftChar;
		public Button rightChar;

		public SpriteRenderer charSelect;
		public TextMesh charSelectName;
		private string[] characterNames = {"David", "Lisa", "Christina","Zane"};
		private int charMaxIndex;
		public Sprite[] characters;
		private int charIndex;

		void Start ()
		{
				Input.simulateMouseWithTouches = true;
				charMaxIndex = 1; // 0 for david, 1 for lisa

				PlayerPrefs.SetInt (characterNames [0], 1); 
				PlayerPrefs.SetInt (characterNames [1], 1);

				//Set the limit based on which characters are unlocked
				for (int i = 2; i < characterNames.Length; i++) {
						if (PlayerPrefs.HasKey (characterNames [i])) {
								if (PlayerPrefs.GetInt (characterNames [i], 0) > 0) {
										charMaxIndex++;
								}
						}
				}

				charIndex = PlayerPrefs.GetInt ("Character Selected", 0);
				charSelectName.text = characterNames [charIndex];
				charSelect.sprite = characters [charIndex];
		}


		public void otherButtonAction (Button thisButton)
		{

				if (thisButton == leftChar) {
						if (charIndex > 0) {
								PlayerPrefs.SetInt ("Character Selected", --charIndex);
						}
				}
		
				if (thisButton == rightChar) {
						if (charIndex < charMaxIndex) {
								PlayerPrefs.SetInt ("Character Selected", ++charIndex);
						}
				}

				charSelectName.text = characterNames [charIndex];
				charSelect.sprite = characters [charIndex];
		}
}
