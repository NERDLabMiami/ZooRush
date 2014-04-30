using UnityEngine;
using System.Collections;

public class EndlessModeChangeAnimalTester : OtherButtonClass
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

	public override void otherButtonAction (Button thisButton)
	{
		sceneManager.introduceAnimal ();
	}
}
