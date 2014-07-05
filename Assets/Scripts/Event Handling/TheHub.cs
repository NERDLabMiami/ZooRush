using UnityEngine;
using System.Collections;

/**
 * The Hub acts as a main controller, it is the delegate and manager for 
 * all interactions between classes in a game level.
 * 
 */ 
public class TheHub : MonoBehaviour
{
		public bool isEndless;

		public Animal animal;
		public Character character;
		public PainIndicator painIndicator;
		public PainKiller painKiller;
		public PlayerControls playerControls;
		public SceneManager sceneManager;

		public Queue eventQueue = new Queue ();
	
		void OnEnable ()
		{
				GameState.StateChanged += OnStateChanged;
		}
	
		void OnDisable ()
		{
				GameState.StateChanged -= OnStateChanged;
		}

		void Update ()
		{
				while (eventQueue.Count > 0) {
						GameEvent currentEvent = eventQueue.Peek () as GameEvent;
						if (currentEvent.verify ()) {
								currentEvent.action ();
						} else {
								Debug.Log ("ERROR: Event was not activated: " + currentEvent.description ());
						}
				}
		}

		void OnStateChanged ()
		{
				switch (GameState.currentState) {
				case GameState.States.Start:
						break;
				case GameState.States.Intro:
						break;
				case GameState.States.Play:
						break;
				case GameState.States.Dialog:
						break;
				case GameState.States.Pause:
						break;
				case GameState.States.Launch:
						break;
				case GameState.States.Transition:
						break;
				case GameState.States.Win:
						break;
				case GameState.States.Lose:
						break;
				default:
						break;
				}
		}

}
