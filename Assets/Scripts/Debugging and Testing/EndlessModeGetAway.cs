using UnityEngine;
using System.Collections;

public class EndlessModeGetAway : OtherButtonClass
{
	public EndlessAnimal animal;
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
		animal.getAway ();
	}
}
