using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class SettingLoader : MonoBehaviour
    {
        [SerializeField] private Setting[] allSettings;

        private void Awake()
        {
            for (int i = 0; i < allSettings.Length; i++)
            {
                allSettings[i].Load();
                allSettings[i].Apply();
            }
        }
    }
}
