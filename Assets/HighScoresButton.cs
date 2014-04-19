using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenKit;
using System;
using Facebook;

public class HighScoresButton : OtherButtonClass
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public override void otherButtonAction (Button thisButton = null)
	{
		Debug.Log ("CLUCK");
		GetLeaderboards ();
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
