// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Scenes.Helpers.AnchorConstraint
using System;
using UnityEngine;

namespace EPPZ.Cloud.Scenes.Helpers
{
	[ExecuteInEditMode]
	public class AnchorConstraint : MonoBehaviour
	{
		private void OnEnable()
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			this._parentRectTransform = base.transform.parent.GetComponent<RectTransform>();
		}

		private void Update()
		{
			if (!base.enabled)
			{
				return;
			}
			float num = this._parentRectTransform.rect.width / this._parentRectTransform.rect.height;
			this._rectTransform.anchorMin = new Vector2(this._rectTransform.anchorMin.x, this._rectTransform.anchorMin.x * num);
			this._rectTransform.anchorMax = new Vector2(this._rectTransform.anchorMax.x, 1f - (1f - this._rectTransform.anchorMax.x) * num);
			RectTransform rectTransform = this._rectTransform;
			Vector2 zero = Vector2.zero;
			this._rectTransform.offsetMax = zero;
			rectTransform.offsetMin = zero;
		}

		private RectTransform _parentRectTransform;

		private RectTransform _rectTransform;
	}
}
