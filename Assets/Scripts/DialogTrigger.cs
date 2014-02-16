using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour
{
	public bool tutOnly;
	private bool opened;
	
	public string[] textDisplay;

	void Start ()
	{	
		opened = false;
	}

	public void openDialog ()
	{
		if (!opened) {
			GameObject.FindObjectOfType<Dialog> ().activateDialog (textDisplay);
		}
	}

}
