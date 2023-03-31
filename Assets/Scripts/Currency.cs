// dnSpy decompiler from Assembly-CSharp.dll class: Currency
using System;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
	

	private void Start()
	{
		Currency.coinAmount = GameState.Instance.GetCoins();
		this.coinText.text = Currency.coinAmount.ToString();
	}

	private void Update()
	{
	}

	


	public static int coinAmount;

	public TextMeshProUGUI coinText;
}
