// dnSpy decompiler from Assembly-CSharp.dll class: LockedLevel
using System;
using TMPro;
using UnityEngine;

public class LockedLevel : MonoBehaviour
{
	private void Start()
	{
		for (int i = 0; i < 5; i++)
		{
			int @int = PlayerPrefs.GetInt(this.gym + (i + 1).ToString());
			if (@int == 1)
			{
				this.count++;
			}
		}
		if (this.count < this.starsToUnlock)
		{
			this.stars.SetActive(false);
			this.lockScreen.SetActive(true);
			this.button.transform.position += Vector3.down * 500f;
			this.reqs.text = this.count.ToString() + "/" + this.starsToUnlock.ToString();
		}
	}

	public GameObject stars;

	public GameObject lockScreen;

	public GameObject button;

	public TextMeshProUGUI reqs;

	[SerializeField]
	private int starsToUnlock;

	private string gym = "GymBest";

	private int count;
}
