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
		//if (IAPShop.m_StoreController == null)
		//{
		//	this.InitializePurchasing();
		//}
		//MoPubManager.OnRewardedVideoReceivedRewardEvent += this.OnRewardedVideoReceivedRewardEvent;
		if (SceneManager.GetActiveScene().name == "NewLvlSelect")
		{
			base.gameObject.SetActive(false);
		}
	}

	private void OnRewardedVideoReceivedRewardEvent(string arg1, string arg2, float arg3)
	{
		LionAdManager.Instance.MarkInterstitialShown();
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
		if (LionAdManager.Instance.HasRewarded())
		{
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

	public void Buy275coins()
	{
		this.BuyProductID(IAPShop.coinsProduct_275);
	}

	public void Buy1400coins()
	{
		this.BuyProductID(IAPShop.coinsProduct_1450);
	}

	public void Buy3000coins()
	{
		this.BuyProductID(IAPShop.coinsProduct_3200);
	}

	public void Buy6500coins()
	{
		this.BuyProductID(IAPShop.coinsProduct_6600);
	}

	public void BuyNoAds()
	{
		this.BuyProductID(IAPShop.noAds);
	}

	public void BuyFlippyBundle()
	{
		this.BuyProductID(IAPShop.bundle);
	}

	private void BuyProductID(string productId)
	{
		if (this.IsInitialized())
		{
			//Product product = IAPShop.m_StoreController.products.WithID(productId);
			//if (product != null && product.availableToPurchase)
			//{
			//	UnityEngine.Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
			//	IAPShop.m_StoreController.InitiatePurchase(product);
			//}
			//else
			//{
			//	UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			//}
		}
		else
		{
			UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public void RestorePurchases()
	{
		if (!this.IsInitialized())
		{
			UnityEngine.Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			UnityEngine.Debug.Log("RestorePurchases started ...");
			//IAppleExtensions extension = IAPShop.m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			//extension.RestoreTransactions(delegate(bool result)
			//{
			//	UnityEngine.Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			//});
		}
		else
		{
			UnityEngine.Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	//public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	//{
	//	UnityEngine.Debug.Log("OnInitialized: PASS");
	//	IAPShop.m_StoreController = controller;
	//	IAPShop.m_StoreExtensionProvider = extensions;
	//}

	//public void OnInitializeFailed(InitializationFailureReason error)
	//{
	//	UnityEngine.Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	//}

	//public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	//{
	//	if (string.Equals(args.purchasedProduct.definition.id, IAPShop.coinsProduct_275, StringComparison.Ordinal))
	//	{
	//		UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
	//		Currency.coinAmount = GameState.Instance.AddCoins(275);
	//		GameState.Instance.Syncronize();
	//		GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = Currency.coinAmount.ToString();
	//		if (TitleMain.analyticsConsent)
	//		{
	//			//AppsFlyer.trackEvent("purchase_0.99", "0.99");
	//		}
	//		this.purchaseEvent.Add("af_content_id", "275_coins");
	//	}
	//	if (string.Equals(args.purchasedProduct.definition.id, IAPShop.coinsProduct_1450, StringComparison.Ordinal))
	//	{
	//		UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
	//		Currency.coinAmount = GameState.Instance.AddCoins(1450);
	//		GameState.Instance.Syncronize();
	//		GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = Currency.coinAmount.ToString();
	//		if (TitleMain.analyticsConsent)
	//		{
	//			//AppsFlyer.trackEvent("purchase_4.99", "4.99");
	//		}
	//		this.purchaseEvent.Add("af_content_id", "1450_coins");
	//	}
	//	if (string.Equals(args.purchasedProduct.definition.id, IAPShop.coinsProduct_3200, StringComparison.Ordinal))
	//	{
	//		UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
	//		Currency.coinAmount = GameState.Instance.AddCoins(3200);
	//		GameState.Instance.Syncronize();
	//		GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = Currency.coinAmount.ToString();
	//		if (TitleMain.analyticsConsent)
	//		{
	//			//AppsFlyer.trackEvent("purchase_9.99", "9.99");
	//		}
	//		this.purchaseEvent.Add("af_content_id", "3200_coins");
	//	}
	//	if (string.Equals(args.purchasedProduct.definition.id, IAPShop.coinsProduct_6600, StringComparison.Ordinal))
	//	{
	//		UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
	//		Currency.coinAmount = GameState.Instance.AddCoins(6600);
	//		GameState.Instance.Syncronize();
	//		GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = Currency.coinAmount.ToString();
	//		if (TitleMain.analyticsConsent)
	//		{
	//			//AppsFlyer.trackEvent("purchase_19.99", "19.99");
	//		}
	//		this.purchaseEvent.Add("af_content_id", "6600_coins");
	//	}
	//	else if (string.Equals(args.purchasedProduct.definition.id, IAPShop.noAds, StringComparison.Ordinal))
	//	{
	//		UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
	//		AFtitle.noAds = true;
	//		GameState.Instance.DisableAds();
	//		GameState.Instance.Syncronize();
	//		if (TitleMain.analyticsConsent)
	//		{
	//			//AppsFlyer.trackEvent("purchase_2.99", "2.99");
	//		}
	//		if (TitleMain.analyticsConsent)
	//		{
	//			Dictionary<string, string> eventValues = new Dictionary<string, string>();
	//			//AppsFlyer.trackRichEvent("no_ads", eventValues);
	//			//AppsFlyer.trackEvent("no_ads", string.Empty);
	//		}
	//		this.purchaseEvent.Add("af_content_id", "no_ads");
	//	}
	//	else if (string.Equals(args.purchasedProduct.definition.id, IAPShop.bundle, StringComparison.Ordinal))
	//	{
	//		UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
	//		AFtitle.noAds = true;
	//		GameState.Instance.DisableAds();
	//		Currency.coinAmount = GameState.Instance.AddCoins(900);
	//		GameState.Instance.Syncronize();
	//		GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = Currency.coinAmount.ToString();
	//		Skipper.skipAmount += 5;
	//		PlayerPrefs.SetInt("Skips", Skipper.skipAmount);
	//		if (TitleMain.analyticsConsent)
	//		{
	//			//AppsFlyer.trackEvent("purchase_6.99", "6.99");
	//		}
	//		if (TitleMain.analyticsConsent)
	//		{
	//			Dictionary<string, string> eventValues2 = new Dictionary<string, string>();
	//			//AppsFlyer.trackRichEvent("flippy_bundle", eventValues2);
	//			//AppsFlyer.trackEvent("flippy_bundle", string.Empty);
	//		}
	//		this.purchaseEvent.Add("af_content_id", "flippy_bundle");
	//	}
	//	else
	//	{
	//		UnityEngine.Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
	//	}
	//	if (TitleMain.analyticsConsent)
	//	{
	//		Dictionary<string, string> eventValues3 = new Dictionary<string, string>();
	//		//AppsFlyer.trackRichEvent("in_app_purchase", eventValues3);
	//		//AppsFlyer.trackEvent("in_app_purchase", string.Empty);
	//	}
	//	if (TitleMain.analyticsConsent)
	//	{
	//		if (SceneManager.GetActiveScene().name == "NewLvlSelect")
	//		{
	//			this.purchaseEvent.Add("af_level", "level_selection_screen");
	//		}
	//		else if (SceneManager.GetActiveScene().name == "Customization")
	//		{
	//			this.purchaseEvent.Add("af_level", "character_customization_screen");
	//		}
	//		else
	//		{
	//			string[] array = new string[]
	//			{
	//				"null",
	//				"gym",
	//				"mountain",
	//				"city",
	//				"funhouse",
	//				"factory",
	//				"ship",
	//				"island"
	//			};
	//			this.purchaseEvent.Add("af_level", array[LvlBtnHandler.activeStage] + "_" + LvlBtnHandler.activeLevel);
	//		}
	//		//AppsFlyer.trackRichEvent("af_purchase", this.purchaseEvent);
	//	}
	//	return PurchaseProcessingResult.Complete;
	//}

	//public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	//{
	//	UnityEngine.Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	//}

	//private static IStoreController m_StoreController;

	//private static IExtensionProvider m_StoreExtensionProvider;

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
