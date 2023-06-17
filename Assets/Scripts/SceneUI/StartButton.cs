using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class StartButton : MonoBehaviour
    {
        public delegate void StartBattle();
        public static event StartBattle OnStartingBattle;//try custom event + delegate sys
        [SerializeField] private Button m_StartButton;
        [SerializeField] private int m_MinimumHeroesNum;
        private StorageLogic storage;

        private void Start()
        {
            storage = GetComponent<StorageLogic>();
            OnStartingBattle += ControlStartButton;
            m_StartButton.gameObject.SetActive(false);
        }

        public void BattleBegin()
        {
            OnStartingBattle();//calls delegate
        }

        public void ControlStartButton()
        {
            int deployedReg = GetGrayIcons();

            if (deployedReg >= m_MinimumHeroesNum)
            {
                m_StartButton.gameObject.SetActive(true);
            }
            else
            {
                m_StartButton.gameObject.SetActive(false);
            }
        }
        public int GetGrayIcons()
        {
            int grayIcons = 0;
            foreach (CharIcon icon in storage.charIcons)
            {
                if (icon.deploed)
                {
                    grayIcons++;
                }
            }
            return grayIcons;
        }
    }
}
