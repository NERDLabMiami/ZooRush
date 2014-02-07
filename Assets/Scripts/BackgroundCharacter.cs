using UnityEngine;
using System.Collections;

public class BackgroundCharacter : MonoBehaviour
{

	public bool anyCharacter;

	private int chosenCharacter;

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
		}

		rigidbody2D.velocity = new Vector2 (8f, 0);

	}
	
	void Update ()
	{
		if (transform.position.x > Camera.main.transform.position.x + 25f) {
			Destroy (transform.parent.gameObject);
		}
	}
}
