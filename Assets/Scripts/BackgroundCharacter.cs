using UnityEngine;
using System.Collections;

public class BackgroundCharacter : MonoBehaviour
{

	public bool anyCharacter;

	private int chosenCharacter;
	private bool left;

	void Awake ()
	{
		chosenCharacter = Random.Range (0, 4);
	}

	void Start ()
	{
		if (anyCharacter) {
			GetComponent<Animator> ().SetInteger ("Character", chosenCharacter);
			GetComponent<Animator> ().SetTrigger ("Change");
		} else {
			while (chosenCharacter  == PlayerPrefs.GetInt ("Character Selected")) {
				chosenCharacter = Random.Range (0, 4);
			}
			GetComponent<Animator> ().SetInteger ("Character", chosenCharacter);
			GetComponent<Animator> ().SetTrigger ("Change");
		}
		if (transform.parent.localScale.x > 0) {
			rigidbody2D.velocity = new Vector2 (8f, 0);
			left = false;
		} else {
			rigidbody2D.velocity = new Vector2 (-8f, 0);
			left = true;
		}
	}

	void OnEnable ()
	{
		GameStateMachine.Paused += OnPause;
		GameStateMachine.PauseToPlay += OnPauseToPlay;
	}
	
	
	void OnDisable ()
	{
		GameStateMachine.Paused -= OnPause;
		GameStateMachine.PauseToPlay -= OnPauseToPlay;
	}

	void OnPause ()
	{
		rigidbody2D.velocity = Vector2.zero;
	}

	void OnPauseToPlay ()
	{
		if (transform.parent.localScale.x > 0) {
			rigidbody2D.velocity = new Vector2 (8f, 0);
		} else {
			rigidbody2D.velocity = new Vector2 (-8f, 0);
		}
	}

	private bool inView ()
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, renderer.bounds);
	}

	void Update ()
	{	
		if (left) {
			if (transform.position.x < Camera.main.transform.position.x && !inView ()) {
				Destroy (transform.parent.gameObject);
			}
		} else {
			if (transform.position.x > Camera.main.transform.position.x && !inView ()) {
				Destroy (transform.parent.gameObject);
			}
		}
	}
}
