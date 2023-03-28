// dnSpy decompiler from Assembly-CSharp.dll class: IAPShop
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.Purchasing;
//using UnityEngine.Purchasing.Extension;
using UnityEngine.SceneManagement;

public class IAPShop : MonoBehaviour/*, IStoreListener*/
{
	private void Start()
	{
		 
		if (SceneManager.GetActiveScene().name == "NewLvlSelect")
		{
			base.gameObject.SetActive(false);
		}
	}

	private void OnRewardedVideoReceivedRewardEvent(string arg1, string arg2, float arg3)
	{
 		if (IAPShop.free25)
		{
			Currency.coinAmount = GameState.Instance.AddCoins(25);
			GameState.Instance.Syncronize();
			GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = Currency.coinAmount.ToString();
		}
		else if (IAPShop.freePrize)
		{
			GameObject gameObject = new GameObject();
			if (SceneManager.GetActiveScene().name == "NewLvlSelect")
			{
				Transform transform = GameObject.Find("DirectionCanvas").transform;
				gameObject = Resources.Load<GameObject>("Prefabs/Spinwheel");
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, transform.position, transform.rotation, transform);
			}
			else
			{
				gameObject = GameObject.Find("MAIN").GetComponent<Main>().spinwheel;
				if (GameObject.Find("RVmodal(Clone)"))
				{
					UnityEngine.Object.Destroy(GameObject.Find("RVmodal(Clone)"));
				}
			}
			gameObject.SetActive(true);
			//if (AppLovinCrossPromo.Instance())
			//{
			//	AppLovinCrossPromo.Instance().HideMRec();
			//}
		}
		else
		{
			if (!IAPShop.doubleCoins)
			{
				return;
			}
			if (StageModel.IsLastLevel())
			{
				GameObject.Find("MAIN").GetComponent<FullStage>().CompletedDoubleCoinsVideo();
			}
			else
			{
				GameObject.Find("MAIN").GetComponent<SubLevel>().CompletedDoubleCoinsVideo();
			}
			GameObject.Find("DoubleCoins").SetActive(false);
			GameObject.Find("MAIN").GetComponent<Main>().AfterModal();
		}
		IAPShop.free25 = false;
		IAPShop.freePrize = false;
		IAPShop.doubleCoins = false;
		if (TitleMain.analyticsConsent)
		{
			//AppsFlyer.trackEvent("request_rewarded", string.Empty);
			//AppsFlyer.trackEvent("watched_rewarded_video", string.Empty);
		}
	}

	public void FreeCoins()
	{
		IAPShop.freePrize = false;
		IAPShop.free25 = true;
		IAPShop.doubleCoins = false;
		 
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

	public void InitializePurchasing()
	{
		if (this.IsInitialized())
		{
			return;
		}
		//ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
		//configurationBuilder.AddProduct(IAPShop.coinsProduct_275, ProductType.Consumable);
		//configurationBuilder.AddProduct(IAPShop.coinsProduct_1450, ProductType.Consumable);
		//configurationBuilder.AddProduct(IAPShop.coinsProduct_3200, ProductType.Consumable);
		//configurationBuilder.AddProduct(IAPShop.coinsProduct_6600, ProductType.Consumable);
		//configurationBuilder.AddProduct(IAPShop.noAds, ProductType.NonConsumable);
		//configurationBuilder.AddProduct(IAPShop.bundle, ProductType.NonConsumable);
		//UnityPurchasing.Initialize(this, configurationBuilder);
	}

	private bool IsInitialized()
	{
        return false;// IAPShop.m_StoreController != null && IAPShop.m_StoreExtensionProvider != null;
	}

	public void Exit()
	{
		GameObject.Find("Shop").SetActive(false);
	}

 
 
	public static string coinsProduct_275 = "coins275";

	public static string coinsProduct_1450 = "coins1450";

	public static string coinsProduct_3200 = "coins3200";

	public static string coinsProduct_6600 = "coins6600";

	public static string noAds = "noads";

	public static string bundle = "flippybundle";

	public static bool free25;

	public static bool doubleCoins;

	public static bool freePrize;

	private Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
}
