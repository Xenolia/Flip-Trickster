// dnSpy decompiler from Assembly-CSharp.dll class: Modal
using System;
using TMPro;
using UnityEngine;

public class Modal : MonoBehaviour
{
	private void Start()
	{
		this.Optimize();
		this.dest = this.modal.localPosition;
		this.start = this.dest;
		this.SetupChallenges();
	}

	private void Optimize()
	{
		if (!TitleMain.oniPhoneX && !LvlModels.onIpad)
		{
			base.transform.localPosition += Vector3.left * 55f + Vector3.down * 5f;
			if (StageModel.IsLastLevel())
			{
				base.transform.localPosition += Vector3.right * 70f;
			}
		}
		else if (LvlModels.onIpad)
		{
			base.transform.localPosition += Vector3.left * 100f + Vector3.down * 10f;
			this.modal.localPosition += Vector3.left * 20f;
			if (StageModel.IsLastLevel())
			{
				base.transform.localPosition += Vector3.right * 50f;
			}
		}
		else
		{
			this.modal.localPosition += Vector3.right * 10f;
			if (StageModel.IsLastLevel())
			{
				base.transform.localPosition += Vector3.right * 90f;
			}
		}
	}

	private void SetupChallenges()
	{
		TextMeshProUGUI[] componentsInChildren = this.modal.GetComponentsInChildren<TextMeshProUGUI>();
		if (!StageModel.IsLastLevel())
		{
			SubLevel component = GameObject.Find("MAIN").GetComponent<SubLevel>();
			componentsInChildren[0].text = component.bronzeScore[LvlBtnHandler.activeLevel - 1].ToString();
			componentsInChildren[1].text = component.silverScore[LvlBtnHandler.activeLevel - 1].ToString();
			componentsInChildren[2].text = component.goldScore[LvlBtnHandler.activeLevel - 1].ToString();
			componentsInChildren[3].text = component.specialTexts[LvlBtnHandler.activeLevel - 1];
			componentsInChildren[4].transform.parent.gameObject.SetActive(false);
			componentsInChildren[5].transform.parent.gameObject.SetActive(false);
			for (int i = 0; i < 4; i++)
			{
				string[] array = new string[]
				{
					"bronze",
					"silver",
					"gold",
					"special"
				};
				int subKey = (i >= 3) ? (i - 3) : -1;
				if (GameState.Instance.GetInt(array[i], LvlBtnHandler.activeStage, LvlBtnHandler.activeLevel, subKey) == 1)
				{
					Transform parent = componentsInChildren[i].transform.parent;
					GameObject original = Resources.Load<GameObject>("Prefabs/CheckUI");
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, parent.position, parent.rotation, parent);
					gameObject.transform.localScale = Vector3.one * 0.2f;
					gameObject.transform.Rotate(0f, 0f, -90f);
					componentsInChildren[i].color = Color.gray;
				}
			}
		}
		else
		{
			FullStage component2 = GameObject.Find("MAIN").GetComponent<FullStage>();
			componentsInChildren[0].text = ((int)component2.bronzeScore).ToString();
			componentsInChildren[1].text = ((int)component2.silverScore).ToString();
			componentsInChildren[2].text = ((int)component2.goldScore).ToString();
			componentsInChildren[3].text = component2.specialTexts[0];
			componentsInChildren[4].text = component2.specialTexts[1];
			componentsInChildren[5].text = component2.specialTexts[2];
			for (int j = 0; j < 6; j++)
			{
				string[] array2 = new string[]
				{
					"bronze",
					"silver",
					"gold",
					"special",
					"special",
					"special"
				};
				int subKey2 = (j >= 3) ? (j - 3) : -1;
				if (GameState.Instance.GetInt(array2[j], LvlBtnHandler.activeStage, LvlBtnHandler.activeLevel, subKey2) == 1)
				{
					Transform parent2 = componentsInChildren[j].transform.parent;
					GameObject original2 = Resources.Load<GameObject>("Prefabs/CheckUI");
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2, parent2.position, parent2.rotation, parent2);
					gameObject2.transform.localScale = Vector3.one * 0.2f;
					gameObject2.transform.Rotate(0f, 0f, -90f);
					componentsInChildren[j].color = Color.gray;
				}
			}
		}
	}

	private void Update()
	{
		if (this.showing && Input.GetMouseButtonDown(0))
		{
			this.Dismiss();
		}
		if (this.showing)
		{
			this.modal.localPosition = Vector3.Lerp(this.modal.localPosition, this.dest, Time.deltaTime * 15f);
		}
	}

	public void ShowModal()
	{
		float y;
		if (LvlBtnHandler.activeLevel != 7)
		{
			y = -270f;
		}
		else
		{
			y = -220f;
		}
		this.dest = new Vector3(this.dest.x, y);
		this.showing = true;
	}

	public void Dismiss()
	{
		this.dest = this.start;
	}

	public Transform modal;

	private Vector3 dest;

	private Vector3 start;

	private bool showing;
}
