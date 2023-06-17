using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class StorageLogic : MonoBehaviour
    {
        [SerializeField] private CurrentProgress m_CurrentProgress;
        [SerializeField] private CharIcon m_CharIconPrefab;

        [SerializeField] private Sprite m_SelectedIcon;
        [SerializeField] private Sprite m_defaultIcon;
        [SerializeField] private Sprite m_deployedRegiment;

        public CharIcon[] charIcons;

        public CurrentProgress CurrentProgress
        {
            get { return m_CurrentProgress; }
        }
        public Sprite DeployedRegiment
        {
            get { return m_deployedRegiment; }
        }
        public Sprite DefaultIcon
        {
            get { return m_defaultIcon; }
        }
        public Sprite SelectedIcon
        {
            get { return m_SelectedIcon; }
        }

        private List<UnitAttributes> regimentIcons;
        private ScrollRect m_ScrollRect;

        // Start is called before the first frame update
        private void Start()
        {
            regimentIcons = new List<UnitAttributes>();
            m_ScrollRect = GetComponent<ScrollRect>();
            CallUnitIcons();

            StartButton.OnStartingBattle += DisableSelf;
            charIcons = GetComponentsInChildren<CharIcon>();
        }

        private void CallUnitIcons()
        {
            regimentIcons = m_CurrentProgress.UnitsOfPlayer;
            Transform parentOfIcons = m_ScrollRect.content.transform;
            for (int i = 0; i < regimentIcons.Count; i++)
            {
                CharIcon fighterIcon =  Instantiate(m_CharIconPrefab, parentOfIcons);
                fighterIcon.unitAttributes = regimentIcons[i];
                fighterIcon.FillIcon();
            }
        }

        public void ReturnRegiment(CharIcon clickedIcon)
        {
            UnitAttributes scriptableRegiment = clickedIcon.unitAttributes;
            Hero[] regimentsBattleground = FindObjectsOfType<Hero>();

            foreach(Hero hero in regimentsBattleground)
            {
                if (hero.HeroData == scriptableRegiment)
                {
                    clickedIcon.deploed = !clickedIcon.deploed;

                    RemoveHero(hero);
                    break;
                }
            }
        }

        private void RemoveHero(Hero hero)
        {
            HexBattleground parentHex = hero.GetComponentInParent<HexBattleground>();
            parentHex.SetDeploymentPosition();
            Destroy(hero.gameObject);

            GetComponent<StartButton>().ControlStartButton();
        }

        public void TintIcon(CharIcon clickedIcon)
        {
            CharIcon[] charIcon = GetComponentsInChildren<CharIcon>();
            foreach (CharIcon icon in charIcon)
            {
                if(!icon.deploed)
                {
                    icon.backgroundImage.sprite = m_defaultIcon;
                }
            }
            clickedIcon.backgroundImage.sprite = m_SelectedIcon;
            DeploymentManager.readForDeploymentIcon = clickedIcon;
        }

        private void DisableSelf()
        {
            gameObject.SetActive(false);
        }
    }
}
