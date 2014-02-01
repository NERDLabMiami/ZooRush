using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour
{

	void OnMouseUp ()
	{
		GetComponent<Animator> ().SetTrigger ("Open");
	}

	public void closeStartScreen ()
	{
		GameObject.FindObjectOfType<SceneManager> ().startPressed = true;
		Destroy (transform.parent.gameObject);
	}
}
