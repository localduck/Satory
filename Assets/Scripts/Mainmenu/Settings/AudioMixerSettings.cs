using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Satory23
{
    [CreateAssetMenu(fileName = "AudioMixerSettings", menuName = "ScriptableObjects/Settings/AudioMixerSettings")]
    public class AudioMixerSettings : Setting
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private string nameSetting;

        // 0 .. -80
        [SerializeField] private float minRealValue;
        [SerializeField] private float maxRealValue;

        [SerializeField] private float virtualStep;
        [SerializeField] private float minVirtualValue;
        [SerializeField] private float maxVirtualValue;

        private float currentValue = 0;
        public override bool isMinValue { get => currentValue == minRealValue; }
        public override bool isMaxValue { get => currentValue == maxRealValue; }

        public override void SetNextValue()
        {
            AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
        }

        public override void SetPreviousValue()
        {
            AddValue(-Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
        }

        public override void SetSliderValue(float SliderValue)
        {
            currentValue = Mathf.Lerp(minRealValue/4, maxRealValue, SliderValue);
        }

        public override string GetStringValue()
        {
            return ((int) Mathf.Lerp(minVirtualValue, maxVirtualValue, (currentValue - minRealValue) / (maxRealValue - minRealValue))).ToString();
        }

        public override object GetValue()
        {
            return currentValue;
        }

        private void AddValue(float value)
        {
            currentValue += value;
            currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
        }

        public override void Apply()
        {
            audioMixer.SetFloat(nameSetting, currentValue);

            Save();
        }

        public override void Load()
        {
            currentValue = PlayerPrefs.GetFloat(title, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(title, currentValue);
        }
    }
}
