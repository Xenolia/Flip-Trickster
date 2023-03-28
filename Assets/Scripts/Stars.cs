// dnSpy decompiler from Assembly-CSharp.dll class: Stars
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
	private void Start()
	{
		this.scoreText.text = PlayerPrefs.GetInt(this.PPBest).ToString();
		this.gray = Color.HSVToRGB(0f, 0f, 0.22f);
		this.yellow = Color.HSVToRGB(0.15f, 1f, 1f);
		this.cScale = this.stars[0].localScale;
		this.cct = base.transform;
		for (int i = 0; i < 5; i++)
		{
			this.stars[i].GetChild(0).GetComponent<Image>().color = this.gray;
			int @int = PlayerPrefs.GetInt(this.PPBest + (i + 1).ToString(), 0);
			if (@int == 1)
			{
				this.count++;
			}
		}
		for (int j = 0; j < this.count; j++)
		{
			this.currentTargets[j] = this.startPoints[j];
			this.stars[j].GetChild(0).GetComponent<Image>().color = this.yellow;
		}
	}

	private void FixedUpdate()
	{
		if (!this.isActive)
		{
			for (int i = 0; i < 5; i++)
			{
				if (this.stars[i] != null && this.currentTargets[i] != null)
				{
					this.stars[i].position = Vector3.Lerp(this.stars[i].position, this.currentTargets[i].position, 1f);
				}
			}
		}
		for (int j = 0; j < 5; j++)
		{
			if (this.stars[j] != null && this.currentTargets[j] != null)
			{
				this.stars[j].position = Vector3.Lerp(this.stars[j].position, this.currentTargets[j].position, Time.fixedDeltaTime * 7f);
				this.stars[j].localScale = Vector3.Lerp(this.stars[j].localScale, this.cScale, Time.fixedDeltaTime * 7f);
			}
		}
		this.challenges.position = Vector3.Lerp(this.challenges.position, this.cct.position, Time.fixedDeltaTime * 7f);
	}

	public void LevelChosen()
	{
		this.cScale = this.tScale;
		this.cct = this.chTarget;
		for (int i = 0; i < 5; i++)
		{
			this.currentTargets[i] = this.targetPoints[i];
		}
		this.ChangeColor();
	}

	public void Unchosen()
	{
		this.cScale = Vector3.one;
		this.cct = base.transform;
		for (int i = 0; i < 5; i++)
		{
			this.currentTargets[i] = this.startPoints[i];
		}
		this.ChangeBack();
	}

	private void ChangeColor()
	{
		for (int i = 0; i < 5; i++)
		{
			this.stars[i].GetChild(0).GetComponent<Image>().color = this.gray;
			this.starValues[i] = PlayerPrefs.GetInt(this.PPBest + (i + 1).ToString());
			if (this.starValues[i] == 1)
			{
				this.stars[i].GetChild(0).GetComponent<Image>().color = this.yellow;
			}
		}
	}

	private void ChangeBack()
	{
		for (int i = 0; i < 5; i++)
		{
			this.stars[i].GetChild(0).GetComponent<Image>().color = this.gray;
		}
		for (int j = 0; j < this.count; j++)
		{
			this.stars[j].GetChild(0).GetComponent<Image>().color = this.yellow;
		}
	}

	public TextMeshProUGUI scoreText;

	public Transform[] stars;

	public Transform[] targetPoints;

	public Transform[] startPoints;

	private Transform[] currentTargets = new Transform[5];

	private Vector3 tScale = new Vector3(0.4f, 0.4f);

	private Vector3 cScale;

	public int[] starValues = new int[5];

	public Transform challenges;

	public Transform chTarget;

	private Transform cct;

	private Color gray;

	private Color yellow;

	public bool isActive;

	[SerializeField]
	private string PPBest;

	private int count;
}
