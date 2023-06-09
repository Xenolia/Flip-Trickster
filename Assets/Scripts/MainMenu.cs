// dnSpy decompiler from Assembly-CSharp.dll class: MainMenu
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	[SerializeField] AdManager _adManager;


    private void Awake()
    {
		_adManager.Init();
    }

    private void Start()
	{
		Application.targetFrameRate = 60;
		QualitySettings.antiAliasing = 0;
	}

	private void Update()
	{
	}

	public void PlayButton()
	{
		//if (AppLovinCrossPromo.Instance())
		//{
		//	AppLovinCrossPromo.Instance().HideMRec();
		//}
		if (!GameState.Instance.HasCompletedTutorial())
		{
			SceneManager.LoadScene("Tutorial");
		}
		else
		{
			SceneManager.LoadScene("NewLvlSelect");
		}
		_adManager.InterstatialAdManager.ShowAd();
	}
}
