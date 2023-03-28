// dnSpy decompiler from Assembly-CSharp.dll class: FinalStageModal
using System;
using TMPro;
using UnityEngine;

public class FinalStageModal : MonoBehaviour
{
	private void Start()
	{
		string[] array = new string[]
		{
			"null",
			"Gym",
			"Mountain",
			"City",
			"Funhouse",
			"Factory",
			"Ship",
			"Island",
			"Haunted House",
			"Space"
		};
		TextMeshProUGUI componentInChildren = base.GetComponentInChildren<TextMeshProUGUI>();
		if (StageModel.IsLastStage(LvlBtnHandler.activeStage))
		{
			string stageName = StageModel.GetStageName(LvlBtnHandler.activeStage);
			componentInChildren.text = "Go through each " + stageName + " level\nto complete the stage";
		}
		else
		{
			string stageName2 = StageModel.GetStageName(LvlBtnHandler.activeStage);
			string stageName3 = StageModel.GetStageName(StageModel.NextStageId());
			componentInChildren.text = string.Concat(new string[]
			{
				"Go through each ",
				stageName2,
				" level\nto move on to the ",
				stageName3,
				"!"
			});
		}
	}

	public void GotIt()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
