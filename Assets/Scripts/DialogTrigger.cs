using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour
{
	public bool tutOnly;
	public bool showArrow;
	private bool opened;
	
	public string[] textDisplay;

	void Start ()
	{	
		opened = false;
		if (GetComponentInChildren<SpriteRenderer> () != null) {
			GetComponentInChildren<SpriteRenderer> ().enabled = false;
		}
	}

	public void openDialog ()
	{
		if (!opened) {
			if (showArrow) {
				GetComponentInChildren<SpriteRenderer> ().enabled = true;
			}
			GameObject.FindObjectOfType<Dialog> ().activateDialog (textDisplay);
			opened = true;
		}
	}

	public void cleanUp ()
	{
		if (showArrow) {
			GetComponentInChildren<SpriteRenderer> ().enabled = false;
		}
	}

}
