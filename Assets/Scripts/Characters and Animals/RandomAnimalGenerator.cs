using UnityEngine;
using System.Collections;

public class RandomAnimalGenerator : MonoBehaviour
{

		public GameObject animalPrefab;
		private GameObject animal;
		private Animator animator;
		private bool left;

		void Start ()
		{
				left = transform.localScale.x < 0;
				animal = Instantiate (animalPrefab, transform.position, Quaternion.identity) as GameObject;
				animator = animal.GetComponent<Animator> ();
				animator.SetInteger ("Animal", Random.Range (0, 10));
				animator.SetTrigger ("Change");
				if (!left) {
						animal.transform.localScale = new Vector3 (
				animal.transform.localScale.x * -1, 
				animal.transform.localScale.y, 
				animal.transform.localScale.z);
				}
				animal.rigidbody2D.velocity = new Vector2 (((left) ? -1 : 1) * 2f, 0);
		}
	
		void FixedUpdate ()
		{
				if (pastCameraPosition () && !inView (animal.renderer)) {
						if (left) {
								animal.transform.position = new Vector3 (
					animal.transform.position.x + Camera.main.transform.position.x + Random.Range (25f, 30f),
					animal.transform.position.y,
					animal.transform.position.z);
						} else {
								animal.transform.position = new Vector3 (
					animal.transform.position.x - (Camera.main.transform.position.x + Random.Range (25f, 30f)),
					animal.transform.position.y,
					animal.transform.position.z);
						}
						animator.SetInteger ("Animal", Random.Range (0, 10));
						animator.SetTrigger ("Change");
				}

		}

		private bool pastCameraPosition ()
		{
				if (left) {
						return animal.transform.position.x < Camera.main.transform.position.x;
				}
				return animal.transform.position.x > Camera.main.transform.position.x;
		}

		private bool inView (Renderer obj)
		{
				Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
				return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
		}
	
}
