// dnSpy decompiler from Assembly-CSharp.dll class: Tutorial1
using System;
using System.Collections;
//using Facebook.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial1 : MonoBehaviour
{
	private void Start()
	{
		if (Stages.stance == 1)
		{
			GameObject.Find("MAIN").GetComponent<Stages>().ChangeStance();
		}
		if (TitleMain.oniPhoneX)
		{
			this.menuButton.localPosition = new Vector3(-430f, 200f);
			this.menuButton.localScale = Vector3.one * 1.2f;
			this.iPad.SetActive(false);
		}
		else if (LvlModels.onIpad)
		{
			Camera.main.fieldOfView = 80f;
			this.menuButton.localPosition = new Vector3(-315f, 240f);
			this.iPhone.SetActive(false);
		}
		else
		{
			this.plane.localScale = new Vector3(0.8f, 1f, 0.45f);
			this.iPad.SetActive(false);
		}
		GameState.Instance.MarkTutorialCompleted();
		GameState.Instance.Syncronize();
		this.jumpButton.SetActive(false);
		this.teacherStartX = this.teacherCam.transform.position.x;
		this.teacherDest = this.teacherCam.transform.position;
		this.handDest = this.hand.localPosition;
		this.handRot = this.hand.localRotation;
		this.handSpeed = 5f;
		for (int i = 0; i < 9; i++)
		{
			if (i < 4)
			{
				this.textDestinations[i] = new Vector3(600f, 100f);
			}
			else
			{
				this.textDestinations[i] = new Vector3(600f, 50f);
			}
			this.texts[i].fontSize = 40f;
		}
		this.text.text = string.Empty;
		this.continueText.text = string.Empty;
		Tutorial1.trying = false;
		base.StartCoroutine(this.TeacherShowing());
	}

	private void Update()
	{
		this.teacherCam.transform.position = Vector3.Lerp(this.teacherCam.transform.position, this.teacherDest, Time.deltaTime * 5f);
		this.hand.localPosition = Vector3.Lerp(this.hand.localPosition, this.handDest, Time.deltaTime * this.handSpeed);
		this.hand.localRotation = Quaternion.Lerp(this.hand.localRotation, this.handRot, Time.deltaTime * this.handSpeed);
		if (this.teacherCam.transform.position.x <= this.teacherStartX + 0.05f && this.handSpeed == 8f)
		{
			this.gameCam.targetTexture = null;
			this.teacherCam.enabled = false;
			this.homeUIButton.enabled = true;
			this.handSpeed = 0f;
		}
		for (int i = 0; i < 9; i++)
		{
			this.textTrans[i].localPosition = Vector3.Lerp(this.textTrans[i].localPosition, this.textDestinations[i], Time.deltaTime * 15f);
		}
		if (Input.GetMouseButtonDown(0) && this.canRemove)
		{
			this.canRemove = false;
			base.StartCoroutine(this.RemoveTexts());
		}
		if (this.tap.gameObject.activeInHierarchy)
		{
			float a = Mathf.Sin(this.tm * 8f) / 2f + 0.5f;
			Color white = Color.white;
			white.a = a;
			this.tap.color = white;
			this.tm += Time.deltaTime;
		}
		if (this.completed)
		{
			Color color = this.back.color;
			color.a = Mathf.Lerp(color.a, 0.4f, Time.deltaTime * 10f);
			this.back.color = color;
			this.ab.transform.localPosition = Vector3.Lerp(this.ab.transform.localPosition, Vector3.down * 135f, Time.deltaTime * 10f);
			float d = Mathf.Sin(Time.time * 8f) / 5f + 1f;
			float z = Mathf.Sin(Time.time * 4f) * 5f;
			this.complimentrans.localScale = Vector3.Lerp(this.complimentrans.localScale, Vector3.one * d, Time.deltaTime * 10f);
			this.complimentrans.localRotation = Quaternion.Euler(0f, 0f, z);
		}
	}

	private IEnumerator ShowText01()
	{
		yield return new WaitForSeconds(1f);
		this.textDestinations[0] = new Vector3(135f, 100f);
		this.texts[0].text = this.welcome[0];
		yield return new WaitForSeconds(0.1f);
		this.textDestinations[1] = new Vector3(232f, 100f);
		this.texts[1].text = this.welcome[1];
		yield return new WaitForSeconds(0.1f);
		this.textDestinations[4] = new Vector3(70f, 50f);
		this.texts[4].text = this.welcome[2];
		yield return new WaitForSeconds(0.1f);
		this.textDestinations[5] = new Vector3(187f, 50f);
		this.texts[5].text = this.welcome[3];
		yield return new WaitForSeconds(0.4f);
		this.tap.gameObject.SetActive(true);
		this.tm = 1.37f;
		this.messageCount++;
		this.canRemove = true;
		yield break;
	}

	private IEnumerator ShowText02()
	{
		this.textDestinations[0] = new Vector3(35f, 100f);
		this.texts[0].text = this.learn[0];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[1] = new Vector3(101f, 100f);
		this.texts[1].text = this.learn[1];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[2] = new Vector3(171f, 100f);
		this.texts[2].text = this.learn[2];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[3] = new Vector3(257f, 100f);
		this.texts[3].text = this.learn[3];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[4] = new Vector3(71f, 50f);
		this.texts[4].text = this.learn[4];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[5] = new Vector3(130f, 50f);
		this.texts[5].text = this.learn[5];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[6] = new Vector3(174f, 50f);
		this.texts[6].text = this.learn[6];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[7] = new Vector3(212f, 50f);
		this.texts[7].text = this.learn[7];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[8] = new Vector3(257f, 50f);
		this.texts[8].text = this.learn[8];
		yield return new WaitForSeconds(0.4f);
		this.tap.gameObject.SetActive(true);
		this.tm = 1.37f;
		this.messageCount++;
		this.canRemove = true;
		yield break;
	}

	private IEnumerator ShowText03()
	{
		this.textDestinations[0] = new Vector3(40f, 100f);
		this.texts[0].text = this.show[0];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[1] = new Vector3(80f, 100f);
		this.texts[1].text = this.show[1];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[2] = new Vector3(160f, 100f);
		this.texts[2].text = this.show[2];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[3] = new Vector3(240f, 100f);
		this.texts[3].text = this.show[3];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[4] = new Vector3(82f, 50f);
		this.texts[4].text = this.show[4];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[5] = new Vector3(150f, 50f);
		this.texts[5].text = this.show[5];
		yield return new WaitForSeconds(0.08f);
		this.textDestinations[6] = new Vector3(223f, 50f);
		this.texts[6].text = this.show[6];
		yield return new WaitForSeconds(0.4f);
		this.tap.gameObject.SetActive(true);
		this.tm = 1.37f;
		this.messageCount++;
		this.canRemove = true;
		yield break;
	}

	private IEnumerator ShowText04()
	{
		this.textDestinations[4] = new Vector3(90f, 50f);
		this.texts[4].text = this.justWatch[0];
		yield return new WaitForSeconds(0.1f);
		this.textDestinations[5] = new Vector3(190f, 50f);
		this.texts[5].text = this.justWatch[1];
		yield return new WaitForSeconds(0.4f);
		this.tap.gameObject.SetActive(true);
		this.tm = 1.37f;
		this.messageCount++;
		this.canRemove = true;
		yield break;
	}

	private IEnumerator ShowText05()
	{
		this.textDestinations[4] = new Vector3(128f, 50f);
		this.texts[4].text = "Now";
		yield return new WaitForSeconds(0.1f);
		this.textDestinations[5] = new Vector3(202f, 50f);
		this.texts[5].text = "you";
		yield return new WaitForSeconds(0.1f);
		this.textDestinations[6] = new Vector3(270f, 50f);
		this.texts[6].text = "try!";
		this.messageCount++;
		yield break;
	}

	public IEnumerator Success()
	{
		yield return new WaitForSeconds(1.5f);
		if (PlayerPrefs.GetInt("TutorialTracked", 0) == 0)
		{
			if (TitleMain.analyticsConsent)
			{
				////AppsFlyer.trackEvent("Tutorial", "Completed");
			}
			//FB.LogAppEvent("CompletedTutorial", null, null);
			PlayerPrefs.SetInt("TutorialTracked", 1);
		}
		this.back.gameObject.SetActive(true);
		this.completed = true;
		this.textDestinations[0] = new Vector3(-130f, 90f);
		this.texts[0].text = "Tutorial";
		this.texts[0].fontSize = 60f;
		yield return new WaitForSeconds(0.1f);
		this.textDestinations[7] = new Vector3(110f, 90f);
		this.texts[7].text = "completed";
		this.texts[7].fontSize = 60f;
		yield return new WaitForSeconds(0.5f);
		this.complimentrans.localScale = Vector3.zero;
		this.complimentrans.gameObject.SetActive(true);
		yield break;
	}

	public IEnumerator Failed()
	{
		yield return new WaitForSeconds(0.3f);
		this.textDestinations[2] = new Vector3(196f, 100f);
		this.texts[2].text = "Almost!";
		yield return new WaitForSeconds(0.1f);
		this.textDestinations[5] = new Vector3(150f, 50f);
		this.texts[5].text = "Try";
		yield return new WaitForSeconds(0.1f);
		this.textDestinations[6] = new Vector3(230f, 50f);
		this.texts[6].text = "again";
		base.StartCoroutine(this.RemoveTexts());
		yield break;
	}

	private IEnumerator RemoveTexts()
	{
		Color color = Color.white;
		color.a = 0f;
		this.tap.color = color;
		this.tap.gameObject.SetActive(false);
		this.tm = 0f;
		if (this.texts[2].text == "Almost!")
		{
			yield return new WaitForSeconds(2f);
		}
		for (int i = 8; i > -1; i--)
		{
			if (this.textTrans[i].localPosition.x <= 490f)
			{
				if (i < 4)
				{
					this.textDestinations[i] = new Vector3(600f, 100f);
				}
				else
				{
					this.textDestinations[i] = new Vector3(600f, 50f);
				}
				yield return new WaitForSeconds(0.08f);
			}
		}
		yield return new WaitForSeconds(0.2f);
		switch (this.messageCount)
		{
		case 1:
			base.StartCoroutine(this.ShowText02());
			break;
		case 2:
			base.StartCoroutine(this.ShowText03());
			break;
		case 3:
			base.StartCoroutine(this.ShowText04());
			break;
		case 4:
			base.StartCoroutine(this.TeacherShowing());
			break;
		case 5:
			yield return new WaitForSeconds(0.3f);
			this.jumpButton.SetActive(true);
			Tutorial1.trying = true;
			break;
		}
		yield break;
	}

	private IEnumerator TeacherShowing()
	{
		yield return new WaitForSeconds(0.8f);
		this.gameCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 100);
		this.camMat.mainTexture = this.gameCam.targetTexture;
		this.teacherCam.enabled = true;
		yield return new WaitForSeconds(0.1f);
		this.homeUIButton.enabled = false;
		this.teacherDest += Vector3.right * 1.4f;
		this.handDest = new Vector3(300f, -164f);
		yield return new WaitForSeconds(1.5f);
		PlayerBF pb = GameObject.Find("Player").GetComponent<PlayerBF>();
		this.handSpeed = 24f;
		this.handRot = Quaternion.Euler(0f, 0f, 40f);
		this.handDest = new Vector3(262f, -182f);
		yield return new WaitForSeconds(0.12f);
		this.press.SetActive(true);
		pb.Squat();
		yield return new WaitForSeconds(0.7f);
		this.press.SetActive(false);
		pb.Jump();
		this.handDest = new Vector3(300f, -164f);
		this.handRot = Quaternion.Euler(0f, 0f, 30f);
		yield return new WaitForSeconds(0.8f);
		this.handRot = Quaternion.Euler(0f, 0f, 40f);
		this.handDest = new Vector3(262f, -182f);
		yield return new WaitForSeconds(0.12f);
		this.press.SetActive(true);
		pb.Tuck();
		yield return new WaitForSeconds(0.78f);
		this.press.SetActive(false);
		pb.PrepareForLanding();
		this.handDest = new Vector3(300f, -164f);
		this.handRot = Quaternion.Euler(0f, 0f, 30f);
		yield return new WaitForSeconds(1.5f);
		this.handSpeed = 8f;
		this.handDest = new Vector3(300f, -494f);
		yield return new WaitForSeconds(0.1f);
		this.teacherDest += Vector3.left * 1.41f;
		yield return new WaitForSeconds(1.2f);
		yield return new WaitForSeconds(0f);
		GameObject.Find("MAIN").GetComponent<Stages>().RetryStage();
		yield return new WaitForSeconds(0.9f);
		this.jumpButton.SetActive(true);
		Tutorial1.trying = true;
		this.canJump = true;
		this.text.text = "Touch and hold to squat";
		yield break;
	}

	public void JumpButtonDown()
	{
		PlayerBF component = GameObject.Find("Player").GetComponent<PlayerBF>();
		if (!this.canJump && this.jumpCounter == 100)
		{
			Color color = this.back.color;
			color.a = 0f;
			this.back.color = color;
			this.continueText.text = string.Empty;
			this.jumpCounter -= 100;
			Time.timeScale = 1f;
		}
		if (!this.canJump && this.jumpCounter == 1)
		{
			this.back.gameObject.SetActive(true);
			Color color2 = this.back.color;
			color2.a = 0.4f;
			this.back.color = color2;
			this.continueText.text = "Wait for the right moment!";
			this.jumpCounter += 100;
			Time.timeScale = 0f;
		}
		if (!this.canJump && this.jumpCounter == 102)
		{
			Color color3 = this.back.color;
			color3.a = 0f;
			this.back.color = color3;
			this.continueText.text = string.Empty;
			this.jumpCounter -= 100;
			Time.timeScale = 1f;
		}
		if (this.canJump && this.jumpCounter == 0)
		{
			base.StartCoroutine(this.StudentFlipping());
			this.text.text = string.Empty;
			this.canJump = false;
		}
		if (this.canJump && this.jumpCounter == 1)
		{
			Time.timeScale = 1f;
			component.Tuck();
			this.text.text = string.Empty;
			this.canJump = false;
			this.jumpCounter = 2;
		}
	}

	public void JumpButtonUp()
	{
		PlayerBF component = GameObject.Find("Player").GetComponent<PlayerBF>();
		if (!this.canJump && this.jumpCounter == 0)
		{
			this.back.gameObject.SetActive(true);
			Color color = this.back.color;
			color.a = 0.4f;
			this.back.color = color;
			this.continueText.text = "Continue to hold!";
			this.jumpCounter += 100;
			Time.timeScale = 0f;
		}
		if (!this.canJump && this.jumpCounter == 101)
		{
			Color color2 = this.back.color;
			color2.a = 0f;
			this.back.color = color2;
			this.continueText.text = string.Empty;
			this.jumpCounter = 1;
			Time.timeScale = 1f;
		}
		if (!this.canJump && this.jumpCounter == 2)
		{
			this.back.gameObject.SetActive(true);
			Color color3 = this.back.color;
			color3.a = 0.4f;
			this.back.color = color3;
			this.continueText.text = "Continue to hold!";
			this.jumpCounter += 100;
			Time.timeScale = 0f;
		}
		if (this.canJump && this.jumpCounter == 0)
		{
			Time.timeScale = 1f;
			component.Jump();
			this.jumpCounter = 1;
			this.text.text = string.Empty;
			this.canJump = false;
		}
		if (this.canJump && this.jumpCounter == 2)
		{
			Time.timeScale = 1f;
			component.PrepareForLanding();
			this.text.text = string.Empty;
			this.canJump = false;
			this.jumpButton.SetActive(false);
			Tutorial1.trying = false;
			base.StartCoroutine(this.Success());
		}
	}

	private IEnumerator StudentFlipping()
	{
		PlayerBF pb = GameObject.Find("Player").GetComponent<PlayerBF>();
		pb.Squat();
		yield return new WaitForSeconds(0.7f);
		this.canJump = true;
		this.text.text = "Release to jump";
		Time.timeScale = 0f;
		if (this.jumpCounter == 100)
		{
			Color color = this.back.color;
			color.a = 0f;
			this.back.color = color;
			this.continueText.text = string.Empty;
			this.jumpCounter -= 100;
			Time.timeScale = 1f;
			Time.timeScale = 1f;
			pb.Jump();
			this.jumpCounter = 1;
			this.text.text = string.Empty;
			this.canJump = false;
		}
		yield return new WaitForSeconds(0.92f);
		this.canJump = true;
		this.text.text = "Touch and hold to tuck";
		Time.timeScale = 0f;
		if (this.jumpCounter == 101)
		{
			Color color2 = this.back.color;
			color2.a = 0f;
			this.back.color = color2;
			this.continueText.text = string.Empty;
			this.jumpCounter = 1;
			Time.timeScale = 1f;
			Time.timeScale = 1f;
			pb.Tuck();
			this.text.text = string.Empty;
			this.canJump = false;
			this.jumpCounter = 2;
		}
		yield return new WaitForSeconds(0.78f);
		this.canJump = true;
		this.text.text = "Release to prepare for landing";
		Time.timeScale = 0f;
		if (this.jumpCounter == 102)
		{
			Color color3 = this.back.color;
			color3.a = 0f;
			this.back.color = color3;
			this.continueText.text = string.Empty;
			this.jumpCounter -= 100;
			Time.timeScale = 1f;
			Time.timeScale = 1f;
			pb.PrepareForLanding();
			this.text.text = string.Empty;
			this.canJump = false;
			this.jumpButton.SetActive(false);
			Tutorial1.trying = false;
			base.StartCoroutine(this.Success());
		}
		yield break;
	}

	public GameObject iPad;

	public GameObject iPhone;

	public Transform complimentrans;

	public Transform ab;

	public Image back;

	public Button homeUIButton;

	public GameObject jumpButton;

	public Transform hand;

	public Camera teacherCam;

	public Camera gameCam;

	private float teacherStartX;

	private Vector3 teacherDest;

	private Vector3 handDest;

	private Quaternion handRot;

	private float handSpeed;

	public Material camMat;

	public Transform plane;

	public Transform spawn;

	public GameObject watch;

	public Transform menuButton;

	public TextMeshProUGUI[] steps;

	public Transform[] destinations;

	public Transform dest;

	public GameObject press;

	private int count;

	private bool move;

	public TextMeshProUGUI tap;

	public Transform[] textTrans;

	private Vector3[] textDestinations = new Vector3[9];

	public TextMeshProUGUI[] texts;

	private string[] welcome = new string[]
	{
		"Welcome",
		"to",
		"Flip",
		"Trickster!"
	};

	private string[] learn = new string[]
	{
		"You",
		"will",
		"now",
		"learn",
		"how",
		"to",
		"do",
		"a",
		"flip"
	};

	private string[] show = new string[]
	{
		"I",
		"will",
		"show",
		"you",
		"how",
		"it's",
		"done"
	};

	private string[] justWatch = new string[]
	{
		"Just",
		"watch!"
	};

	private int messageCount;

	private bool canRemove;

	public static bool trying;

	private float tm;

	private bool completed;

	private bool holding;

	public TextMeshProUGUI text;

	public TextMeshProUGUI continueText;

	private int jumpCounter;

	private bool canJump;
}
