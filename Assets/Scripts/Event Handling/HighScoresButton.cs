using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenKit;
using System;
using Facebook;

public class HighScoresButton : OtherButtonClass
{
		public override void otherButtonAction (Button thisButton = null)
		{
				Debug.Log ("CLUCK");
				OKManager.ShowLeaderboards ();
		}		
}
