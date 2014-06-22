using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class RandomFact : MonoBehaviour
{

		public TextMesh text;
		public TextAsset factsSource;

		private static string[] facts;
	
		void Awake ()
		{
				facts = factsSource.text.Split ("\n" [0]);
				for (int i = 0; i < facts.Length; i++) {
						facts [i] = StringFunctions.lineWrap (facts [i], 58, true);
				}
		}

		void Start ()
		{
				changeFact ();
		}

		void changeFact ()
		{
				text.text = facts [Random.Range (0, facts.Length)];
		}

}
