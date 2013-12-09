using UnityEngine;
using System.Collections;

public class LevelOption : MonoBehaviour
{
	public bool activated;
	public bool unlocked;
	
	public string SceneName;
	
	void Start ()
	{
		activated = false;
		unlocked = false;
	}
	
	void Update ()
	{
		if (activated) {
			ActivateLevel ();
		}
		
		if (unlocked) {
			unlockLevel ();
		} else {
			lockLevel ();
		}
	}
	
	private void unlockLevel ()
	{
	
	}
	
	private void lockLevel ()
	{
	
	}
	
	private void ActivateLevel ()
	{
		Application.LoadLevel (SceneName);
	}
	
}
