using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour
{
	private RaycastHit hit; 

	void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if (Input.GetMouseButtonUp (0)) {
				GetComponent<Animator> ().SetTrigger ("Open");
			}
			Debug.Log (hit.transform.name);
		}
		
	}
//	void OnMouseUp ()
//	{
//		GetComponent<Animator> ().SetTrigger ("Open");
//	}

	public void closeStartScreen ()
	{
		GameObject.FindObjectOfType<SceneManager> ().startPressed = true;
		Destroy (transform.parent.gameObject);
	}
}
