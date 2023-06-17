using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class BotWheel : Hero, INeighborFinder, IEvaluateHex
    {//Like rdd Archer

        [SerializeField] private Projectile m_Projectile;
        [SerializeField] private Vector3 initPositionCorrect;
        public Vector3 InitPositionCorrect
        {
            get { return initPositionCorrect; }
            set { initPositionCorrect = value; }
        }

        public override void HeroIsAtacking()
        {
            base.HeroIsAtacking();
            GetComponentInChildren<Animator>().SetTrigger("IsAtack");
            InstantiateProjectile();
        }

        private void InstantiateProjectile()
        {
            Vector3 projectilePos = new Vector3(transform.position.x, transform.position.y + initPositionCorrect.y, transform.position.z);
            Quaternion rotation = new Quaternion();
            Projectile projectile = Instantiate(m_Projectile, projectilePos, rotation, transform);
            projectile.Fire();
        }

        public override void DealsDamage(HexBattleground target)
        {
            
        }

        public override void DefineTargets()
        {
            IDefineTarget sideLookForTarget = new TargetPlayerRange();
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
            } else
            {
                nborFinder = new PositionForGround();
            }
            return nborFinder;
        }
    }
}
