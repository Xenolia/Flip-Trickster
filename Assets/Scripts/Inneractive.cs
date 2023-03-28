// dnSpy decompiler from Assembly-CSharp.dll class: Inneractive
using System;
using UnityEngine;

public class Inneractive
{
	public static void InitializeSdk(string appId)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		Inneractive.inneractiveAdManager.CallStatic("initialize", new object[]
		{
			androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"),
			appId
		});
	}

	public static AndroidJavaClass inneractiveAdManager = new AndroidJavaClass("com.fyber.inneractive.sdk.external.InneractiveAdManager");
}
