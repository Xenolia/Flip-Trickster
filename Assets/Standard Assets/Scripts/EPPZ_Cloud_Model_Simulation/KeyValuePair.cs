// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Model.Simulation.KeyValuePair
using System;
using UnityEngine;

namespace EPPZ.Cloud.Model.Simulation
{
	[Serializable]
	public class KeyValuePair
	{
		public virtual string stringValue
		{
			get
			{
				return this._stringValue;
			}
			set
			{
				this._stringValue = value;
			}
		}

		public string key;

		//public KeyValuePair.Type type;

		public bool isChanged;

		private string _stringValue;

		public float floatValue;

		public int intValue;

		public bool boolValue;

		[HideInInspector]
		public bool foldedOut = true;
	}
}
