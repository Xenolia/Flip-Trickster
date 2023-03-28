// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Plugin.Cloud_Editor
using System;
using EPPZ.Cloud.Model.Simulation;

namespace EPPZ.Cloud.Plugin
{
	public class Cloud_Editor : Cloud
	{
		private KeyValueStore keyValueStore
		{
			get
			{
				return null;
			}
		}

		public override void Synchronize()
		{
			this.keyValueStore.SimulateCloudDidChange();
		}

		public override string StringForKey(string key)
		{
			return this.keyValueStore.KeyValuePairForKey(key).stringValue;
		}

		public override void SetStringForKey(string value, string key)
		{
			base.Log(string.Concat(new string[]
			{
				"Cloud_Editor.SetStringForKey(`",
				value,
				"`, `",
				key,
				"`)"
			}));
			this.keyValueStore.KeyValuePairForKey(key).stringValue = value;
		}

		public override float FloatForKey(string key)
		{
			return this.keyValueStore.KeyValuePairForKey(key).floatValue;
		}

		public override void SetFloatForKey(float value, string key)
		{
			base.Log(string.Concat(new object[]
			{
				"Cloud_Editor.SetFloatForKey(`",
				value,
				"`, `",
				key,
				"`)"
			}));
			this.keyValueStore.KeyValuePairForKey(key).floatValue = value;
		}

		public override int IntForKey(string key)
		{
			return this.keyValueStore.KeyValuePairForKey(key).intValue;
		}

		public override void SetIntForKey(int value, string key)
		{
			base.Log(string.Concat(new object[]
			{
				"Cloud_Editor.SetIntForKey(`",
				value,
				"`, `",
				key,
				"`)"
			}));
			this.keyValueStore.KeyValuePairForKey(key).intValue = value;
		}

		public override bool BoolForKey(string key)
		{
			return this.keyValueStore.KeyValuePairForKey(key).boolValue;
		}

		public override void SetBoolForKey(bool value, string key)
		{
			base.Log(string.Concat(new object[]
			{
				"Cloud_Editor.SetBoolForKey(`",
				value,
				"`, `",
				key,
				"`)"
			}));
			this.keyValueStore.KeyValuePairForKey(key).boolValue = value;
		}
	}
}
