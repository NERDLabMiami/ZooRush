using UnityEngine;
using System.Collections;

public class GUILevelFailedMenuController : MonoBehaviour
{
		public static string failReason;
		public TextMesh title;
		
		public TextTransitionAutomatic textTransitioner;
		private string[] titleOptions = {	
		"Sickle Cell Crisis", 
		"Infection!",
		"It Got Away!",
		"Ouch! Watch Out Next Time!"
	};
		

		public void activate ()
		{
				switch (failReason) {
				case "Fainted":
						title.text = titleOptions [0];
						break;
				case "Infected":
						title.text = titleOptions [1];
						break;
				case "TimeOut":
						title.text = titleOptions [2];
						break;
				default:
						title.text = titleOptions [3];
						break;
				}
				textTransitioner.activate ();
				BreadCrumbs.nextScene = Application.loadedLevelName;
				if (AudioModel.sound) {
						Debug.Log ("Playing Audio");
						audio.Play ();
				}
		}

}
