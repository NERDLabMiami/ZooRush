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
		GetComponentInChildren<SpriteRenderer> ().enabled = false;
	}

	public void openDialog ()
	{
		if (!opened) {
			if (showArrow) {
				GetComponentInChildren<SpriteRenderer> ().enabled = true;
			}
			GameObject.FindObjectOfType<Dialog> ().activateDialog (textDisplay);
		}
	}

}
