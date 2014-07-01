using UnityEngine;
using System.Collections;

public class HelpMenu : MonoBehaviour
{
	public Character character;

	public HelpMenuSet[] menuSets;

	private HelpMenuSet currentMenu;

	public void receiveTrigger (HelpButton.HelpButtons helpValue)
	{
		if (currentMenu != null) {
			currentMenu.dismiss ();
			currentMenu.reset ();
		}

		if ((int)helpValue < menuSets.Length && menuSets [(int)helpValue] != null) {
			currentMenu = menuSets [(int)helpValue];
			currentMenu.activate ();
		}

//		switch (helpValue) {
//		case HelpButton.HelpButtons.moving:
//			Debug.Log ("MOVING");
//			break;
//		case HelpButton.HelpButtons.catching:
//			Debug.Log ("CATCHING");
//			
//			break;
//		case HelpButton.HelpButtons.infections:
//			Debug.Log ("INFECTIONS");
//			
//			break;
//		case HelpButton.HelpButtons.physician:
//			Debug.Log ("PHYSICIAN");
//			
//			break;
//		case HelpButton.HelpButtons.water:
//			Debug.Log ("WATER");
//			
//			break;
//		case HelpButton.HelpButtons.painkillers:
//			Debug.Log ("PAINKILLERS");
//			
//			break;
//		case HelpButton.HelpButtons.credits:
//			Debug.Log ("CREDITS");
//			
//			break;
//		case HelpButton.HelpButtons.main_menu:
//		default:
//			Debug.Log ("MAIN MENU");
//			break;
//		}
	}
}
