// dnSpy decompiler from Assembly-CSharp.dll class: Customization
using System;
using UnityEngine;

public class Customization : MonoBehaviour
{
	private void Start()
	{
		if (LvlModels.onIpad)
		{
			Camera.main.fieldOfView = 55f;
			GameObject.Find("ButtonPanel").transform.localScale = Vector3.one * 0.75f;
		}
	}

	private void Update()
	{
	}

	public void OpenShop()
	{
		this.shopMenu.SetActive(true);
	}

	public void Exit()
	{
		this.shopMenu.SetActive(false);
	}

	public GameObject shopMenu;
}
