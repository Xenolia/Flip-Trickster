// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Plugin.Cloud_iOS
using System;
using System.Runtime.InteropServices;
using EPPZ.Cloud.Plugin.iOS;
using UnityEngine;

namespace EPPZ.Cloud.Plugin
{
	public class Cloud_iOS : Cloud
	{
		[DllImport("__Internal")]
		private static extern void EPPZ_Cloud_InitializeWithGameObjectName(string gameObjectName);

		[DllImport("__Internal")]
		private static extern bool EPPZ_Cloud_Synchronize();

		[DllImport("__Internal")]
		private static extern string EPPZ_Cloud_StringForKey(string key);

		[DllImport("__Internal")]
		private static extern void EPPZ_Cloud_SetStringForKey(string value, string key);

		[DllImport("__Internal")]
		private static extern float EPPZ_Cloud_FloatForKey(string key);

		[DllImport("__Internal")]
		private static extern void EPPZ_Cloud_SetFloatForKey(float value, string key);

		[DllImport("__Internal")]
		private static extern int EPPZ_Cloud_IntForKey(string key);

		[DllImport("__Internal")]
		private static extern void EPPZ_Cloud_SetIntForKey(int value, string key);

		[DllImport("__Internal")]
		private static extern bool EPPZ_Cloud_BoolForKey(string key);

		[DllImport("__Internal")]
		private static extern void EPPZ_Cloud_SetBoolForKey(bool value, string key);

		public override void InitializeWithGameObjectName(string gameObjectName)
		{
			Cloud_iOS.EPPZ_Cloud_InitializeWithGameObjectName(gameObjectName);
		}

		public override void Synchronize()
		{
			Cloud_iOS.EPPZ_Cloud_Synchronize();
		}

		public override void CloudDidChange(string message)
		{
			base.Log("Cloud_iOS.CloudDidChange(`" + message + "`)");
			UserInfo userInfo = new UserInfo();
			JsonUtility.FromJsonOverwrite(message, userInfo);
			ChangeReason nsubiquitousKeyValueStoreChangeReasonKey = (ChangeReason)userInfo.NSUbiquitousKeyValueStoreChangeReasonKey;
			string[] nsubiquitousKeyValueStoreChangedKeysKey = userInfo.NSUbiquitousKeyValueStoreChangedKeysKey;
			base.Log("Cloud_iOS.CloudDidChange.changeReason: `" + nsubiquitousKeyValueStoreChangeReasonKey + "`");
			base.Log("Cloud_iOS.CloudDidChange.changedKeys: `" + nsubiquitousKeyValueStoreChangedKeysKey + "`");
			this.cloudObject._OnCloudChange(nsubiquitousKeyValueStoreChangedKeysKey, nsubiquitousKeyValueStoreChangeReasonKey);
		}

		public override string StringForKey(string key)
		{
			return Cloud_iOS.EPPZ_Cloud_StringForKey(key);
		}

		public override void SetStringForKey(string value, string key)
		{
			base.Log(string.Concat(new string[]
			{
				"Cloud_iOS.SetStringForKey(`",
				value,
				"`, `",
				key,
				"`)"
			}));
			Cloud_iOS.EPPZ_Cloud_SetStringForKey(value, key);
		}

		public override float FloatForKey(string key)
		{
			return Cloud_iOS.EPPZ_Cloud_FloatForKey(key);
		}

		public override void SetFloatForKey(float value, string key)
		{
			base.Log(string.Concat(new object[]
			{
				"Cloud_iOS.SetFloatForKey(`",
				value,
				"`, `",
				key,
				"`)"
			}));
			Cloud_iOS.EPPZ_Cloud_SetFloatForKey(value, key);
		}

		public override int IntForKey(string key)
		{
			return Cloud_iOS.EPPZ_Cloud_IntForKey(key);
		}

		public override void SetIntForKey(int value, string key)
		{
			base.Log(string.Concat(new object[]
			{
				"Cloud_iOS.SetIntForKey(`",
				value,
				"`, `",
				key,
				"`)"
			}));
			Cloud_iOS.EPPZ_Cloud_SetIntForKey(value, key);
		}

		public override bool BoolForKey(string key)
		{
			return Cloud_iOS.EPPZ_Cloud_BoolForKey(key);
		}

		public override void SetBoolForKey(bool value, string key)
		{
			base.Log(string.Concat(new object[]
			{
				"Cloud_iOS.SetBoolForKey(`",
				value,
				"`, `",
				key,
				"`)"
			}));
			Cloud_iOS.EPPZ_Cloud_SetBoolForKey(value, key);
		}
	}
}
