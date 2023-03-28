// dnSpy decompiler from Assembly-CSharp.dll class: Switcher
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Switcher : MonoBehaviour
{
	private void Start()
	{
		this.dots[this.active].color = Color.white;
		this.offColor = this.dots[this.active + 1].color;
		this.points[0] = this.main;
		this.points[1] = this.left;
		this.points[2] = this.right;
		this.InitializeClothes();
	}

	private void FixedUpdate()
	{
		if (!this.start)
		{
			return;
		}
		this.MoveClothes();
	}

	public void Back()
	{
		SceneManager.LoadScene("LevelSelect");
	}

	public void SwapRight()
	{
		this.dots[this.active].color = this.offColor;
		this.active++;
		this.active = ((this.active != this.dots.Length) ? this.active : 0);
		this.dots[this.active].color = Color.white;
	}

	public void SwapLeft()
	{
		this.dots[this.active].color = this.offColor;
		this.active--;
		this.active = ((this.active != -1) ? this.active : (this.dots.Length - 1));
		this.dots[this.active].color = Color.white;
	}

	private void InitializeClothes()
	{
		Transform[] array = new Transform[]
		{
			this.main,
			this.right,
			this.left
		};
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.models[0], this.main.position, Quaternion.identity);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.models[0], this.right.position, Quaternion.identity);
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.models[0], this.left.position, Quaternion.identity);
		this.clothes[0] = gameObject.transform;
		this.clothes[1] = gameObject2.transform;
		this.clothes[2] = gameObject3.transform;
		this.start = true;
	}

	private void MoveClothes()
	{
		this.clothes[0].position = Vector3.Lerp(this.clothes[0].position, this.points[this.active].position, Time.fixedDeltaTime * 15f);
		int num = (this.active + 1 != this.dots.Length) ? (this.active + 1) : 0;
		this.clothes[1].position = Vector3.Lerp(this.clothes[1].position, this.points[num].position, Time.fixedDeltaTime * 15f);
		int num2 = (this.active + 2 != this.dots.Length) ? (this.active + 2) : 0;
		num2 = ((num2 <= this.dots.Length) ? num2 : 1);
		MonoBehaviour.print(num2);
		MonoBehaviour.print(this.dots.Length);
		this.clothes[2].position = Vector3.Lerp(this.clothes[2].position, this.points[num2].position, Time.fixedDeltaTime * 15f);
	}

	public Image[] dots;

	private int active;

	private Color offColor;

	public GameObject[] models;

	private Transform[] clothes = new Transform[3];

	public Transform main;

	public Transform left;

	public Transform right;

	private Transform[] points = new Transform[3];

	private bool start;
}
