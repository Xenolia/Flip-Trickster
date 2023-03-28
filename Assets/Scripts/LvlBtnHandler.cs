// dnSpy decompiler from Assembly-CSharp.dll class: LvlBtnHandler
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlBtnHandler : MonoBehaviour
{
	private void Start()
	{
		this.Setup();
		this.CorrectStuff();
		if (LvlModels.onIpad)
		{
			this.tutorialText.transform.position += Vector3.left * 0f;
			this.challengeTexts.transform.position += Vector3.left * 0.15f;
			this.challengeTexts.transform.position += Vector3.down * 0.03f;
			this.playButton.localScale *= 0.7f;
		}
		if (TitleMain.oniPhoneX)
		{
			this.challengeTexts.transform.position += Vector3.right * 0.05f;
			this.tutorialText.transform.position += Vector3.right * 0.05f;
		}
		this.SetupFills();
		this.SetupOverlays();
		this.playButton.localPosition = this.playBtnDestination;
	}

	private void SetupOverlays()
	{
		for (int i = 1; i <= this.overlays.Length; i++)
		{
			if (GameState.Instance.HasLevelAccess(i + 1, 1))
			{
				this.overlays[i - 1].SetActive(false);
			}
		}
	}

	private void Setup()
	{
		this.gymOutlines = new GameObject[63];
		this.bronzeFills = new GameObject[63];
		this.silverFills = new GameObject[63];
		this.goldFills = new GameObject[63];
		this.specialFills = new GameObject[63];
		for (int i = 0; i < LvlModels.lvlNum; i++)
		{
			Transform[] componentsInChildren = this.uis[i].GetComponentsInChildren<Transform>(true);
			Transform[] array = new Transform[]
			{
				componentsInChildren[1],
				componentsInChildren[14],
				componentsInChildren[27],
				componentsInChildren[40],
				componentsInChildren[53],
				componentsInChildren[66],
				componentsInChildren[79]
			};
			Transform[] array2 = new Transform[7];
			for (int j = 0; j < 7; j++)
			{
				Transform[] componentsInChildren2 = array[j].GetComponentsInChildren<Transform>(true);
				array2[j] = componentsInChildren2[3];
				int num = 7 * i + j;
				this.gymOutlines[num] = array2[j].gameObject;
			}
			Transform[] array3 = new Transform[7];
			Transform[] array4 = new Transform[7];
			Transform[] array5 = new Transform[7];
			Transform[] array6 = new Transform[7];
			for (int k = 0; k < 7; k++)
			{
				Transform[] componentsInChildren3 = array[k].GetComponentsInChildren<Transform>(true);
				array3[k] = componentsInChildren3[6];
				array4[k] = componentsInChildren3[8];
				array5[k] = componentsInChildren3[10];
				array6[k] = componentsInChildren3[12];
				int num2 = 7 * i + k;
				this.bronzeFills[num2] = array3[k].gameObject;
				this.silverFills[num2] = array4[k].gameObject;
				this.goldFills[num2] = array5[k].gameObject;
				this.specialFills[num2] = array6[k].gameObject;
			}
		}
	}

	private void CorrectStuff()
	{
		int[] array = new int[7];
		int[] array2 = new int[7];
		int[] array3 = new int[7];
		for (int i = 0; i < 7; i++)
		{
			array[i] = this.bronzeScores[21 + i];
			array2[i] = this.bronzeScores[28 + i];
			array3[i] = this.bronzeScores[14 + i];
			this.bronzeScores[14 + i] = array[i];
			this.bronzeScores[21 + i] = array2[i];
			this.bronzeScores[28 + i] = array3[i];
		}
		int[] array4 = new int[7];
		int[] array5 = new int[7];
		int[] array6 = new int[7];
		for (int j = 0; j < 7; j++)
		{
			array4[j] = this.silverScores[21 + j];
			array5[j] = this.silverScores[28 + j];
			array6[j] = this.silverScores[14 + j];
			this.silverScores[14 + j] = array4[j];
			this.silverScores[21 + j] = array5[j];
			this.silverScores[28 + j] = array6[j];
		}
		int[] array7 = new int[7];
		int[] array8 = new int[7];
		int[] array9 = new int[7];
		for (int k = 0; k < 7; k++)
		{
			array7[k] = this.goldScores[21 + k];
			array8[k] = this.goldScores[28 + k];
			array9[k] = this.goldScores[14 + k];
			this.goldScores[14 + k] = array7[k];
			this.goldScores[21 + k] = array8[k];
			this.goldScores[28 + k] = array9[k];
		}
		string[] array10 = new string[9];
		string[] array11 = new string[9];
		string[] array12 = new string[9];
		for (int l = 0; l < 9; l++)
		{
			array10[l] = this.specialChallenges[27 + l];
			array11[l] = this.specialChallenges[36 + l];
			array12[l] = this.specialChallenges[18 + l];
			this.specialChallenges[18 + l] = array10[l];
			this.specialChallenges[27 + l] = array11[l];
			this.specialChallenges[36 + l] = array12[l];
		}
		GameObject[] array13 = new GameObject[2];
		GameObject[] array14 = new GameObject[2];
		GameObject[] array15 = new GameObject[2];
		for (int m = 0; m < 2; m++)
		{
			array13[m] = this.stageSpecialFills[6 + m];
			array14[m] = this.stageSpecialFills[8 + m];
			array15[m] = this.stageSpecialFills[4 + m];
			this.stageSpecialFills[4 + m] = array13[m];
			this.stageSpecialFills[6 + m] = array14[m];
			this.stageSpecialFills[8 + m] = array15[m];
		}
	}

	private void Update()
	{
		this.playButton.localPosition = Vector3.Lerp(this.playButton.localPosition, this.playBtnDestination, Time.deltaTime * 15f);
	}

	public void LevelSelected(int level, int stage)
	{
		if (!this.challengeTexts.activeInHierarchy && stage != 0)
		{
			this.challengeTexts.SetActive(true);
		}
		if (stage == 0)
		{
			this.tutorialText.SetActive(true);
		}
		this.ActivateOutline(level, stage);
		this.ChangeChallenges(level, stage);
		if (!LvlModels.onIpad)
		{
			this.playBtnDestination = new Vector3(443f, -210f);
		}
		else
		{
			this.playBtnDestination = new Vector3(310f, -210f);
		}
		int highScore = GameState.Instance.GetHighScore(stage, level);
		this.hiScoreText.text = highScore.ToString();
	}

	private void ActivateOutline(int level, int stage)
	{
		switch (stage)
		{
		case 0:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1].SetActive(false);
			}
			this.tutorialOutline.SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		case 1:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1].SetActive(false);
			}
			this.gymOutlines[level - 1].SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		case 2:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1 + 7].SetActive(false);
			}
			this.gymOutlines[level - 1 + 7].SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		case 3:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1 + 14].SetActive(false);
			}
			this.gymOutlines[level - 1 + 14].SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		case 4:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1 + 21].SetActive(false);
			}
			this.gymOutlines[level - 1 + 21].SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		case 5:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1 + 28].SetActive(false);
			}
			this.gymOutlines[level - 1 + 28].SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		case 6:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1 + 35].SetActive(false);
			}
			this.gymOutlines[level - 1 + 35].SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		case 7:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1 + 42].SetActive(false);
			}
			this.gymOutlines[level - 1 + 42].SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		case 8:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1 + 49].SetActive(false);
			}
			this.gymOutlines[level - 1 + 49].SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		case 9:
			if (LvlBtnHandler.activeLevel > 0)
			{
				this.gymOutlines[LvlBtnHandler.activeLevel - 1 + 56].SetActive(false);
			}
			this.gymOutlines[level - 1 + 56].SetActive(true);
			LvlBtnHandler.activeLevel = level;
			break;
		}
	}

	private void ChangeChallenges(int level, int stage)
	{
		if (stage == 0)
		{
			return;
		}
		if (StageModel.IsLastLevel(stage, level))
		{
			this.stageSpecials[0].SetActive(true);
			this.stageSpecials[1].SetActive(true);
		}
		else
		{
			this.stageSpecials[0].SetActive(false);
			this.stageSpecials[1].SetActive(false);
		}
		int num = 7 * (LvlBtnHandler.activeStage - 1);
		int num2 = 9 * (LvlBtnHandler.activeStage - 1);
		this.bronzeText.text = this.bronzeScores[level - 1 + num].ToString();
		this.silverText.text = this.silverScores[level - 1 + num].ToString();
		this.goldText.text = this.goldScores[level - 1 + num].ToString();
		this.specialText.text = this.specialChallenges[level - 1 + num2];
		if (StageModel.IsLastLevel(stage, level))
		{
			this.stageSpecialTexts[0].text = this.specialChallenges[7 + num2];
			this.stageSpecialTexts[1].text = this.specialChallenges[8 + num2];
			this.stageSpecialTexts[2].text = "Special challenges";
		}
		else
		{
			this.stageSpecialTexts[2].text = "Special challenge";
		}
		this.SetupDots(level, stage);
	}

	private void SetupDots(int level, int stage)
	{
		if (LvlBtnHandler.activeLevel > 0)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("CheckUI");
			for (int i = 0; i < array.Length; i++)
			{
				UnityEngine.Object.Destroy(array[i]);
			}
		}
		if (GameState.Instance.HasBronze(stage, level))
		{
			this.DotCheck(this.bronzeDots[0]);
		}
		if (GameState.Instance.HasSilver(stage, level))
		{
			this.DotCheck(this.silverDots[0]);
		}
		if (GameState.Instance.HasGold(stage, level))
		{
			this.DotCheck(this.goldDots[0]);
		}
		if (GameState.Instance.HasSpecial(stage, level, 0))
		{
			this.DotCheck(this.specialDots[0]);
		}
		if (StageModel.IsLastLevel(stage, level))
		{
			if (GameState.Instance.HasSpecial(stage, level, 1))
			{
				this.DotCheck(this.specialDots1[0]);
			}
			if (GameState.Instance.HasSpecial(stage, level, 2))
			{
				this.DotCheck(this.specialDots2[0]);
			}
		}
	}

	private void DotCheck(GameObject dot)
	{
		Transform parent = dot.transform.parent;
		GameObject original = Resources.Load<GameObject>("Prefabs/CheckUI");
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, parent.position, Quaternion.Euler(0f, 0f, -90f), parent);
		gameObject.transform.localScale = Vector3.one * 0.3f;
	}

	private void SetupFills()
	{
		for (int i = (LvlBtnHandler.activeStage - 1) * 7; i < LvlBtnHandler.activeStage * 7; i++)
		{
			int levelId = i - 7 * (LvlBtnHandler.activeStage - 1) + 1;
			if (GameState.Instance.HasBronze(LvlBtnHandler.activeStage, levelId))
			{
				this.GetChecked(this.bronzeFills[i]);
			}
			if (GameState.Instance.HasSilver(LvlBtnHandler.activeStage, levelId))
			{
				this.GetChecked(this.silverFills[i]);
			}
			if (GameState.Instance.HasGold(LvlBtnHandler.activeStage, levelId))
			{
				this.GetChecked(this.goldFills[i]);
			}
			if (GameState.Instance.HasSpecial(LvlBtnHandler.activeStage, levelId, 0))
			{
				this.GetChecked(this.specialFills[i]);
			}
		}
		if (LvlBtnHandler.activeStage > 0 && GameState.Instance.HasSpecial(LvlBtnHandler.activeStage, StageModel.GetLastLevelId(LvlBtnHandler.activeStage), 1))
		{
			this.GetChecked(this.stageSpecialFills[(LvlBtnHandler.activeStage - 1) * 2]);
		}
		if (LvlBtnHandler.activeStage > 0 && GameState.Instance.HasSpecial(LvlBtnHandler.activeStage, StageModel.GetLastLevelId(LvlBtnHandler.activeStage), 2))
		{
			this.GetChecked(this.stageSpecialFills[1 + (LvlBtnHandler.activeStage - 1) * 2]);
		}
	}

	private void GetChecked(GameObject fill)
	{
		Transform parent = fill.transform.parent;
		GameObject original = Resources.Load<GameObject>("Prefabs/Check");
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, parent.position, Quaternion.Euler(0f, 0f, 45f), parent);
	}

	public void ChangeStage(int direction)
	{
		if (LvlBtnHandler.activeLevel == 0)
		{
			this.tutorialOutline.SetActive(false);
			this.tutorialText.SetActive(false);
		}
		if (LvlBtnHandler.activeLevel > 0)
		{
			this.gymOutlines[LvlBtnHandler.activeLevel - 1 + 7 * (LvlBtnHandler.activeStage - 1)].SetActive(false);
		}
		if (this.challengeTexts.activeInHierarchy)
		{
			this.challengeTexts.SetActive(false);
		}
		this.playBtnDestination = new Vector3(443f, -390f);
		LvlBtnHandler.activeStage += direction;
		this.SetupFills();
		LvlBtnHandler.activeLevel = 0;
	}

	public void Play()
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			SceneManager.LoadScene("Tutorial");
			return;
		}
		string[] array = new string[]
		{
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
		SceneManager.LoadScene(array[LvlBtnHandler.activeStage - 1]);
	}

	public GameObject[] uis;

	public GameObject[] gymOutlines;

	public GameObject tutorialOutline;

	public Transform playButton;

	private Vector3 playBtnDestination = new Vector3(443f, -390f);

	public GameObject tutorialText;

	public GameObject challengeTexts;

	public TextMeshProUGUI bronzeText;

	public TextMeshProUGUI silverText;

	public TextMeshProUGUI goldText;

	public TextMeshProUGUI specialText;

	public TextMeshProUGUI[] stageSpecialTexts;

	public int[] bronzeScores;

	public int[] silverScores;

	public int[] goldScores;

	public string[] specialChallenges;

	public GameObject[] stageSpecials;

	public GameObject[] bronzeDots;

	public GameObject[] silverDots;

	public GameObject[] goldDots;

	public GameObject[] specialDots;

	public GameObject[] specialDots1;

	public GameObject[] specialDots2;

	public GameObject[] bronzeFills;

	public GameObject[] silverFills;

	public GameObject[] goldFills;

	public GameObject[] specialFills;

	public GameObject[] stageSpecialFills;

	public TextMeshProUGUI hiScoreText;

	public GameObject[] overlays;

	public static int activeLevel;

	public static int activeStage;
}
