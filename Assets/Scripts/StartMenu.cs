using UnityEngine;
using System.Collections;

/** Controls user interaction with the splash screen.
 *	@author Ebtissam Wahman
 */
public class StartMenu : MonoBehaviour
{
	
	private GameObject[] gameOptions;
	private GameObject currentlySelectedOption;
	Color optionUnselected = Color.white;
	Color optionSelected = Color.yellow;
	
	int currentOption;
	string[] optionNames = {"Play","Options","Other"};
	
	bool mathWait = false;
	
	void Start ()
	{
		currentOption = 0;
		gameOptions = GameObject.FindGameObjectsWithTag ("option");
		foreach (GameObject option in gameOptions) {
			if (option.name.Contains (optionNames [currentOption])) {
				currentlySelectedOption = option;
				currentlySelectedOption.GetComponent<TextMesh> ().color = optionSelected;
			} else {
				option.GetComponent<TextMesh> ().color = optionUnselected;
			}
		}
	}
	
	void FixedUpdate ()
	{
		//Debug.Log (currentOption);
		
		if (Input.mousePresent) {
			//Debug.Log ("MOUSE PRESENT");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D mouseHover = Physics2D.Raycast (ray.origin, ray.direction);
			if (mouseHover.collider != null) {
				//Debug.Log (mouseHover.collider.name);
				foreach (GameObject option in gameOptions) {
					if (mouseHover.collider.name.Equals (option.name)) {
						currentlySelectedOption = option;
						currentlySelectedOption.GetComponent<TextMesh> ().color = optionSelected;
					} else {
						option.GetComponent<TextMesh> ().color = optionUnselected;
					}
				}
				for (int i = 0; i < optionNames.Length; i++) {
					if (optionNames [i].Equals (currentlySelectedOption.name)) {
						currentOption = i;
					}
				}
			}
		} else {
			float yInput = Input.GetAxis ("Vertical");
			if (yInput > 0 && currentOption > 0) { //pushed up button
				if (!mathWait) {
					mathWait = true;
					StartCoroutine (currentOptionChange (true));
				}
			}
			if (yInput < 0 && currentOption < 2) { //pushed down button
				if (!mathWait) {
					mathWait = true;
					StartCoroutine (currentOptionChange (false));
				}
			}
		}
		
		reselectCurrentOption ();
	}
	
	private IEnumerator currentOptionChange (bool substract)
	{	
		if (substract) {
			currentOption = currentOption - 1;
		} else {
			currentOption = currentOption + 1;
		}
		yield return new WaitForSeconds (0.1f);
		mathWait = false;
	}
	
	void reselectCurrentOption ()
	{
		foreach (GameObject option in gameOptions) {
			if (option.name.Contains (optionNames [currentOption])) {
				currentlySelectedOption = option;
				currentlySelectedOption.GetComponent<TextMesh> ().color = optionSelected;
			} else {
				option.GetComponent<TextMesh> ().color = optionUnselected;
			}
		}
	}
}
