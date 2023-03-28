// dnSpy decompiler from Assembly-CSharp.dll class: LevelCompleted
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		this.best = PlayerPrefs.GetInt(this.PPbest);
		this.yellow = Color.HSVToRGB(0.15f, 1f, 1f);
		this.gray = Color.HSVToRGB(0f, 0f, 0.25f);
		for (int i = 0; i < 5; i++)
		{
			this.stars[i].color = this.gray;
		}
	}

	public void SetScore()
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			return;
		}
		this.score = Score.finalScore;
		this.best = PlayerPrefs.GetInt(this.PPbest);
		if (this.score > this.best)
		{
			this.best = this.score;
			PlayerPrefs.SetInt(this.PPbest, this.best);
		}
		this.scoreText.text = this.score.ToString();
		this.bestText.text = this.best.ToString();
		this.CheckStarScore();
		this.CheckChallenges();
	}

	private void CheckStarScore()
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			return;
		}
		int @int = PlayerPrefs.GetInt(this.PPbest + "1");
		int int2 = PlayerPrefs.GetInt(this.PPbest + "2");
		int int3 = PlayerPrefs.GetInt(this.PPbest + "3");
		if (this.score >= this.ch1 || @int == 1)
		{
			this.stars[0].color = this.yellow;
			PlayerPrefs.SetInt(this.PPbest + "1", 1);
		}
		if (this.score >= this.ch2 || int2 == 1)
		{
			this.stars[1].color = this.yellow;
			PlayerPrefs.SetInt(this.PPbest + "2", 1);
		}
		if (this.score >= this.ch3 || int3 == 1)
		{
			this.stars[2].color = this.yellow;
			PlayerPrefs.SetInt(this.PPbest + "3", 1);
		}
	}

	private void CheckChallenges()
	{
		int @int = PlayerPrefs.GetInt(this.PPbest + "4");
		int int2 = PlayerPrefs.GetInt(this.PPbest + "5");
	}

	private void AskforReview()
	{
	}

	public Image[] stars;

	public TextMeshProUGUI scoreText;

	public TextMeshProUGUI bestText;

	private int score;

	private int best;

	public int ch1;

	public int ch2;

	public int ch3;

	[SerializeField]
	private string PPbest;

	private Color yellow;

	private Color gray;
}
