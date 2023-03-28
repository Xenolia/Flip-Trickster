// dnSpy decompiler from Assembly-CSharp.dll class: Prompter
using System;
using UnityEngine;

public class Prompter : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		float num = Mathf.Sin(Time.time * 10f) / 1.5f;
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y + num);
	}
}
