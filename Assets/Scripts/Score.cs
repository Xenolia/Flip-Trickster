// dnSpy decompiler from Assembly-CSharp.dll class: Score
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
	private void Start()
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			this.scoreText.text = string.Empty;
			this.totalText.text = string.Empty;
			this.flipEx.text = string.Empty;
			return;
		}
		Score.scoringPoints = false;
		Score.triple = false;
		Score.doubleF = false;
		this.tucked = false;
		Score.failedAllInARow = false;
		Score.score = 0f;
		this.total = 0;
		this.scoreText.text = string.Empty;
		this.mplierText.text = string.Empty;
		this.flipEx.text = string.Empty;
		this.totalText.text = this.total.ToString();
		this.flipStart = this.flipEx.transform.localPosition;
		if (!StageModel.IsLastLevel())
		{
			this.mGoal = 220f;
		}
	}

	private void FixedUpdate()
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			return;
		}
		if (Score.scoringPoints)
		{
			Score.score += Time.fixedDeltaTime * 100f;
			if (PlayerBF.subLevel)
			{
				this.totalText.text = ((int)Score.score).ToString();
			}
			else
			{
				this.scoreText.text = "+" + ((int)Score.score).ToString();
			}
		}
		if (this.flipEx.text != string.Empty)
		{
			Transform transform = this.flipEx.transform;
			this.qz = Mathf.Lerp(this.qz, 10f, Time.fixedDeltaTime * 10f);
			transform.localRotation = Quaternion.Euler(0f, 0f, this.qz);
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.fixedDeltaTime * 10f);
		}
		if (this.mplierText.text != string.Empty)
		{
			Transform transform2 = this.mplierText.transform;
			transform2.localScale = Vector3.Lerp(transform2.localScale, this.mScale, Time.fixedDeltaTime * 8f);
			transform2.localPosition = Vector3.Lerp(transform2.localPosition, this.mDestination, Time.fixedDeltaTime * 8f);
			float z = Mathf.Sin(Time.time * 15f) * 8f;
			transform2.localRotation = Quaternion.Euler(0f, 0f, z);
			if (transform2.position.y >= this.totalText.transform.position.y && !StageModel.IsLastLevel())
			{
				transform2.localScale = Vector3.zero;
			}
			else if (transform2.position.y >= this.scoreText.transform.position.y && StageModel.IsLastLevel())
			{
				transform2.localScale = Vector3.zero;
			}
		}
	}

	public void FinishedLevel()
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			return;
		}
		Score.finalScore = this.total;
		if (!Score.failedAllInARow)
		{
			GameObject.Find("LevelCompleted").GetComponent<LevelCompleted>().SetScore();
		}
	}

	public void NullifyText()
	{
	}

	public void SetTotalScore()
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			return;
		}
		Score.scoringPoints = false;
		if (PlayerBF.subLevel)
		{
			this.total = 0;
			Score.score = 0f;
			this.scoreText.text = string.Empty;
			this.totalText.text = "0";
			return;
		}
		if (PlayerBF.freeplay)
		{
			Score.score = 0f;
			this.scoreText.text = string.Empty;
			Score.finalScore = this.total;
			return;
		}
		this.total += (int)Score.score;
		if (this.total < 0)
		{
			this.total = 0;
		}
		Score.score = 0f;
		this.scoreText.text = string.Empty;
		this.totalText.text = this.total.ToString();
		Score.finalScore = this.total;
		this.tucked = false;
	}

	public void ShowMultiplier(float multiplier)
	{
		this.mplierText.text = "x" + multiplier;
		Transform transform = this.mplierText.transform;
		transform.localScale = Vector3.zero;
		transform.localPosition = new Vector3(100f, -100f);
		this.mDestination = new Vector3(200f, 0f);
		this.mScale = Vector3.one;
		base.StartCoroutine(this.Multiply(multiplier));
	}

	private IEnumerator Multiply(float multiplier)
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			yield break;
		}
		yield return new WaitForSeconds(1f);
		if (LvlModels.onIpad)
		{
			this.mDestination = ((!StageModel.IsLastLevel()) ? new Vector3(350f, 280f) : new Vector3(350f, 200f));
		}
		else if (TitleMain.oniPhoneX)
		{
			this.mDestination = ((!StageModel.IsLastLevel()) ? new Vector3(600f, 260f) : new Vector3(600f, 190f));
		}
		else
		{
			this.mDestination = ((!StageModel.IsLastLevel()) ? new Vector3(500f, 260f) : new Vector3(500f, 200f));
		}
		this.mScale = Vector3.one * 0.2f;
		yield return new WaitForSeconds(0.2f);
		float newScore = (float)((int)Score.score) * multiplier;
		for (;;)
		{
			Score.score += 11f;
			if (StageModel.IsLastLevel())
			{
				this.scoreText.text = "+" + (int)Score.score;
			}
			else
			{
				this.totalText.text = ((int)Score.score).ToString();
			}
			if (Score.score >= (float)((int)newScore - 12))
			{
				break;
			}
			yield return new WaitForSeconds(0.02f);
		}
		Score.score = newScore;
		if (StageModel.IsLastLevel())
		{
			this.scoreText.text = "+" + (int)Score.score;
		}
		else
		{
			this.totalText.text = ((int)Score.score).ToString();
		}
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("Player").GetComponent<PlayerBF>().NextStage();
		yield break;
	}

	public void LandedOutside()
	{
		Score.score = 0f;
		this.scoreText.text = string.Empty;
		Score.failedAllInARow = true;
		Score.triple = false;
		Score.doubleF = false;
	}

	public void Flipped(int pts)
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			return;
		}
		Score.score += (float)pts;
		Color color = Color.HSVToRGB(0.3f, 0.55f, 1f);
		this.flipEx.text = "FLIP +" + pts.ToString();
		this.flipEx.transform.localRotation = Quaternion.Euler(0f, 180f, 300f);
		this.flipEx.transform.localScale = Vector3.zero;
		this.qz = 370f;
		base.CancelInvoke("NullifyFlip");
		base.Invoke("NullifyFlip", 2f);
		if (pts == 200)
		{
			Score.doubleF = true;
		}
		if (pts == 300)
		{
			Score.triple = true;
		}
	}

	private void NullifyFlip()
	{
		this.flipEx.text = string.Empty;
	}

	public TextMeshProUGUI scoreText;

	public TextMeshProUGUI mplierText;

	public TextMeshProUGUI totalText;

	public TextMeshProUGUI flipEx;

	public static bool scoringPoints;

	public static float score;

	private int total;

	public static int finalScore;

	private bool tucked;

	public static bool failedAllInARow;

	public static bool triple;

	public static bool doubleF;

	private Vector3 startPoint;

	private Vector3 flipStart;

	private Vector3 mDestination;

	private Vector3 mScale;

	private float mGoal = 180f;

	private float qz = 370f;
}
