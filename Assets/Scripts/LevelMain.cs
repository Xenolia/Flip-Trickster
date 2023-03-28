// dnSpy decompiler from Assembly-CSharp.dll class: LevelMain
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMain : MonoBehaviour
{
	private void Start()
	{
		Spinner.activeHatNum = PlayerPrefs.GetInt("ActiveHatNum", -1);
		this.soundText.text = ((AudioListener.volume != 1f) ? "Sound: Off" : "Sound: On");
		LionAdManager.Instance.HideBanner();
		LevelMain.canSwipe = true;
		if (LevelMain.counter < 4)
		{
			LevelMain.counter++;
			return;
		}
		LevelMain.counter = 0;
	}

	private void Update()
	{
		if (this.settingsMenu.activeInHierarchy || this.shopMenu.activeInHierarchy)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			this.start = Camera.main.ScreenToViewportPoint(UnityEngine.Input.mousePosition);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.start = Vector3.zero;
			LevelMain.canSwipe = true;
		}
		if (this.start != Vector3.zero)
		{
			if (!LevelMain.canSwipe)
			{
				return;
			}
			this.current = Camera.main.ScreenToViewportPoint(UnityEngine.Input.mousePosition);
			float num = this.current.x - this.start.x;
			if (num > 0.04f)
			{
				this.start = Vector3.zero;
				GameObject.Find("MAIN").GetComponent<LvlModels>().GoLeft();
			}
			else if (num < -0.04f)
			{
				this.start = Vector3.zero;
				GameObject.Find("MAIN").GetComponent<LvlModels>().GoRight();
			}
		}
	}

	public void CharacterCustomization()
	{
		SceneManager.LoadScene("Customization");
	}

	public void Settings()
	{
		this.settingsMenu.SetActive(true);
	}

	public void Shop()
	{
		this.shopMenu.SetActive(true);
	}

	public void Back()
	{
		this.settingsMenu.SetActive(false);
		this.shopMenu.SetActive(false);
	}

	public void Sound()
	{
		if (AudioListener.volume == 0f)
		{
			AudioListener.volume = 1f;
			this.soundText.text = "Sound: On";
		}
		else
		{
			AudioListener.volume = 0f;
			this.soundText.text = "Sound: Off";
		}
		PlayerPrefs.SetInt("Sound", (int)AudioListener.volume);
	}

	public void Privacy()
	{
		this.gdpr.SetActive(true);
	}

	public GameObject settingsMenu;

	public GameObject shopMenu;

	public GameObject gdpr;

	public TextMeshProUGUI soundText;

	public static int counter;

	private Vector3 start;

	private Vector3 current;

	public static bool canSwipe;
}
