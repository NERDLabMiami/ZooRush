using UnityEngine;
using System.Collections;

public interface GameEvent
{
		bool action ();
		bool verify ();
		string description ();
}
