using UnityEngine;
using System.Collections;

public class DirectToScene : Button
{
	public string sceneName;

	protected override void action ()
	{
		Application.LoadLevel (sceneName);
	}

}
