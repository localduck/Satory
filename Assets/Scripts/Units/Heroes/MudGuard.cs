using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class MudGuard : Hero, INeighborFinder, IEvaluateHex
    {
        IAtacking dealsDamage = new MeleeAtack();
        public override void DealsDamage(HexBattleground target)
        {
            dealsDamage.HeroIsDealingDamage(this, BattleController.currentTarget);
        }

        public override void DefineTargets()
        {
            HexBattleground initHex = GetComponentInParent<HexBattleground>();
            string caseMethod = "BoolTarget";
            List<HexBattleground> nborToCheck = HexCalculation.GetHexNeighbours(initHex, caseMethod);
            if(nborToCheck.Count > 0)
            {
                HeroIsAtacking();
            } else
            {
                turn.TurnIsCompleted();
            }
        }

        public override void HeroIsAtacking()
        {
            base.HeroIsAtacking();
            GetComponentInChildren<Animator>().SetTrigger("IsAtack");
        }

        public bool EvaluateHex(HexBattleground evaluatedHex)
        {
            throw new System.NotImplementedException();
        }

        public void GetAdjacentHexesExtended(HexBattleground initHex)
        {
            throw new System.NotImplementedException();
        }

        public override INeighborFinder GetTypeOfHero()
        {
            INeighborFinder nborFinder = new PositionForSky();
            if (m_HeroData.isFlying)
            {
                nborFinder = new PositionForSky();
            }
            else
            {
                nborFinder = new PositionForGroundAI();
            }
            return nborFinder;
        }
    }
}
