// dnSpy decompiler from Assembly-CSharp.dll class: LvlModels
using System;
using UnityEngine;
using UnityEngine.UI;

public class LvlModels : MonoBehaviour
{
	private void Start()
	{
		this.AskForReview();
		SubLevel.attempt = 0;
		PlayerPrefs.SetInt("FinMod", -3);
		Skipper.skipAmount = PlayerPrefs.GetInt("Skips", 0);
		if (GameState.Instance.HasCompletedTutorial() && LvlModels.activeStage == -1)
		{
			this.fromTut = true;
			base.Invoke("GoRight", 0.5f);
		}
		else if (LvlModels.activeStage == -1 && GameState.Instance.HasCompletedTutorial())
		{
			LvlModels.activeStage = 0;
		}
		if (LvlModels.justUnlockedNewStage)
		{
			LvlModels.justUnlockedNewStage = false;
		}
		LvlBtnHandler.activeStage = LvlModels.activeStage + 1;
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = 1.77777779f - num;
		if (num2 > 0.18f)
		{
			LvlModels.onIpad = true;
			Camera.main.fieldOfView = 20f;
			this.gymUI.localScale = Vector3.one * 0.8f;
			this.mountUI.localScale = Vector3.one * 0.8f;
			this.galleryUI.localScale = Vector3.one * 0.8f;
			this.cityUI.localScale = Vector3.one * 0.8f;
			this.houseUI.localScale = Vector3.one * 0.8f;
			this.shipUI.localScale = Vector3.one * 0.8f;
			this.islandUI.localScale = Vector3.one * 0.8f;
			this.hauntedUI.localScale = Vector3.one * 0.8f;
			this.text.localPosition += new Vector3(10f, 80f);
			this.text.localScale = Vector3.one * 0.8f;
			this.directionButtons[0].localScale *= 0.7f;
			this.directionButtons[0].localPosition += Vector3.right * 50f;
			this.directionButtons[1].localScale *= 0.7f;
			this.directionButtons[1].localPosition += Vector3.left * 50f;
		}
		if (num2 < -0.1f)
		{
			TitleMain.oniPhoneX = true;
		}
		this.btnsDestination = Vector3.right * -this.distance * (float)LvlModels.activeStage;
		this.buttons.localPosition = this.btnsDestination;
		this.destination = Vector3.right * -1.85f * (float)LvlModels.activeStage;
		this.models.position = this.destination;
		if (LvlModels.activeStage == -1)
		{
			this.lBtn.SetActive(false);
		}
		if (LvlModels.activeStage == LvlModels.lvlNum - 1)
		{
			this.rBtn.SetActive(false);
		}
	}

	private void AskForReview()
	{
		if (LvlModels.activeStage != 1 || PlayerPrefs.GetInt("Asked") == 0)
		{
		}
	}

	private void Update()
	{
		this.buttons.localPosition = Vector3.Lerp(this.buttons.localPosition, this.btnsDestination, Time.deltaTime * 10f);
		this.models.position = Vector3.Lerp(this.models.position, this.destination, Time.deltaTime * 10f);
	}

	public void GoRight()
	{
		if (LvlModels.activeStage >= LvlModels.lvlNum - 1)
		{
			return;
		}
		if (!this.fromTut)
		{
			LevelMain.canSwipe = false;
		}
		this.btnsDestination += Vector3.right * -this.distance;
		GameObject.Find("MAIN").GetComponent<LvlBtnHandler>().ChangeStage(1);
		this.destination += Vector3.right * -1.85f;
		LvlModels.activeStage++;
		if (LvlModels.activeStage > LvlModels.lvlNum - 2)
		{
			this.rBtn.SetActive(false);
		}
		if (LvlModels.activeStage > -1)
		{
			this.lBtn.SetActive(true);
		}
		base.Invoke("EnablePrompt", 0.2f);
	}

	public void GoLeft()
	{
		if (LvlModels.activeStage <= -1)
		{
			return;
		}
		LevelMain.canSwipe = false;
		this.btnsDestination += Vector3.right * this.distance;
		GameObject.Find("MAIN").GetComponent<LvlBtnHandler>().ChangeStage(-1);
		this.destination += Vector3.right * 1.85f;
		LvlModels.activeStage--;
		if (LvlModels.activeStage < 0)
		{
			this.lBtn.SetActive(false);
		}
		if (LvlModels.activeStage < LvlModels.lvlNum - 1)
		{
			this.rBtn.SetActive(true);
		}
		base.Invoke("EnablePrompt", 0.2f);
	}

	private void EnablePrompt()
	{
		GameObject.Find("Prompt").GetComponent<Image>().enabled = true;
	}

	public Transform buttons;

	public Transform text;

	public Transform gymUI;

	public Transform mountUI;

	public Transform cityUI;

	public Transform houseUI;

	public Transform galleryUI;

	public Transform shipUI;

	public Transform islandUI;

	public Transform hauntedUI;

	private Vector3 btnsDestination;

	public GameObject rBtn;

	public GameObject lBtn;

	public Transform models;

	public Transform[] directionButtons;

	private Vector3 destination;

	private float distance = 1000f;

	public static int lvlNum = 9;

	public static int activeStage;

	public static bool onIpad;

	private bool fromTut;

	public Transform names;

	public static bool justUnlockedNewStage;

	public static string[] stageNames = new string[]
	{
		"null",
		"gym",
		"mountain",
		"city",
		"funhouse",
		"factory",
		"ship",
		"island",
		"haunted",
		"ufo"
	};
}
