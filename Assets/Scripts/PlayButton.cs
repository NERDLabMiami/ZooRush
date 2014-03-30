using UnityEngine;
using System.Collections;

public class PlayButton : ButtonOld
{
	public string sceneName;

	protected override void action ()
	{
		Application.LoadLevel (sceneName);
	}
}
