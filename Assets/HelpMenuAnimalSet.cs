using UnityEngine;
using System.Collections;

public class HelpMenuAnimalSet : HelpMenuSet
{
		public AnimalStory animal;

		void OnEnable ()
		{
				animal.speed = Vector2.zero;
		}

		public override void activate ()
		{
				transform.parent = Camera.main.transform;
				transform.localPosition = Vector3.zero;
				animal.setSpeed ();
				GameState.requestLaunch ();
				transform.parent = null;
				
		}
	
		public override void dismiss ()
		{
				transform.position = originalPosition;
		}
	
		public override void reset ()
		{

		}

		void Update ()
		{
				if (animal.caught) {
						Debug.Log ("ANIMAL CAUGHT, START DISSMISSAL");
				}
		}
}