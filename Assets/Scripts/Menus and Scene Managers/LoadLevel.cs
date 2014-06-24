using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{

		public static string levelToLoad;
		public Animator animalAnimator;
//	private bool loadingLevel;
		private int animalValue;

		private enum AnimalValues
		{
				Bear,
				Cheetah,
				Crocodile,
				Elephant,
				Flamingo,
				Gorilla,
				Ostritch,
				Penguin,
				Rhino,
				Tortoise
	}
		;
	
		void Start ()
		{
//		loadingLevel = false;
				switch (levelToLoad) {
				case "Level1-Tutorial":
						animalValue = 9;
						break;
				case "Level2-Zoo":
						animalValue = 2;
						break;
				case "Level3-Suburbs":
						animalValue = 4;
						break;
				case "Level4-Suburbs":
						animalValue = 8;
						break;
				case "Level5-Suburbs":
						animalValue = 7;
						break;
				case "Level6-Beach":
						animalValue = 5;
						break;
				case "Level7-Beach":
						animalValue = 6;
						break;
				case "Level8-Downtown":
						animalValue = 3;
						break;
				case "Level9-Downtown":
						animalValue = 1;
						break;
				case "Level10-Downtown":
						animalValue = 0;
						break;
				default:
						break;
				}

				animalAnimator.SetInteger ("Animal", animalValue);
				animalAnimator.SetTrigger ("Change");
				animalAnimator.rigidbody2D.velocity = new Vector2 (6f, 0);
//		StartCoroutine (loadLevel ());
		}


		private IEnumerator loadLevel ()
		{
				AsyncOperation async = Application.LoadLevelAsync (levelToLoad);
				yield return async;
				Debug.Log ("Loading complete");
		}
}
