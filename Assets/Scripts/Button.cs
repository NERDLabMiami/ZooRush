using UnityEngine;
using System.Collections;

public abstract class Button : MonoBehaviour
{
	public static string previousScene;
	private Color originalColor;
	private RaycastHit hit; 
	protected bool clicked;

	void Start ()
	{
		clicked = false;
		if (GetComponent<TextMesh> () != null) {
			originalColor = GetComponent<TextMesh> ().color;
		}
	}
	
	void Update ()
	{
		Debug.Log ("WORKING");
		if (!clicked) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject == gameObject) {
					selectText ();
					Debug.Log ("OOO");
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
