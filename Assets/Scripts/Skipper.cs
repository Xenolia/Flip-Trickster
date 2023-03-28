// dnSpy decompiler from Assembly-CSharp.dll class: Skipper
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Skipper : MonoBehaviour
{
	private void Start()
	{
		Skipper.skipAmount = PlayerPrefs.GetInt("Skips", 0);
		if (Skipper.skipAmount > 0)
		{
			this.skipsLeft.SetActive(true);
			this.sltext.text = Skipper.skipAmount.ToString();
			this.skipFromStash = true;
		}
		this.shop = GameObject.Find("Shop");
		this.shop.transform.localPosition = Vector3.zero;
		this.shop.SetActive(false);
		this.CheckIfSkippable();
	}

	private void Update()
	{
	}

	public void ShowModal()
	{
		if (Skipper.prompting && PlayerPrefs.GetInt("FreeSkip") == 0)
		{
			GameObject gameObject = GameObject.Find("NewCanvas02");
			this.modal = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/FreeSkipModal"), gameObject.transform);
			Button[] componentsInChildren = this.modal.GetComponentsInChildren<Button>();
			componentsInChildren[0].onClick.AddListener(new UnityAction(this.Exit));
			componentsInChildren[1].onClick.AddListener(new UnityAction(this.SkipFree));
			PlayerPrefs.SetInt("FreeSkip", 1);
			return;
		}
		if (!this.skipFromStash)
		{
			GameObject gameObject2 = GameObject.Find("NewCanvas02");
			this.modal = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/SkipModal"), gameObject2.transform);
			Button[] componentsInChildren2 = this.modal.GetComponentsInChildren<Button>();
			componentsInChildren2[0].onClick.AddListener(new UnityAction(this.Exit));
			componentsInChildren2[1].onClick.AddListener(new UnityAction(this.SkipOne));
			componentsInChildren2[2].onClick.AddListener(new UnityAction(this.SkipThree));
		}
		else
		{
			GameObject gameObject3 = GameObject.Find("NewCanvas02");
			this.modal = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/SkipStashModal"), gameObject3.transform);
			Button[] componentsInChildren3 = this.modal.GetComponentsInChildren<Button>();
			componentsInChildren3[0].onClick.AddListener(new UnityAction(this.Exit));
			componentsInChildren3[1].onClick.AddListener(new UnityAction(this.SkipFromStash));
			TextMeshProUGUI[] componentsInChildren4 = this.modal.GetComponentsInChildren<TextMeshProUGUI>();
			componentsInChildren4[2].text = Skipper.skipAmount.ToString();
		}
		if (SubLevel.crashCount == 3)
		{
			this.modal.GetComponentInChildren<TextMeshProUGUI>().text = "Skip this level?";
		}
	}

	private void CheckIfSkippable()
	{
		if (StageModel.IsLastLevel() && StageModel.IsLastStage(LvlBtnHandler.activeStage))
		{
			Transform transform = GameObject.Find("Coins").transform;
			transform.localPosition += Vector3.right * 80f;
			Skipper.skipActive = false;
			base.gameObject.SetActive(false);
		}
		else if (!StageModel.IsLastLevel() && !GameState.Instance.HasLevelAccess(LvlBtnHandler.activeStage, StageModel.NextLevelId()))
		{
			if (LvlBtnHandler.activeLevel == 8)
			{
				Transform transform2 = GameObject.Find("Coins").transform;
				transform2.localPosition += Vector3.right * 80f;
				Skipper.skipActive = false;
				base.gameObject.SetActive(false);
			}
			else
			{
				Skipper.skipActive = true;
				if (TitleMain.oniPhoneX)
				{
					Transform transform3 = GameObject.Find("Coins").transform;
					transform3.localPosition += Vector3.left * 10f;
				}
			}
		}
		else
		{
			if (!StageModel.IsLastLevel() || GameState.Instance.HasLevelAccess(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage()))
			{
				Transform transform4 = GameObject.Find("Coins").transform;
				transform4.localPosition += Vector3.right * 80f;
				Skipper.skipActive = false;
				base.gameObject.SetActive(false);
				return;
			}
			Skipper.skipActive = true;
			if (TitleMain.oniPhoneX)
			{
				Transform transform5 = GameObject.Find("Coins").transform;
				transform5.localPosition += Vector3.left * 10f;
			}
		}
		if (Skipper.prompting)
		{
			Image[] componentsInChildren = base.GetComponentsInChildren<Image>();
			this.tris[0] = componentsInChildren[3];
			this.tris[1] = componentsInChildren[4];
			base.StartCoroutine(this.Prompt());
		}
	}

	private IEnumerator Prompt()
	{
		for (;;)
		{
			this.tris[0].color = Color.white;
			this.tris[1].color = Color.white;
			yield return new WaitForSeconds(0.5f);
			this.tris[0].color = Color.black;
			this.tris[1].color = Color.black;
			yield return new WaitForSeconds(0.5f);
		}
		yield break;
	}

	public void SkipFromStash()
	{
		Skipper.skipAmount--;
		PlayerPrefs.SetInt("Skips", Skipper.skipAmount);
		if (!StageModel.IsLastLevel())
		{
			GameState.Instance.SetLevelUnlocked(LvlBtnHandler.activeStage, StageModel.NextLevelId());
			GameState.Instance.Syncronize();
			Main.skippedLevel = true;
			GameObject.Find("MAIN").GetComponent<Main>().NextLevel();
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackEvent("skip_level", string.Empty);
			}
		}
		else
		{
			GameState.Instance.SetLevelUnlocked(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage());
			GameState.Instance.Syncronize();
			Main.skippedLevel = true;
			GameObject.Find("MAIN").GetComponent<Main>().NextStage();
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackEvent("skip_stage", string.Empty);
			}
		}
		Skipper.prompting = false;
		this.TrackSkip();
		this.Exit();
	}

	public void SkipOne()
	{
		if (Currency.coinAmount < 90)
		{
			this.shop.SetActive(true);
			this.shop.transform.SetAsLastSibling();
			GameObject.Find("Coins").transform.SetAsLastSibling();
			return;
		}
		Currency.coinAmount = GameState.Instance.RemoveCoins(90);
		GameState.Instance.Syncronize();
		GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = Currency.coinAmount.ToString();
		if (!StageModel.IsLastLevel())
		{
			GameState.Instance.SetLevelUnlocked(LvlBtnHandler.activeStage, StageModel.NextLevelId());
			GameState.Instance.Syncronize();
			Main.skippedLevel = true;
			GameObject.Find("MAIN").GetComponent<Main>().NextLevel();
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackEvent("skip_level", string.Empty);
			}
		}
		else
		{
			GameState.Instance.SetLevelUnlocked(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage());
			GameState.Instance.Syncronize();
			Main.skippedLevel = true;
			GameObject.Find("MAIN").GetComponent<Main>().NextStage();
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackEvent("skip_stage", string.Empty);
			}
		}
		Skipper.prompting = false;
		this.TrackSkip();
		this.Exit();
	}

	public void SkipThree()
	{
		if (Currency.coinAmount < 180)
		{
			this.shop.SetActive(true);
			this.shop.transform.SetAsLastSibling();
			GameObject.Find("Coins").transform.SetAsLastSibling();
			return;
		}
		Currency.coinAmount = GameState.Instance.RemoveCoins(180);
		GameState.Instance.Syncronize();
		GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = Currency.coinAmount.ToString();
		Skipper.skipAmount += 2;
		PlayerPrefs.SetInt("Skips", Skipper.skipAmount);
		if (!StageModel.IsLastLevel())
		{
			GameState.Instance.SetLevelUnlocked(LvlBtnHandler.activeStage, StageModel.NextLevelId());
			GameState.Instance.Syncronize();
			Main.skippedLevel = true;
			GameObject.Find("MAIN").GetComponent<Main>().NextLevel();
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackEvent("skip_level", string.Empty);
			}
		}
		else
		{
			GameState.Instance.SetLevelUnlocked(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage());
			GameState.Instance.Syncronize();
			Main.skippedLevel = true;
			GameObject.Find("MAIN").GetComponent<Main>().NextStage();
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackEvent("skip_stage", string.Empty);
			}
		}
		Skipper.prompting = false;
		this.TrackSkip();
		this.Exit();
	}

	public void SkipFree()
	{
		if (!StageModel.IsLastLevel())
		{
			GameState.Instance.SetLevelUnlocked(LvlBtnHandler.activeStage, StageModel.NextLevelId());
			GameState.Instance.Syncronize();
			Main.skippedLevel = true;
			GameObject.Find("MAIN").GetComponent<Main>().NextLevel();
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackEvent("skip_level", string.Empty);
			}
		}
		else
		{
			GameState.Instance.SetLevelUnlocked(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage());
			GameState.Instance.Syncronize();
			Main.skippedLevel = true;
			GameObject.Find("MAIN").GetComponent<Main>().NextStage();
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackEvent("skip_stage", string.Empty);
			}
		}
		Skipper.prompting = false;
		this.TrackSkip();
		this.Exit();
	}

	private void TrackSkip()
	{
		string[] array = new string[]
		{
			"null",
			"gym",
			"mountain",
			"city",
			"funhouse",
			"factory",
			"ship",
			"island"
		};
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		if (StageModel.IsLastLevel())
		{
			dictionary.Add("af_level", array[LvlBtnHandler.activeStage]);
			//AppsFlyer.trackRichEvent("skip_stage", dictionary);
			return;
		}
		dictionary.Add("af_level", array[LvlBtnHandler.activeStage] + "_" + LvlBtnHandler.activeLevel);
		//AppsFlyer.trackRichEvent("skip_level", dictionary);
	}

	public void Exit()
	{
		UnityEngine.Object.Destroy(this.modal);
	}

	public static int skipAmount;

	public static bool skipActive;

	private bool skipFromStash;

	private GameObject modal;

	private GameObject shop;

	public GameObject skipsLeft;

	public TextMeshProUGUI sltext;

	public static bool prompting;

	private Image[] tris = new Image[2];
}
