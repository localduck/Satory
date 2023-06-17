using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class TargetPlayerMelee : MonoBehaviour, IDefineTarget
    {
        HexBattleground initHex;
        List<HexBattleground> nborToChek;
        string caseMethod = "BoolTarget";
        Turn turn;
        public void DefineTargets(Hero currentAtacker)
        {
            initHex = currentAtacker.GetComponentInParent<HexBattleground>();

            nborToChek = HexCalculation.GetHexNeighbours(initHex, caseMethod);
            int currentAttackerVelocity = BattleController.currentAtacker.HeroData.VelocityCurrent;
            if (nborToChek.Count > 0)
            {
                foreach (HexBattleground hex in nborToChek)
                {
                    hex.PotentialTergetDefined();
                }
            }
            else if (nborToChek.Count == 0 && currentAttackerVelocity == 0)
            {
                turn = FindObjectOfType<Turn>();
                turn.TurnIsCompleted();
            }
        }
    }
}
