using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23 {
    public class Ocean : HexBattleground
    {
        void Start()
        {
            SetVisualModel();
        }
        public override void SetTargetToMove()
        {
            m_ClickThis.ClearPreviousSelectionOfTargetHex();
        }

        public override void SetAvalible()
        {
            if (VisualModel == null)
            {
                SetVisualModel();
            }
            if (m_CurrentStateImg == null)
            {
                SetCurrentStateImg();
            }
            m_CurrentStateImg.color = new Color32(255, 255, 255, 0);
        }

        public override bool AvailableToGround()
        {
            return false;
        }
    }
}
