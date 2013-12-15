using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{

	public static string levelToLoad;
	private bool loadingLevel;
	
	void Start ()
	{
		loadingLevel = false;
	}
	
	void Update ()
	{
		if (!loadingLevel) {
			loadingLevel = true;
			Application.LoadLevel (levelToLoad);
		}
	}
}
