// dnSpy decompiler from Assembly-CSharp.dll class: Confetti
using System;
using UnityEngine;
using UnityEngine.UI;

public class Confetti : MonoBehaviour
{
	private void Start()
	{
		this.SetColor();
		this.dir *= UnityEngine.Random.Range(0.6f, 1.6f);
		base.transform.Rotate(0f, UnityEngine.Random.Range(0f, 360f), 0f);
		this.movex = UnityEngine.Random.Range(-700f, 700f);
		this.movey = UnityEngine.Random.Range(1f, 4f);
		UnityEngine.Object.Destroy(base.gameObject, 2f);
	}

	private void FixedUpdate()
	{
		base.transform.Rotate(0f, 30f, 0f);
		base.transform.localPosition += this.dir * Time.fixedDeltaTime * 600f;
		base.transform.localPosition += Vector3.right * this.movex * Time.fixedDeltaTime;
		this.dir += Vector3.down * Time.fixedDeltaTime * this.movey;
	}

	private void SetColor()
	{
		int num = UnityEngine.Random.Range(0, this.colors.Length);
		Image componentInChildren = base.GetComponentInChildren<Image>();
		componentInChildren.color = this.colors[num];
	}

	private Vector3 dir = Vector3.up;

	private float movex;

	private float movey;

	public Color[] colors;
}
