using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class TargetPlayerRange : MonoBehaviour, IDefineTarget
    {

        private HexBattleground initHex;
        private List<HexBattleground> nborToCheck;
        string caseMethod = "BoolTarget";
        private IInitialHexes getInitialHexes = new InitialTarget();
        Turn turn;
        public void DefineTargets(Hero currentAtacker)
        {
            if (TargetsNearby(currentAtacker) == false)
            {
                TargetsAtAttackDistance(currentAtacker);
            }
        }
        private bool TargetsNearby(Hero currentAtacker)
        {
            bool targetNearby = false;
            initHex = currentAtacker.GetComponentInParent<HexBattleground>();

            nborToCheck = HexCalculation.GetHexNeighbours(initHex, caseMethod);
            if (nborToCheck.Count > 0)
            {
                foreach (HexBattleground hex in nborToCheck)
                {
                    hex.PotentialTergetDefined();
                }
                targetNearby = true;
            }
            return targetNearby;
        }
        
        private void TargetsAtAttackDistance(Hero currentAtacker)
        {
            int stepsLimit = currentAtacker.HeroData.Atackdistanse;
            HexBattleground initHex = currentAtacker.GetComponentInParent<HexBattleground>();
            INeighborFinder nborFinder = new MarkTarget();
            currentAtacker.GetComponent<AvalibleHexPos>().GetAvailablePositions(stepsLimit, nborFinder, getInitialHexes);
            CheckIfItIsNewTurn();
        }

        private void CheckIfItIsNewTurn()
        {
            BattleController battleController = FindObjectOfType<BattleController>();
            if (battleController.IsLookingForPotentialTargets().Count == 0
                && BattleController.currentAtacker.HeroData.VelocityCurrent == 0)
            {
                turn = FindObjectOfType<Turn>();
                turn.TurnIsCompleted();
            }
        }
    }
}
