using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class UISettingButton : UISelectableButton, IScriptableObjectProperty
    {
        [SerializeField] private Setting setting;
        [SerializeField] private Text titleText;
        [SerializeField] private Text valueText;

        [SerializeField] private Image leftImage;
        [SerializeField] private Image rightImage;

        private void Start()
        {
            ApplyProperty(setting);
        }

        public void SetNextValueSetting()
        {
            setting?.SetNextValue();
            setting?.Apply();
            UpdateInfo();
        }
        public void SetPreviousValueSetting()
        {
            setting?.SetPreviousValue();
            setting?.Apply();
            UpdateInfo();
        }

        public void SetSliderValueSetting(float SliderValue)
        {
            setting?.SetSliderValue(SliderValue);
            setting?.Apply();
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            titleText.text = setting.Title;
            valueText.text = setting.GetStringValue();

            if (leftImage != null)
            {
                leftImage.enabled = !setting.isMinValue;
                rightImage.enabled = !setting.isMaxValue;
            }
        }

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;
            if (property is Setting == false) return;

            setting = property as Setting;

            UpdateInfo();
        }
    }
}
