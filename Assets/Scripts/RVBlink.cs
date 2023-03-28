// dnSpy decompiler from Assembly-CSharp.dll class: RVBlink
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RVBlink : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		base.StartCoroutine(this.Blink());
	}

	private void OnDisable()
	{
		base.StopAllCoroutines();
	}

	private IEnumerator Blink()
	{
		for (;;)
		{
			this.img.color = Color.white;
			yield return new WaitForSeconds(0.5f);
			this.img.color = Color.black;
			yield return new WaitForSeconds(0.5f);
		}
		yield break;
	}

	public Image img;
}
