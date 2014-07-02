using UnityEngine;
using System.Collections;

public class HelpMenuWaterSet : HelpMenuSet
{

	public PowerUp[] waterBottles;

	private Vector3 originalPosition;
	
	void OnEnable ()
	{
		originalPosition = transform.position;
	}
	
	public override void activate ()
	{
		transform.parent = Camera.main.transform;
		GameState.requestPause ();
		transform.localPosition = Vector3.zero;
		transform.parent = null;
	}
	
	public override void dismiss ()
	{
		transform.parent = null;
		transform.position = originalPosition;
	}
	
	public override void reset ()
	{
		
	}
}
