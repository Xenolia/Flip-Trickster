// dnSpy decompiler from Assembly-CSharp.dll class: ButtonPress
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	private void Start()
	{
		this.start = this.buttonImage.localScale;
		this.dest = this.start;
	}

	private void Update()
	{
		this.buttonImage.localScale = Vector3.Lerp(this.buttonImage.localScale, this.dest, Time.deltaTime * 20f);
	}

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
		this.dest = this.start * 1.2f;
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
		this.dest = this.start;
	}

	public Transform buttonImage;

	private Vector3 start;

	private Vector3 dest;
}
