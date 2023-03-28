// dnSpy decompiler from Assembly-CSharp.dll class: Squish
using System;
using UnityEngine;

public class Squish : MonoBehaviour
{
	private void Start()
	{
		this.startSize = base.transform.localScale.x;
	}

	private void Update()
	{
		float d = Mathf.Sin(Time.time * 5f) + this.startSize;
		base.transform.localScale = Vector3.one * d;
	}

	private float startSize;
}
