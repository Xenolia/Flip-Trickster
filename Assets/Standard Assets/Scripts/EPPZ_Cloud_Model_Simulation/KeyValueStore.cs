// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Model.Simulation.KeyValueStore
using System;
using System.Collections.Generic;
using EPPZ.Cloud.Plugin;
using UnityEngine;

namespace EPPZ.Cloud.Model.Simulation
{
	[CreateAssetMenu(fileName = "Key-value store simulation", menuName = "eppz!/Cloud/Key-value store simulation")]
	public class KeyValueStore : ScriptableObject
	{
		public virtual void EnumerateKeyValuePairs(Action<KeyValuePair> action)
		{
			foreach (KeyValuePair obj in this.keyValuePairs)
			{
				action(obj);
			}
		}

		public virtual KeyValuePair KeyValuePairForKey(string key)
		{
			foreach (KeyValuePair keyValuePair in this.keyValuePairs)
			{
				if (keyValuePair.key == key)
				{
					return keyValuePair;
				}
			}
			UnityEngine.Debug.LogWarning("eppz! Cloud: No Key-value pair defined for key `" + key + "`");
			return null;
		}

		[ContextMenu("Simulate `CloudDidChange`")]
		public virtual void SimulateCloudDidChange()
		{
			UnityEngine.Debug.Log("SimulateCloudDidChange()");
			List<string> changedKeys = new List<string>();
			this.EnumerateKeyValuePairs(delegate(KeyValuePair eachKeyValuePair)
			{
				if (eachKeyValuePair.isChanged)
				{
					changedKeys.Add(eachKeyValuePair.key);
					eachKeyValuePair.isChanged = false;
				}
			});
			UnityEngine.Debug.Log("changedKeys: `" + changedKeys.ToArray() + "`");
			Cloud.InvokeOnKeysChanged(changedKeys.ToArray(), this.changeReason);
		}

		public ChangeReason changeReason;

		public KeyValuePair[] keyValuePairs;

		public Cloud_Editor plugin;
	}
}
