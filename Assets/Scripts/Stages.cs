// dnSpy decompiler from Assembly-CSharp.dll class: Stages
using System;
using UnityEngine;

public class Stages : MonoBehaviour
{
	private void Start()
	{
		LvlModels.activeStage = LvlBtnHandler.activeStage - 1;
		if (LvlModels.onIpad && LvlBtnHandler.activeStage == 0)
		{
			Camera.main.fieldOfView = 80f;
		}
		if (LvlBtnHandler.activeLevel == 0 || StageModel.IsLastLevel())
		{
			this.StartFullStage();
		}
		else
		{
			PlayerBF.subLevel = true;
			int num = LvlBtnHandler.activeLevel - 1;
			Stages.activeSpawn = num;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.player[Stages.stance], this.spawns[num].transform.position, this.spawns[num].transform.rotation);
			Transform transform = gameObject.GetComponentInChildren<PlayerBF>().hips_rb.transform;
			this.spawns[num].SetActive(true);
			this.spawns[num].GetComponentInChildren<PlayerFollow>().player = transform;
			this.cf = Camera.main.GetComponent<CameraFollow>();
			this.cf.player = transform;
			this.cf.followPoint = this.spawns[num].GetComponentInChildren<PlayerFollow>().camFollow;
			this.cf.pointer = this.spawns[num].GetComponentInChildren<PlayerFollow>().pointer;
		}
	}

	private void Update()
	{
	}

	private void StartFullStage()
	{
		PlayerBF.subLevel = false;
		Stages.activeSpawn = 0;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.player[Stages.stance], this.spawns[0].transform.position, this.spawns[0].transform.rotation);
		Transform transform = gameObject.GetComponentInChildren<PlayerBF>().hips_rb.transform;
		this.spawns[0].SetActive(true);
		this.spawns[0].GetComponentInChildren<PlayerFollow>().player = transform;
		this.cf = Camera.main.GetComponent<CameraFollow>();
		this.cf.player = transform;
		this.cf.followPoint = this.spawns[0].GetComponentInChildren<PlayerFollow>().camFollow;
		this.cf.pointer = this.spawns[0].GetComponentInChildren<PlayerFollow>().pointer;
	}

	public void NextStage()
	{
		if (LvlBtnHandler.activeStage == 0 && !Tutorial1.trying)
		{
			this.RetryStage();
			return;
		}
		if (Stages.activeSpawn >= this.spawns.Length - 1)
		{
			this.LevelCompleted();
			return;
		}
		this.spawns[Stages.activeSpawn].SetActive(false);
		Stages.activeSpawn++;
		if (Stages.activeSpawn >= this.spawns.Length)
		{
			Stages.activeSpawn = 0;
		}
		UnityEngine.Object.Destroy(GameObject.FindWithTag("Rotater"));
		this.spawns[Stages.activeSpawn].SetActive(true);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.player[Stages.stance], this.spawns[Stages.activeSpawn].transform.position, this.spawns[Stages.activeSpawn].transform.rotation);
		Transform transform = gameObject.GetComponentInChildren<PlayerBF>().hips_rb.transform;
		this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().player = transform;
		this.cf.player = transform;
		this.cf.followPoint = this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().camFollow;
		this.cf.pointer = this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().pointer;
		base.GetComponent<Main>().EnableVariations();
	}

	public void RetryStage()
	{
		if (Tutorial1.trying)
		{
			return;
		}
		UnityEngine.Object.Destroy(GameObject.FindWithTag("Rotater"));
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.player[Stages.stance], this.spawns[Stages.activeSpawn].transform.position, this.spawns[Stages.activeSpawn].transform.rotation);
		Transform transform = gameObject.GetComponentInChildren<PlayerBF>().hips_rb.transform;
		this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().player = transform;
		this.cf.player = transform;
		this.cf.followPoint = this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().camFollow;
		this.cf.pointer = this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().pointer;
		base.GetComponent<Main>().EnableVariations();
	}

	private void LevelCompleted()
	{
		GameObject.Find("MAIN").GetComponent<FullStage>().CompletedStage();
	}

	public void PreviousStage()
	{
		this.spawns[Stages.activeSpawn].SetActive(false);
		Stages.activeSpawn--;
		if (Stages.activeSpawn <= -1)
		{
			Stages.activeSpawn = this.spawns.Length - 1;
		}
		UnityEngine.Object.Destroy(GameObject.FindWithTag("Rotater"));
		this.spawns[Stages.activeSpawn].SetActive(true);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.player[Stages.stance], this.spawns[Stages.activeSpawn].transform.position, this.spawns[Stages.activeSpawn].transform.rotation);
		Transform transform = gameObject.GetComponentInChildren<PlayerBF>().hips_rb.transform;
		this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().player = transform;
		this.cf.player = transform;
		this.cf.followPoint = this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().camFollow;
		this.cf.pointer = this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().pointer;
		base.GetComponent<Main>().EnableVariations();
	}

	public void ChangeStance()
	{
		if (!PlayerBF.canSwitch)
		{
			return;
		}
		Stages.stance = ((Stages.stance != 0) ? 0 : 1);
		if (LvlBtnHandler.activeStage == 5 && LvlBtnHandler.activeLevel == 1)
		{
			Challenges.gallerySpecial[0] = ((Stages.stance != 1) ? false : true);
		}
		if (LvlBtnHandler.activeStage == 3 && LvlBtnHandler.activeLevel == 6)
		{
			Challenges.citySpecial[5] = ((Stages.stance != 1) ? false : true);
		}
		UnityEngine.Object.Destroy(GameObject.FindWithTag("Rotater"));
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.player[Stages.stance], this.spawns[Stages.activeSpawn].transform.position, this.spawns[Stages.activeSpawn].transform.rotation);
		Transform transform = gameObject.GetComponentInChildren<PlayerBF>().hips_rb.transform;
		this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().player = transform;
		this.cf.player = transform;
		this.cf.followPoint = this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().camFollow;
		this.cf.pointer = this.spawns[Stages.activeSpawn].GetComponentInChildren<PlayerFollow>().pointer;
	}

	public GameObject[] player;

	public static int stance;

	public Transform stanceImg;

	public GameObject retryButton;

	public GameObject[] spawns;

	public static int activeSpawn;

	private CameraFollow cf;
}
