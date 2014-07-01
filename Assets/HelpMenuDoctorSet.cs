using UnityEngine;
using System.Collections;

public class HelpMenuDoctorSet : HelpMenuSet
{
	public Building doctorOffice;
	private Vector3 originalPosition;
	
	void OnEnable ()
	{
		originalPosition = transform.position;
	}
	
	public override void activate ()
	{
		transform.localPosition = Vector3.zero;
	}
	
	public override void dismiss ()
	{
		transform.position = originalPosition;
	}
	
	public override void reset ()
	{
		
	}
}
