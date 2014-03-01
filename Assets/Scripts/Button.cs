using UnityEngine;
using System.Collections;

/** Generic Button Class, used for clicakble objects in the game.
 * Objects are requires to have a 3d collider in order to perform proper interaction. 
 */ 
public abstract class Button : MonoBehaviour
{
	public static string previousScene;
	private Color originalColor;
	private RaycastHit hit; 
	protected bool clicked;

	protected void Start ()
	{
		clicked = false;
		if (GetComponent<TextMesh> () != null) {
			originalColor = GetComponent<TextMesh> ().color;
		}
	}
	
	protected void Update ()
	{
		if (!clicked) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject == gameObject) {
					selectText ();
					if (Input.GetMouseButtonUp (0)) {
						previousScene = Application.loadedLevelName;
						clicked = true;
						action ();
					}
				} else {
					deselectText ();
				}
			}
		}
	}

	private void onHoverAndClick ()
	{
		
	}

	private void onClickOnly ()
	{
		if (Input.GetMouseButtonUp (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject == gameObject) {
				}
			}

		}
	}

	public void selectText ()
	{
		if (GetComponent<TextMesh> () != null) {
			GetComponent<TextMesh> ().color = Color.yellow;
		}
	}

	public void deselectText ()
	{
		if (GetComponent<TextMesh> () != null) {
			GetComponent<TextMesh> ().color = originalColor;
		}
	}

	protected abstract void action ();
}
