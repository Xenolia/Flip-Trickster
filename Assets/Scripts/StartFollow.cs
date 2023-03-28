// dnSpy decompiler from Assembly-CSharp.dll class: StartFollow
using System;
using UnityEngine;

public class StartFollow : MonoBehaviour
{
	private void Update()
	{
		base.transform.position = this.target.position;
	}

	public Transform target;
}
