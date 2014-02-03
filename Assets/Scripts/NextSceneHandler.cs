using UnityEngine;
using System.Collections;

public class NextSceneHandler : MonoBehaviour
{
	public static void nextLevel (string levelName)
	{
		LoadLevel.levelToLoad = levelName;
		Application.LoadLevel ("Loading");
	}

	public static void fainted ()
	{
		LevelFailModel.failReason = "Fainted";
		LevelFailModel.levelFailed = Application.loadedLevelName;
		Application.LoadLevel ("LevelFail");
	}

	public static void infected ()
	{
		LevelFailModel.failReason = "Infected";
		LevelFailModel.levelFailed = Application.loadedLevelName;
		Application.LoadLevel ("LevelFail");
	}

	public static void hitByCar ()
	{
		LevelFailModel.failReason = "Hit";
		LevelFailModel.levelFailed = Application.loadedLevelName;
		Application.LoadLevel ("LevelFail");
	}
}
