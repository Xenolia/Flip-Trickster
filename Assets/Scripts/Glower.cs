// dnSpy decompiler from Assembly-CSharp.dll class: Glower
using System;
using UnityEngine;

public class Glower : MonoBehaviour
{
	private void Start()
	{
		this.start = base.transform.localPosition;
	}

	private void Update()
	{
		float d = Mathf.Sin(Time.time * 8f) * 5f;
		base.transform.localPosition = this.start + base.transform.up * d;
	}

	private Vector3 start;
}
