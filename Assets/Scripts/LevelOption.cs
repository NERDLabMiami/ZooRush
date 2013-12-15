using UnityEngine;
using System.Collections;

public class LevelOption : MonoBehaviour
{
	public bool activated;
	public bool unlocked;
	
	public string SceneName;
	
	public Sprite[] starImages;
	
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
		setStars ();
		foreach (SpriteRenderer sprite in sprites) {
			if (sprite.name.Equals ("Dimmer")) {
				sprite.renderer.enabled = false;
			}
			if (sprite.name.Equals ("lock")) {
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
			if (sprite.name.Equals ("lock")) {
				sprite.renderer.enabled = true;
			}
			if (sprite.name.Contains ("Animal")) {
				sprite.color = Color.black;
			}
		}
	}
	
	private void setStars ()
	{
		int stars = ((PlayerPrefs.HasKey (SceneName + "Stars")) ? PlayerPrefs.GetInt (SceneName + "Stars") : 0);
		
		if (stars > 0) {//# of stars > 0
			SpriteRenderer[] starShapes = GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer star in starShapes) {
				if (star.name.Contains ("Star 1")) {
					if (stars >= 1) {
						star.sprite = starImages [1];
					} else {
						star.sprite = starImages [0];
					}
				}
				if (star.name.Contains ("Star 2")) {
					if (stars >= 2) {
						star.sprite = starImages [1];
					} else {
						star.sprite = starImages [0];
					}
				}
				if (star.name.Contains ("Star 3")) {
					if (stars >= 3) {
						star.sprite = starImages [1];
					} else {
						star.sprite = starImages [0];
					}
				}
			}
		}
	}
	
	private void ActivateLevel ()
	{
		Application.LoadLevel (SceneName);
	}
	
}
