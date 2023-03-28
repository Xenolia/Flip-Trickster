// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Model.KeyValuePair
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EPPZ.Cloud.Model
{
	[Serializable]
	public class KeyValuePair
	{
		public KeyValuePair()
		{
			this.onChangeActions = new Dictionary<int, object>();
			this.invokersForTypes = new Dictionary<KeyValuePair.Type, Action<object>>
			{
				{
					KeyValuePair.Type.String,
					delegate(object eachAction)
					{
						((Action<string>)eachAction)(this.stringValue);
					}
				},
				{
					KeyValuePair.Type.Float,
					delegate(object eachAction)
					{
						((Action<float>)eachAction)(this.floatValue);
					}
				},
				{
					KeyValuePair.Type.Int,
					delegate(object eachAction)
					{
						((Action<int>)eachAction)(this.intValue);
					}
				},
				{
					KeyValuePair.Type.Bool,
					delegate(object eachAction)
					{
						((Action<bool>)eachAction)(this.boolValue);
					}
				}
			};
		}

		private System.Type actionType
		{
			get
			{
				return this.actionTypesForTypes[this.type];
			}
		}

		private Action<object> invoker
		{
			get
			{
				return this.invokersForTypes[this.type];
			}
		}

		public void InvokeOnValueChangedAction()
		{
			UnityEngine.Debug.Log("InvokeOnValueChangedAction()");
			UnityEngine.Debug.Log("onChangeActions.Count: " + this.onChangeActions.Count);
			if (this.onChangeActions.Count == 0)
			{
				return;
			}
			List<int> list = this.onChangeActions.Keys.ToList<int>();
			list.Sort();
			foreach (int num in list)
			{
				this.invoker(this.onChangeActions[num]);
			}
		}

		public void AddOnChangeAction(object action, int priority)
		{
			if (action.GetType() != this.actionType)
			{
				UnityEngine.Debug.LogWarning(string.Concat(new object[]
				{
					"eppz! Cloud: Cannot add on change action for key `",
					this.key,
					"` with type `",
					this.type,
					"`. Types mismatched."
				}));
				return;
			}
			this.onChangeActions.Add(priority, action);
		}

		public void RemoveOnChangeAction(object action)
		{
			foreach (KeyValuePair<int, object> keyValuePair2 in (from keyValuePair in this.onChangeActions
			where keyValuePair.Value == action
			select keyValuePair).ToList<KeyValuePair<int, object>>())
			{
				this.onChangeActions.Remove(keyValuePair2.Key);
			}
		}

		public void RemoveOnChangeActions()
		{
			this.onChangeActions.Clear();
		}

		public string stringValue
		{
			get
			{
				return Cloud.StringForKey(this.key);
			}
		}

		public float floatValue
		{
			get
			{
				return Cloud.FloatForKey(this.key);
			}
		}

		public int intValue
		{
			get
			{
				return Cloud.IntForKey(this.key);
			}
		}

		public bool boolValue
		{
			get
			{
				return Cloud.BoolForKey(this.key);
			}
		}

		public string key;

		public KeyValuePair.Type type;

		private Dictionary<KeyValuePair.Type, System.Type> actionTypesForTypes = new Dictionary<KeyValuePair.Type, System.Type>
		{
			{
				KeyValuePair.Type.String,
				typeof(Action<string>)
			},
			{
				KeyValuePair.Type.Float,
				typeof(Action<float>)
			},
			{
				KeyValuePair.Type.Int,
				typeof(Action<int>)
			},
			{
				KeyValuePair.Type.Bool,
				typeof(Action<bool>)
			}
		};

		private Dictionary<int, object> onChangeActions;

		private Dictionary<KeyValuePair.Type, Action<object>> invokersForTypes;

		public enum Type
		{
			String,
			Float,
			Int,
			Bool
		}
	}
}
