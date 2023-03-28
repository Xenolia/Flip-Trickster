// dnSpy decompiler from Assembly-CSharp.dll class: RVModal
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RVModal : MonoBehaviour
{
	private void Start()
	{
		RVModal.time = true;
		this.bar = GameObject.Find("LoadingBar").transform;
		Button[] componentsInChildren = base.GetComponentsInChildren<Button>();
		componentsInChildren[0].onClick.AddListener(new UnityAction(GameObject.Find("MAIN").GetComponent<Main>().DoubleCoins));
		componentsInChildren[1].onClick.AddListener(new UnityAction(GameObject.Find("MAIN").GetComponent<Main>().FreePrize));
		if (!false)
		{
			this.dbl.SetActive(false);
		}
		else
		{
			this.frz.SetActive(false);
		}
	}

	private void Update()
	{
		this.ShrinkTimer();
	}

	private void ShrinkTimer()
	{
		if (!RVModal.time)
		{
			return;
		}
		this.bar.localPosition -= new Vector3(60f * Time.deltaTime, 0f);
		this.bar.localScale -= new Vector3(0.333f * Time.deltaTime, 0f);
		if (this.bar.localScale.x <= 0f)
		{
			this.bar.localScale = new Vector3(0f, 1f);
			this.Next();
		}
	}

	public void Next()
	{
		GameObject.Find("MAIN").GetComponent<Main>().AfterModal();
	}

	private Transform bar;

	public static bool time;

	public GameObject dbl;

	public GameObject frz;
}
