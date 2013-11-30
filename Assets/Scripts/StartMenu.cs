using UnityEngine;
using System.Collections;

/** Controls user interaction with the splash screen.
 *	@author Ebtissam Wahman
 */
public class StartMenu : MonoBehaviour
{
	public static GameObject currentlySelectedOption;
	int currentOption;
	bool mathWait;
	
	void Start ()
	{
		mathWait = false;
		TextOption[] textOptions = GameObject.FindObjectsOfType<TextOption> ();
		foreach (TextOption textOption in textOptions) {
			textOption.optionEnabled = true;
		}
	}
	
	void FixedUpdate ()
	{
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
}
