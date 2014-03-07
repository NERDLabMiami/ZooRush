using UnityEngine;
using System.Collections;

/** Controller for The Animal Object that handles touches made by the user.
 * 
 * @author Ebtissam Wahman
 */
public class AnimalTouched : TouchHandler
{
	private bool touched; //Indicator for whether the animal has been touched
	private Animal animal; //animal model
	
	void Start ()
	{
		touched = false; //by default the animal starts off as untouched
		animal = GetComponent<Animal> ();
	}

	public override void objectTouched ()
	{
		if (!touched && Input.GetMouseButton (0)) {
			touched = true;
			animal.touched ();
		}
	}

	public override void objectUntouched ()
	{

	}

	protected override IEnumerator waitToResetTouch ()
	{
		yield return new WaitForSeconds (0.2f);
		touched = false;
	}

}
