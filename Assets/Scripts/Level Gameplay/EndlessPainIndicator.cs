﻿using UnityEngine;
using System.Collections;

public class EndlessPainIndicator : PainIndicator
{
	
		void Start ()
		{
				waiting = false;
				audioController = GameObject.FindObjectOfType<AudioController> ();
				sceneManager = GameObject.FindObjectOfType<EndlessSceneManager> ();
				animator = GetComponent<Animator> ();
				painPoints = 0f;
				painRate = 3.5f;
				//healthFaces = GameObject.FindObjectOfType<PlayerControls> ().faceIcons;
				sprite = GetComponent<SpriteRenderer> ();
//				sprite.sprite = healthFaces [0];
		}
	
		void FixedUpdate ()
		{
				if (GameState.checkForState (GameState.States.Play) || GameState.checkForState (GameState.States.Launch)) {
						painPoints += (Time.deltaTime * painRate);
				}
		}
	
		void Update ()
		{
				if (painPoints >= 100f) {
//						Debug.Log ("OWWWWWWWW!!!!");
						painPoints = 100f;
						sceneManager.fainted = true;
						StartCoroutine (((EndlessSceneManager)sceneManager).callEndMenu (true));
				}
				if (painPoints < 0) {
						painPoints = 0;
				}
				if (animator) {
						animator.SetFloat ("Pain", painPoints);
				}
//				animator.SetBool ("Playing", true);

		
				//Change face sprite and/or play crisis music
//				if (!waiting) {
//						if (painPoints < 33f) { // Change to normal face
//								changeSprite (0);
//						}
//						if (painPoints > 33f && painPoints < 75f) { // Change to discomfort face
//								changeSprite (1);
//						}
//						if (painPoints > 75f) { // Change to pain face
//								changeSprite (2);
//								audioController.objectInteraction (clip);
//						}
//				}
		}
	
		public override void subtractPoints (int points)
		{
				painPoints -= points;
				temporarySpriteChange (0);
		}
	
		public override void addPoints (int points)
		{
				painPoints += points;
				temporarySpriteChange (2);
		}
	
		public override void objectInteraction (GameObject obj)
		{
				if (obj.name.Contains ("Doctor") || obj.name.Contains ("First Aid")) {
						painPoints = 0f;
				} 
				//		scoreKeeper.addToCount (obj);
		}
	
		private void temporarySpriteChange (int index)
		{
				waiting = true;
//				changeSprite (index);
				StartCoroutine (waitToStopWaiting ());
		}
	
		private IEnumerator waitToStopWaiting ()
		{
				yield return new WaitForSeconds (2);
				waiting = false;
		}
	
//		private void changeSprite (int index)
//		{
//				if (!sprite.sprite.Equals (healthFaces [index])) {
//						sprite.sprite = healthFaces [index];
//						switch (index) {
//						case 1: //discomfort
//								GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("Got Water?");
//								break;
//						case 2: //pain
//								GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("So thirsty!");
//								break;
//						default:
//								break;
//						}
//				}
//		}
}
