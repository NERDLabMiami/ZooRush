using UnityEngine;
using System.Collections;

public class NextButton : Button
{

	public bool left;
	public SpriteRenderer buttonSprite;
	private bool waitForTouchReset;

	void OnEnable ()
	{
		waitForTouchReset = false;
	}

	public override void onPressUp ()
	{
		StartCoroutine (animatePress ());
	}
	
	public override void onPressDown ()
	{
		animatePressDown ();
	}

	public override void reset ()
	{
		buttonSprite.color = Color.white;
	}

	private void animatePressDown ()
	{
		buttonSprite.color = Color.grey;
	}
	
	private IEnumerator animatePress ()
	{
		yield return new WaitForSeconds (0.3f);
		
		reset ();
		
		callAction ();
	}

	private void callAction ()
	{
		if (!waitForTouchReset) {
			StartCoroutine (resetTouch ());
			buttonAction ();
		}
	}

	private IEnumerator resetTouch ()
	{
		waitForTouchReset = true;
		yield return new WaitForSeconds (0.3f);
		waitForTouchReset = false;
	}
}
