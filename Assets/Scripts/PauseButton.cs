using UnityEngine;
using System.Collections;

public class PauseButton : Button
{
	private SpriteRenderer[] renderers;
	private bool visible;

	new void Start ()
	{
		base.Start ();
		visible = true;
		renderers = GetComponentsInChildren<SpriteRenderer> ();
	}
	
	new void Update ()
	{
		base.Update ();
		visible = hideShowButton (GameStateMachine.currentState == (int)GameStateMachine.GameState.Play);
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

	protected override void action ()
	{
		if (visible) {
			GameObject.FindObjectOfType<LevelGUIController> ().pauseMenu ();
		}
		clicked = false;
	}
}
