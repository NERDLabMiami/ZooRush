using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class GA_Autorun
{
	#if UNITY_EDITOR
	static GA_Autorun()
	{
		GA_Inspector.CheckForUpdates();
		
		GA_Tracking.Setup();
	}
	#endif
}