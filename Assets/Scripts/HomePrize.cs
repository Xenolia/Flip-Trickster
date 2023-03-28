// dnSpy decompiler from Assembly-CSharp.dll class: HomePrize
using System;
using TMPro;
using UnityEngine;

public class HomePrize : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void FreePrize()
	{
		if (LionAdManager.Instance.HasRewarded())
		{
			IAPShop.freePrize = true;
			IAPShop.free25 = false;
			IAPShop.doubleCoins = false;
			LionAdManager.Instance.MaybeShowRewarded();
			return;
		}
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			GameObject original = Resources.Load<GameObject>("Prefabs/AdNotAvailable");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, base.transform.parent.position, base.transform.parent.rotation, base.transform.parent);
			return;
		}
		GameObject original2 = Resources.Load<GameObject>("Prefabs/AdNotAvailable");
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2, base.transform.parent.position, base.transform.parent.rotation, base.transform.parent);
		gameObject2.GetComponentInChildren<TextMeshProUGUI>().text = "Loading ads";
	}
}
