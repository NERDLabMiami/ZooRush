using UnityEngine;
using System.Collections;

public class EndlessModeEndMenu : MonoBehaviour
{
		public EndlessSceneManager sceneManager;
		public EndlessAnimalCountIndicator animalCounter;
		public TextMesh menuTitleText;

		public void activate ()
		{
				menuTitleText.text = string.Format ("The {0} got away!", sceneManager.currentAnimal.ToString ());
				animalCounter.activate ();
		}
}
