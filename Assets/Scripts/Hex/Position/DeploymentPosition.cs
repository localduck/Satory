using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public enum PositionForRegiment { none, player, enemy };
    public class DeploymentPosition : MonoBehaviour
    {
        [SerializeField] private Image m_VisualModel;
        [SerializeField] private PositionForRegiment m_RegimentPosition;
        private HexBattleground parentHex;
        public PositionForRegiment RegimentPosition
        {
            get { return m_RegimentPosition; }
            set { m_RegimentPosition = value; }
        }

        // Start is called before the first frame update
        private void Start()
        {
            parentHex = GetComponentInParent<HexBattleground>();//finds the parent hex
            StartButton.OnStartingBattle += DisableSelf;
        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void OnMouseDown()
        {
            if(DeploymentManager.readForDeploymentIcon != null && m_RegimentPosition == PositionForRegiment.player)
            {
                DeploymentManager.DeployRegiment(parentHex);
            }
        }

        private void DisableSelf()
        {
            parentHex.ClearDeploymentPosition();
        }
    }
}
