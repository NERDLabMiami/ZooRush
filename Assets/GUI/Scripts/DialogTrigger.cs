using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour
{
	public bool tutOnly;
	public bool showArrow;
	public SpriteRenderer arrowSprite;
	private bool opened;
	
	public string[] textDisplay;

	void Start ()
	{	
		opened = false;
		arrowSprite.enabled = false;
	}

	void OnEnable ()
	{
		GameState.dialogDismissed += cleanUp;
	}
	
	void OnDisable ()
	{
		GameState.dialogDismissed -= cleanUp;
		
	}

	public void openDialog ()
	{
		if (tutOnly && PlayerPrefs.GetInt ("TUTORIAL", 0) == 1) {
			opened = true;
		}
		if (!opened) {
			if (showArrow) {
				arrowSprite.enabled = true;
			}
			GameObject.FindObjectOfType<DialogBox> ().dialog = textDisplay;
			opened = true;
			GameState.requestDialog ();
		}
	}

	public void cleanUp ()
	{
		if (showArrow) {
			arrowSprite.enabled = false;
		}
	}

}
