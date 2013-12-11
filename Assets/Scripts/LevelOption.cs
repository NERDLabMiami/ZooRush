using UnityEngine;
using System.Collections;

public class LevelOption : MonoBehaviour
{
	public bool activated;
	public bool unlocked;
	
	public string SceneName;
	
	void Start ()
	{
//		activated = false;
//		unlocked = false;
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
		SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer sprite in sprites) {
			if (sprite.name.Equals ("Dimmer")) {
				sprite.renderer.enabled = false;
			}
			if (sprite.name.Contains ("Animal")) {
				sprite.color = Color.white;
			}
		}
	}
	
	private void lockLevel ()
	{
		SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer sprite in sprites) {
			if (sprite.name.Equals ("Dimmer")) {
				sprite.renderer.enabled = true;
			}
			if (sprite.name.Contains ("Animal")) {
				sprite.color = Color.black;
			}
		}
	}
	
	private void ActivateLevel ()
	{
		Application.LoadLevel (SceneName);
	}
	
}
