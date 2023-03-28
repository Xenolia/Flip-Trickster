// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: EPPZ.Cloud.Scenes.Controller
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EPPZ.Cloud.Scenes
{
	public class Controller : MonoBehaviour
	{
		private void Start()
		{
			Cloud.onCloudChange = (Cloud.OnCloudChange)Delegate.Combine(Cloud.onCloudChange, new Cloud.OnCloudChange(this.OnCloudChange));
			this.AddElementUpdatingActions();
			this.PopulateElementsFromCloud();
		}

		private void OnDestroy()
		{
			Cloud.onCloudChange = (Cloud.OnCloudChange)Delegate.Remove(Cloud.onCloudChange, new Cloud.OnCloudChange(this.OnCloudChange));
		}

		private void PopulateElementsFromCloud()
		{
			this.elements.nameLabel.text = Cloud.StringForKey("name");
			this.elements.soundToggle.isOn = Cloud.BoolForKey("sound");
			this.elements.volumeSlider.value = Cloud.FloatForKey("volume");
			this.elements.levelDropdown.value = Cloud.IntForKey("level");
			this.elements.firstTrophyToggle.isOn = Cloud.BoolForKey("firstTrophy");
			this.elements.secondTrophyToggle.isOn = Cloud.BoolForKey("secondTrophy");
			this.elements.thirdTrophyToggle.isOn = Cloud.BoolForKey("thirdTrophy");
		}

		private Cloud.Should OnCloudChange(string[] changedKeys, ChangeReason changeReason)
		{
			if (changeReason == ChangeReason.InitialSyncChange)
			{
				this.PopulateElementsFromCloud();
				return Cloud.Should.StopUpdateKeys;
			}
			if (changeReason == ChangeReason.QuotaViolationChange)
			{
				return Cloud.Should.StopUpdateKeys;
			}
			if (changeReason == ChangeReason.AccountChange)
			{
				this.PopulateElementsFromCloud();
				return Cloud.Should.StopUpdateKeys;
			}
			return Cloud.Should.UpdateKeys;
		}

		public void OnNameInputFieldEndEdit(string text)
		{
			Cloud.SetStringForKey(text, "name");
			Cloud.Synchrnonize();
		}

		public void OnSoundToggleValueChanged(bool isOn)
		{
			Cloud.SetBoolForKey(isOn, "sound");
			Cloud.Synchrnonize();
		}

		public void OnVolumeSliderEndDrag(BaseEventData eventData)
		{
			Cloud.SetFloatForKey(this.elements.volumeSlider.value, "volume");
			Cloud.Synchrnonize();
		}

		public void OnLevelDropDownValueChanged(int value)
		{
			Cloud.SetIntForKey(value, "level");
			Cloud.Synchrnonize();
		}

		public void OnFirstTrophyToggleValueChanged(bool isOn)
		{
			Cloud.SetBoolForKey(isOn, "firstTrophy");
			Cloud.Synchrnonize();
		}

		public void OnSecondTrophyToggleValueChanged(bool isOn)
		{
			Cloud.SetBoolForKey(isOn, "secondTrophy");
			Cloud.Synchrnonize();
		}

		public void OnThirdTrophyToggleValueChanged(bool isOn)
		{
			Cloud.SetBoolForKey(isOn, "thirdTrophy");
			Cloud.Synchrnonize();
		}

		public void OnConflictResolutionToggleValueChanged(bool isOn)
		{
			if (isOn)
			{
				this.AddConflictResolvingActions();
			}
			else
			{
				this.RemoveConflictResolvingActions();
			}
		}

		private void AddElementUpdatingActions()
		{
			Cloud.OnKeyChange("name", delegate(string value)
			{
				this.elements.nameLabel.text = value;
				this.elements.nameLabelAnimation.Play("Blink");
			}, 2);
			Cloud.OnKeyChange("sound", delegate(bool value)
			{
				this.elements.soundToggle.isOn = value;
				this.elements.soundToggleAnimation.Play("Blink");
			}, 2);
			Cloud.OnKeyChange("volume", delegate(float value)
			{
				this.elements.volumeSlider.value = value;
				this.elements.volumeSliderAnimation.Play("Blink");
			}, 2);
			Cloud.OnKeyChange("level", delegate(int value)
			{
				this.elements.levelDropdown.value = value;
				this.elements.levelDropdownAnimation.Play("Blink");
			}, 2);
			Cloud.OnKeyChange("firstTrophy", delegate(bool value)
			{
				this.elements.firstTrophyToggle.isOn = value;
				this.elements.firstTrophyToggleAnimation.Play("Blink");
			}, 2);
			Cloud.OnKeyChange("secondTrophy", delegate(bool value)
			{
				this.elements.secondTrophyToggle.isOn = value;
				this.elements.secondTrophyToggleAnimation.Play("Blink");
			}, 2);
			Cloud.OnKeyChange("thirdTrophy", delegate(bool value)
			{
				this.elements.thirdTrophyToggle.isOn = value;
				this.elements.thirdTrophyToggleAnimation.Play("Blink");
			}, 2);
		}

		private void AddConflictResolvingActions()
		{
			Cloud.OnKeyChange("level", new Action<int>(this.ResolveConflictForLevel), 1);
			Cloud.OnKeyChange("firstTrophy", new Action<bool>(this.ResolveConflictForFirstTrophy), 1);
			Cloud.OnKeyChange("secondTrophy", new Action<bool>(this.ResolveConflictForSecondTrophy), 1);
			Cloud.OnKeyChange("thirdTrophy", new Action<bool>(this.ResolveConflictForThirdTrophy), 1);
		}

		private void RemoveConflictResolvingActions()
		{
			Cloud.RemoveOnKeyChangeAction("level", new Action<int>(this.ResolveConflictForLevel));
			Cloud.RemoveOnKeyChangeAction("firstTrophy", new Action<bool>(this.ResolveConflictForFirstTrophy));
			Cloud.RemoveOnKeyChangeAction("secondTrophy", new Action<bool>(this.ResolveConflictForSecondTrophy));
			Cloud.RemoveOnKeyChangeAction("thirdTrophy", new Action<bool>(this.ResolveConflictForThirdTrophy));
		}

		private void ResolveConflictForLevel(int value)
		{
			UnityEngine.Debug.Log("ResolveConflictForLevel(" + value + ")");
			bool flag = this.elements.levelDropdown.value != value;
			if (flag)
			{
				this.elements.levelDropdown.value = Math.Max(this.elements.levelDropdown.value, value);
				this.OnLevelDropDownValueChanged(this.elements.levelDropdown.value);
			}
		}

		private void ResolveConflictForFirstTrophy(bool value)
		{
			UnityEngine.Debug.Log("ResolveConflictForFirstTrophy(" + value + ")");
			bool flag = this.elements.firstTrophyToggle.isOn != value;
			if (flag)
			{
				this.elements.firstTrophyToggle.isOn = (this.elements.firstTrophyToggle.isOn || value);
				this.OnFirstTrophyToggleValueChanged(this.elements.firstTrophyToggle.isOn);
			}
		}

		private void ResolveConflictForSecondTrophy(bool value)
		{
			UnityEngine.Debug.Log("ResolveConflictForSecondTrophy(" + value + ")");
			bool flag = this.elements.secondTrophyToggle.isOn != value;
			if (flag)
			{
				this.elements.secondTrophyToggle.isOn = (this.elements.secondTrophyToggle.isOn || value);
				this.OnSecondTrophyToggleValueChanged(this.elements.secondTrophyToggle.isOn);
			}
		}

		private void ResolveConflictForThirdTrophy(bool value)
		{
			UnityEngine.Debug.Log("ResolveConflictForThirdTrophy(" + value + ")");
			bool flag = this.elements.thirdTrophyToggle.isOn != value;
			if (flag)
			{
				this.elements.thirdTrophyToggle.isOn = (this.elements.thirdTrophyToggle.isOn || value);
				this.OnThirdTrophyToggleValueChanged(this.elements.thirdTrophyToggle.isOn);
			}
		}

		public Controller.Elements elements;

		[Serializable]
		public class Elements
		{
			public InputField nameLabel;

			public Animation nameLabelAnimation;

			[Space]
			public Toggle soundToggle;

			public Animation soundToggleAnimation;

			[Space]
			public Slider volumeSlider;

			public Animation volumeSliderAnimation;

			[Space]
			public Dropdown levelDropdown;

			public Animation levelDropdownAnimation;

			[Space]
			public Toggle firstTrophyToggle;

			public Animation firstTrophyToggleAnimation;

			[Space]
			public Toggle secondTrophyToggle;

			public Animation secondTrophyToggleAnimation;

			[Space]
			public Toggle thirdTrophyToggle;

			public Animation thirdTrophyToggleAnimation;
		}
	}
}
