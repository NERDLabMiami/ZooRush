using UnityEngine;
using System.Collections;

public class EndlessModeChangeAnimalTester : MonoBehaviour, OtherButtonClass
{

		public EndlessSceneManager sceneManager;

		public void otherButtonAction (Button thisButton)
		{
				sceneManager.introduceAnimal ();
		}
}
