using UnityEngine;
using System.Collections;

public class RectangleButton : Button
{
	public SpriteRenderer buttonSprite;
	public Sprite clickedSprite;
	public TextMesh buttonText;
	private Sprite unclicked;
	private bool waitForTouchReset;
	private bool hasText;

	private Color unclickedTextColor;
	private Color clickedTextColor = Color.yellow;
		
	void Start ()
	{
		unclicked = buttonSprite.sprite;
		waitForTouchReset = false;
		hasText = buttonText != null;
		if (hasText) {
			unclickedTextColor = buttonText.color;
		}
	}

	public override void reset ()
	{
		buttonSprite.sprite = unclicked;
		if (hasText) {
			buttonText.color = unclickedTextColor;
		}
	}

	public override void onPressUp ()
	{
		StartCoroutine (animatePress ());
	}

	public override void onPressDown ()
	{
		animatePressDown ();
	}

	private void animatePressDown ()
	{
		buttonSprite.sprite = clickedSprite;
		if (hasText) {
			buttonText.color = clickedTextColor;
		}
	}
		
	private IEnumerator animatePress ()
	{
		yield return new WaitForSeconds (0.15f);
				
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
		yield return new WaitForSeconds (0.15f);
		waitForTouchReset = false;
	}
		
}
