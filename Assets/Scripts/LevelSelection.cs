// dnSpy decompiler from Assembly-CSharp.dll class: LevelSelection
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
	private void Start()
	{
		this.aud = base.GetComponent<AudioSource>();
		this.lvlButtons = new Button[this.levels.Length];
		for (int i = 0; i < this.levels.Length; i++)
		{
			this.lvlButtons[i] = this.levels[i].GetComponentInChildren<Button>();
		}
		this.playStart = this.playBtn.position;
		this.backStart = this.backBtn.position;
		this.targetPos = base.transform.position;
		this.rightStart = this.rightBtn.position;
		this.leftStart = this.leftBtn.position;
		this.lvlScale = this.levels[0].localScale;
		this.distance = this.levels[1].position.x - base.transform.position.x;
		this.AskForReview();
		if (GameState.Instance.HasCompletedTutorial())
		{
			this.GoRight();
		}
	}

	private void Update()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, this.targetPos, Time.deltaTime * 10f);
		this.levels[this.activeLvl].localScale = Vector3.Lerp(this.levels[this.activeLvl].localScale, this.lvlScale, Time.deltaTime * 10f);
		this.rightBtn.position = ((!this.levelChosen) ? Vector3.Lerp(this.rightBtn.position, this.rightStart, Time.deltaTime * 10f) : Vector3.Lerp(this.rightBtn.position, this.rightTarget.position, Time.deltaTime * 10f));
		this.leftBtn.position = ((!this.levelChosen) ? Vector3.Lerp(this.leftBtn.position, this.leftStart, Time.deltaTime * 10f) : Vector3.Lerp(this.leftBtn.position, this.leftTarget.position, Time.deltaTime * 10f));
		this.playBtn.position = ((!this.levelChosen) ? Vector3.Lerp(this.playBtn.position, this.playStart, Time.deltaTime * 10f) : Vector3.Lerp(this.playBtn.position, this.playTarget.position, Time.deltaTime * 10f));
		this.backBtn.position = ((!this.levelChosen) ? Vector3.Lerp(this.backBtn.position, this.backStart, Time.deltaTime * 10f) : Vector3.Lerp(this.backBtn.position, this.backTarget.position, Time.deltaTime * 10f));
		this.freeBtn.position = ((!this.play) ? Vector3.Lerp(this.freeBtn.position, this.playBtn.position, Time.deltaTime * 10f) : Vector3.Lerp(this.freeBtn.position, this.freeTarget.position, Time.deltaTime * 10f));
	}

	public void GoRight()
	{
		this.aud.Play();
		this.targetPos += Vector3.left * this.distance;
		this.activeLvl++;
		if (this.activeLvl == 1)
		{
			this.leftBtn.gameObject.SetActive(true);
		}
		if (this.activeLvl == this.levels.Length - 1)
		{
			this.rightBtn.gameObject.SetActive(false);
		}
		if (this.lvlButtons[this.activeLvl - 1].transform.parent.GetComponentInChildren<Stars>() != null)
		{
			this.lvlButtons[this.activeLvl - 1].transform.parent.GetComponentInChildren<Stars>().isActive = false;
		}
		if (this.lvlButtons[this.activeLvl].transform.parent.GetComponentInChildren<Stars>() != null)
		{
			this.lvlButtons[this.activeLvl].transform.parent.GetComponentInChildren<Stars>().isActive = true;
		}
	}

	public void GoLeft()
	{
		this.aud.Play();
		this.targetPos += Vector3.right * this.distance;
		this.activeLvl--;
		if (this.activeLvl == 0)
		{
			this.leftBtn.gameObject.SetActive(false);
		}
		if (this.activeLvl == this.levels.Length - 2)
		{
			this.rightBtn.gameObject.SetActive(true);
		}
		if (this.lvlButtons[this.activeLvl + 1].transform.parent.GetComponentInChildren<Stars>() != null)
		{
			this.lvlButtons[this.activeLvl + 1].transform.parent.GetComponentInChildren<Stars>().isActive = false;
		}
		if (this.lvlButtons[this.activeLvl].transform.parent.GetComponentInChildren<Stars>() != null)
		{
			this.lvlButtons[this.activeLvl].transform.parent.GetComponentInChildren<Stars>().isActive = true;
		}
		MonoBehaviour.print(this.activeLvl);
	}

	public void SelectLevel()
	{
		this.aud.Play();
		this.lvlButtons[this.activeLvl].gameObject.SetActive(false);
		this.lvlScale *= 1.2f;
		this.levelChosen = true;
		this.lvlButtons[this.activeLvl].transform.parent.GetComponentInChildren<Stars>().LevelChosen();
	}

	public void Play()
	{
		this.aud.Play();
		if (this.activeLvl == 0)
		{
			this.Normal();
			return;
		}
		if (this.play)
		{
			this.Normal();
		}
		this.freeBtn.gameObject.SetActive(true);
		this.playText.text = "NORMAL";
		this.playText.fontSize = 50f;
		this.play = true;
	}

	public void Back()
	{
		this.aud.Play();
		this.lvlButtons[this.activeLvl].gameObject.SetActive(true);
		this.lvlScale -= Vector3.one * 0.2f;
		this.levelChosen = false;
		this.play = false;
		this.freeBtn.gameObject.SetActive(false);
		this.playText.text = "PLAY";
		this.playText.fontSize = 75f;
		this.lvlButtons[this.activeLvl].transform.parent.GetComponentInChildren<Stars>().Unchosen();
	}

	public void Freeplay()
	{
		this.aud.Play();
		SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - this.activeLvl - 1);
	}

	private void Normal()
	{
		this.aud.Play();
		int num = this.activeLvl;
		if (num != 0)
		{
			if (num == 1)
			{
				SceneManager.LoadScene("Gym");
			}
		}
		else
		{
			SceneManager.LoadScene("Tutorial");
		}
	}

	private void AskForReview()
	{
		if (LevelSelection.justUnlockedMount)
		{
			LevelSelection.justUnlockedMount = false;
			PlayerPrefs.SetInt("Asked", 1);
		}
	}

	private AudioSource aud;

	public TextMeshProUGUI playText;

	private Button[] lvlButtons;

	public Transform playBtn;

	public Transform backBtn;

	public Transform freeBtn;

	public Transform[] levels;

	public Transform leftBtn;

	public Transform rightBtn;

	private Vector3 targetPos;

	private Vector3 rightStart;

	public Transform rightTarget;

	private Vector3 leftStart;

	public Transform leftTarget;

	private Vector3 lvlScale;

	private Vector3 playStart;

	public Transform playTarget;

	private Vector3 backStart;

	public Transform backTarget;

	public Transform freeTarget;

	private int activeLvl;

	private float distance;

	private bool levelChosen;

	private bool play;

	public static bool justUnlockedMount;
}
