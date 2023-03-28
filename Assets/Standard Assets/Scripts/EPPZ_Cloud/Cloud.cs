// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Cloud
using System;
using EPPZ.Cloud.Model;
using EPPZ.Cloud.Model.Simulation;
using EPPZ.Cloud.Plugin;
using UnityEngine;

namespace EPPZ.Cloud
{
	public class Cloud : MonoBehaviour, ICloud
	{
		private void Awake()
		{
			Cloud._instance = this;
		}

		private Cloud plugin
		{
			get
			{
				if (this._plugin == null)
				{
					//this._plugin = Cloud.NativePluginInstance(this);
				}
				return this._plugin;
			}
		}

		private void Start()
		{
			if (this.settings.initializeOnStart)
			{
				this._Initialize();
			}
		}

		private void _Initialize()
		{
			//this.plugin.InitializeWithGameObjectName(base.name);
		}

		private void OnDestroy()
		{
			this._RemoveOnKeyChangeActions();
		}

		public static void Initialize()
		{
			Cloud._instance._Initialize();
		}

		public static void Synchrnonize()
		{
			//Cloud._instance.plugin.Synchronize();
		}

		public static void OnKeyChange(string key, Action<string> action, int priority = 0)
		{
			Cloud._instance._OnKeyChange(key, action, priority);
		}

		public static void OnKeyChange(string key, Action<float> action, int priority = 0)
		{
			Cloud._instance._OnKeyChange(key, action, priority);
		}

		public static void OnKeyChange(string key, Action<int> action, int priority = 0)
		{
			Cloud._instance._OnKeyChange(key, action, priority);
		}

		public static void OnKeyChange(string key, Action<bool> action, int priority = 0)
		{
			Cloud._instance._OnKeyChange(key, action, priority);
		}

		public static void RemoveOnKeyChangeAction(string key, Action<string> action)
		{
			Cloud._instance._RemoveOnKeyChangeAction(key, action);
		}

		public static void RemoveOnKeyChangeAction(string key, Action<float> action)
		{
			Cloud._instance._RemoveOnKeyChangeAction(key, action);
		}

		public static void RemoveOnKeyChangeAction(string key, Action<int> action)
		{
			Cloud._instance._RemoveOnKeyChangeAction(key, action);
		}

		public static void RemoveOnKeyChangeAction(string key, Action<bool> action)
		{
			Cloud._instance._RemoveOnKeyChangeAction(key, action);
		}

		public static void SetStringForKey(string value, string key)
		{
			//Cloud._instance.plugin.SetStringForKey(value, key);
		}

		public static string StringForKey(string key)
		{
            return "";// Cloud._instance.plugin.StringForKey(key);
		}

		public static void SetFloatForKey(float value, string key)
		{
			//Cloud._instance.plugin.SetFloatForKey(value, key);
		}

		public static float FloatForKey(string key)
		{
            return 0f;// Cloud._instance.plugin.FloatForKey(key);
		}

		public static void SetIntForKey(int value, string key)
		{
			//Cloud._instance.plugin.SetIntForKey(value, key);
		}

		public static int IntForKey(string key)
		{
            return 0;// Cloud._instance.plugin.IntForKey(key);
		}

		public static void SetBoolForKey(bool value, string key)
		{
			//Cloud._instance.plugin.SetBoolForKey(value, key);
		}

		public static bool BoolForKey(string key)
		{
            return false;// Cloud._instance.plugin.BoolForKey(key);
		}

		public static ChangeReason LatestChangeReason()
		{
			return Cloud._instance.latestChangeReason;
		}

		public static void Log(string message)
		{
			if (Cloud._instance.settings.log)
			{
				UnityEngine.Debug.Log(message);
			}
		}

		public static KeyValueStore SimulationKeyValueStore()
		{
			return Cloud._instance.simulationKeyValueStore;
		}

		public static void InvokeOnKeysChanged(string[] changedKeys, ChangeReason changeReason)
		{
			Cloud.Log(string.Concat(new object[]
			{
				"Cloud.InvokeOnKeysChanged(`",
				changedKeys,
				"`, `",
				changeReason,
				"`)"
			}));
			Cloud._instance._OnCloudChange(changedKeys, changeReason);
		}

		private void _RemoveOnKeyChangeAction(string key, object action)
		{
			EPPZ.Cloud.Model.KeyValuePair keyValuePair = this.settings.KeyValuePairForKey(key);
			if (keyValuePair != null)
			{
				keyValuePair.RemoveOnChangeAction(action);
			}
		}

		private void _OnKeyChange(string key, object action, int priority)
		{
			EPPZ.Cloud.Model.KeyValuePair keyValuePair = this.settings.KeyValuePairForKey(key);
			if (keyValuePair != null)
			{
				keyValuePair.AddOnChangeAction(action, priority);
			}
		}

		private void _RemoveOnKeyChangeActions()
		{
			foreach (EPPZ.Cloud.Model.KeyValuePair keyValuePair in this.settings.keyValuePairs)
			{
				keyValuePair.RemoveOnChangeActions();
			}
		}

		public void _CloudDidChange(string message)
		{
			//this.plugin.CloudDidChange(message);
		}

		public void _OnCloudChange(string[] changedKeys, ChangeReason changeReason)
		{
			Cloud.Log(string.Concat(new object[]
			{
				"Cloud._OnCloudChange(`",
				changedKeys,
				"`, `",
				changeReason,
				"`)"
			}));
			this.latestChangeReason = changeReason;
			if (Cloud.onCloudChange != null)
			{
				Cloud.Should should = Cloud.onCloudChange(changedKeys, changeReason);
				if (should == Cloud.Should.StopUpdateKeys)
				{
					return;
				}
			}
			foreach (string b in changedKeys)
			{
				foreach (EPPZ.Cloud.Model.KeyValuePair keyValuePair in this.settings.keyValuePairs)
				{
					if (keyValuePair.key == b)
					{
						keyValuePair.InvokeOnValueChangedAction();
					}
				}
			}
		}

		private static Cloud _instance;

		public Settings settings;

		public KeyValueStore simulationKeyValueStore;

		public static Cloud.OnCloudChange onCloudChange;

		private ChangeReason latestChangeReason;

		private Cloud _plugin;

		public enum Should
		{
			UpdateKeys,
			StopUpdateKeys
		}

		public delegate Cloud.Should OnCloudChange(string[] changedKeys, ChangeReason changeReason);
	}
}
