using UnityEngine;
using System.Collections;

public class ToggleButtonOld : ButtonOld
{
	public GameObject toggle;
	public bool activated;
	
	public void Deactivate ()
	{
		toggle.SetActive (false);
		activated = false;
	}

	public void Activate ()
	{
		toggle.SetActive (true);
		activated = true;
	}

	protected override void action ()
	{
		if (activated) {
			Deactivate ();
		} else {
			Activate ();
		}
		clicked = false;
	}
}
