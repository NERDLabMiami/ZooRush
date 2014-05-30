using UnityEngine;
using System.Collections;

public class EndlessModeChangeAnimalTester : MonoBehaviour, OtherButtonClass
{

		public EndlessSceneManager sceneManager;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void otherButtonAction (Button thisButton)
		{
				sceneManager.introduceAnimal ();
		}
}
