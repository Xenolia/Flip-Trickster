// dnSpy decompiler from Assembly-CSharp.dll class: ConBoom
using System;
using UnityEngine;

public class ConBoom : MonoBehaviour
{
	private void Start()
	{
		GameObject original = Resources.Load<GameObject>("Prefabs/Confetti");
		for (int i = 0; i < 50; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, base.transform.position, base.transform.rotation, base.transform);
		}
	}
}
