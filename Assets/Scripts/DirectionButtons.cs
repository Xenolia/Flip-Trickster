// dnSpy decompiler from Assembly-CSharp.dll class: DirectionButtons
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionButtons : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	private void Start()
	{
	}

	private void OnDisable()
	{
		this.image.localScale = Vector3.one;
	}

	private void Update()
	{
		this.image.localScale = Vector3.Lerp(this.image.localScale, Vector3.one, Time.deltaTime * 10f);
	}

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
		this.image.localScale = Vector3.one * 1.8f;
		if (this.image.name == "Image (1)")
		{
			GameObject.Find("MAIN").GetComponent<LvlModels>().GoRight();
		}
		else
		{
			GameObject.Find("MAIN").GetComponent<LvlModels>().GoLeft();
		}
	}

	public Transform image;
}
