// dnSpy decompiler from Assembly-CSharp.dll class: InGamePanel
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{
	private void Start()
	{
		bool interstitialsDisabled = LvlBtnHandler.activeStage > 0 && LvlBtnHandler.activeLevel > 0 && StageModel.IsFirstStage(LvlBtnHandler.activeStage) && !GameState.Instance.HasLevelAccess(StageModel.GetSecondStageId(), 1) && !StageModel.IsLastLevel();
		LionAdManager.Instance.SetInterstitialsDisabled(interstitialsDisabled);
		Button[] componentsInChildren = base.GetComponentsInChildren<Button>();
		componentsInChildren[2].onClick.AddListener(new UnityAction(this.ShowHomeModal));
	}

	private void Update()
	{
	}

	private void Exit()
	{
		UnityEngine.Object.Destroy(this.modal);
	}

	public void Restart()
	{
		SubLevel.attempt++;
		if (LionAdManager.Instance.IsTimeToShowIntersitial() && !AFtitle.noAds)
		{
			AFtitle.watchedReward = false;
			LionAdManager.Instance.MaybeShowInterstitial();
		}
		//if (AppLovinCrossPromo.Instance() != null && !AFtitle.noAds)
		//{
		//	AppLovinCrossPromo.Instance().HideMRec();
		//}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ShowHomeModal()
	{
		GameObject gameObject = GameObject.Find("NewCanvas02");
		this.modal = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/HomeModal"), gameObject.transform);
		Button[] componentsInChildren = this.modal.GetComponentsInChildren<Button>();
		componentsInChildren[0].onClick.AddListener(new UnityAction(this.Exit));
		componentsInChildren[1].onClick.AddListener(new UnityAction(this.Menu));
		componentsInChildren[2].onClick.AddListener(new UnityAction(this.Exit));
	}

	public void Menu()
	{
		if (SceneManager.GetActiveScene().name != "Customization" && !AFtitle.noAds && LionAdManager.Instance.IsTimeToShowIntersitial() && LvlBtnHandler.activeStage != 0)
		{
			AFtitle.watchedReward = false;
			LionAdManager.Instance.MaybeShowInterstitial();
		}
		//if (AppLovinCrossPromo.Instance() != null)
		//{
		//	AppLovinCrossPromo.Instance().HideMRec();
		//}
		if (LvlBtnHandler.activeStage == 0)
		{
			Time.timeScale = 1f;
		}
		PlayerBF.jumped = false;
		PlayerBF.land = false;
		PlayerBF.fallen = false;
		SceneManager.LoadScene("NewLvlSelect");
	}

	public void NextStage()
	{
		LvlBtnHandler.activeStage++;
		LvlBtnHandler.activeLevel = 1;
		string[] array = new string[]
		{
			string.Empty,
			"GymNew",
			"MountNew",
			"City",
			"House",
			"Gallery",
			"Ship"
		};
		SceneManager.LoadScene(array[LvlBtnHandler.activeStage]);
	}

	private void GameOver()
	{
		this.gameOverPanel.SetActive(true);
		base.Invoke("Restart", 2f);
	}

	public void LevelCompleted()
	{
		this.lvlCompletePanel.SetActive(true);
		GameObject.Find("Score").GetComponent<Score>().SetTotalScore();
		GameObject.Find("Score").GetComponent<Score>().FinishedLevel();
	}

	public GameObject gameOverPanel;

	public GameObject lvlCompletePanel;

	private GameObject modal;
}
