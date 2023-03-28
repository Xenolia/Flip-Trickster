// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Scenes.Helpers.CanvasScaleToScreenWidthConstraint
using System;
using UnityEngine;
using UnityEngine.UI;

namespace EPPZ.Cloud.Scenes.Helpers
{
	[ExecuteInEditMode]
	public class CanvasScaleToScreenWidthConstraint : MonoBehaviour
	{
		private void Update()
		{
			base.GetComponent<CanvasScaler>().scaleFactor = this.canvasScaleToScreenWidth.Evaluate((float)Screen.width);
		}

		public AnimationCurve canvasScaleToScreenWidth;
	}
}
