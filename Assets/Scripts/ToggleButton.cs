using UnityEngine;
using System.Collections;

public class ToggleButton : MonoBehaviour
{

	public GameObject toggle;
	public string propertyChanged;

	void Start ()
	{
	}
	
	void Update ()
	{
	
	}

	void OnMouseUp ()
	{
		if (toggle.activeSelf) {
			toggle.SetActive (false);
		} else {
			toggle.SetActive (true);
		}
	}

	public void Deactivate ()
	{
		toggle.SetActive (false);
	}

	public void Activate ()
	{
		toggle.SetActive (true);
	}

}
