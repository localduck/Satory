using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class CharIcon : MonoBehaviour
    {
        [SerializeField] private Image m_UnitImage;
        [SerializeField] private Image m_BackgroundImage;
        [SerializeField] private TMPro.TextMeshProUGUI m_StackText;
        [SerializeField] private UnitAttributes m_UnitAttributes;
        private bool m_Deploed = false;
        private StorageLogic storage;
        public bool deploed
        {
            get { return m_Deploed; }
            set { m_Deploed = value; }
        }

        public Image backgroundImage
        {
            get { return m_BackgroundImage; }
            set { m_BackgroundImage = value; }
        }
        public UnitAttributes unitAttributes
        {
            get { return m_UnitAttributes; }
            set { m_UnitAttributes = value; }
        }

        private void Start()
        {
            storage = GetComponentInParent<StorageLogic>();
        }

        public void UnitIsDeploed()
        {
            m_BackgroundImage.sprite = storage.DeployedRegiment;
            m_Deploed = true;
        }

        public void FillIcon()
        {
            m_UnitImage.sprite = m_UnitAttributes.unitSprite;
            m_StackText.text = m_UnitAttributes.stack.ToString();
        }

        public void OnCharIconClicked()
        {
            if(!m_Deploed)
            {
                storage.TintIcon(this);
            } else
            {
                storage.ReturnRegiment(this);
                ReturnDefaultState(m_UnitAttributes);
            }
        }

        private void ReturnDefaultState(UnitAttributes selectedUnitAttributes)
        {
            if(selectedUnitAttributes == m_UnitAttributes)
            {
                m_BackgroundImage.sprite = GetComponentInParent<StorageLogic>().DefaultIcon;
                m_Deploed = false;
            }
        }
    }
}
