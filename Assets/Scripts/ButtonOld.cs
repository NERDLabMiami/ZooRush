using UnityEngine;
using System.Collections;

/** Generic Button Class, used for clicakble objects in the game.
 * Objects are required to have a 2d collider in order to perform proper interaction. 
 * @author Ebtissam Wahman
 */ 
public abstract class ButtonOld : TouchHandler
{
	public static string previousScene;
	protected Color originalColor;
	protected bool clicked;
	protected bool touched;
	protected TextMesh textMesh;

	protected void Start ()
	{
		clicked = false;
		textMesh = GetComponentInChildren<TextMesh> ();
		if (textMesh) {
			originalColor = textMesh.color;
		}
	}

	public override void objectTouched ()
	{
//		if (textMesh) {
//			selectText ();
//		}
		if (!clicked) {
			if (Input.GetMouseButtonUp (0)) {
				previousScene = Application.loadedLevelName;
				clicked = true;
				action ();
			}
		}
	}

	public override void objectUntouched ()
	{
//		if (textMesh) {
//			deselectText ();
//		}
	}
	
	public void selectText ()
	{
		if (textMesh && !textMesh.color.Equals (Color.yellow)) {
			textMesh.color = Color.yellow;
		}
	}

	public void deselectText ()
	{
		if (textMesh && !textMesh.color.Equals (originalColor)) {
			textMesh.color = originalColor;
		}
	}

	protected abstract void action ();

	protected override IEnumerator waitToResetTouch ()
	{
		yield return new WaitForSeconds (0.2f);
		clicked = false;
	}
}
