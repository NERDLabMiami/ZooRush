using UnityEngine;
using System.Collections;

/** Launches A net at the animal being captured.
 * 
 * @author Ebtissam Wahman
 */ 

public class NetLauncher : MonoBehaviour
{
		public AudioClip clip;
		public bool launchEnabled; //Indicates if it is possible to launch a net 
		public Rigidbody2D prefab; //The net prefab that will be instantiated
	
		private Animal animal;	//Pointer to the animal class
		private float speed;	//Speed at which the net will be launched on the x-axis
		private bool firing;	//Indicates if the character is currently firing a net
		private int throwCount; //number of tries that have been made 
		private int currentNetIndex;
		public Rigidbody2D[] nets; //pool of nets to save instatiating while firing
		private Rigidbody2D netInstance;
		private bool endlessMode;

		void Start ()
		{	
				speed = 30f;	
				firing = false;
				throwCount = 0;
				animal = GameObject.FindObjectOfType<Animal> ();
				if (GameObject.FindObjectOfType<EndlessSceneManager> () != null) {
						endlessMode = true;
				}
				currentNetIndex = 0;
				nets = new Rigidbody2D[5];
				for (int i = 0; i < nets.Length; i++) {
						Rigidbody2D net = Instantiate (prefab, (Vector2.zero - 10 * Vector2.up), prefab.transform.rotation) as Rigidbody2D;
						if (endlessMode) {
								net.GetComponent<CircleCollider2D> ().radius = 0.15f;
						}
						nets [i] = net;
				}
		}

		public void resetNets ()
		{
				foreach (Rigidbody2D net in nets) {
						net.transform.position = Vector2.zero - 10 * Vector2.up - 10 * Vector2.right;
						net.transform.rotation = prefab.transform.rotation;
						net.renderer.enabled = true;
				}
		}

		void OnEnable ()
		{
				GameState.StateChanged += OnStateChanged;
		}
	
		void OnDisable ()
		{
				GameState.StateChanged -= OnStateChanged;
		}
	
		private void OnStateChanged ()
		{
				switch (GameState.currentState) {
				case GameState.States.Launch:
						resetThrowCount ();
						break;
				default:
			//NOP;
						break;
				}
		}

		public void throwNet ()
		{
				if (GameState.checkForState (GameState.States.Launch)) {
						if (!firing && throwCount < 3) {
								firing = true;
								fire ();
						}
						if (throwCount >= 3) { //Pauses character momentarily and resets the netthrow count
								StartCoroutine (waitForNetToSettle ());
						}
				}
		}

		private IEnumerator waitForNetToSettle ()
		{
				while (!Mathf.Approximately(netInstance.velocity.x, 0)) {
						Debug.Log ("IN THE WAITING FOR NET LOOP");
						yield return new WaitForFixedUpdate ();
				}

//				if (endlessMode) {
//						GameObject.FindObjectOfType<EndlessAnimal> ().getAway ();
//				}
				if (animal && !animal.caught) {
						animal.getAway ();
						GameState.requestPlay ();
				}
		}

		private IEnumerator resetFiringState ()
		{
				yield return new WaitForSeconds (0.1f);
				firing = false;
		}

		public void resetThrowCount ()
		{
				throwCount = 0;
				resetNets ();

		}

		/** 
	*	Instatiates a new net object and applies a velocity in the +x-direction
	*/
		private void fire ()
		{
//		Debug.Log ("FIRE CALLED");
				GameObject.FindObjectOfType<AudioController> ().objectInteraction (clip);
				netInstance = nets [currentNetIndex];
				netInstance.transform.position = transform.position;
				netInstance.isKinematic = false;
				netInstance.velocity = new Vector2 (speed, 0f);
				throwCount += 1;
				currentNetIndex = (currentNetIndex + 1) % nets.Length;
				StartCoroutine (resetFiringState ());
		}

}
