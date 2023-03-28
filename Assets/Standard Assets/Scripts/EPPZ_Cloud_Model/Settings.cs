// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Model.Settings
using System;
using UnityEngine;

namespace EPPZ.Cloud.Model
{
	[CreateAssetMenu(fileName = "Cloud settings", menuName = "eppz!/Cloud/Settings")]
	public class Settings : ScriptableObject
	{
		public KeyValuePair KeyValuePairForKey(string key)
		{
			foreach (KeyValuePair keyValuePair in this.keyValuePairs)
			{
				if (keyValuePair.key == key)
				{
					return keyValuePair;
				}
			}
			UnityEngine.Debug.LogWarning("eppz! Cloud: Cannot find registered key for `" + key + "`.");
			return null;
		}

		public KeyValuePair[] keyValuePairs;

		public bool initializeOnStart = true;

		public bool log = true;
	}
}
