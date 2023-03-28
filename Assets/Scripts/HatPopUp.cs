// dnSpy decompiler from Assembly-CSharp.dll class: HatPopUp
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HatPopUp : MonoBehaviour
{
	private void Start()
	{
		bool flag = GameState.Instance.HasLevelAccess(LvlBtnHandler.activeStage, LvlBtnHandler.activeLevel);
		bool flag2 = GameState.Instance.HasLevelAccess(StageModel.GetSecondStageId(), 1);
		if (flag && flag2)
		{
			MonoBehaviour.print("NAJS");
			string key = "NewHat";
			if (PlayerPrefs.GetInt(key, 0) == 0)
			{
				base.transform.localPosition = Vector3.up * 268f;
				PlayerPrefs.SetInt(key, 1);
				GameState.Instance.AwardHat(0);
				GameState.Instance.Syncronize();
			}
			return;
		}
	}

	public void Exit()
	{
		base.gameObject.SetActive(false);
	}

	public void Click()
	{
		PlayerPrefs.SetInt("ActiveHatNum", 0);
		SceneManager.LoadScene("Customization");
	}

	public static bool shouldPop;
}
