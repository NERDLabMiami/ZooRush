using UnityEngine;
using System.Collections;

public class CharacterSelectMenu : MonoBehaviour
{
	private GameObject[] characterOptions;
	private int selection;
	private GameObject selectedCharacter;
	private string[] characterFileNames = {"BoyZoo_0", "GirlZoo_0", "GirlZooHispanic_0"};
	
	/** Character Selection Values:
	 *	0 - BoyZoo
	 *	1 - GirlZoo
	 *	2 - GirlZooHispanic
	 */
	
	void Start ()
	{
		selection = PlayerPrefs.GetInt ("Character Selection");
		characterOptions = GameObject.FindGameObjectsWithTag ("option");
		selectedCharacter = GameObject.Find ("Selected Character");
		foreach (GameObject character in characterOptions) {
			string characterName = character.name.Substring (character.name.IndexOf ('-') + 2);
			if (PlayerPrefs.HasKey (characterName)) {
				if (PlayerPrefs.GetString (characterName).Equals ("true")) {
					SpriteRenderer[] renderers = character.GetComponentsInChildren<SpriteRenderer> ();
					foreach (SpriteRenderer show in renderers) {
						show.color = Color.white;
					}
				}
			}
		}
	}
	
	void FixedUpdate ()
	{
		float yInput = Input.GetAxis ("Vertical");
		if (yInput > 0) { //up button pushed
			//TODO Add Menu traversal
		} else {
			if (yInput < 0) { //down button pushed
				//TODO Add Menu traversal//TODO Add Menu traversal
			}
		}
	}
}
