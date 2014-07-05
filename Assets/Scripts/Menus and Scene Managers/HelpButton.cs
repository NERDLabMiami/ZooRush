using UnityEngine;
using System.Collections;

public class HelpButton : OtherButtonClass
{
	public HelpMenu helpMenu;
	public Button[] buttons;
	public enum HelpButtons
	{
		moving = 0,
		catching,
		infections,
		physician,
		water,
		painkillers,
		credits,
		main_menu,
		COUNT
	}
	;

	private HelpButtons buttonIndex;

	public override void otherButtonAction (Button thisButton)
	{
		buttonIndex = HelpButtons.main_menu;

		for (int i = 0; i < buttons.Length; i++) {
			if (buttons [i] == thisButton) {
				buttonIndex = (HelpButtons)i;
				break;
			}
		}

		helpMenu.receiveTrigger (buttonIndex);
	}
}
