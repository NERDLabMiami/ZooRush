﻿using System;
using UnityEditor;
using UnityEngine;
using OpenKit;

public class OpenKitBuildMenu : EditorWindow {

	[MenuItem("OpenKit/ExportPackage")]
	public static void ShowWindow()
	{
		string[] OpenKitAssetPaths = {
			"Assets/Editor/OpenKitPostprocessBuildPlayer.cs",
			"Assets/Editor/OpenKitSettingsWindow.cs",
			"Assets/Examples/OKDemoScene.cs",
			"Assets/Examples/OKDemoScene.unity",
			"Assets/Plugins/Android/OpenKitSDK",
			"Assets/Plugins/iOS/libOpenKit.a",
			"Assets/Plugins/iOS/libOpenKitUnity.a",
			"Assets/Plugins/iOS/OpenKit_Vendor",
			"Assets/Plugins/iOS/OpenKitResources",
			"Assets/Plugins/OpenKit",
			"Assets/Plugins/RestSharp.dll",
			"Assets/Prefabs/OpenKitPrefab.prefab"
		};

		string SDKVersion = OKManager.OPENKIT_SDK_VERSION;

		string PackageName = "SDKPackages/OpenKitUnityPlugin." + SDKVersion + ".unitypackage";

		AssetDatabase.ExportPackage(OpenKitAssetPaths,PackageName,ExportPackageOptions.Recurse | ExportPackageOptions.Interactive);

		Debug.Log ("Exported OpenKit Package");
	}

}
