using UnityEngine;
using System.Collections;

public class HelpMenu : MonoBehaviour
{
	public CameraFollow camFollower;
	public Character character;

	public HelpMenuSet[] menuSets;

	private HelpMenuSet currentMenu;

	void Start ()
	{
		GameState.requestIntro ();
		camFollower.moveCameraToCharacterOffset (transform.position.x - character.transform.position.x);
		GameState.requestPlay ();
	}

	public void receiveTrigger (HelpButton.HelpButtons helpValue)
	{
		if (helpValue == HelpButton.HelpButtons.main_menu) {
			Application.LoadLevel ("Main Menu");
			return;
		}
		if (currentMenu != null) {
			currentMenu.dismiss ();
			currentMenu.reset ();
		}

		if ((int)helpValue < menuSets.Length && menuSets [(int)helpValue] != null) {
			currentMenu = menuSets [(int)helpValue];
			currentMenu.activate ();
		}
	}
}
