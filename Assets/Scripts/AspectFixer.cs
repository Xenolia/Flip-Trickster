// dnSpy decompiler from Assembly-CSharp.dll class: AspectFixer
using System;
using UnityEngine;

public class AspectFixer : MonoBehaviour
{
	private void Start()
	{
		if (LvlModels.onIpad)
		{
			return;
		}
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = num - 1.778f;
		num2 /= 12f;
		this.posFixer = ((!(base.name == "Menustuff")) ? GameObject.Find("StancePos").transform : GameObject.Find("MenuPos").transform);
		this.posFixer = ((!(base.name == "SkipStuff")) ? this.posFixer : GameObject.Find("SkipPos").transform);
		base.transform.position = ((base.transform.localPosition.x <= 0f) ? (this.posFixer.position + base.transform.right * num2) : (this.posFixer.position + base.transform.right * -num2));
		MonoBehaviour.print(base.transform.localPosition);
	}

	private void Update()
	{
	}

	private Transform posFixer;
}
