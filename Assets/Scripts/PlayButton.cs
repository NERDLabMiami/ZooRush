using UnityEngine;
using System.Collections;

public class PlayButton : Button
{
	public string sceneName;

	protected override void action ()
	{
		Application.LoadLevel (sceneName);
	}
}
