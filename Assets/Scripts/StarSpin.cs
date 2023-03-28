// dnSpy decompiler from Assembly-CSharp.dll class: StarSpin
using System;
using UnityEngine;

public class StarSpin : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		this.oStar.Rotate(0f, 0f, Time.deltaTime * 60f);
		this.yStar.Rotate(0f, 0f, Time.deltaTime * -60f);
		float num = Mathf.Sin(Time.time * 12f) * 1f;
		float num2 = Mathf.Sin(Time.time * 12f + 5f) * 1f + 2f;
		this.oStar.localScale = Vector3.one * (1f + num / 8f);
		this.yStar.localScale = Vector3.one * (1f + num2 / 8f);
	}

	public Transform oStar;

	public Transform yStar;
}
