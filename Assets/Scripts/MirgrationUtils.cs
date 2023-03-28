// dnSpy decompiler from Assembly-CSharp.dll class: MirgrationUtils
using System;
using UnityEngine;

public class MirgrationUtils
{
	public static void MaybeMigrateToGameState()
	{
		if (PlayerPrefs.GetInt("state_migrated_v1", 0) == 0)
		{
			UnityEngine.Debug.Log("MirgrationUtils: Migrating to v1...");
			for (int i = 1; i < LvlModels.lvlNum + 1; i++)
			{
				for (int j = 1; j < 8; j++)
				{
					string key = "best" + j + i;
					int @int = PlayerPrefs.GetInt(key, 0);
					if (@int != 0)
					{
						GameState.Instance.SetHighScore(i, j, @int);
					}
				}
			}
			for (int k = 1; k < LvlModels.lvlNum + 1; k++)
			{
				for (int l = 1; l < 8; l++)
				{
					string key2 = "lock" + l + k;
					if (PlayerPrefs.GetInt(key2, 0) == 1)
					{
						GameState.Instance.SetLevelUnlocked(k, l);
					}
				}
			}
			for (int m = 1; m < LvlModels.lvlNum + 1; m++)
			{
				for (int n = 1; n < 8; n++)
				{
					string key3 = "bronze" + n + m;
					if (PlayerPrefs.GetInt(key3, 0) == 1)
					{
						GameState.Instance.AwardBronze(m, n);
					}
					string key4 = "silver" + n + m;
					if (PlayerPrefs.GetInt(key4, 0) == 1)
					{
						GameState.Instance.AwardSilver(m, n);
					}
					string key5 = "gold" + n + m;
					if (PlayerPrefs.GetInt(key5, 0) == 1)
					{
						GameState.Instance.AwardGold(m, n);
					}
					string key6 = "special" + n + m;
					if (PlayerPrefs.GetInt(key6, 0) == 1)
					{
						GameState.Instance.AwardSpecial(m, n, 0);
					}
					if (n == 7)
					{
						string key7 = string.Concat(new object[]
						{
							"special",
							n,
							m,
							1
						});
						if (PlayerPrefs.GetInt(key7, 0) == 1)
						{
							GameState.Instance.AwardSpecial(m, n, 1);
						}
						string key8 = string.Concat(new object[]
						{
							"special",
							n,
							m,
							2
						});
						if (PlayerPrefs.GetInt(key8, 0) == 1)
						{
							GameState.Instance.AwardSpecial(m, n, 2);
						}
					}
				}
			}
			int int2 = PlayerPrefs.GetInt("Coins");
			GameState.Instance.AddCoins(int2);
			for (int num = 0; num < 12; num++)
			{
				int int3 = PlayerPrefs.GetInt("hatOwned" + num);
				if (int3 == 1)
				{
					GameState.Instance.AwardHat(num);
				}
			}
			if (PlayerPrefs.GetInt("NoAds") == 1)
			{
				GameState.Instance.DisableAds();
			}
			if (PlayerPrefs.GetInt("TutorialCompleted") == 1)
			{
				GameState.Instance.MarkTutorialCompleted();
			}
			GameState.Instance.Syncronize();
			PlayerPrefs.SetInt("state_migrated_v1", 1);
		}
	}

	private const string Tag = "MirgrationUtils";

	private const string KeyStateMigratedV1 = "state_migrated_v1";
}
