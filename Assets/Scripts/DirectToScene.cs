using UnityEngine;
using System.Collections;

public class DirectToScene : ButtonOld
{
	public string sceneName;

	protected override void action ()
	{
		Application.LoadLevel (sceneName);
	}

}
