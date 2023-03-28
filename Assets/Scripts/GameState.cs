// dnSpy decompiler from Assembly-CSharp.dll class: GameState
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
	private GameState()
	{
	}

	public void EnsureInitialized()
	{
		if (this._initialized)
		{
			return;
		}
		UnityEngine.Debug.Log("GameState: Initializing game state...");
		string @string = PlayerPrefs.GetString("com.fliptrickster.game.state");
		this._currentState = GameState.PropsStringToDict(@string);
		this._coinsAdded = PlayerPrefs.GetInt("com.fliptrickster.coin.added");
		this._coinsRemoved = PlayerPrefs.GetInt("com.fliptrickster.coin.removed");
		this._adsDisabled = PlayerPrefs.GetInt("com.fliptrickster.ads.disabled");
		string string2 = PlayerPrefs.GetString("com.fliptrickster.hats");
		this.LoadHatsFromPropsString(string2);
		this._initialized = true;
		MirgrationUtils.MaybeMigrateToGameState();
	}

	private void LoadHatsFromPropsString(string hatsJson)
	{
		HashSet<string> hashSet = GameState.PropsStringToSet(hatsJson);
		foreach (string item in hashSet)
		{
			this._hats.Add(item);
		}
	}

	public void Syncronize()
	{
		this.EnsureInitialized();
		UnityEngine.Debug.Log("GameStateSynchronizing game state...");
		string value = GameState.DictToPropsString(this._currentState);
		string value2 = GameState.SetToPropsString(this._hats);
		PlayerPrefs.SetString("com.fliptrickster.game.state", value);
		PlayerPrefs.SetString("com.fliptrickster.hats", value2);
		PlayerPrefs.SetInt("com.fliptrickster.coin.added", this._coinsAdded);
		PlayerPrefs.SetInt("com.fliptrickster.coin.removed", this._coinsRemoved);
		PlayerPrefs.SetInt("com.fliptrickster.ads.disabled", this._adsDisabled);
	}

	public void SetHighScore(int stageId, int levelId, int score)
	{
		this.SetInt(score, "high", stageId, levelId, -1);
	}

	public int GetHighScore(int stageId, int levelId)
	{
		return this.GetInt("high", stageId, levelId, -1);
	}

	public void SetLevelUnlocked(int stageId, int levelId)
	{
		this.SetInt(1, "special", stageId, levelId, -1);
	}

	public bool HasLevelAccess(int stageId, int levelId)
	{
		return this.GetInt("special", stageId, levelId, -1) == 1;
	}

	public bool HasNoLevelAccess(int stageId, int levelId)
	{
		return this.GetInt("special", stageId, levelId, -1) == 0;
	}

	public bool HasBronze(int stageId, int levelId)
	{
		int @int = this.GetInt("bronze", stageId, levelId, -1);
		return @int == 1;
	}

	public bool HasSilver(int stageId, int levelId)
	{
		int @int = this.GetInt("silver", stageId, levelId, -1);
		return @int == 1;
	}

	public bool HasGold(int stageId, int levelId)
	{
		int @int = this.GetInt("gold", stageId, levelId, -1);
		return @int == 1;
	}

	public bool HasSpecial(int stageId, int levelId, int specialId)
	{
		int @int = this.GetInt("special", stageId, levelId, specialId);
		return @int == 1;
	}

	public void AwardBronze(int stageId, int levelId)
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"GameStateAwarded bronze for stage ",
			stageId,
			", level ",
			levelId
		}));
		this.SetInt(1, "bronze", stageId, levelId, -1);
	}

	public void AwardSilver(int stageId, int levelId)
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"GameStateAwarded silver for stage ",
			stageId,
			", level ",
			levelId
		}));
		this.SetInt(1, "silver", stageId, levelId, -1);
	}

	public void AwardGold(int stageId, int levelId)
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"GameStateAwarded gold  for stage ",
			stageId,
			", level ",
			levelId
		}));
		this.SetInt(1, "gold", stageId, levelId, -1);
	}

	public void AwardSpecial(int stageId, int levelId, int specialId)
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"GameStateAwarded special for stage ",
			stageId,
			", level ",
			levelId
		}));
		this.SetInt(1, "special", stageId, levelId, specialId);
	}

	public int AddCoins(int countCount)
	{
		this.EnsureInitialized();
		UnityEngine.Debug.Log("GameStateAdded " + countCount + " coins...");
		this._coinsAdded += countCount;
		return this.GetCoins();
	}

	public int RemoveCoins(int countCount)
	{
		this.EnsureInitialized();
		UnityEngine.Debug.Log("GameStateRemoving " + countCount + " coins...");
		this._coinsRemoved += countCount;
		return this.GetCoins();
	}

	public int GetCoins()
	{
		return this._coinsAdded - this._coinsRemoved;
	}

	public void AwardHat(int hatId)
	{
		this.EnsureInitialized();
		UnityEngine.Debug.Log("GameStateAwarded hat " + hatId);
		this._hats.Add(hatId.ToString());
	}

	public bool HasHat(int hatId)
	{
		this.EnsureInitialized();
		return this._hats.Contains(hatId.ToString());
	}

	public void DisableAds()
	{
		this.EnsureInitialized();
		this._adsDisabled = 1;
	}

	public bool HasAdsDisabled()
	{
		this.EnsureInitialized();
		return this._adsDisabled == 1;
	}

	public void MarkTutorialCompleted()
	{
		this.SetInt(1, "tutorial", -1, -1, -1);
	}

	public bool HasCompletedTutorial()
	{
		return this.GetInt("tutorial", -1, -1, -1) == 1;
	}

	private void SetInt(int value, string prefix, int subKey1 = -1, int subKey2 = -1, int subKey3 = -1)
	{
		this.EnsureInitialized();
		string key = this.CreateKey(prefix, subKey1, subKey2, subKey3);
		this._currentState[key] = value;
	}

	public int GetInt(string prefix, int subKey1 = -1, int subKey2 = -1, int subKey3 = -1)
	{
		this.EnsureInitialized();
		string key = this.CreateKey(prefix, subKey1, subKey2, subKey3);
		if (this._currentState.ContainsKey(key))
		{
			return this._currentState[key];
		}
		return 0;
	}

	private string CreateKey(string prefix, int subKey1 = -1, int subKey2 = -1, int subKey3 = -1)
	{
		string text = prefix;
		if (subKey1 >= 0)
		{
			text = text + ":" + subKey1;
		}
		if (subKey2 >= 0)
		{
			text = text + ":" + subKey2;
		}
		if (subKey3 >= 0)
		{
			text = text + ":" + subKey3;
		}
		return text;
	}

	private Dictionary<string, int> MergeGameState(Dictionary<string, int> state1, Dictionary<string, int> state2)
	{
		if (state1.Count == 0)
		{
			return state2;
		}
		if (state2.Count == 0)
		{
			return state1;
		}
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (KeyValuePair<string, int> keyValuePair in state1)
		{
			int value = keyValuePair.Value;
			if (state2.ContainsKey(keyValuePair.Key))
			{
				value = Math.Max(keyValuePair.Value, state2[keyValuePair.Key]);
			}
			dictionary[keyValuePair.Key] = value;
		}
		foreach (KeyValuePair<string, int> keyValuePair2 in state2)
		{
			int value2 = keyValuePair2.Value;
			if (state1.ContainsKey(keyValuePair2.Key))
			{
				value2 = Math.Max(keyValuePair2.Value, state1[keyValuePair2.Key]);
			}
			dictionary[keyValuePair2.Key] = value2;
		}
		UnityEngine.Debug.Log("GameState: Game state merged");
		return dictionary;
	}

	private static string SetToPropsString(HashSet<string> set)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (string key in set)
		{
			dictionary[key] = 1;
		}
		return GameState.DictToPropsString(dictionary);
	}

	private static HashSet<string> PropsStringToSet(string str)
	{
		Dictionary<string, int> dictionary = GameState.PropsStringToDict(str);
		HashSet<string> hashSet = new HashSet<string>();
		foreach (KeyValuePair<string, int> keyValuePair in dictionary)
		{
			hashSet.Add(keyValuePair.Key);
		}
		return hashSet;
	}

	private static string DictToPropsString(Dictionary<string, int> dict)
	{
		string text = string.Empty;
		foreach (KeyValuePair<string, int> keyValuePair in dict)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				keyValuePair.Key,
				"=",
				keyValuePair.Value,
				"\n"
			});
		}
		return text;
	}

	private static Dictionary<string, int> PropsStringToDict(string str)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		if (!string.IsNullOrEmpty(str))
		{
			string[] array = str.Split(new char[]
			{
				'\n'
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					'='
				});
				if (array3.Length == 2)
				{
					string key = array3[0];
					if (!dictionary.ContainsKey(key))
					{
						int value = 0;
						int.TryParse(array3[1], out value);
						dictionary[key] = value;
					}
				}
			}
		}
		return dictionary;
	}

	public static readonly GameState Instance = new GameState();

	private const string Tag = "GameState";

	private const string KeyGameState = "com.fliptrickster.game.state";

	private const string KeyCoinsAdded = "com.fliptrickster.coin.added";

	private const string KeyCoinsRemoved = "com.fliptrickster.coin.removed";

	private const string KeyAdsDisabled = "com.fliptrickster.ads.disabled";

	private const string KeyHats = "com.fliptrickster.hats";

	private const string KeyUnlocked = "special";

	private const string KeyHigh = "high";

	public const string KeyBronze = "bronze";

	public const string KeySilver = "silver";

	public const string KeyGold = "gold";

	public const string KeySpecial = "special";

	private const string KeyTutorialCompleted = "tutorial";

	private bool _initialized;

	private Dictionary<string, int> _currentState = new Dictionary<string, int>();

	private readonly HashSet<string> _hats = new HashSet<string>();

	private int _coinsAdded;

	private int _coinsRemoved;

	private int _adsDisabled;
}
