using UnityEngine;
using System.Collections;

public class DirectToScene : MonoBehaviour
{
	public string sceneName;

	void OnMouseUp ()
	{
		Application.LoadLevel (sceneName);
	}
}
