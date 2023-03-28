// dnSpy decompiler from Assembly-CSharp.dll class: Main
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
	private void Start()
	{
		Main.shouldShowRV = false;
		if (this.freeplay)
		{
			PlayerBF.freeplay = true;
		}
		else
		{
			PlayerBF.freeplay = false;
		}
		if (TitleMain.oniPhoneX)
		{
		}
		this.SpawnHat();
 
	}

	private void OnEnable()
	{
		//MoPubManager.OnRewardedVideoClosedEvent += this.OnRewardedVideoClosedEvent;
		//MoPubManager.OnRewardedVideoFailedEvent += this.OnRewardedVideoFailedEvent;
		//MoPubManager.OnRewardedVideoClickedEvent += this.OnRewardedVideoClickedEvent;
		//MoPubManager.OnRewardedVideoShownEvent += this.OnRewardedVideoShownEvent;
		//MoPubManager.OnInterstitialDismissedEvent += this.OnInterstitialDismissedEvent;
		//MoPubManager.OnInterstitialClickedEvent += this.OnInterstitialClickedEvent;
		//MoPubManager.OnInterstitialFailedEvent += this.OnInterstitialFailedEvent;
		//MoPubManager.OnInterstitialShownEvent += this.OnInterstitialShownEvent;
		//MoPubManager.OnAdClickedEvent += this.OnAdClickedEvent;
		//MoPubManager.OnAdLoadedEvent += this.OnAdLoadedEvent;
		//LionAdManager.Instance.EnsureAdsLoaded();
	}

	private void OnDisable()
	{
		//MoPubManager.OnRewardedVideoClosedEvent -= this.OnRewardedVideoClosedEvent;
		//MoPubManager.OnRewardedVideoFailedEvent -= this.OnRewardedVideoFailedEvent;
		//MoPubManager.OnRewardedVideoClickedEvent -= this.OnRewardedVideoClickedEvent;
		//MoPubManager.OnRewardedVideoShownEvent -= this.OnRewardedVideoShownEvent;
		//MoPubManager.OnInterstitialDismissedEvent -= this.OnInterstitialDismissedEvent;
		//MoPubManager.OnInterstitialClickedEvent -= this.OnInterstitialClickedEvent;
		//MoPubManager.OnInterstitialFailedEvent -= this.OnInterstitialFailedEvent;
		//MoPubManager.OnInterstitialShownEvent -= this.OnInterstitialShownEvent;
		//MoPubManager.OnAdClickedEvent -= this.OnAdClickedEvent;
		//MoPubManager.OnAdLoadedEvent -= this.OnAdLoadedEvent;
	}

	private void OnAdLoadedEvent(string arg1, float arg2)
	{
		if (TitleMain.analyticsConsent)
		{
			//AppsFlyer.trackEvent("view_banner", string.Empty);
		}
	}

	private void OnAdClickedEvent(string obj)
	{
		if (TitleMain.analyticsConsent)
		{
			//AppsFlyer.trackEvent("click_banner", string.Empty);
		}
	}

	private void OnInterstitialShownEvent(string obj)
	{
		if (TitleMain.analyticsConsent)
		{
			//AppsFlyer.trackEvent("view_interstitial", string.Empty);
		}
	}

	private void OnInterstitialFailedEvent(string arg1, string arg2)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			return;
		}
	}

	private void OnInterstitialClickedEvent(string obj)
	{
		if (TitleMain.analyticsConsent)
		{
			//AppsFlyer.trackEvent("click_interstitial", string.Empty);
		}
	}

	private void OnInterstitialDismissedEvent(string obj)
	{
	}

	private void OnRewardedVideoClickedEvent(string obj)
	{
		if (TitleMain.analyticsConsent)
		{
			//AppsFlyer.trackEvent("click_rewarded", string.Empty);
		}
	}

	private void OnRewardedVideoShownEvent(string obj)
	{
		if (TitleMain.analyticsConsent)
		{
			Dictionary<string, string> eventValues = new Dictionary<string, string>();
			//AppsFlyer.trackRichEvent("view_rewarded", eventValues);
			//AppsFlyer.trackEvent("view_rewarded", string.Empty);
		}
	}

	private void OnRewardedVideoFailedEvent(string arg1, string arg2)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			return;
		}
	}

	private void OnRewardedVideoClosedEvent(string obj)
	{
	}

	private void Update()
	{
	}

	private void SpawnHat()
	{
		if (Spinner.activeHatNum == -1)
		{
			return;
		}
		if (LvlBtnHandler.activeStage == 0 && PlayerPrefs.GetInt("TutorialTracked") == 0)
		{
			return;
		}
		string[] array = new string[]
		{
			"NoobHat",
			"Beanie",
			"Beanie2",
			"Beanie3",
			"Beanie4",
			"Cap",
			"Cap2",
			"Cap3",
			"Cap4",
			"Helmet",
			"TopHat",
			"Viking"
		};
		string path = "Prefabs/Hats/" + array[Spinner.activeHatNum];
		GameObject original = Resources.Load<GameObject>(path);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, Vector3.zero, Quaternion.identity);
	}

	public void DoubleCoins()
	{
		RVModal.time = false;
		if (TitleMain.analyticsConsent)
		{
			//AppsFlyer.trackEvent("doublecoins_click", string.Empty);
		}
		 
		Transform transform = (!(SceneManager.GetActiveScene().name == "NewLvlSelect")) ? GameObject.Find("NewCanvas02").transform : base.transform.parent;
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			GameObject original = Resources.Load<GameObject>("Prefabs/AdNotAvailable");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, transform.position, transform.rotation, transform);
			return;
		}
		GameObject original2 = Resources.Load<GameObject>("Prefabs/AdNotAvailable");
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2, transform.position, transform.rotation, transform);
		gameObject2.GetComponentInChildren<TextMeshProUGUI>().text = "Loading ads";
	}

	public void FreePrize()
	{
		RVModal.time = false;
		if (TitleMain.analyticsConsent)
		{
			//AppsFlyer.trackEvent("spinner_click", string.Empty);
		}
		if (PlayerPrefs.GetInt("FirstSpin") == 0 && SceneManager.GetActiveScene().name != "NewLvlSelect")
		{
			GameObject gameObject = new GameObject();
			gameObject = GameObject.Find("MAIN").GetComponent<Main>().spinwheel;
			if (GameObject.Find("RVmodal(Clone)"))
			{
				UnityEngine.Object.Destroy(GameObject.Find("RVmodal(Clone)"));
			}
			gameObject.SetActive(true);
			//if (AppLovinCrossPromo.Instance())
			//{
			//	AppLovinCrossPromo.Instance().HideMRec();
			//}
			PlayerPrefs.SetInt("FirstSpin", 1);
			return;
		}
		 
		Transform transform = (!(SceneManager.GetActiveScene().name == "NewLvlSelect")) ? GameObject.Find("NewCanvas02").transform : base.transform.parent;
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			GameObject original = Resources.Load<GameObject>("Prefabs/AdNotAvailable");
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original, transform.position, transform.rotation, transform);
			return;
		}
		GameObject original2 = Resources.Load<GameObject>("Prefabs/AdNotAvailable");
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(original2, transform.position, transform.rotation, transform);
		gameObject3.GetComponentInChildren<TextMeshProUGUI>().text = "Loading ads";
	}

	public void NextLevel()
	{
		if (StageModel.IsLastLevel())
		{
			return;
		}
		if (Main.skippedLevel)
		{
			Main.skippedLevel = false;
			//if (AppLovinCrossPromo.Instance() != null)
			//{
			//	AppLovinCrossPromo.Instance().HideMRec();
			//}
			LvlBtnHandler.activeLevel++;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			return;
		}
		//if (AppLovinCrossPromo.Instance() != null && !AFtitle.noAds)
		//{
		//	AppLovinCrossPromo.Instance().HideMRec();
		//}
		if (!Main.shouldShowRV)
		{
			this.AfterModal();
			return;
		}
		if (AFtitle.rvmodalTimer > 90f)
		{
			AFtitle.rvmodalTimer = 0f;
			Transform transform = GameObject.Find("NewCanvas02").transform;
			GameObject original = Resources.Load<GameObject>("Prefabs/RVmodal");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, transform.position, transform.rotation, transform);
			return;
		}
		this.AfterModal();
	}

	public void NextStage()
	{
		if (Main.skippedLevel)
		{
			Main.skippedLevel = false;
			//if (AppLovinCrossPromo.Instance() != null)
			//{
			//	AppLovinCrossPromo.Instance().HideMRec();
			//}
			LvlBtnHandler.activeStage = StageModel.NextStageId(LvlBtnHandler.activeStage);
			LvlBtnHandler.activeLevel = StageModel.FirstLevelId(LvlBtnHandler.activeLevel);
			SceneManager.LoadScene(StageModel.GetSceneName(LvlBtnHandler.activeStage));
			return;
		}
		//if (AppLovinCrossPromo.Instance() != null)
		//{
		//	AppLovinCrossPromo.Instance().HideMRec();
		//}
		if (!Main.shouldShowRV)
		{
			this.AfterModal();
			return;
		}
		if (AFtitle.rvmodalTimer > 90f)
		{
			AFtitle.rvmodalTimer = 0f;
			Transform transform = GameObject.Find("NewCanvas02").transform;
			GameObject original = Resources.Load<GameObject>("Prefabs/RVmodal");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, transform.position, transform.rotation, transform);
			return;
		}
		this.AfterModal();
	}

	public void AfterModal()
	{
		 
		Main.skippedLevel = false;
		if (StageModel.IsLastLevel())
		{
			LvlBtnHandler.activeStage = StageModel.NextStageId();
			LvlBtnHandler.activeLevel = StageModel.FirstLevelIdOfNextStage();
			SceneManager.LoadScene(StageModel.GetSceneName(LvlBtnHandler.activeStage));
		}
		else
		{
			LvlBtnHandler.activeLevel = StageModel.NextLevelId(LvlBtnHandler.activeStage, LvlBtnHandler.activeLevel);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void PreviousLevel()
	{
		 
		//if (AppLovinCrossPromo.Instance() != null)
		//{
		//	AppLovinCrossPromo.Instance().HideMRec();
		//}
		LvlBtnHandler.activeLevel = StageModel.PreviousLevelId(LvlBtnHandler.activeStage, LvlBtnHandler.activeLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void PressDown()
	{
		GameObject.Find("Player").GetComponent<PlayerBF>().QueueMouseDown();
	}

	public void PressUp()
	{
		GameObject.Find("Player").GetComponent<PlayerBF>().QueueMouseUp();
	}

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void DisableButtons()
	{
	}

	public void EnableButtons()
	{

	}

	public void EnableVariations()
	{
		if (this.switchStance)
		{
			this.switchStance.SetActive(true);
		}
	}

	public bool freeplay;

	public GameObject menu;

	public GameObject retry;

	public GameObject next;

	public GameObject previous;

	public GameObject switchStance;

	public Transform restartPosition;

	public Transform buttonPanel;

	public static bool hasCreatedBanner;

	public static bool shouldShowRV;

	public GameObject spinwheel;

	public static bool skippedLevel;

	private string[] scenes = new string[]
	{
		"Null",
		"GymNew",
		"MountNew",
		"City",
		"House",
		"Gallery",
		"Ship",
		"Thailand",
		"Haunted",
		"Space"
	};
}
