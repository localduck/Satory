using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class SpiritBoxer : Hero, INeighborFinder, IEvaluateHex
    {
        IAtacking dealsDamage = new StunAtack();
        public override void DealsDamage(HexBattleground target)
        {
            dealsDamage.HeroIsDealingDamage(this, BattleController.currentTarget);
        }

        public override void HeroIsAtacking()
        {
            base.HeroIsAtacking();
            GetComponentInChildren<Animator>().SetTrigger("IsAtack");
        }

        public override void DefineTargets()
        {
            IDefineTarget sideLookForTarget = new TargetPlayerMelee();
            sideLookForTarget.DefineTargets(this);
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
                nborFinder = new PositionForGround();
            }
            return nborFinder;
        }
    }
}
