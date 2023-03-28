// dnSpy decompiler from Assembly-CSharp.dll class: LvlButtons
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LvlButtons : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	private void Start()
	{
		this.FixCurrentUsers();
		if ((this.level == 1 && this.stage == 1) || this.stage == 0)
		{
			GameState.Instance.SetLevelUnlocked(this.stage, this.level);
			GameState.Instance.Syncronize();
		}
		if (GameState.Instance.HasLevelAccess(this.stage, this.level))
		{
			this.unlocked = true;
		}
		else
		{
			this.LockLevel();
		}
		if (this.stage == 0)
		{
			return;
		}
		base.Invoke("SetupPrompt", 0.05f);
	}

	private void FixCurrentUsers()
	{
		if (PlayerPrefs.GetInt("IsFixed", 0) == 1)
		{
			for (int i = 1; i < 8; i++)
			{
				int @int = PlayerPrefs.GetInt("bronze" + i + 4);
				PlayerPrefs.SetInt("bronze" + i + 5, @int);
				PlayerPrefs.SetInt("bronze" + i + 4, 0);
				int int2 = PlayerPrefs.GetInt("silver" + i + 4);
				PlayerPrefs.SetInt("silver" + i + 5, int2);
				PlayerPrefs.SetInt("silver" + i + 4, 0);
				int int3 = PlayerPrefs.GetInt("gold" + i + 4);
				PlayerPrefs.SetInt("gold" + i + 5, int3);
				PlayerPrefs.SetInt("gold" + i + 4, 0);
				int int4 = PlayerPrefs.GetInt("special" + i + 4);
				PlayerPrefs.SetInt("special" + i + 5, int4);
				PlayerPrefs.SetInt("special" + i + 4, 0);
			}
			int int5 = PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"special",
				7,
				4,
				1
			}));
			PlayerPrefs.SetInt(string.Concat(new object[]
			{
				"special",
				7,
				5,
				1
			}), int5);
			PlayerPrefs.SetInt(string.Concat(new object[]
			{
				"special",
				7,
				4,
				1
			}), 0);
			int int6 = PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"special",
				7,
				4,
				2
			}));
			PlayerPrefs.SetInt(string.Concat(new object[]
			{
				"special",
				7,
				5,
				2
			}), int6);
			PlayerPrefs.SetInt(string.Concat(new object[]
			{
				"special",
				7,
				4,
				2
			}), 0);
			PlayerPrefs.SetInt("IsFixed", 0);
		}
		if (PlayerPrefs.GetInt("silver" + this.level + this.stage) == 1)
		{
			PlayerPrefs.SetInt("lock" + this.level + this.stage, 1);
			if (StageModel.IsLastLevel(this.stage, this.level))
			{
				PlayerPrefs.SetInt("lock" + 1 + (this.stage + 1), 1);
			}
			else
			{
				PlayerPrefs.SetInt("lock" + (this.level + 1) + this.stage, 1);
			}
		}
	}

	private void SetupPrompt()
	{
		if (!StageModel.IsLastStage(this.stage))
		{
			if (GameState.Instance.HasLevelAccess(this.stage, this.level) && !GameState.Instance.HasLevelAccess(StageModel.NextStageId(this.stage), 1))
			{
				GameObject.Find("Prompt").transform.position = base.transform.position + Vector3.up * 0.075f;
				this.prompting = true;
			}
		}
		else if (GameState.Instance.HasLevelAccess(this.stage, this.level) && !GameState.Instance.HasLevelAccess(this.stage, StageModel.NextLevelId(this.stage, this.level)))
		{
			GameObject.Find("Prompt").transform.position = base.transform.position + Vector3.up * 0.075f;
			this.prompting = true;
		}
	}

	private void LockLevel()
	{
		Image component = base.GetComponent<Image>();
		SpriteRenderer componentInChildren = base.GetComponentInChildren<SpriteRenderer>();
		component.color = Color.black;
		componentInChildren.color = Color.black;
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		componentsInChildren[3].gameObject.SetActive(false);
		base.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
		if (base.name == "Full Stage")
		{
			componentsInChildren[1].gameObject.SetActive(false);
		}
		Image image = new GameObject
		{
			transform = 
			{
				parent = base.transform,
				position = base.transform.position,
				rotation = componentsInChildren[3].rotation,
				localScale = new Vector3(0.25f, 0.35f)
			}
		}.AddComponent<Image>();
		image.sprite = Resources.Load<Sprite>("Sprites/Lock");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!Debug.isDebugBuild && !this.unlocked)
		{
			return;
		}
		GameObject.Find("MAIN").GetComponent<LvlBtnHandler>().LevelSelected(this.level, this.stage);
		if (this.prompting)
		{
			GameObject.Find("Prompt").GetComponent<Image>().enabled = false;
		}
		else
		{
			GameObject.Find("Prompt").GetComponent<Image>().enabled = true;
		}
	}

	public int level;

	public int stage;

	private bool unlocked;

	private bool prompting;
}
