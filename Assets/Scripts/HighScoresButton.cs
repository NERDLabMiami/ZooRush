using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenKit;
using System;
using Facebook;

public class HighScoresButton : MonoBehaviour, OtherButtonClass
{
		public void otherButtonAction (Button thisButton = null)
		{
				Debug.Log ("CLUCK");
				OKManager.ShowLeaderboards ();
		}		
}
