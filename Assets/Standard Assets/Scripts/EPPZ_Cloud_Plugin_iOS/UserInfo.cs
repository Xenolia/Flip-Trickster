// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Plugin.iOS.UserInfo
using System;

namespace EPPZ.Cloud.Plugin.iOS
{
	[Serializable]
	public class UserInfo
	{
		public UserInfo.NSUbiquitousKeyValueStoreChangeReason NSUbiquitousKeyValueStoreChangeReasonKey;

		public string[] NSUbiquitousKeyValueStoreChangedKeysKey;

		public enum NSUbiquitousKeyValueStoreChangeReason
		{
			NSUbiquitousKeyValueStoreServerChange,
			NSUbiquitousKeyValueStoreInitialSyncChange,
			NSUbiquitousKeyValueStoreQuotaViolationChange,
			NSUbiquitousKeyValueStoreAccountChange
		}
	}
}
