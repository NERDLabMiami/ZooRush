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

		// Get the list of leaderboards in C# (native unity)
		void GetLeaderboards ()
		{
				OKLeaderboard.GetLeaderboards ((List<OKLeaderboard> leaderboards, OKException exception) => {
			
						if (leaderboards != null) {
								OKLog.Info ("Received " + leaderboards.Count + " leaderboards ");
				
								OKLeaderboard leaderboard = (OKLeaderboard)leaderboards [0];
				
								OKLog.Info ("Getting scores for leaderboard ID: " + leaderboard.LeaderboardID + " named: " + leaderboard.Name);
								leaderboard.GetGlobalScores (1, (List<OKScore> scores, OKException exception2) => {
										if (exception2 == null) {
												OKLog.Info ("Got global scores in the callback");
										}
								});
						} else {
								OKLog.Info ("Error getting leaderboards");
						}
				});
		}

}
