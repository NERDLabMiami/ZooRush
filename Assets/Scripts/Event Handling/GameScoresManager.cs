using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenKit;
using System;
using Facebook;

public class GameScoresManager : MonoBehaviour
{

		private static GameScoresManager _instance;
		
		public static GameScoresManager instance {
				get {
						if (!GameObject.FindObjectOfType<GameScoresManager> ()) {
								new GameObject ("Game Score Manager", typeof(GameScoresManager));
						}
						if (_instance == null) {
								_instance = GameObject.FindObjectOfType<GameScoresManager> ();
				
								//Tell unity not to destroy this object when loading a new scene!
								DontDestroyOnLoad (_instance.gameObject);
						}
			
						return _instance;
				}
		}
	
		void Awake ()
		{
				if (_instance == null) {
						//If I am the first instance, make me the Singleton
						_instance = this;
						DontDestroyOnLoad (this);
						Setup ();
				} else {
						//If a Singleton already exists and you find
						//another reference in scene, destroy it!
						if (this != _instance)
								Destroy (this.gameObject);
				}
		}

		public void Setup ()
		{
				Debug.Log ("Setup for GameScoresManager Called");
				// Authenticate the local player with GameCenter (iOS only).
				OKManager.authenticateGameCenterLocalPlayer ();
				if (OKManager.IsCurrentUserAuthenticated ()) {
						OKLog.Info ("OK: Found OpenKit user");
				} else {
						ShowLoginUI ();
						OKLog.Info ("OK: Did not find OpenKit user");
				}
		}

		void ShowLoginUI ()
		{
				OKLog.Info ("Showing login UI");
				OKManager.ShowLoginToOpenKitWithDismissCallback (() => {
						OKLog.Info ("OK: Finished showing OpenKit login window, in the callback");
				});

				OKLog.Info ("OK: Is fb session Open: " + OKManager.IsFBSessionOpen ());
		}

		public void showLeaderboards ()
		{
				OKManager.ShowLeaderboards ();
		}

		public void showAchievements ()
		{
				OKManager.ShowAchievements ();
		}

		private enum EndlessLevelLeaderBoardIDs
		{
				Zoo = 2102,
				Suburbs = 2013,
				Beach = 2104,
				Downtown = 2105
		}
		;

		public bool submitEndlessLevelScore (int levelScore, string levelName)
		{
				if (OKUser.GetCurrentUser () == null) {
						Debug.Log ("Player is not logged in.");
				} else {
						Debug.Log ("Player is logged in.");
				}

				Debug.Log ("ENDLESS MODE SCORE SUBMITTAL BEGAN");
				int leaderBoardID = 0;
				string gameCenterCategory = "";
				switch (levelName) {
				case "Endless-Zoo":
						leaderBoardID = (int)EndlessLevelLeaderBoardIDs.Zoo;
						gameCenterCategory = "endless_zoo";
						break;
				case "Endless-Suburbs":
						leaderBoardID = (int)EndlessLevelLeaderBoardIDs.Suburbs;
						gameCenterCategory = "endless_suburb";

						break;
				case "Endless-Beach":
						leaderBoardID = (int)EndlessLevelLeaderBoardIDs.Beach;
						gameCenterCategory = "endless_beach";
						break;
				case "Endless-Downtown":
						leaderBoardID = (int)EndlessLevelLeaderBoardIDs.Downtown;
						gameCenterCategory = "endless_downtown";

						break;
				default:
						Debug.Log ("ERROR: Did not submit score, level name not properly obtained");
						return false;
				}

				OKScore score = new OKScore (levelScore, leaderBoardID);
				score.displayString = score + "Animals Caught";
				score.gameCenterLeaderboardCategory = gameCenterCategory;

				Action<bool, string> nativeHandle = (success, errorMessage) => {
						if (success) {
								OKLog.Info ("Score submitted successfully!");
						} else {
								OKLog.Info ("Score did not submit. Error: " + errorMessage);
						}
				};
		
				score.SubmitScoreNatively (nativeHandle);
				
				return true;
		}
}
