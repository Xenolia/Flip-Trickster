// dnSpy decompiler from Assembly-CSharp.dll class: TutorialMove
using System;
using System.Collections;
using UnityEngine;

public class TutorialMove : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.PosChooser());
	}

	private void Update()
	{
	}

	private IEnumerator PosChooser()
	{
		for (;;)
		{
			float x = UnityEngine.Random.Range(-10f, 10f);
			float y = UnityEngine.Random.Range(-10f, 10f);
			this.destination = new Vector3(x / 2f, y / 2f);
			yield return new WaitForSeconds(0.2f);
		}
		yield break;
	}

	private Vector3 destination;
}
