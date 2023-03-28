// dnSpy decompiler from Assembly-CSharp.dll class: PlayerBF
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerBF : MonoBehaviour
{
	private void Start()
	{
		if (GameObject.Find("HandDest"))
		{
			this.tutorial = true;
		}
		this.jumpForce = 1250f;
		this.flipScript = base.GetComponent<Flips>();
		this.footForward = base.transform.forward;
		PlayerBF.fallen = false;
		PlayerBF.jumped = false;
		PlayerBF.land = false;
		this.tucked = false;
		PlayerBF.freeplay = false;
		PlayerBF.canSwitch = true;
		this.SetupChallenges();
		this.One();
		base.Invoke("NowYouCanJump", 1f);
	}

	private void NowYouCanJump()
	{
		this.canJump = true;
	}

	private void NowYouCanLand()
	{
		this.canLand = true;
	}

	private void SetupChallenges()
	{
		if (LvlBtnHandler.activeLevel == 7 && Stages.activeSpawn != 0)
		{
			return;
		}
		for (int i = 0; i < 6; i++)
		{
			Challenges.gymSpecial[i] = false;
			Challenges.mountSpecial[i] = false;
			Challenges.gallerySpecial[i] = false;
		}
		if (LvlBtnHandler.activeLevel == 7)
		{
			Challenges.gymSpecial[6] = true;
			Challenges.gymSpecial[7] = true;
			Challenges.gallerySpecial[6] = true;
			Challenges.gallerySpecial[7] = true;
			Challenges.citySpecial[5] = true;
			Challenges.citySpecial[7] = true;
		}
		if (LvlBtnHandler.activeStage == 5 && LvlBtnHandler.activeLevel == 1 && Stages.stance == 1)
		{
			Challenges.gallerySpecial[0] = true;
		}
		if (LvlBtnHandler.activeLevel != 7)
		{
			Challenges.citySpecial[5] = false;
		}
		if (LvlBtnHandler.activeStage == 0)
		{
			return;
		}
		TextMeshProUGUI component = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
		component.text = string.Empty;
		component.color = Color.white;
		this.tuckCount = 0;
		Challenges.redZoneCount = 0;
		this.waited = false;
		PlayerBF.landedOnObs = false;
	}

	private IEnumerator WaitForTuck()
	{
		TextMeshProUGUI timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
		float t = 10f;
		if (LvlBtnHandler.activeStage == 2)
		{
			t = 20f;
		}
		if (LvlBtnHandler.activeStage == 5)
		{
			t = 20f;
		}
		timer.text = t / 10f + ".0";
		yield return new WaitForSeconds(0.1f);
		while (this.tuckCount <= 0)
		{
			t -= 1f;
			string ts = t / 10f + string.Empty;
			if (ts.Length == 1)
			{
				ts += ".0";
			}
			timer.text = ts;
			if (t == 0f)
			{
				this.waited = true;
				timer.color = this.green;
				IL_1B2:
				yield break;
			}
			yield return new WaitForSeconds(0.1f);
		}
		timer.color = this.red;
		//goto IL_1B2;
	}

	private IEnumerator WaitForStopTuck()
	{
		TextMeshProUGUI timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
		float t = 10f;
		if (LvlBtnHandler.activeStage == 5)
		{
			t = 20f;
		}
		timer.text = t / 10f + ".0";
		yield return new WaitForSeconds(0.1f);
		for (;;)
		{
			t -= 1f;
			string ts = t / 10f + string.Empty;
			if (ts.Length == 1)
			{
				ts += ".0";
			}
			timer.text = ts;
			if (t == 0f && this.stoppedTuck)
			{
				break;
			}
			if (t == 0f && !this.stoppedTuck)
			{
				goto Block_6;
			}
			yield return new WaitForSeconds(0.1f);
		}
		timer.color = this.green;
		goto IL_1AF;
		Block_6:
		timer.color = this.red;
		IL_1AF:
		yield break;
	}

	public void QueueMouseDown()
	{
		this.handleMouseDown = true;
	}

	public void QueueMouseUp()
	{
		this.handleMouseUp = true;
	}

	public void FixedUpdate()
	{
		if (this.handleMouseDown)
		{
			UnityEngine.Debug.Log("Mouse down");
			this.handleMouseDown = false;
			this.ActionButtonPress();
		}
		if (this.handleMouseUp)
		{
			UnityEngine.Debug.Log("Mouse up");
			this.handleMouseUp = false;
			this.ActionButtonRelease();
		}
		if (this.shouldJump)
		{
			if (!this.jumping)
			{
				this.Jump();
			}
			this.shouldJump = false;
		}
		if (this.jumping)
		{
			this.hips_rb.angularVelocity = base.transform.right * -this.spinSpeed;
		}
		if (!this.jumping)
		{
			this.hips_rb.AddForce(Vector3.up * Time.fixedDeltaTime * 1000f);
		}
		if (PlayerBF.land)
		{
			if (!this.canJump)
			{
				return;
			}
			this.HitGround();
			PlayerBF.land = false;
		}
		this.MoveJoints();
		if (PlayerBF.freeplay)
		{
			return;
		}
		this.CheckForFlip();
	}

	public void ActionButtonPress()
	{
		if (!this.jumping && !this.squatting && !this.nailedIt && !this.tutorial)
		{
			this.Squat();
		}
		if (this.jumping && !PlayerBF.fallen && !this.nailedIt && !this.tutorial)
		{
			this.tuckCount++;
			this.Tuck();
			TextMeshProUGUI component = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
			if (this.stoppedTuck && component.color == this.green)
			{
				this.stoppedTuck = false;
				component.color = this.red;
			}
		}
	}

	public void ActionButtonRelease()
	{
		if (this.squatting && !this.nailedIt && !this.tutorial)
		{
			this.shouldJump = true;
			if (LvlBtnHandler.activeLevel == 4 && LvlBtnHandler.activeStage == 1)
			{
				base.StartCoroutine(this.WaitForTuck());
			}
			if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 2)
			{
				base.StartCoroutine(this.WaitForTuck());
			}
			if (LvlBtnHandler.activeLevel == 4 && LvlBtnHandler.activeStage == 5)
			{
				base.StartCoroutine(this.WaitForTuck());
			}
			if (LvlBtnHandler.activeLevel == 6 && LvlBtnHandler.activeStage == 5)
			{
				base.StartCoroutine(this.WaitForStopTuck());
			}
		}
		if (this.spinning && !PlayerBF.fallen && !this.nailedIt && !this.tutorial)
		{
			this.PrepareForLanding();
			TextMeshProUGUI component = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
			if (component.color != this.red)
			{
				this.stoppedTuck = true;
			}
		}
	}

	public void Squat()
	{
		this.squatting = true;
		float num = Vector3.Angle(base.transform.forward, Vector3.forward);
		if (num == 0f || num == 180f)
		{
			this.hips_rb.constraints &= (RigidbodyConstraints)(-9);
		}
		else
		{
			this.hips_rb.constraints &= (RigidbodyConstraints)(-3);
		}
		this.Two();
		if (this.flipScript.gainer)
		{
			this.hips_rb.AddForce(base.transform.forward * 400f * Time.fixedDeltaTime, ForceMode.Impulse);
			this.hips_rb.AddForce(Vector3.down * 300f * Time.fixedDeltaTime, ForceMode.Impulse);
		}
	}

	public void Jump()
	{
		GameObject.Find("MAIN").GetComponent<Main>().DisableButtons();
		this.ulr = -10f;
		this.ull = -10f;
		this.sr = 180f;
		this.sl = 180f;
		PlayerBF.canSwitch = false;
		UnityEngine.Object.Instantiate<GameObject>(this.svoosh, base.transform.position, Quaternion.identity);
		this.foot_rb_R.mass = 10f;
		this.foot_rb_L.mass = 10f;
		this.PreFlipSpecifics();
		base.Invoke("JumpStepTwo", 0.15f);
	}

	public void JumpStepTwo()
	{
		this.jumping = true;
		this.squatting = false;
		Score.scoringPoints = true;
		this.Three();
		base.Invoke("JumpStepThree", 0.1f);
	}

	public void JumpStepThree()
	{
		this.foot_rb_R.mass = 1f;
		this.foot_rb_L.mass = 1f;
		if (!PlayerBF.freeplay)
		{
			for (int i = 0; i < 6; i++)
			{
				this.lines[i] = GameObject.Find("Line" + i).transform;
			}
		}
		this.FlipSpecifics();
		base.Invoke("NowYouCanLand", 0.5f);
	}

	public void PrepareForLanding()
	{
		PlayerBF.jumped = true;
		this.spinning = false;
		this.spinSpeed = 5f;
		this.Four();
		if (PlayerBF.freeplay)
		{
			return;
		}
	}

	public void Tuck()
	{
		this.spinning = true;
		this.spinSpeed = 500f;
		this.Five();
		this.tucked = true;
	}

	private void MoveJoints()
	{
		float num = 1.2f;
		this.JS_hip.targetPosition = Mathf.Lerp(this.JS_hip.targetPosition, this.ht, 10f * Time.fixedDeltaTime * this.iphoneTime * num);
		this.JS_ulr.targetPosition = Mathf.Lerp(this.JS_ulr.targetPosition, this.ulr, this.ulr_T * Time.fixedDeltaTime * this.iphoneTime * num);
		this.JS_dlr.targetPosition = Mathf.Lerp(this.JS_dlr.targetPosition, this.dlr, this.dlr_T * Time.fixedDeltaTime * this.iphoneTime * num);
		this.JS_fr.targetPosition = Mathf.Lerp(this.JS_fr.targetPosition, this.fr, this.fr_T * Time.fixedDeltaTime * this.iphoneTime * num);
		this.JS_sr.targetPosition = Mathf.Lerp(this.JS_sr.targetPosition, this.sr, this.sr_T * Time.fixedDeltaTime * this.iphoneTime * num);
		this.JS_ull.targetPosition = Mathf.Lerp(this.JS_ull.targetPosition, this.ull, this.ull_T * Time.fixedDeltaTime * this.iphoneTime * num);
		this.JS_dll.targetPosition = Mathf.Lerp(this.JS_dll.targetPosition, this.dll, this.dll_T * Time.fixedDeltaTime * this.iphoneTime * num);
		this.JS_fl.targetPosition = Mathf.Lerp(this.JS_fl.targetPosition, this.fl, this.fl_T * Time.fixedDeltaTime * this.iphoneTime * num);
		this.JS_sl.targetPosition = Mathf.Lerp(this.JS_sl.targetPosition, this.sl, this.sl_T * Time.fixedDeltaTime * this.iphoneTime * num);
		this.SetSprings();
	}

	private void SetSprings()
	{
		this.hip.spring = this.JS_hip;
		this.upLeg_R.spring = this.JS_ulr;
		this.downLeg_R.spring = this.JS_dlr;
		this.foot_R.spring = this.JS_fr;
		this.shoulder_R.spring = this.JS_sr;
		this.upLeg_L.spring = this.JS_ull;
		this.downLeg_L.spring = this.JS_dll;
		this.foot_L.spring = this.JS_fl;
		this.shoulder_L.spring = this.JS_sl;
	}

	private void HitGround()
	{
		if (!this.canJump || !this.canLand)
		{
			return;
		}
		Score.scoringPoints = false;
		if (PlayerBF.fallen || this.nailedIt)
		{
			return;
		}
		Vector3 a = Vector3.Lerp(this.foot_rb_L.position, this.foot_rb_R.position, 0.5f);
		Vector3 normalized = (a - this.head.position).normalized;
		float num = Vector3.Angle(normalized, (2f * Vector3.down + this.hips_rb.velocity.normalized).normalized);
		float num2 = (float)((LvlBtnHandler.activeStage == 0) ? 90 : 40);
		if (num > num2)
		{
			this.CrashLanded();
			return;
		}
		this.nailedIt = true;
		GameObject.Find("MAIN").GetComponent<Main>().EnableButtons();
		if (!PlayerBF.freeplay)
		{
			Vector3 from = this.lines[0].position - this.lines[5].position;
			float num3 = Vector3.Angle(from, Vector3.forward);
			if (num3 > 45f)
			{
				MonoBehaviour.print(num3);
				a += base.transform.forward * 0.02f;
				for (int i = 0; i < 5; i++)
				{
					if (a.y - this.lines[3].position.y > 3f)
					{
						break;
					}
					if (a.y - this.lines[0].position.y < -1f)
					{
						break;
					}
					if (a.x > this.lines[i].position.x && a.x < this.lines[i + 1].position.x)
					{
						this.AddMultiplier(i);
						a.x = 100000f;
						break;
					}
					if (a.x < this.lines[i].position.x && a.x > this.lines[i + 1].position.x)
					{
						this.AddMultiplier(i);
						a.x = 100000f;
						break;
					}
				}
				if (a.x < this.lines[0].position.x && LvlBtnHandler.activeStage == 2 && LvlBtnHandler.activeLevel == 1)
				{
					Challenges.mountSpecial[0] = true;
				}
				if (a.x < this.lines[0].position.x && LvlBtnHandler.activeStage == 4 && LvlBtnHandler.activeLevel == 2)
				{
					Challenges.houseSpecial[1] = true;
				}
				if (a.x != 100000f)
				{
					if (LvlBtnHandler.activeLevel == 7 && LvlBtnHandler.activeStage == 5)
					{
						Challenges.gallerySpecial[7] = false;
					}
					if (a.x > this.lines[0].position.x && LvlBtnHandler.activeStage == 7 && LvlBtnHandler.activeLevel == 4)
					{
						PlayerBF.landedOnObs = true;
					}
					base.Invoke("NextStage", 2.5f);
				}
			}
			else if (num3 < 45f)
			{
				a += base.transform.forward * 0.08f;
				for (int j = 0; j < 5; j++)
				{
					if (a.y - this.lines[3].position.y > 3f)
					{
						break;
					}
					if (a.z > this.lines[j].position.z && a.z < this.lines[j + 1].position.z)
					{
						this.AddMultiplier(j);
						a.x = 100000f;
						break;
					}
					if (a.z < this.lines[j].position.z && a.z > this.lines[j + 1].position.z)
					{
						this.AddMultiplier(j);
						a.x = 100000f;
						break;
					}
				}
				if (a.z < this.lines[5].position.z && LvlBtnHandler.activeStage == 2 && LvlBtnHandler.activeLevel == 4)
				{
					Challenges.mountSpecial[3] = true;
				}
				if (a.z < this.lines[5].position.z && LvlBtnHandler.activeStage == 3 && LvlBtnHandler.activeLevel == 5)
				{
					PlayerBF.landedOnObs = false;
				}
				if (a.z < this.lines[5].position.z && LvlBtnHandler.activeStage == 4 && LvlBtnHandler.activeLevel == 1)
				{
					Challenges.houseSpecial[0] = true;
				}
				if (a.x != 100000f)
				{
					if (LvlBtnHandler.activeLevel == 7 && LvlBtnHandler.activeStage == 5)
					{
						Challenges.gallerySpecial[7] = false;
					}
					if (LvlBtnHandler.activeStage == 0 && Tutorial1.trying)
					{
						base.StartCoroutine(GameObject.Find("MAIN").GetComponent<Tutorial1>().Success());
					}
					base.Invoke("NextStage", 2.5f);
				}
				else if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 7)
				{
					PlayerBF.landedOnObs = false;
				}
			}
		}
		else if (PlayerBF.freeplay)
		{
			base.Invoke("RetryStage", 2f);
		}
		this.jumping = false;
		this.Six();
	}

	public void NextStage()
	{
		if (PlayerBF.subLevel)
		{
			if (this.tuckCount == 1 && LvlBtnHandler.activeLevel == 2 && LvlBtnHandler.activeStage == 1 && !PlayerBF.freeplay)
			{
				Challenges.gymSpecial[1] = true;
			}
			if (this.waited && LvlBtnHandler.activeLevel == 4 && LvlBtnHandler.activeStage == 1 && !PlayerBF.freeplay)
			{
				Challenges.gymSpecial[3] = true;
			}
			if (LvlBtnHandler.activeLevel == 3 && LvlBtnHandler.activeStage == 1 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.gymSpecial[2] = true;
			}
			if (LvlBtnHandler.activeLevel == 6 && LvlBtnHandler.activeStage == 1 && this.flips == 2 && !PlayerBF.freeplay)
			{
				Challenges.gymSpecial[5] = true;
			}
			if (LvlBtnHandler.activeLevel == 2 && LvlBtnHandler.activeStage == 2 && this.flips == 2 && !PlayerBF.freeplay)
			{
				Challenges.mountSpecial[1] = true;
			}
			if (LvlBtnHandler.activeLevel == 3 && LvlBtnHandler.activeStage == 2 && this.tuckCount == 3 && !PlayerBF.freeplay)
			{
				Challenges.mountSpecial[2] = true;
			}
			if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 2 && this.waited && !PlayerBF.freeplay)
			{
				Challenges.mountSpecial[4] = true;
			}
			if (LvlBtnHandler.activeLevel == 6 && LvlBtnHandler.activeStage == 2 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.mountSpecial[5] = true;
			}
			if (LvlBtnHandler.activeStage == 5 && LvlBtnHandler.activeLevel == 3 && this.flips == 2 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.gallerySpecial[2] = true;
			}
			if (LvlBtnHandler.activeStage == 5 && LvlBtnHandler.activeLevel == 4 && this.waited && !PlayerBF.freeplay)
			{
				Challenges.gallerySpecial[3] = true;
			}
			if (LvlBtnHandler.activeStage == 5 && LvlBtnHandler.activeLevel == 6 && this.stoppedTuck && !PlayerBF.freeplay)
			{
				Challenges.gallerySpecial[5] = true;
			}
			if (LvlBtnHandler.activeStage == 3 && LvlBtnHandler.activeLevel == 2 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.citySpecial[1] = true;
			}
			if (LvlBtnHandler.activeStage == 3 && LvlBtnHandler.activeLevel == 3 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.citySpecial[2] = true;
			}
			if (LvlBtnHandler.activeStage == 3 && LvlBtnHandler.activeLevel == 4 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.citySpecial[3] = true;
			}
			if (LvlBtnHandler.activeStage == 3 && LvlBtnHandler.activeLevel == 5 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.citySpecial[4] = true;
			}
			if (LvlBtnHandler.activeStage == 3 && LvlBtnHandler.activeLevel == 6 && this.flips == 2 && Stages.stance == 1 && !PlayerBF.freeplay)
			{
				Challenges.citySpecial[5] = true;
			}
			if (LvlBtnHandler.activeLevel == 3 && LvlBtnHandler.activeStage == 4 && this.tuckCount == 5 && !PlayerBF.freeplay)
			{
				Challenges.houseSpecial[2] = true;
			}
			if (LvlBtnHandler.activeLevel == 4 && LvlBtnHandler.activeStage == 4 && this.flips == 3 && !PlayerBF.freeplay)
			{
				Challenges.houseSpecial[3] = true;
			}
			if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 4 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.houseSpecial[4] = true;
			}
			if (LvlBtnHandler.activeLevel == 6 && LvlBtnHandler.activeStage == 4 && this.flips == 4 && !PlayerBF.freeplay)
			{
				Challenges.houseSpecial[5] = true;
			}
			if (LvlBtnHandler.activeLevel == 1 && LvlBtnHandler.activeStage == 6 && this.flips == 0 && !PlayerBF.freeplay)
			{
				Challenges.shipSpecial[0] = true;
			}
			if (LvlBtnHandler.activeLevel == 2 && LvlBtnHandler.activeStage == 6 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.shipSpecial[1] = true;
			}
			if (LvlBtnHandler.activeLevel == 3 && LvlBtnHandler.activeStage == 6 && this.flips == 2 && Stages.stance == 1 && !PlayerBF.freeplay)
			{
				Challenges.shipSpecial[2] = true;
			}
			if (LvlBtnHandler.activeLevel == 4 && LvlBtnHandler.activeStage == 6 && this.flips == 3 && !PlayerBF.freeplay)
			{
				Challenges.shipSpecial[3] = true;
			}
			if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 6 && PlayerBF.landedOnObs && PlayerBF.freeplay)
			{
				Challenges.shipSpecial[4] = true;
			}
			if (LvlBtnHandler.activeLevel == 6 && LvlBtnHandler.activeStage == 6 && this.flips == 0 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.shipSpecial[5] = true;
			}
			if (LvlBtnHandler.activeLevel == 1 && LvlBtnHandler.activeStage == 7 && this.flips == 0 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.islandSpecial[0] = true;
			}
			if (LvlBtnHandler.activeLevel == 2 && LvlBtnHandler.activeStage == 7 && this.flips > 0 && Stages.stance == 1 && !PlayerBF.freeplay)
			{
				Challenges.islandSpecial[1] = true;
			}
			if (LvlBtnHandler.activeLevel == 3 && LvlBtnHandler.activeStage == 7 && PlayerBF.landedOnObs && PlayerBF.freeplay)
			{
				Challenges.islandSpecial[2] = true;
			}
			if (LvlBtnHandler.activeLevel == 4 && LvlBtnHandler.activeStage == 7 && PlayerBF.landedOnObs && Stages.stance == 1 && !PlayerBF.freeplay)
			{
				Challenges.islandSpecial[3] = true;
			}
			if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 7 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.islandSpecial[4] = true;
			}
			if (LvlBtnHandler.activeLevel == 6 && LvlBtnHandler.activeStage == 7 && this.flips == 1 && !PlayerBF.freeplay)
			{
				Challenges.islandSpecial[5] = true;
			}
			if (LvlBtnHandler.activeLevel == 1 && LvlBtnHandler.activeStage == 8 && PlayerBF.landedOnObs && this.flips == 0 && !PlayerBF.freeplay)
			{
				Challenges.hauntedSpecial[0] = true;
			}
			if (LvlBtnHandler.activeLevel == 2 && LvlBtnHandler.activeStage == 8 && PlayerBF.landedOnObs && this.flips == 0 && !PlayerBF.freeplay)
			{
				Challenges.hauntedSpecial[1] = true;
			}
			if (LvlBtnHandler.activeLevel == 3 && LvlBtnHandler.activeStage == 8 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.hauntedSpecial[2] = true;
			}
			if (LvlBtnHandler.activeLevel == 4 && LvlBtnHandler.activeStage == 8 && PlayerBF.landedOnObs && PlayerBF.freeplay)
			{
				Challenges.hauntedSpecial[3] = true;
			}
			if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 8 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.hauntedSpecial[4] = true;
			}
			if (LvlBtnHandler.activeLevel == 6 && LvlBtnHandler.activeStage == 8 && this.flips == 0 && !PlayerBF.freeplay)
			{
				Challenges.hauntedSpecial[5] = true;
			}
			if (LvlBtnHandler.activeLevel == 1 && LvlBtnHandler.activeStage == 9 && PlayerBF.landedOnObs && Challenges.targetCount == 1 && !PlayerBF.freeplay)
			{
				Challenges.ufoSpecial[0] = true;
			}
			if (LvlBtnHandler.activeLevel == 2 && LvlBtnHandler.activeStage == 9 && this.tuckCount == 0 && !PlayerBF.freeplay)
			{
				Challenges.ufoSpecial[1] = true;
			}
			if (LvlBtnHandler.activeLevel == 3 && LvlBtnHandler.activeStage == 9 && Challenges.redZoneCount == 1 && this.flips == 0 && !PlayerBF.freeplay)
			{
				Challenges.ufoSpecial[2] = true;
			}
			if (LvlBtnHandler.activeLevel == 4 && LvlBtnHandler.activeStage == 9 && PlayerBF.landedOnObs && PlayerBF.freeplay)
			{
				Challenges.ufoSpecial[3] = true;
			}
			if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 9 && PlayerBF.landedOnObs && !PlayerBF.freeplay)
			{
				Challenges.ufoSpecial[4] = true;
			}
			if (LvlBtnHandler.activeLevel == 6 && LvlBtnHandler.activeStage == 9 && this.tuckCount == 0 && this.flips == 1 && !PlayerBF.freeplay)
			{
				Challenges.ufoSpecial[5] = true;
			}
			GameObject.Find("MAIN").GetComponent<SubLevel>().Landed();
			return;
		}
		if (LvlBtnHandler.activeStage == 1 && Challenges.redZoneCount >= 2)
		{
			Challenges.gymSpecial[5] = true;
		}
		if (LvlBtnHandler.activeLevel == 7 && LvlBtnHandler.activeStage == 2 && this.flips == 3 && !PlayerBF.freeplay)
		{
			Challenges.mountSpecial[5] = true;
		}
		if (LvlBtnHandler.activeLevel == 7 && !PlayerBF.freeplay)
		{
			Challenges.flipCount += this.flips;
		}
		if (LvlBtnHandler.activeStage == 5 && Challenges.redZoneCount >= 3)
		{
			Challenges.gallerySpecial[5] = true;
		}
		if (LvlBtnHandler.activeStage == 6 && Stages.stance == 0)
		{
			Challenges.shipSpecial[6] = false;
		}
		if (LvlBtnHandler.activeStage == 7 && this.flips == 4)
		{
			Challenges.islandSpecial[5] = true;
		}
		if (LvlBtnHandler.activeStage == 8 && this.flips == 5)
		{
			Challenges.hauntedSpecial[5] = true;
		}
		if (LvlBtnHandler.activeStage == 8 && Stages.stance == 0)
		{
			Challenges.hauntedSpecial[6] = false;
		}
		if (LvlBtnHandler.activeStage == 9 && !this.tucked && !PlayerBF.freeplay)
		{
			Challenges.noTuckCount++;
		}
		base.Invoke("StageNext", 0.1f);
		if (PlayerBF.freeplay && LvlBtnHandler.activeLevel != 7)
		{
			return;
		}
		GameObject.Find("Score").GetComponent<Score>().SetTotalScore();
	}

	private void StageNext()
	{
		if (LvlBtnHandler.activeStage == 0)
		{
			return;
		}
		GameObject.Find("MAIN").GetComponent<Stages>().NextStage();
	}

	private void RetryStage()
	{
		GameObject.Find("MAIN").GetComponent<Stages>().RetryStage();
		if (PlayerBF.freeplay)
		{
			return;
		}
		GameObject.Find("Score").GetComponent<Score>().NullifyText();
		GameObject.Find("Score").GetComponent<Score>().SetTotalScore();
		GameObject.Find("Score").GetComponent<Score>().LandedOutside();
	}

	private void AddMultiplier(int multiplier)
	{
		float multiplier2 = 0f;
		if (LvlBtnHandler.activeStage == 1 && LvlBtnHandler.activeLevel == 7)
		{
			Challenges.gymSpecial[7] = false;
		}
		if (LvlBtnHandler.activeStage == 5 && LvlBtnHandler.activeLevel == 2 && Stages.stance == 1)
		{
			Challenges.gallerySpecial[1] = true;
		}
		if (LvlBtnHandler.activeStage == 6 && LvlBtnHandler.activeLevel == 6)
		{
			PlayerBF.landedOnObs = true;
		}
		if (LvlBtnHandler.activeStage == 7)
		{
			Challenges.islandSpecial[6] = false;
		}
		Challenges.citySpecial[7] = false;
		Challenges.targetCount++;
		switch (multiplier)
		{
		case 0:
		case 4:
			multiplier2 = 1.2f;
			break;
		case 1:
		case 3:
			multiplier2 = 1.5f;
			Challenges.yellowZoneCount++;
			break;
		case 2:
			multiplier2 = 2f;
			Challenges.redZoneCount++;
			Challenges.yellowZoneCount++;
			break;
		}
		GameObject.Find("Score").GetComponent<Score>().ShowMultiplier(multiplier2);
	}

	public void CrashLanded()
	{
		if (this.nailedIt || PlayerBF.fallen)
		{
			return;
		}
		if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 1)
		{
			Challenges.gymSpecial[4] = true;
		}
		if (LvlBtnHandler.activeLevel == 7 && LvlBtnHandler.activeStage == 1)
		{
			Challenges.gymSpecial[6] = false;
		}
		Challenges.gymSpecial[7] = false;
		if (LvlBtnHandler.activeLevel == 1 && LvlBtnHandler.activeStage == 5)
		{
			Challenges.gallerySpecial[0] = false;
		}
		if (LvlBtnHandler.activeLevel == 5 && LvlBtnHandler.activeStage == 5 && PlayerBF.landedOnObs)
		{
			Challenges.gallerySpecial[4] = true;
		}
		if (LvlBtnHandler.activeLevel == 7 && LvlBtnHandler.activeStage == 5)
		{
			Challenges.gallerySpecial[6] = false;
		}
		if (LvlBtnHandler.activeLevel == 7 && LvlBtnHandler.activeStage == 5)
		{
			Challenges.gallerySpecial[7] = false;
		}
		if (LvlBtnHandler.activeLevel == 7 && LvlBtnHandler.activeStage == 3)
		{
			Challenges.citySpecial[5] = false;
		}
		Challenges.citySpecial[7] = false;
		Challenges.houseSpecial[7] = false;
		Challenges.shipSpecial[6] = false;
		Challenges.shipSpecial[5] = false;
		Challenges.islandSpecial[6] = false;
		Challenges.islandSpecial[7] = false;
		Challenges.hauntedSpecial[6] = false;
		Score.scoringPoints = false;
		GameObject.Find("MAIN").GetComponent<Main>().EnableButtons();
		this.Seven();
		PlayerBF.fallen = true;
		if (LvlBtnHandler.activeStage == 0)
		{
			Tutorial1 component = GameObject.Find("MAIN").GetComponent<Tutorial1>();
			if (component != null)
			{
				base.StartCoroutine(component.Failed());
			}
			base.Invoke("RetryStage", 2f);
		}
		else
		{
			base.Invoke("NextStage", 2f);
		}
		if (PlayerBF.freeplay)
		{
			return;
		}
		PlayerBF.freeplay = true;
	}

	private void CheckForFlip()
	{
		if (PlayerBF.fallen || PlayerBF.freeplay)
		{
			return;
		}
		Vector3 normalized = (this.head.position - this.foot_rb_L.transform.position).normalized;
		if (normalized.y < -0.9f && this.up)
		{
			GameObject.Find("Score").GetComponent<Score>().Flipped(100 * (this.flips + 1));
			this.up = false;
			this.down = true;
			this.flips++;
		}
		if (normalized.y > 0.9f && this.down)
		{
			this.down = false;
			this.up = true;
		}
	}

	private void FlipSpecifics()
	{
		Vector3 b = Vector3.Lerp(this.foot_rb_L.position, this.foot_rb_R.position, 0.5f);
		Vector3 normalized = (this.hips_rb.position - b).normalized;
		if (this.flipScript.backflip)
		{
			this.hips_rb.AddForce(normalized * this.jumpForce * Time.fixedDeltaTime / 2f, ForceMode.Impulse);
			this.hips_rb.AddForce(Vector3.up * this.jumpForce * Time.fixedDeltaTime / 2f, ForceMode.Impulse);
		}
		if (this.flipScript.gainer)
		{
			this.hips_rb.AddForce(Vector3.up * this.jumpForce * Time.fixedDeltaTime / 2f, ForceMode.Impulse);
			this.foot_rb_L.AddForce(this.footForward * 75f * Time.fixedDeltaTime, ForceMode.Impulse);
			this.foot_rb_R.AddForce(this.footForward * 75f * Time.fixedDeltaTime, ForceMode.Impulse);
		}
	}

	private void PreFlipSpecifics()
	{
		Vector3 b = Vector3.Lerp(this.foot_rb_L.position, this.foot_rb_R.position, 0.5f);
		Vector3 normalized = (this.head.position - b).normalized;
		float num = Vector3.Angle(Vector3.up, normalized);
		if (num > 30f)
		{
			num = 30f - (num - 30f);
		}
		float d = num * 20f;
		if (this.flipScript.gainer)
		{
			this.hips_rb.AddForce(-this.footForward * d * Time.fixedDeltaTime, ForceMode.Impulse);
			this.fr = 0f;
			this.fl = 0f;
		}
	}

	private void One()
	{
		this.JS_hip.spring = this.hip.spring.spring;
		this.JS_ulr.spring = this.upLeg_R.spring.spring;
		this.JS_dlr.spring = this.downLeg_R.spring.spring;
		this.JS_fr.spring = this.foot_R.spring.spring;
		this.JS_sr.spring = this.shoulder_R.spring.spring;
		this.JS_ull.spring = this.upLeg_L.spring.spring;
		this.JS_dll.spring = this.downLeg_L.spring.spring;
		this.JS_fl.spring = this.foot_L.spring.spring;
		this.JS_sl.spring = this.shoulder_L.spring.spring;
		this.ulr = -2f;
		this.ull = -2f;
		this.foot_rb_R.mass = 10f;
		this.foot_rb_L.mass = 10f;
	}

	private void Two()
	{
		this.ulr = this.flipScript.Flip(0);
		this.dlr = this.flipScript.Flip(1);
		this.fr = this.flipScript.Flip(2);
		this.sr = this.flipScript.Flip(3);
		this.ull = this.flipScript.Flip(0);
		this.dll = this.flipScript.Flip(1);
		this.fl = this.flipScript.Flip(2);
		this.sl = this.flipScript.Flip(3);
	}

	private void Three()
	{
		this.dlr = 0f;
		this.fr = -45f;
		this.dll = 0f;
		this.fl = -45f;
		this.ulr_T = 10f;
		this.dlr_T = 20f;
		this.fr_T = 10f;
		this.ull_T = 10f;
		this.dll_T = 20f;
		this.fl_T = 10f;
	}

	private void Four()
	{
		this.ht = -20f;
		this.ulr = 10f;
		this.dlr = -20f;
		this.fr = 10f;
		this.sr = 120f;
		this.ull = 10f;
		this.dll = -20f;
		this.fl = 10f;
		this.sl = 120f;
	}

	private void Five()
	{
		this.ht = -45f;
		this.ulr = 100f;
		this.dlr = -130f;
		this.fr = -20f;
		this.sr = 45f;
		this.ull = 100f;
		this.dll = -130f;
		this.fl = -20f;
		this.sl = 45f;
	}

	private void Six()
	{
		this.hips_rb.velocity = Vector3.zero;
		this.fl = 10f;
		this.fr = 10f;
		this.JS_fr.spring = 200f;
		this.JS_fl.spring = 200f;
		this.foot_rb_R.mass = 1000f;
		this.foot_rb_L.mass = 1000f;
		this.foot_rb_R.velocity = Vector3.zero;
		this.foot_rb_L.velocity = Vector3.zero;
		this.foot_rb_R.drag = 5f;
		this.foot_rb_L.drag = 5f;
		this.hips_rb.drag = 20f;
		this.ht = -10f;
		this.sr = 0f;
		this.sl = 0f;
		this.sr_T = 3f;
		this.sl_T = 3f;
	}

	private void Seven()
	{
		this.JS_hip.spring = 5f;
		this.JS_ulr.spring = 5f;
		this.JS_dlr.spring = 5f;
		this.JS_fr.spring = 5f;
		this.JS_sr.spring = 5f;
		this.JS_ull.spring = 5f;
		this.JS_dll.spring = 5f;
		this.JS_fl.spring = 5f;
		this.JS_sl.spring = 5f;
	}

	public GameObject svoosh;

	private Transform[] lines = new Transform[6];

	public Rigidbody foot_rb_R;

	public Rigidbody foot_rb_L;

	public Rigidbody hips_rb;

	public Transform head;

	public HingeJoint hip;

	private JointSpring JS_hip;

	private float ht;

	public HingeJoint upLeg_R;

	public HingeJoint downLeg_R;

	public HingeJoint foot_R;

	public HingeJoint shoulder_R;

	private JointSpring JS_ulr;

	private JointSpring JS_dlr;

	private JointSpring JS_fr;

	private JointSpring JS_sr;

	private float ulr;

	private float dlr;

	private float fr;

	private float sr;

	private float ulr_T = 5f;

	private float dlr_T = 5f;

	private float fr_T = 5f;

	private float sr_T = 10f;

	public HingeJoint upLeg_L;

	public HingeJoint downLeg_L;

	public HingeJoint foot_L;

	public HingeJoint shoulder_L;

	private JointSpring JS_ull;

	private JointSpring JS_dll;

	private JointSpring JS_fl;

	private JointSpring JS_sl;

	private float ull;

	private float dll;

	private float fl;

	private float sl;

	private float ull_T = 5f;

	private float dll_T = 5f;

	private float fl_T = 5f;

	private float sl_T = 10f;

	private bool squatting;

	private bool jumping;

	private bool spinning;

	private bool shouldJump;

	private float spinSpeed = 2f;

	private float powerspin = 10f;

	private float jumpForce = 1300f;

	private Vector3 footForward;

	private float iphoneTime = 0.75f;

	public static bool jumped;

	public static bool land;

	public static bool fallen;

	public static bool canSwitch;

	private bool nailedIt;

	private bool tucked;

	private Flips flipScript;

	public static bool freeplay;

	public static bool subLevel;

	private bool tutorial;

	private int multiplierIndex;

	private bool canJump;

	private bool canLand;

	public static bool landedOnObs;

	private int tuckCount;

	private bool waited;

	private bool stoppedTuck;

	public Color green;

	public Color red;

	private bool handleMouseDown;

	private bool handleMouseUp;

	private bool up = true;

	private bool down;

	private int flips;
}
