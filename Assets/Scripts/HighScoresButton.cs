using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenKit;
using System;
using Facebook;

public class HighScoresButton : OtherButtonClass
{

		void Start ()
		{
				Setup ();
		}

		void Setup ()
		{
				// Authenticate the local player with GameCenter (iOS only).
				OKManager.authenticateGameCenterLocalPlayer ();
				if (OKManager.IsCurrentUserAuthenticated ()) {
						OKLog.Info ("Found OpenKit user");
				} else {
						ShowLoginUI ();
						OKLog.Info ("Did not find OpenKit user");
				}
		}

		void ShowLoginUI ()
		{
				OKLog.Info ("Showing login UI");
				OKManager.ShowLoginToOpenKitWithDismissCallback (() => {
						OKLog.Info ("Finished showing OpenKit login window, in the callback");
				});
		
				OKLog.Info ("Is fb session Open: " + OKManager.IsFBSessionOpen ());
		}

		public override void otherButtonAction (Button thisButton = null)
		{
				Debug.Log ("CLUCK");
				OKManager.ShowLeaderboards ();
		}		

}
