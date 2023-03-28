// dnSpy decompiler from Assembly-CSharp.dll class: FullStage
using System;
using System.Collections;
using System.Collections.Generic;
//using Facebook.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FullStage : MonoBehaviour
{
	private void Start()
	{
		if (LvlBtnHandler.activeLevel != 7)
		{
			return;
		}
		if (LvlModels.onIpad)
		{
			Camera.main.fieldOfView = 80f;
			this.fontDest = 30f;
			this.textOffset = 0.73f;
			this.bestOffset = 0.43f;
			this.flip.fontSize = 40f;
			this.total.fontSize = 50f;
			this.score.fontSize = 35f;
			this.timer.localPosition = new Vector3(140f, 270f);
			this.timer.localScale *= 0.8f;
		}
		this.SetUpCoins();
		this.SetUp();
		this.ShowFinalStageModal();
	}

	private void ShowFinalStageModal()
	{
		if (GameState.Instance.HasLevelAccess(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage()))
		{
			return;
		}
		if (PlayerPrefs.GetInt("FinMod") == LvlBtnHandler.activeStage)
		{
			return;
		}
		PlayerPrefs.SetInt("FinMod", LvlBtnHandler.activeStage);
		GameObject original = Resources.Load<GameObject>("Prefabs/FinalStageModal");
		Transform transform = GameObject.Find("NewCanvas02").transform;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, transform.position, transform.rotation, transform);
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

	private void SetUp()
	{
		this.complimentrans = this.complimentText.transform;
		this.homeButton = GameObject.Find("Home").transform;
		Challenges.yellowZoneCount = 0;
		Challenges.flipCount = 0;
		Challenges.targetCount = 0;
		Challenges.houseSpecial[7] = true;
		Challenges.shipSpecial[6] = true;
		Challenges.shipSpecial[5] = true;
		Challenges.islandSpecial[5] = false;
		Challenges.islandSpecial[6] = true;
		Challenges.islandSpecial[7] = true;
		Challenges.hauntedSpecial[6] = true;
		Challenges.noTuckCount = 0;
		this.activeLvl = LvlBtnHandler.activeLevel;
		this.stage = LvlBtnHandler.activeStage;
		this.flip.text = string.Empty;
		this.score.text = string.Empty;
		this.total.text = "0";
		this.texts[0].text = this.bronzeScore.ToString();
		this.texts[1].text = this.silverScore.ToString();
		this.texts[2].text = this.goldScore.ToString();
		this.texts[3].text = this.specialTexts[0];
		this.texts[4].text = this.specialTexts[1];
		this.texts[5].text = this.specialTexts[2];
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
		if (GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
		{
			this.SetupCheck(4);
		}
		if (GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
		{
			this.SetupCheck(5);
		}
		this.bestScore = GameState.Instance.GetHighScore(this.stage, this.activeLvl);
		this.bestText.text = "Best: " + this.bestScore;
		this.abDest = new Vector3(0f, -240f);
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
		this.stageSpecialDots[0].SetActive(true);
		this.stageSpecialDots[1].SetActive(true);
		this.nextButton = GameObject.Find("NextLevel").transform;
		this.skipButton = GameObject.Find("Skip");
		this.fullStage = GameObject.Find("FullStage");
		this.fullStage.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
		Button componentInChildren = this.fullStage.GetComponentInChildren<Button>();
		componentInChildren.onClick.AddListener(new UnityAction(GameObject.Find("MAIN").GetComponent<Main>().NextStage));
		this.skipButton.SetActive(false);
		if (StageModel.IsLastLevel() && LvlBtnHandler.activeStage == LvlModels.lvlNum)
		{
			this.nextButton.gameObject.SetActive(false);
		}
		if (TitleMain.oniPhoneX)
		{
			this.total.fontSize = 80f;
			this.menuStuff.localScale = Vector3.one * 1.2f;
			this.downStuff.localScale = Vector3.one * 1.2f;
			this.dotGroup.localPosition += Vector3.right * -130f + Vector3.down * 520f;
			this.dotGroup.localScale += Vector3.one * 0.2f;
			this.afterButtons.localScale += Vector3.one * 0.2f;
		}
		if (GameState.Instance.HasLevelAccess(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage()))
		{
			GameObject.Find("CompleteSilver").SetActive(false);
		}
		if (StageModel.IsLastLevel() && StageModel.IsLastStage() && GameObject.Find("CompleteSilver"))
		{
			GameObject.Find("CompleteSilver").SetActive(false);
		}
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
			this.bestText.transform.position = this.dotGroup.position + this.bestText.transform.up * 0.145f + this.bestText.transform.right * this.bestOffset;
		}
		else
		{
			this.bestText.transform.position = this.dotGroup.position + this.bestText.transform.up * 1f;
		}
		this.bestText.fontSize = Mathf.Lerp(this.bestText.fontSize, this.fontDest + 8f, Time.fixedDeltaTime * 5f);
		this.afterButtons.localPosition = Vector3.Lerp(this.afterButtons.localPosition, this.abDest, Time.fixedDeltaTime * 10f);
		if (this.complimenting)
		{
			float d = Mathf.Sin(Time.time * 8f) / 5f + 1f;
			float z = Mathf.Sin(Time.time * 4f) * 5f;
			this.complimentrans.localScale = Vector3.Lerp(this.complimentrans.localScale, Vector3.one * d, Time.fixedDeltaTime * 10f);
			this.complimentrans.localRotation = Quaternion.Euler(0f, 0f, z);
			this.homeButton.localScale = Vector3.Lerp(this.homeButton.localScale, Vector3.one, Time.fixedDeltaTime * 1f);
		}
		if (this.gotCoin)
		{
			this.CoinMove();
		}
	}

	private void CoinMove()
	{
		if (this.coinTimer == 100f)
		{
			this.coinAwards[6].localScale = Vector3.Lerp(this.coinAwards[6].localScale, Vector3.one * 0.8f, Time.fixedDeltaTime * 8f);
			Vector3 vector = (!LvlModels.onIpad) ? Vector3.one : new Vector3(80f, -60f);
			return;
		}
		Vector3 vector2 = Vector3.up * 520f;
		vector2 = ((!TitleMain.oniPhoneX) ? vector2 : (Vector3.up * 710f));
		vector2 += ((!LvlModels.onIpad) ? Vector3.zero : (Vector3.right * 50f));
		int num = 3;
		if (this.coinAwards[0])
		{
			this.coinAwards[0].localPosition = Vector3.Lerp(this.coinAwards[0].localPosition, vector2 + Vector3.left * -30f, Time.fixedDeltaTime * (float)num);
		}
		if (this.coinAwards[1])
		{
			this.coinAwards[1].localPosition = Vector3.Lerp(this.coinAwards[1].localPosition, vector2 + Vector3.left * 10f, Time.fixedDeltaTime * (float)num);
		}
		if (this.coinAwards[2])
		{
			this.coinAwards[2].localPosition = Vector3.Lerp(this.coinAwards[2].localPosition, vector2 + Vector3.left * 50f, Time.fixedDeltaTime * (float)num);
		}
		if (this.coinAwards[3])
		{
			this.coinAwards[3].localPosition = Vector3.Lerp(this.coinAwards[3].localPosition, vector2 + Vector3.left * 90f, Time.fixedDeltaTime * (float)num);
		}
		if (this.coinAwards[4])
		{
			this.coinAwards[4].localPosition = Vector3.Lerp(this.coinAwards[4].localPosition, vector2 + Vector3.left * 130f, Time.fixedDeltaTime * (float)num);
		}
		if (this.coinAwards[5])
		{
			this.coinAwards[5].localPosition = Vector3.Lerp(this.coinAwards[5].localPosition, vector2 + Vector3.left * 170f, Time.fixedDeltaTime * (float)num);
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
			if (this.coinAwards[4])
			{
				UnityEngine.Object.Destroy(this.coinAwards[4].gameObject);
			}
			if (this.coinAwards[5])
			{
				UnityEngine.Object.Destroy(this.coinAwards[5].gameObject);
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
		this.coinAwards[6] = copy;
		Vector3 spawn = new Vector3(30f, 550f);
		copy.GetComponent<TextMeshProUGUI>().text = "+" + this.coinsAwarded;
		copy.localRotation = Quaternion.Euler(0f, 0f, 90f);
		if (TitleMain.oniPhoneX)
		{
			copy.localPosition = spawn + new Vector3(-35f, 145f);
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

	public void CompletedStage()
	{
		if (LvlBtnHandler.activeStage == 2 && Challenges.yellowZoneCount >= 4)
		{
			Challenges.mountSpecial[6] = true;
		}
		if (LvlBtnHandler.activeStage == 2 && Challenges.flipCount >= 12)
		{
			Challenges.mountSpecial[7] = true;
		}
		if (LvlBtnHandler.activeStage == 5 && Challenges.flipCount > 6)
		{
			Challenges.gallerySpecial[6] = false;
		}
		if (LvlBtnHandler.activeStage == 3 && Challenges.flipCount != 6)
		{
			Challenges.citySpecial[5] = false;
		}
		if (LvlBtnHandler.activeStage == 3 && Challenges.redZoneCount >= 3)
		{
			Challenges.citySpecial[6] = true;
		}
		if (LvlBtnHandler.activeStage == 4 && Challenges.targetCount >= 3)
		{
			Challenges.houseSpecial[5] = true;
		}
		if (LvlBtnHandler.activeStage == 4 && Challenges.redZoneCount >= 3)
		{
			Challenges.houseSpecial[6] = true;
		}
		if (LvlBtnHandler.activeStage == 6 && Challenges.yellowZoneCount == 6)
		{
			Challenges.shipSpecial[7] = true;
		}
		if (LvlBtnHandler.activeStage == 6 && Challenges.flipCount != 6)
		{
			Challenges.shipSpecial[5] = false;
		}
		if (LvlBtnHandler.activeStage == 7 && Challenges.flipCount > 2)
		{
			Challenges.islandSpecial[7] = false;
		}
		if (LvlBtnHandler.activeStage == 8 && Challenges.redZoneCount >= 4)
		{
			Challenges.hauntedSpecial[7] = true;
		}
		if (LvlBtnHandler.activeStage == 9 && Challenges.noTuckCount >= 4)
		{
			Challenges.ufoSpecial[5] = true;
		}
		if (LvlBtnHandler.activeStage == 9 && Challenges.flipCount == 0 && FullStage.crashCount == 0)
		{
			Challenges.ufoSpecial[6] = true;
		}
		if (LvlBtnHandler.activeStage == 9 && Challenges.redZoneCount == 5)
		{
			Challenges.ufoSpecial[7] = true;
		}
		this.CheckScoreChallenges((float)Score.finalScore);
	}

	public void CheckScoreChallenges(float score)
	{
		this.CheckSpecial();
		if (score >= this.goldScore)
		{
			if ((int)score > this.bestScore)
			{
				this.bestScore = (int)score;
			}
			base.StartCoroutine(this.CompletedChallenge(2));
			return;
		}
		if (score >= this.silverScore)
		{
			if ((int)score > this.bestScore)
			{
				this.bestScore = (int)score;
			}
			base.StartCoroutine(this.CompletedChallenge(1));
			return;
		}
		if (score >= this.bronzeScore)
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
			this.dotDestination = new Vector3(-300f, 65f);
			this.dotScale = Vector3.one * 1.2f;
		}
		else if (LvlModels.onIpad)
		{
			this.dotDestination = new Vector3(-450f, 0f);
			this.dotScale = Vector3.one * 1f;
		}
		else
		{
			this.dotDestination = new Vector3(-380f, 60f);
			this.dotScale = Vector3.one * 1.3f;
		}
		this.SetPlayerPrefs(challenge);
		this.completedChallenge = true;
		this.dotRotation = -90f;
		this.color.a = 0f;
		this.alpha = 0.5f;
		this.back.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.8f);
		if (this.bestCompleted)
		{
			this.bestText.fontSize = 70f;
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
		if (this.specialCompleted00)
		{
			this.dots[3].transform.localScale = Vector3.one * 1.5f;
			this.texts[3].fontSize = 55f;
			this.GetChecked(3);
			Transform copy4 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/CoinAward"), this.dots[3].transform).transform;
			this.coinAwards[3] = copy4;
			Vector3 spawn4 = this.dots[3].transform.position;
			copy4.GetComponent<TextMeshProUGUI>().text = "+15";
			copy4.localRotation = Quaternion.Euler(0f, 0f, 90f);
			copy4.localPosition = spawn4;
			copy4.localScale = Vector3.one * 0.6f;
			this.coinsAwarded += 15;
			Currency.coinAmount += 15;
			this.gotCoin = true;
			this.coinTimer = 0f;
			yield return new WaitForSeconds(0.4f);
		}
		if (this.specialCompleted01)
		{
			this.dots[4].transform.localScale = Vector3.one * 1.5f;
			this.texts[4].fontSize = (float)((!Challenges.bigtext) ? 55 : 50);
			this.GetChecked(4);
			Transform copy5 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/CoinAward"), this.dots[4].transform).transform;
			this.coinAwards[4] = copy5;
			Vector3 spawn5 = this.dots[4].transform.position;
			copy5.GetComponent<TextMeshProUGUI>().text = "+15";
			copy5.localRotation = Quaternion.Euler(0f, 0f, 90f);
			copy5.localPosition = spawn5;
			copy5.localScale = Vector3.one * 0.6f;
			this.coinsAwarded += 15;
			Currency.coinAmount += 15;
			this.gotCoin = true;
			this.coinTimer = 0f;
			yield return new WaitForSeconds(0.4f);
		}
		if (this.specialCompleted02)
		{
			this.dots[5].transform.localScale = Vector3.one * 1.5f;
			this.texts[5].fontSize = 55f;
			this.GetChecked(5);
			Transform copy6 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/CoinAward"), this.dots[5].transform).transform;
			this.coinAwards[5] = copy6;
			Vector3 spawn6 = this.dots[5].transform.position;
			copy6.GetComponent<TextMeshProUGUI>().text = "+15";
			copy6.localRotation = Quaternion.Euler(0f, 0f, 90f);
			copy6.localPosition = spawn6;
			copy6.localScale = Vector3.one * 0.6f;
			this.coinsAwarded += 15;
			Currency.coinAmount += 15;
			this.gotCoin = true;
			this.coinTimer = 0f;
			yield return new WaitForSeconds(0.4f);
		}
		if (this.bronzeCompleted || this.silverCompleted || this.goldCompleted || this.specialCompleted || this.specialCompleted01 || this.specialCompleted02)
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
		yield break;
	}

	private void SetPlayerPrefs(int challenge)
	{
		//if (AppLovinCrossPromo.Instance() != null && !AFtitle.noAds)
		//{
		//	float num = (!TitleMain.oniPhoneX) ? 1f : 1.4f;
		//	float widthPercentage = 26f * num;
		//	float heightPercentage = 35.75f * num;
		//	if (TitleMain.oniPhoneX)
		//	{
		//		AppLovinCrossPromo.Instance().ShowMRec(0f, 38f, widthPercentage, heightPercentage, -10f);
		//	}
		//	else
		//	{
		//		AppLovinCrossPromo.Instance().ShowMRec(1f, 38f, widthPercentage, heightPercentage, -10f);
		//	}
		//}
		bool flag = GameState.Instance.HasBronze(this.stage, this.activeLvl);
		bool flag2 = GameState.Instance.HasSilver(this.stage, this.activeLvl);
		bool flag3 = GameState.Instance.HasGold(this.stage, this.activeLvl);
		bool flag4 = GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0);
		bool flag5 = GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1);
		bool flag6 = GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2);
		int highScore = GameState.Instance.GetHighScore(this.stage, this.activeLvl);
		if (this.bestScore > highScore)
		{
			this.bestCompleted = true;
			GameState.Instance.SetHighScore(this.stage, this.activeLvl, this.bestScore);
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackRichEvent("level_score", new Dictionary<string, string>
				//{
				//	{
				//		"af_score",
				//		this.bestScore.ToString()
				//	},
				//	{
				//		"af_level",
				//		LvlModels.stageNames[LvlBtnHandler.activeStage] + "_full_stage"
				//	}
				//});
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
			PlayerPrefs.SetInt("silver" + this.activeLvl + this.stage, 1);
			GameState.Instance.AwardSilver(this.stage, this.activeLvl);
		}
		if (!flag3 && challenge >= 2)
		{
			this.goldCompleted = true;
			GameState.Instance.AwardGold(this.stage, this.activeLvl);
		}
		if (!flag4 && this.specialCompleted)
		{
			this.specialCompleted00 = true;
			GameState.Instance.AwardSpecial(this.stage, this.activeLvl, 0);
		}
		if (!flag5 && this.specialCompleted1)
		{
			this.specialCompleted01 = true;
			GameState.Instance.AwardSpecial(this.stage, this.activeLvl, 1);
		}
		if (!flag6 && this.specialCompleted2)
		{
			this.specialCompleted02 = true;
			GameState.Instance.AwardSpecial(this.stage, this.activeLvl, 2);
		}
		if (this.bronzeCompleted || this.silverCompleted || this.goldCompleted || this.specialCompleted || this.specialCompleted1 || this.specialCompleted2)
		{
			FullStage.crashCount = 0;
			Skipper.prompting = false;
		}
		GameState.Instance.Syncronize();
		if (this.silverCompleted || this.goldCompleted)
		{
			if (!GameState.Instance.HasLevelAccess(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage()))
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["fb_level"] = (LvlBtnHandler.activeLevel + 7 * (LvlBtnHandler.activeStage - 1)).ToString();
				//FB.LogAppEvent("fb_mobile_level_achieved", null, dictionary);
				//if (TitleMain.analyticsConsent)
				//{
				//	Dictionary<string, string> eventValues = new Dictionary<string, string>();
				//	//AppsFlyer.trackRichEvent("full_stage_" + LvlModels.stageNames[LvlBtnHandler.activeStage], eventValues);
				//	//AppsFlyer.trackEvent("full_stage_" + LvlModels.stageNames[LvlBtnHandler.activeStage], string.Empty);
				//}
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
					//		LvlModels.stageNames[LvlBtnHandler.activeStage] + "_full_stage"
					//	}
					//});
					//AppsFlyer.trackRichEvent("success", new Dictionary<string, string>
					//{
					//	{
					//		"af_level",
					//		LvlModels.stageNames[LvlBtnHandler.activeStage] + "_full_stage"
					//	}
					//});
				}
				SubLevel.attempt = 0;
			}
			if (TitleMain.analyticsConsent && this.goldCompleted)
			{
				Dictionary<string, string> eventValues2 = new Dictionary<string, string>();
				//AppsFlyer.trackRichEvent("gold_full_stage_" + LvlModels.stageNames[LvlBtnHandler.activeStage], eventValues2);
				//AppsFlyer.trackEvent("gold_full_stage_" + LvlModels.stageNames[LvlBtnHandler.activeStage], string.Empty);
			}
			GameState.Instance.SetLevelUnlocked(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage());
			GameState.Instance.Syncronize();
			LvlModels.justUnlockedNewStage = true;
			if (GameObject.Find("CompleteSilver"))
			{
				GameObject.Find("CompleteSilver").SetActive(false);
			}
		}
		else if (GameState.Instance.HasNoLevelAccess(StageModel.NextStageId(), StageModel.FirstLevelIdOfNextStage()))
		{
			this.skipButton.SetActive(true);
			FullStage.crashCount++;
			if (FullStage.crashCount == 3)
			{
				Skipper.prompting = true;
				GameObject.Find("SkipStuff").GetComponent<Skipper>().ShowModal();
				FullStage.crashCount = 0;
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
				//		LvlModels.stageNames[LvlBtnHandler.activeStage] + "_full_stage"
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
			if (Challenges.gymSpecial[5] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.specialCompleted = true;
			}
			if (Challenges.gymSpecial[6] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
			{
				this.specialCompleted1 = true;
			}
			if (Challenges.gymSpecial[7] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
			{
				this.specialCompleted2 = true;
			}
			break;
		case 2:
			if (Challenges.mountSpecial[5] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.specialCompleted = true;
			}
			if (Challenges.mountSpecial[6] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
			{
				this.specialCompleted1 = true;
			}
			if (Challenges.mountSpecial[7] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
			{
				this.specialCompleted2 = true;
			}
			break;
		case 3:
			if (Challenges.citySpecial[5] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.specialCompleted = true;
			}
			if (Challenges.citySpecial[6] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
			{
				this.specialCompleted1 = true;
			}
			if (Challenges.citySpecial[7] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
			{
				this.specialCompleted2 = true;
			}
			break;
		case 4:
			if (Challenges.houseSpecial[5] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.specialCompleted = true;
			}
			if (Challenges.houseSpecial[6] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
			{
				this.specialCompleted1 = true;
			}
			if (Challenges.houseSpecial[7] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
			{
				this.specialCompleted2 = true;
			}
			break;
		case 5:
			if (Challenges.gallerySpecial[5] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.specialCompleted = true;
			}
			if (Challenges.gallerySpecial[6] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
			{
				this.specialCompleted1 = true;
			}
			if (Challenges.gallerySpecial[7] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
			{
				this.specialCompleted2 = true;
			}
			break;
		case 6:
			if (Challenges.shipSpecial[5] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.specialCompleted = true;
			}
			if (Challenges.shipSpecial[6] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
			{
				this.specialCompleted1 = true;
			}
			if (Challenges.shipSpecial[7] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
			{
				this.specialCompleted2 = true;
			}
			break;
		case 7:
			if (Challenges.islandSpecial[5] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.specialCompleted = true;
			}
			if (Challenges.islandSpecial[6] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
			{
				this.specialCompleted1 = true;
			}
			if (Challenges.islandSpecial[7] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
			{
				this.specialCompleted2 = true;
			}
			break;
		case 8:
			if (Challenges.hauntedSpecial[5] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.specialCompleted = true;
			}
			if (Challenges.hauntedSpecial[6] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
			{
				this.specialCompleted1 = true;
			}
			if (Challenges.hauntedSpecial[7] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
			{
				this.specialCompleted2 = true;
			}
			break;
		case 9:
			if (Challenges.ufoSpecial[5] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 0))
			{
				this.specialCompleted = true;
			}
			if (Challenges.ufoSpecial[6] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 1))
			{
				this.specialCompleted1 = true;
			}
			if (Challenges.ufoSpecial[7] && !GameState.Instance.HasSpecial(this.stage, this.activeLvl, 2))
			{
				this.specialCompleted2 = true;
			}
			break;
		}
	}

	private IEnumerator Pump()
	{
		for (;;)
		{
			yield return new WaitForSeconds(1f);
			this.nextButton.localScale = Vector3.one * 1.1f;
			yield return new WaitForSeconds(0.25f);
			this.nextButton.localScale = Vector3.one * 1.1f;
		}
		yield break;
	}

	private TextMeshProUGUI coinText;

	private Transform[] coinAwards = new Transform[9];

	private bool gotCoin;

	public Transform completedButtons;

	private Vector3 compDest = new Vector3(0f, -520f);

	public Transform dotGroup;

	public GameObject[] stageSpecialDots;

	public float bronzeScore;

	public float silverScore;

	public float goldScore;

	public string[] specialTexts;

	private int stage;

	private int activeLvl;

	public Image back;

	private Color color;

	private float alpha;

	private Vector3 dotDestination;

	private Vector3 dotScale;

	private float dotRotation;

	public Transform[] dots;

	public TextMeshProUGUI[] texts;

	public TextMeshProUGUI bestText;

	private int bestScore;

	private bool completedChallenge;

	private bool specialCompleted;

	private bool specialCompleted1;

	private bool specialCompleted2;

	private float fontDest = 36f;

	private float textOffset = 0.63f;

	private float bestOffset = 0.37f;

	public TextMeshProUGUI flip;

	public TextMeshProUGUI total;

	public TextMeshProUGUI score;

	public Transform timer;

	public Transform homeButton;

	public Transform afterButtons;

	public Transform nextButton;

	public Transform previousButton;

	public Transform menuStuff;

	public Transform downStuff;

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

	private Vector3 abDest;

	private List<Transform> checks = new List<Transform>();

	private GameObject skipButton;

	private GameObject fullStage;

	private int coinsAwarded;

	private float coinTimer;

	private bool bestCompleted;

	private bool bronzeCompleted;

	private bool silverCompleted;

	private bool goldCompleted;

	private bool specialCompleted00;

	private bool specialCompleted01;

	private bool specialCompleted02;

	public static int crashCount;
}
