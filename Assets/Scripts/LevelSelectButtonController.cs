using UnityEngine;
using System.Collections;

public class LevelSelectButtonController : MonoBehaviour
{
	public bool go, clicked;
	private bool waitForClickReset;
	private RaycastHit hit; 
	public GameObject left, right, center, back;
	private LevelSelectCameraControls cameraControl;

	void Start ()
	{
		waitForClickReset = false;
		cameraControl = GameObject.FindObjectOfType<LevelSelectCameraControls> ();
	}
	

	void Update ()
	{
		if (!clicked) {
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);
			Debug.DrawRay (ray.origin, ray.direction);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject) {
					if (Input.GetMouseButtonUp (0)) {
						if (hit.transform.gameObject == left && !waitForClickReset) {
							go = false;
							clicked = true;
							cameraControl.moveRight ();
							StartCoroutine (waitToResetClicked ());
						} else {
							if (hit.transform.gameObject == right && !waitForClickReset) {
								go = false;
								clicked = true;
								cameraControl.moveLeft ();
								StartCoroutine (waitToResetClicked ());
							} else {
								if (hit.transform.gameObject == center) {
									go = true;
									clicked = true;
								} else {
									if (hit.transform.gameObject == back) {
										NextSceneHandler.nextLevel ("MainMenu");
									}
								}
							}
						}
					}
				}
			}
		}
	}

	private IEnumerator waitToResetClicked ()
	{
		waitForClickReset = true;
		yield return new WaitForSeconds (0.2f);
		clicked = false;
		waitForClickReset = false;
	}
}
