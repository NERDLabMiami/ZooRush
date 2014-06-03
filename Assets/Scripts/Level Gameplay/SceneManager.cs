using UnityEngine;
using System.Collections;

public abstract class SceneManager : MonoBehaviour
{
		public string NextSceneName; 		//Filename of the next scene 

		
		public bool timedOut;
		public float targetTimeVar;
		public float multiplier1;
		public float multiplier2;
	
		public Animal animalController;
		public GameObject characterObject;
		public GameObject animalObject;
		public CameraFollow cameraFollow;
	
		public bool isPlaying;
		public bool pauseAudio;
		protected bool tutEnabled;
		public bool fainted;

		void OnEnable ()
		{
				GameState.StateChanged += OnStateChanged;
		}
	
		void OnDisable ()
		{
				GameState.StateChanged -= OnStateChanged;
		}

		protected abstract void OnStateChanged ();


		public void updatePillCount (int pillCount)
		{
				string theCount = "x" + pillCount;
				TextMesh pillCountText = GameObject.Find ("Pill Count").GetComponentInChildren<TextMesh> ();
				pillCountText.text = theCount;
		}

}
