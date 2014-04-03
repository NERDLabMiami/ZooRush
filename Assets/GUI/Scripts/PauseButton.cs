using UnityEngine;
using System.Collections;

public class PauseButton : OtherButtonClass
{
	private SpriteRenderer[] renderers;
	private LevelGUIController guiControls;
	private bool visible;

	void Start ()
	{
		visible = true;
		renderers = GetComponentsInChildren<SpriteRenderer> ();
		guiControls = GameObject.FindObjectOfType<LevelGUIController> ();
	}
	
	void Update ()
	{
		visible = hideShowButton (GameState.checkForState (GameState.States.Play));
	}

	private bool hideShowButton (bool show)
	{
		if (show) { //display sprites
			if (renderers [0].enabled != true) {
				foreach (SpriteRenderer sprite in renderers) {
					sprite.enabled = true;
				}
			}
			return true;
		} else { //hide sprites
			if (renderers [0].enabled == true) {
				foreach (SpriteRenderer sprite in renderers) {
					sprite.enabled = false;
				}
			}
			return false;
		}
	}

	public override void otherButtonAction (Button thisButton)
	{
		if (visible) {
			guiControls.pauseMenu ();
		}
	}
}
