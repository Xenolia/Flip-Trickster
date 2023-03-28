// dnSpy decompiler from Assembly-CSharp.dll class: MobileUtilsScript
using System;
using System.Collections;
using UnityEngine;

public class MobileUtilsScript : MonoBehaviour
{
	private void Start()
	{
		if (Debug.isDebugBuild)
		{
			base.StartCoroutine(this.FPS());
		}
	}

	private IEnumerator FPS()
	{
		for (;;)
		{
			int lastFrameCount = Time.frameCount;
			float lastTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(this.frequency);
			float timeSpan = Time.realtimeSinceStartup - lastTime;
			int frameCount = Time.frameCount - lastFrameCount;
			this.fps = string.Format("FPS: {0}", Mathf.RoundToInt((float)frameCount / timeSpan));
		}
		yield break;
	}

	private void OnGUI()
	{
		if (Debug.isDebugBuild)
		{
			GUI.Label(new Rect((float)(Screen.width - 100), 10f, 150f, 20f), this.fps);
		}
	}

	private int FramesPerSec;

	private float frequency = 1f;

	private string fps;
}
