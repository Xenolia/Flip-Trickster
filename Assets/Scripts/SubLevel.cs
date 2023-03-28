// dnSpy decompiler from Assembly-CSharp.dll class: SubLevel
using System;
using System.Collections;
using System.Collections.Generic;
//using Facebook.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubLevel : MonoBehaviour
{
	private void Start()
	{
		if (!StageModel.IsLastLevel() && LvlBtnHandler.activeLevel != 0)
		{
			this.SetUpSubLevel();
			this.SetUpCoins();
			this.complimentrans = this.complimentText.transform;
			this.homeButton = GameObject.Find("Home").transform;
			if (LvlModels.onIpad)
			{
				Camera.main.fieldOfView = 80f;
				this.fontDest = 30f;
				this.textOffset = 0.73f;
				this.bestOffset = 0.43f;
				this.flip.fontSize = 40f;
				this.total.fontSize = 50f;
				this.score.fontSize = 35f;
				this.timer.localScale *= 0.8f;
			}
			else if (LvlModels.onIpad || !TitleMain.oniPhoneX)
			{
			}
			this.flip.text = string.Empty;
			this.score.text = string.Empty;
			this.total.text = string.Empty;
			this.texts[0].text = this.bronzeScore[this.activeLvl - 1].ToString();
			this.texts[1].text = this.silverScore[this.activeLvl - 1].ToString();
			this.texts[2].text = this.goldScore[this.activeLvl - 1].ToString();
			this.texts[3].text = this.specialTexts[this.activeLvl - 1];
			if (GameState.Instance.HasBronze(this.stage, this.activeLvl))
			{
				this.SetupCheck(0);
			}
			if (GameState.Instance.HasSilver(this.stage, this.activeLvl))
			{
				this.SetupCheck(1);
			}
			if (GameState.Instance.HasGold(this.stage, this.activeLvl))
			{
				this.SetupCheck(2);
			}
			if (GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.SetupCheck(3);
			}
			this.bestScore = GameState.Instance.GetHighScore(this.stage, this.activeLvl);
			this.bestText.text = "Best: " + this.bestScore;
			if (!LvlModels.onIpad)
			{
				this.dotGroup.localPosition = new Vector3(-235f, 230f);
				if (!LvlModels.onIpad && !TitleMain.oniPhoneX)
				{
					this.dotGroup.localPosition += Vector3.right * -170f + Vector3.down * 520f;
				}
			}
			else
			{
				this.menuStuff.localPosition += new Vector3(-55f, 40f);
				this.downStuff.localPosition += new Vector3(-55f, -40f);
				this.dotGroup.localPosition = new Vector3(-435f, -290f);
				this.dotGroup.localScale = Vector3.one * 0.7f;
				this.flip.fontSize += 20f;
				this.flip.transform.localPosition += Vector3.right * 80f;
				this.afterButtons.localScale *= 0.7f;
			}
			this.abDest = new Vector3(0f, -230f);
			if (TitleMain.oniPhoneX)
			{
				this.total.fontSize = 80f;
				this.menuStuff.localScale = Vector3.one * 1.2f;
				this.downStuff.localScale = Vector3.one * 1.2f;
				this.dotGroup.localPosition += Vector3.right * -130f + Vector3.down * 520f;
				this.dotGroup.localScale += Vector3.one * 0.2f;
				this.afterButtons.localScale += Vector3.one * 0.2f;
			}
			this.skipButton = GameObject.Find("Skip");
			this.fullStage = GameObject.Find("FullStage");
			this.nextButton = GameObject.Find("NextLevel").transform;
			this.fullStage.SetActive(false);
			this.skipButton.SetActive(false);
			Challenges.targetCount = 0;
			Challenges.redZoneCount = 0;
			if (GameState.Instance.HasLevelAccess(LvlBtnHandler.activeStage, StageModel.NextLevelId()))
			{
				GameObject.Find("CompleteSilver").SetActive(false);
			}
			if (LvlBtnHandler.activeLevel == 1)
			{
				this.previousButton.gameObject.SetActive(false);
			}
			else if (LvlBtnHandler.activeLevel == 6)
			{
				this.fullStage.SetActive(true);
			}
			return;
		}
	}

	private void SetupCheck(int dot)
	{
		Transform transform = this.dots[dot].transform;
		GameObject original = Resources.Load<GameObject>("Prefabs/CheckUI");
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -90f), transform);
		gameObject.transform.localScale = Vector3.one * 0.3f;
		this.checks.Add(gameObject.transform);
	}

	private void SetUpCoins()
	{
		this.coinText = GameObject.Find("Coins").GetComponent<TextMeshProUGUI>();
		this.coinText.text = Currency.coinAmount.ToString();
	}

	private void FixedUpdate()
	{
		if (!this.completedChallenge)
		{
			return;
		}
		this.dotGroup.localPosition = Vector3.Lerp(this.dotGroup.localPosition, this.dotDestination, Time.fixedDeltaTime * 10f);
		this.dotGroup.localScale = Vector3.Lerp(this.dotGroup.localScale, this.dotScale, Time.fixedDeltaTime * 10f);
		this.dotGroup.localRotation = Quaternion.Lerp(this.dotGroup.localRotation, Quaternion.Euler(0f, 0f, this.dotRotation), Time.fixedDeltaTime * 10f);
		for (int i = 0; i < this.checks.Count; i++)
		{
			this.checks[i].localRotation = Quaternion.Lerp(this.checks[i].localRotation, Quaternion.Euler(0f, 0f, 0f), Time.fixedDeltaTime * 10f);
		}
		this.color.a = Mathf.Lerp(this.color.a, this.alpha, Time.fixedDeltaTime * 10f);
		this.back.color = this.color;
		for (int j = 0; j < this.texts.Length; j++)
		{
			if (this.alpha > 0f)
			{
				this.texts[j].transform.position = this.dots[j].position + this.texts[j].transform.right * this.textOffset;
			}
			else
			{
				this.texts[j].transform.position = this.dots[j].position + this.texts[j].transform.up * 1f;
			}
			this.texts[j].fontSize = Mathf.Lerp(this.texts[j].fontSize, this.fontDest, Time.fixedDeltaTime * 5f);
			this.dots[j].transform.localScale = Vector3.Lerp(this.dots[j].transform.localScale, Vector3.one * 1.1f, Time.fixedDeltaTime * 5f);
		}
		if (this.alpha > 0f)
		{
			this.bestText.transform.position = this.dotGroup.position + this.bestText.transform.up * 0.16f + this.bestText.transform.right * this.bestOffset;
		}
		else
		{
			this.bestText.transform.position = this.dotGroup.position + this.bestText.transform.up * 90f;
		}
		this.bestText.fontSize = Mathf.Lerp(this.bestText.fontSize, this.fontDest + 8f, Time.fixedDeltaTime * 5f);
		this.afterButtons.localPosition = Vector3.Lerp(this.afterButtons.localPosition, this.abDest, Time.fixedDeltaTime * 10f);
		if (this.complimenting)
		{
			float d = Mathf.Sin(Time.time * 8f) / 5f + 1f;
			float z = Mathf.Sin(Time.time * 4f) * 5f;
			this.complimentrans.localScale = Vector3.Lerp(this.complimentrans.localScale, Vector3.one * d, Time.fixedDeltaTime * 10f);
			this.complimentrans.localRotation = Quaternion.Euler(0f, 0f, z);
			this.nextButton.localScale = Vector3.Lerp(this.nextButton.localScale, Vector3.one, Time.fixedDeltaTime * 1f);
			this.homeButton.localScale = Vector3.Lerp(this.homeButton.localScale, Vector3.one, Time.fixedDeltaTime * 1f);
		}
		if (this.gotCoin)
		{
			this.CoinMove();
		}
	}

	private void SetUpSubLevel()
	{
		this.activeLvl = LvlBtnHandler.activeLevel;
		PlayerBF.landedOnObs = false;
		if (!LvlModels.onIpad)
		{
			this.dotGroup.localPosition = new Vector3(-200f, 267f);
		}
		else
		{
			this.dotGroup.localPosition = new Vector3(-260f, 267f);
			this.dotGroup.localScale = Vector3.one * 0.7f;
		}
	}

	public void Landed()
	{
		if (PlayerBF.freeplay)
		{
			Score.score = 0f;
		}
		if (this.stage == 1 && this.activeLvl == 1 && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0 && !PlayerBF.freeplay)
		{
			Challenges.gymSpecial[0] = true;
		}
		this.CheckScoreChallenges(Score.score);
	}

	public void CheckScoreChallenges(float score)
	{
		this.CheckSpecial();
		if (score >= this.goldScore[this.activeLvl - 1])
		{
			if ((int)score > this.bestScore)
			{
				this.bestScore = (int)score;
			}
			base.StartCoroutine(this.CompletedChallenge(2));
			return;
		}
		if (score >= this.silverScore[this.activeLvl - 1])
		{
			if ((int)score > this.bestScore)
			{
				this.bestScore = (int)score;
			}
			base.StartCoroutine(this.CompletedChallenge(1));
			return;
		}
		if (score >= this.bronzeScore[this.activeLvl - 1])
		{
			if ((int)score > this.bestScore)
			{
				this.bestScore = (int)score;
			}
			base.StartCoroutine(this.CompletedChallenge(0));
			return;
		}
		if (this.specialCompleted)
		{
			if ((int)score > this.bestScore)
			{
				this.bestScore = (int)score;
			}
			base.StartCoroutine(this.CompletedChallenge(-1));
			return;
		}
		if ((int)score > this.bestScore)
		{
			this.bestScore = (int)score;
		}
		base.StartCoroutine(this.CompletedChallenge(-1));
	}

	private void CoinMove()
	{
		if (this.coinTimer == 100f)
		{
			this.coinAwards[4].localScale = Vector3.Lerp(this.coinAwards[4].localScale, Vector3.one * 0.8f, Time.fixedDeltaTime * 8f);
			Vector3 vector = (!LvlModels.onIpad) ? Vector3.one : new Vector3(80f, -60f);
			return;
		}
		Vector3 vector2 = Vector3.up * 520f;
		vector2 = ((!TitleMain.oniPhoneX) ? vector2 : (Vector3.up * 640f));
		vector2 += ((!LvlModels.onIpad) ? Vector3.zero : (Vector3.right * 50f));
		int num = 3;
		if (this.coinAwards[0])
		{
			this.coinAwards[0].localPosition = Vector3.Lerp(this.coinAwards[0].localPosition, vector2 + Vector3.left * 10f, Time.fixedDeltaTime * (float)num);
		}
		if (this.coinAwards[1])
		{
			this.coinAwards[1].localPosition = Vector3.Lerp(this.coinAwards[1].localPosition, vector2 + Vector3.left * 50f, Time.fixedDeltaTime * (float)num);
		}
		if (this.coinAwards[2])
		{
			this.coinAwards[2].localPosition = Vector3.Lerp(this.coinAwards[2].localPosition, vector2 + Vector3.left * 90f, Time.fixedDeltaTime * (float)num);
		}
		if (this.coinAwards[3])
		{
			this.coinAwards[3].localPosition = Vector3.Lerp(this.coinAwards[3].localPosition, vector2 + Vector3.left * 130f, Time.fixedDeltaTime * (float)num);
		}
		this.coinTimer = Mathf.Lerp(this.coinTimer, 40f, Time.fixedDeltaTime * (float)num);
		if (this.coinTimer > 38f)
		{
			if (this.coinAwards[0])
			{
				UnityEngine.Object.Destroy(this.coinAwards[0].gameObject);
			}
			if (this.coinAwards[1])
			{
				UnityEngine.Object.Destroy(this.coinAwards[1].gameObject);
			}
			if (this.coinAwards[2])
			{
				UnityEngine.Object.Destroy(this.coinAwards[2].gameObject);
			}
			if (this.coinAwards[3])
			{
				UnityEngine.Object.Destroy(this.coinAwards[3].gameObject);
			}
			this.coinTimer = 100f;
			base.StartCoroutine(this.CoinTimeline());
		}
	}

	private IEnumerator CoinTimeline()
	{
		Currency.coinAmount = GameState.Instance.AddCoins(this.coinsAwarded);
		GameState.Instance.Syncronize();
		this.coinText.text = Currency.coinAmount.ToString();
		Transform copy = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/CoinAward"), this.dots[0].transform).transform;
		this.coinAwards[4] = copy;
		Vector3 spawn = new Vector3(0f, 550f);
		copy.GetComponent<TextMeshProUGUI>().text = "+" + this.coinsAwarded;
		copy.localRotation = Quaternion.Euler(0f, 0f, 90f);
		if (TitleMain.oniPhoneX)
		{
			copy.localPosition = spawn + new Vector3(-25f, 75f);
		}
		else if (!TitleMain.oniPhoneX && !LvlModels.onIpad)
		{
			copy.localPosition = spawn + new Vector3(-25f, -30f);
		}
		else
		{
			copy.localPosition = spawn + new Vector3(10f, -80f);
		}
		copy.localScale = Vector3.one * 1.3f;
		yield return new WaitForSeconds(0.4f);
		yield break;
	}

	public void CompletedDoubleCoinsVideo()
	{
		Currency.coinAmount = GameState.Instance.AddCoins(this.coinsAwarded);
		GameState.Instance.Syncronize();
		this.coinText.text = Currency.coinAmount.ToString();
	}

	private void GetChecked(int dot)
	{
		Transform transform = this.dots[dot].transform;
		GameObject original = Resources.Load<GameObject>("Prefabs/CheckUI");
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, transform.position, transform.rotation, transform);
		gameObject.transform.localScale = Vector3.one * 0.3f;
	}

	private IEnumerator CompletedChallenge(int challenge)
	{
		if (TitleMain.oniPhoneX)
		{
			this.dotDestination = new Vector3(-280f, 0f);
			this.dotScale = Vector3.one * 1.3f;
		}
		else if (LvlModels.onIpad)
		{
			this.dotDestination = new Vector3(-400f, 0f);
			this.dotScale = Vector3.one * 1f;
		}
		else
		{
			this.dotDestination = new Vector3(-360f, 0f);
			this.dotScale = Vector3.one * 1.3f;
		}
		this.SetPlayerPrefs(challenge);
		this.completedChallenge = true;
		this.dotRotation = -90f;
		this.color.a = 0f;
		this.alpha = 0.5f;
		this.back.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		if (this.bestCompleted)
		{
			this.bestText.fontSize = 75f;
			this.bestText.text = "Best: " + this.bestScore;
			yield return new WaitForSeconds(0.4f);
		}
		if (this.bronzeCompleted)
		{
			this.dots[0].transform.localScale = Vector3.one * 1.5f;
			this.texts[0].fontSize = 55f;
			this.GetChecked(0);
			Transform copy = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/CoinAward"), this.dots[0].transform).transform;
			this.coinAwards[0] = copy;
			Vector3 spawn = this.dots[0].transform.position;
			copy.GetComponent<TextMeshProUGUI>().text = "+5";
			copy.localRotation = Quaternion.Euler(0f, 0f, 90f);
			copy.localPosition = spawn;
			copy.localScale = Vector3.one * 0.6f;
			this.coinsAwarded += 5;
			Currency.coinAmount += 5;
			this.gotCoin = true;
			yield return new WaitForSeconds(0.4f);
		}
		if (this.silverCompleted)
		{
			this.dots[1].transform.localScale = Vector3.one * 1.5f;
			this.texts[1].fontSize = 55f;
			this.GetChecked(1);
			Transform copy2 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/CoinAward"), this.dots[1].transform).transform;
			this.coinAwards[1] = copy2;
			Vector3 spawn2 = this.dots[1].transform.position;
			copy2.GetComponent<TextMeshProUGUI>().text = "+10";
			copy2.localRotation = Quaternion.Euler(0f, 0f, 90f);
			copy2.localPosition = spawn2;
			copy2.localScale = Vector3.one * 0.6f;
			this.coinsAwarded += 10;
			Currency.coinAmount += 10;
			this.gotCoin = true;
			this.coinTimer = 0f;
			yield return new WaitForSeconds(0.4f);
		}
		if (this.goldCompleted)
		{
			this.dots[2].transform.localScale = Vector3.one * 1.5f;
			this.texts[2].fontSize = 55f;
			this.GetChecked(2);
			Transform copy3 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/CoinAward"), this.dots[2].transform).transform;
			this.coinAwards[2] = copy3;
			Vector3 spawn3 = this.dots[2].transform.position;
			copy3.GetComponent<TextMeshProUGUI>().text = "+15";
			copy3.localRotation = Quaternion.Euler(0f, 0f, 90f);
			copy3.localPosition = spawn3;
			copy3.localScale = Vector3.one * 0.6f;
			this.coinsAwarded += 15;
			Currency.coinAmount += 15;
			this.gotCoin = true;
			this.coinTimer = 0f;
			yield return new WaitForSeconds(0.4f);
		}
		if (this.specialCompleted2)
		{
			this.dots[3].transform.localScale = Vector3.one * 1.5f;
			this.texts[3].fontSize = (float)((!Challenges.bigtext) ? 50 : 45);
			this.GetChecked(3);
			Transform copy4 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/CoinAward"), this.dots[3].transform).transform;
			this.coinAwards[3] = copy4;
			Vector3 spawn4 = this.dots[3].transform.position;
			copy4.GetComponent<TextMeshProUGUI>().text = "+10";
			copy4.localRotation = Quaternion.Euler(0f, 0f, 90f);
			copy4.localPosition = spawn4;
			copy4.localScale = Vector3.one * 0.6f;
			this.coinsAwarded += 10;
			Currency.coinAmount += 10;
			this.gotCoin = true;
			this.coinTimer = 0f;
			yield return new WaitForSeconds(0.4f);
		}
		if (this.bronzeCompleted || this.silverCompleted || this.goldCompleted || this.specialCompleted)
		{
			Main.shouldShowRV = true;
			this.complimenting = true;
			if (TitleMain.oniPhoneX)
			{
				this.complimentrans.localPosition = new Vector3(210f, 100f);
			}
			else if (LvlModels.onIpad)
			{
				this.complimentrans.localPosition += new Vector3(-300f, -300f);
			}
			else
			{
				this.complimentrans.localPosition = new Vector3(120f, 80f);
			}
			this.complimentrans.localScale = Vector3.zero;
			this.complimentText.text = this.compliments[UnityEngine.Random.Range(0, 6)];
			if (this.silverCompleted)
			{
				base.StartCoroutine(this.Pump());
				Transform transform = GameObject.Find("NewCanvas02").transform;
				GameObject original = Resources.Load<GameObject>("Prefabs/ConfettiBoom");
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, transform.position, transform.rotation, transform);
			}
		}
		yield return new WaitForSeconds(1.2f);
		yield break;
	}

	private void SetPlayerPrefs(int challenge)
	{
		//if (AppLovinCrossPromo.Instance() != null && !AFtitle.noAds)
		//{
		//	float num = (!TitleMain.oniPhoneX) ? 1f : 1.4f;
		//	float widthPercentage = 28f * num;
		//	float heightPercentage = 38.5f * num;
		//	if (TitleMain.oniPhoneX)
		//	{
		//		//AppLovinCrossPromo.Instance().ShowMRec(0f, 38f, widthPercentage, heightPercentage, -10f);
		//	}
		//	else
		//	{
		//		//AppLovinCrossPromo.Instance().ShowMRec(2f, 38f, widthPercentage, heightPercentage, -10f);
		//	}
		//}
		bool flag = GameState.Instance.HasBronze(this.stage, this.activeLvl);
		bool flag2 = GameState.Instance.HasSilver(this.stage, this.activeLvl);
		bool flag3 = GameState.Instance.HasGold(this.stage, this.activeLvl);
		bool flag4 = GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0);
		int highScore = GameState.Instance.GetHighScore(this.stage, this.activeLvl);
		if (this.bestScore > highScore)
		{
			this.bestCompleted = true;
			GameState.Instance.SetHighScore(this.stage, this.activeLvl, this.bestScore);
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackRichEvent("level_score", new Dictionary<string, string>
			//	{
			//		{
			//			"af_score",
			//			this.bestScore.ToString()
			//		},
			//		{
			//			"af_level",
			//			LvlModels.stageNames[LvlBtnHandler.activeStage] + "_" + LvlBtnHandler.activeLevel
			//		}
			//	});
			}
		}
		if (!flag && challenge >= 0)
		{
			this.bronzeCompleted = true;
			GameState.Instance.AwardBronze(this.stage, this.activeLvl);
		}
		if (!flag2 && challenge >= 1)
		{
			this.silverCompleted = true;
			GameState.Instance.AwardSilver(this.stage, this.activeLvl);
		}
		if (!flag3 && challenge >= 2)
		{
			this.goldCompleted = true;
			GameState.Instance.AwardGold(this.stage, this.activeLvl);
		}
		if (!flag4 && this.specialCompleted)
		{
			this.specialCompleted2 = true;
			GameState.Instance.AwardSpecial(this.stage, this.activeLvl, 0);
		}
		if (this.bronzeCompleted || this.silverCompleted || this.goldCompleted || this.specialCompleted)
		{
			SubLevel.crashCount = 0;
			Skipper.prompting = false;
		}
		GameState.Instance.Syncronize();
		if (this.silverCompleted || this.goldCompleted)
		{
			if (GameState.Instance.HasNoLevelAccess(LvlBtnHandler.activeStage, StageModel.NextLevelId()))
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["fb_level"] = (LvlBtnHandler.activeLevel + 7 * (LvlBtnHandler.activeStage - 1)).ToString();
				//FB.LogAppEvent("fb_mobile_level_achieved", null, dictionary);
				if (TitleMain.analyticsConsent && LvlBtnHandler.activeStage == 1)
				{
					Dictionary<string, string> eventValues = new Dictionary<string, string>();
					//AppsFlyer.trackRichEvent("gym_stage_" + LvlBtnHandler.activeLevel, eventValues);
					//AppsFlyer.trackEvent("gym_stage_" + LvlBtnHandler.activeLevel, string.Empty);
				}
				SubLevel.attempt++;
				if (TitleMain.analyticsConsent)
				{
					//AppsFlyer.trackRichEvent("attempt", new Dictionary<string, string>
					//{
					//	{
					//		"attempt",
					//		SubLevel.attempt.ToString()
					//	},
					//	{
					//		"af_level",
					//		LvlModels.stageNames[LvlBtnHandler.activeStage] + "_" + LvlBtnHandler.activeLevel
					//	}
					//});
					//AppsFlyer.trackRichEvent("success", new Dictionary<string, string>
					//{
					//	{
					//		"af_level",
					//		LvlModels.stageNames[LvlBtnHandler.activeStage] + "_" + LvlBtnHandler.activeLevel
					//	}
					//});
				}
			}
			if (!StageModel.IsLastLevel(LvlBtnHandler.activeStage, LvlBtnHandler.activeLevel))
			{
				GameState.Instance.SetLevelUnlocked(LvlBtnHandler.activeStage, StageModel.NextLevelId());
			}
			if (GameObject.Find("CompleteSilver") != null)
			{
				GameObject.Find("CompleteSilver").SetActive(false);
			}
			GameState.Instance.Syncronize();
			SubLevel.attempt = 0;
		}
		else if (GameState.Instance.HasNoLevelAccess(LvlBtnHandler.activeStage, StageModel.NextLevelId()))
		{
			this.fullStage.SetActive(false);
			this.skipButton.SetActive(true);
			SubLevel.crashCount++;
			if (SubLevel.crashCount == 3)
			{
				Skipper.prompting = true;
				GameObject.Find("SkipStuff").GetComponent<Skipper>().ShowModal();
				SubLevel.crashCount = 0;
			}
			SubLevel.attempt++;
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackRichEvent("attempt", new Dictionary<string, string>
				//{
				//	{
				//		"attempt",
				//		SubLevel.attempt.ToString()
				//	},
				//	{
				//		"af_level",
				//		LvlModels.stageNames[LvlBtnHandler.activeStage] + "_" + LvlBtnHandler.activeLevel
				//	}
				//});
			}
		}
	}

	private void CheckSpecial()
	{
		switch (this.stage)
		{
		case 1:
			if (Challenges.gymSpecial[this.activeLvl - 1] && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0)
			{
				this.specialCompleted = true;
			}
			break;
		case 2:
			if (Challenges.mountSpecial[this.activeLvl - 1] && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0)
			{
				this.specialCompleted = true;
			}
			break;
		case 3:
			if (Challenges.citySpecial[this.activeLvl - 1] && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0)
			{
				this.specialCompleted = true;
			}
			break;
		case 4:
			if (Challenges.houseSpecial[this.activeLvl - 1] && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0)
			{
				this.specialCompleted = true;
			}
			break;
		case 5:
			if (Challenges.gallerySpecial[this.activeLvl - 1] && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0)
			{
				this.specialCompleted = true;
			}
			break;
		case 6:
			if (Challenges.shipSpecial[this.activeLvl - 1] && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0)
			{
				this.specialCompleted = true;
			}
			break;
		case 7:
			if (Challenges.islandSpecial[this.activeLvl - 1] && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0)
			{
				this.specialCompleted = true;
			}
			break;
		case 8:
			if (Challenges.hauntedSpecial[this.activeLvl - 1] && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0)
			{
				this.specialCompleted = true;
			}
			break;
		case 9:
			if (Challenges.ufoSpecial[this.activeLvl - 1] && PlayerPrefs.GetInt("special" + this.activeLvl + this.stage, 0) == 0)
			{
				this.specialCompleted = true;
			}
			break;
		}
	}

	private IEnumerator Pump()
	{
		if (LvlBtnHandler.activeLevel == 60)
		{
			for (;;)
			{
				yield return new WaitForSeconds(1f);
				this.homeButton.localScale = Vector3.one * 1.1f;
				yield return new WaitForSeconds(0.25f);
				this.homeButton.localScale = Vector3.one * 1.1f;
			}
		}
		else
		{
			for (;;)
			{
				yield return new WaitForSeconds(1f);
				this.nextButton.localScale = Vector3.one * 1.1f;
				yield return new WaitForSeconds(0.25f);
				this.nextButton.localScale = Vector3.one * 1.1f;
			}
		}
		yield break;
	}

	private TextMeshProUGUI coinText;

	private Transform[] coinAwards = new Transform[7];

	private bool gotCoin;

	public Transform afterButtons;

	public Transform canvas;

	private Vector3 abDest;

	public Transform nextButton;

	public Transform previousButton;

	private Transform complimentrans;

	public TextMeshProUGUI complimentText;

	private string[] compliments = new string[]
	{
		"Nice one!",
		"Good job!",
		"Nailed it!",
		"Awesome!",
		"You got it!",
		"Superb!"
	};

	private bool complimenting;

	public float[] bronzeScore;

	public float[] silverScore;

	public float[] goldScore;

	public string[] specialTexts;

	public int stage;

	private int activeLvl;

	public Image back;

	private Color color;

	private float alpha;

	public Transform dotGroup;

	private Vector3 dotDestination;

	private Vector3 dotScale;

	private float dotRotation;

	public Transform[] dots;

	public TextMeshProUGUI[] texts;

	public TextMeshProUGUI bestText;

	private int bestScore;

	private bool completedChallenge;

	private bool specialCompleted;

	private float fontDest = 36f;

	private float textOffset = 0.63f;

	private float bestOffset = 0.37f;

	public TextMeshProUGUI flip;

	public TextMeshProUGUI total;

	public TextMeshProUGUI score;

	public Transform timer;

	public Transform menuStuff;

	public Transform downStuff;

	private Transform homeButton;

	private GameObject fullStage;

	private GameObject skipButton;

	private List<Transform> checks = new List<Transform>();

	private bool bestCompleted;

	private bool bronzeCompleted;

	private bool silverCompleted;

	private bool goldCompleted;

	private bool specialCompleted2;

	private int coinsAwarded;

	private float coinTimer;

	public static int attempt;

	public static int crashCount;
}
