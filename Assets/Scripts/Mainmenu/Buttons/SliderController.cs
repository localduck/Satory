using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private UISettingButton SettingButton;
        [SerializeField] private Slider ControlledSlider;

        public void Tick()
        {
            SettingButton.SetSliderValueSetting(ControlledSlider.value);
        }
    }
}
