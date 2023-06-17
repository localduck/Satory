using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Satory23
{
    public class HexClicked : MonoBehaviour, IPointerClickHandler
    {
        private HexBattleground m_Hex;
        private FieldManager m_FieldManager;
        private bool m_IsTargetToMove = false;
        private BattleController battleController;

        public bool IsTargetToMove
        {
            get { return m_IsTargetToMove; }
            set { m_IsTargetToMove = value; }
        }

        private void Awake()
        {
            m_Hex = GetComponent<HexBattleground>();
            m_FieldManager = GetComponent<FieldManager>();
            battleController = FindObjectOfType<BattleController>();
        }
        
        public void ClearPreviousSelectionOfTargetHex()
        {
            foreach (HexBattleground hex in FieldManager.activeHexList)
            {
                if(hex.ClickThis.IsTargetToMove == true)
                {
                    hex.GetComponent<HexClicked>().IsTargetToMove = false;
                    hex.SetAvalible();
                }
                hex.VisualModel.color = new Color32(250, 250, 250, 250);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {//Unity interface
            if (m_Hex.potentialTarget)
            {
                battleController.events.gameObject.SetActive(false);

                BattleController.currentTarget = this.GetComponentInChildren<Hero>();
                BattleController.currentAtacker.HeroIsAtacking();
                return;
            }

            if (!m_IsTargetToMove)
            {
                SelectTargetToMove();
            } else
            {
                BattleController.currentAtacker.GetComponent<UniteMove>().StartsMoving();
            }
        }

        private void SelectTargetToMove()
        {
            ClearPreviousSelectionOfTargetHex();
            if (m_Hex.IsNeighbourHex)
            {
                m_Hex.SetTargetToMove();
                BattleController.currentAtacker.GetComponent<OptimalPath>().MatchPath();
            }
        }
    }
}
