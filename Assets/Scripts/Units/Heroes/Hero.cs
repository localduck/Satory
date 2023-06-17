using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public abstract class Hero : MonoBehaviour
    {
        public StackTxt stack;
        [SerializeField] private int m_Velocity = 5;
        [SerializeField] protected UnitAttributes m_HeroData;
        private StartButton startButton;
        private UniteMove moveActivity;
        private BattleController battleController;
        public Turn turn;

        public int Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        public UnitAttributes HeroData
        {
            get { return m_HeroData; }
            set { m_HeroData = value; }
        }

        private void Awake()
        {
            if (m_HeroData != null)  m_HeroData.SetCurrentAttributes();//loads the current characteristics of the hero
            moveActivity = GetComponent<UniteMove>();
            battleController = FindObjectOfType<BattleController>();
            turn = FindObjectOfType<Turn>();
        }

        protected void Start()
        {
            startButton = FindObjectOfType<StartButton>();
            stack = GetComponentInChildren<StackTxt>();
            Turn.OnNewRound += m_HeroData.SetDefaultVelocityAndInitiative; //!!! NEW TURN
        }

        public void SetAttributes()
        {
            m_HeroData.SetCurrentAttributes();
        }

        public abstract void DealsDamage(HexBattleground target);

        private void DestroySelf(UnitAttributes SOHero)//destroys this object
        {
            if (SOHero == m_HeroData)
            {
                HexBattleground parentHex = GetComponentInParent<HexBattleground>();
                parentHex.SetDeploymentPosition();
                startButton.ControlStartButton();
                Destroy(gameObject);
            }
        }

        public abstract INeighborFinder GetTypeOfHero();//determines the type of movement
        public abstract void DefineTargets();
        public virtual void HeroIsAtacking()
        {
            Vector3 targetPos = BattleController.currentTarget.transform.position;
            moveActivity.ControlDirection(targetPos);
        }

        public void PlayerTurn(IInitialHexes getInitHexs)
        {
            INeighborFinder nborFinder = GetTypeOfHero();
            int stepsLimit = m_HeroData.VelocityCurrent;

            GetComponent<AvalibleHexPos>().GetAvailablePositions(stepsLimit, nborFinder, getInitHexs);
            DefineTargets();
        }

        public void HeroIsKilled()
        {
            Turn.OnNewRound -= m_HeroData.SetDefaultVelocityAndInitiative;
            battleController.RemoveHeroWhenItIsKilled(this);
        }
    }
}
